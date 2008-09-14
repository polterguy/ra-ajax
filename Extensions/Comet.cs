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

[assembly: ASP.WebResource("Extensions.Comet.js", "text/javascript")]

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:Comet runat=\"server\" />")]
    public class Comet : RaControl
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
            // Checking to see if this is a Comet request...
            string comet = Page.Request.Params[this.ClientID];
            if (comet == "comet")
            {
                string previousMessage = Page.Request.Params["prevMsg"];
                Page.Response.Clear();
                string nextMessage = Queue.WaitForNextMessage(previousMessage);
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
                    return;
                }
            }
            base.OnInit(e);
            AjaxManager.Instance.IncludeScriptFromResource(typeof(Timer), "Extensions.Comet.js");
        }

        public override void DispatchEvent(string name)
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
			if (Enabled && Tick != null)
				return "enabled:true";
			else
				return string.Empty;
		}


		protected override string GetClientSideScriptType()
		{
			return "new Ra.Comet";
		}
		
        public override string GetOpeningHTML()
        {
            // Dummy HTML DOM element to make registration and such easier...
			// TODO: Make it possible to have IN-visible controls (maybe...?)
            return string.Format("<span style=\"display:none;\" id=\"{0}\">&nbsp;</span>", ClientID);
        }
    }
}
