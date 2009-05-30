/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;
using System.Threading;
using System.Collections.Generic;
using Extensions.Helpers;
using System.Web;
using System.Reflection;

[assembly: ASP.WebResource("Extensions.Js.Comet.js", "text/javascript")]

namespace Ra.Extensions
{
    /**
     * Comet component, also known under the LazyHttp name, StreamingHttp and several other pseudonyms.
     * Basically real-time event capability for the client. You can also send new Comet events
     * to the Comet queue by using the URL of your page and append a GET parameter called "cometEvent"
     * with the value of your event ID, but only if the AllowExternalEvents property is set to true.
     * Notice that this control should be considered HIGHLY EXPERIMENTAL and is probably not very stable.
     */
    [ASP.ToolboxData("<{0}:Comet runat=\"server\" />")]
    public class Comet : RaControl, IRaControl
    {
        /**
         * Passed into the Tick event handler. Just a wrapper around the ID of the Comet Event being raised.
         */
		public class CometEventArgs : EventArgs
		{
			private string _id;

			internal CometEventArgs(string id)
			{
				_id = id;
			}

            /**
             * id of event raised
             */
            public string Id
			{
				get { return _id; }
			}
		}

        private delegate string EnterQueue(string lastEvent, int timeout);
        private EnterQueue _enter;

        /**
         * Raised when an event have been raised. The EventArgs for this type will be of
         * type CometEventArgs from which you can retrieve the ID property to see which
         * event this is.
         */
        public event EventHandler<CometEventArgs> Tick;

        /**
         * If true then the Comet control is enabled, otherwise it is disabled.
         */
        [DefaultValue(true)]
        public bool Enabled
        {
            get { return ViewState["Enabled"] == null ? true : (bool)ViewState["Enabled"]; }
            set
            {
                if (value != Enabled)
                    SetJSONValueBool("Enabled", value);
                ViewState["Enabled"] = value;
            }
        }

        /**
         * If true you can create events on the comet queue by appending "cometEvent=xxx" as GET 
         * parameter to the URL and sending a GET request. Which will return true back as the response
         * and not actual HTML.Be careful with this one though since raising a Comet event is pretty
         * expensive and having this publicly exposed might pose a security risk since then all the
         * user needs to do to create a "DDOS attack" is to keep posting events to your pages.
         */
        [DefaultValue(true)]
        public bool AllowExternalEvents
        {
            get { return ViewState["AllowExternalEvents"] == null ? true : (bool)ViewState["AllowExternalEvents"]; }
            set { ViewState["AllowExternalEvents"] = value; }
        }

        /**
         * If this property is true then this request can actually NOT connect to comet queue. 
         * If MaxClients are reached then the return value will be true and no actual comet request 
         * will be enabled for this user since queue is filled up. Note you can actually override 
         * this value in e.g. Page_Load by explicitingly setting the Enabled property of the comet 
         * object to true, but then you will have no effect of the MaxClients and might as well set 
         * it to -1. If the value of the MaxClients is -1 then no user will be denied and your webserver
         * will keep on accepting Comet requests until the sere breaks down or no more users exists.
         */
        public bool IsQueueFull
        {
            get { return MaxClients != -1 && NumberOfConnections >= MaxClients; }
        }

        /**
         * Maximum number of simultaneous connected Comet connections before users are denied to connect
         * to Comet queue. Default is -1 which means "no limits". Note that this is a "shared" value and not
         * really on a "per component instance" level...
         */
        [DefaultValue(-1)]
        public int MaxClients
        {
            get { return ViewState["MaxClients"] == null ? -1 : (int)ViewState["MaxClients"]; }
            set { ViewState["MaxClients"] = value; }
        }

        private CometQueue Queue
        {
            get
            {
                // Note that this one expects the ClientID of the Comet Instance to
                // be UNIQUE across all pages inside of application...
                // TODO: Create stronger logic here to ensure we have Unique CometQueue
                CometQueue queue = Page.Application[this.ClientID] as CometQueue;
                if (queue == null)
                {
                    queue = new CometQueue();
                    Page.Application[this.ClientID] = queue;
                }
                return queue;
            }
        }

        /**
         * Number of currently active connections. Note when the request is released due to a comet event
         * being raised this value will be significantly reduced, often to 0 due to all the comet requests being 
         * released and will not report the accurate value. In other Ajax requests it will mostly report 
         * accurate numbers and can be trusted.
         */
        public int NumberOfConnections
        {
            get
            {
                object numberOfConnections = Page.Application[this.ClientID + "_connections"];
                if (numberOfConnections == null)
                {
                    numberOfConnections = 0;
                    Page.Application[this.ClientID + "_connections"] = numberOfConnections;
                }
                return (int)numberOfConnections;
            }
            private set
            {
                Page.Application[this.ClientID + "_connections"] = value;
            }
        }

        /**
         * Will raise a Tick event with the given ID which will release all locked requests and fire
         * the Tick event with the given ID.
         */
        public void SendMessage(string id)
        {
            Queue.SignalizeNewEvent(id);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (AllowExternalEvents && !string.IsNullOrEmpty(Page.Request.Params["cometEvent"]))
            {
                SendMessage(Page.Request.Params["cometEvent"]);
                Page.Response.Clear();
                Page.Response.Write("true");
                try
                {
                    // Will throw an exception...
                    Page.Response.End();
                }
                catch (Exception)
                {
                    // Silently catching due to "by design decision" from Microsoft...
                    // Maybe we should use a Response-Filter here...?
                    // The logic we currently have interferes quite heavily with existing Ajax Filters on Response
                    // object...
                    return;
                }
            }
            base.OnLoad(e);
        }

        protected override void OnInit(EventArgs e)
        {
            // Safeguarding against insane values for Timeout....
            if (Page.AsyncTimeout.TotalSeconds < 5 || Page.AsyncTimeout.TotalSeconds > 120)
                throw new ArgumentException("Cannot have AsyncTimeout on page being outside of range [5,120] milliseconds");

            // Comet request
            if (Page.Request.Params[this.ClientID] == "comet")
            {
                // REMOVING filters if there are eny on the page...
                AjaxManager.Instance.SupressAjaxFilters = true;
                Page.Response.Filter = null;

                // Adding up the Async event handlers...
                Page.RegisterAsyncTask(
                    new System.Web.UI.PageAsyncTask(
                        BeginEnterCometQueue,
                        EndEnterCometQueue,
                        TimeoutCometQueue,
                        Page.Request.Params["prevMsg"]));

                // TODO: Not sure if we need to call base here, if we can avoid calling base we will
                // avoid the removal of the Response Filters since they're added in RaControl...
                // Maybe add up an "else" to call base implementation...?
            }
            else if (string.IsNullOrEmpty(Page.Request.Params["cometEvent"]) && this.IsQueueFull)
            {
                // Full queue
                this.Enabled = false;
            }
            base.OnInit(e);
            AjaxManager.Instance.IncludeScriptFromResource(typeof(Comet), "Extensions.Js.Comet.js");
        }

        private IAsyncResult BeginEnterCometQueue(object src, EventArgs args, AsyncCallback cb, object state)
        {
            NumberOfConnections += 1;
            _enter = new EnterQueue(Queue.WaitForNextMessage);
            return _enter.BeginInvoke(state as string, (int)Page.AsyncTimeout.TotalSeconds, cb, null);
        }

        private void EndEnterCometQueue(IAsyncResult ar)
        {
            // Decrementing number of connections
            // TODO: Lock...!
            NumberOfConnections -= 1;

            // Retrieving message
            string nextMessage = _enter.EndInvoke(ar);

            // Flushing response and writing our Comet Event ID back to the client.
            // The "nextMessage" (if any) will be the event id passed back into the Tick event handler
            Page.Response.Clear();
            if (nextMessage != null)
                Page.Response.Write(nextMessage);
            try
            {
                // Will throw an exception...
                Page.Response.End();
            }
            catch (Exception)
            {
                // Silently catching due to "by design decision" from Microsoft...
                // Maybe we should use a Response-Filter here...?
                // The logic we currently have interferes quite heavily with existing Ajax Filters on Response
                // object...
                return;
            }
        }

        private void TimeoutCometQueue(IAsyncResult ar)
        {
            Page.Response.Clear();

            // Decrementing the number of connections
            // TODO: Lock...!
            NumberOfConnections -= 1;
            try
            {
                // Will throw an exception...
                Page.Response.End();
            }
            catch (Exception)
            {
                // Silently catching due to "by design decision" from Microsoft...
                // Maybe we should use a Response-Filter here...?
                // The logic we currently have interferes quite heavily with existing Ajax Filters on Response
                // object...
                return;
            }
        }

        void IRaControl.DispatchEvent(string name)
        {
            switch (name)
            {
                case "tick":
				    string evtId = Page.Request.Params["__EVENT_ARGS"];
                    if (Tick != null)
                        Tick(this, new CometEventArgs(evtId));
                    break;
            }
        }
		
		protected override string GetClientSideScriptOptions ()
		{
			if (!Enabled || Tick == null)
				return "enabled:false";
			return string.Empty;
		}


		protected override string GetClientSideScriptType()
		{
			return "new Ra.Comet";
		}

        protected override string GetOpeningHTML()
        {
            // Dummy HTML DOM element to make registration and such easier...
			// TODO: Make it possible to have IN-visible controls (maybe...?)
            return string.Format("<span style=\"display:none;\" id=\"{0}\">&nbsp;</span>", ClientID);
        }
    }
}
