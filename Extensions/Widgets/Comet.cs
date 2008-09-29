/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Helpers;
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
    [ASP.ToolboxData("<{0}:Comet runat=\"server\" />")]
    public class Comet : RaControl, IRaControl
    {
		public class CometEventArgs : EventArgs
		{
			private string _id;

			internal CometEventArgs(string id)
			{
				_id = id;
			}
			
			public string Id
			{
				get { return _id; }
			}
		}

        private delegate string EnterQueue(string lastEvent, int timeout);
        private EnterQueue _enter;

        public event EventHandler<CometEventArgs> Tick;

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

        // This one will create a message that will make sure
        // all Comet listeners are returning and being signalized back
        // to the client which again will raise the Tick Event...
        public void SendMessage(string messageName)
        {
            Queue.SignalizeNewEvent(messageName);
        }

        protected override void OnInit(EventArgs e)
        {
            // Safeguarding against insane values for Timeout....
            if (Page.AsyncTimeout.TotalSeconds < 5 || Page.AsyncTimeout.TotalSeconds > 120)
                throw new ArgumentException("Cannot have AsyncTimeout on page being outside of range [5,120] milliseconds");

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

                // TODO: Not sure if we need to call base here, if we can avoid caling base we will
                // avoid the removal of the Response Filters since they're added in RaControl...
                // Maybe add up an "else" to call base implementation...?
            }
            base.OnInit(e);
            AjaxManager.Instance.IncludeScriptFromResource(typeof(Comet), "Extensions.Js.Comet.js");
        }

        private IAsyncResult BeginEnterCometQueue(object src, EventArgs args, AsyncCallback cb, object state)
        {
            _enter = new EnterQueue(Queue.WaitForNextMessage);
            return _enter.BeginInvoke(state as string, (int)Page.AsyncTimeout.TotalSeconds, cb, null);
        }

        private void TimeoutCometQueue(IAsyncResult ar)
        {
            Page.Response.Clear();
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

        private void EndEnterCometQueue(IAsyncResult ar)
        {
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
