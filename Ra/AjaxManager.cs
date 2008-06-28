using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web;
using System.IO;

[assembly: WebResource("Ra.Js.JsCore.Ra.js", "text/javascript")]
[assembly: WebResource("Ra.Js.Control.js", "text/javascript")]

namespace Ra
{
    public sealed class AjaxManager
    {
        private List<Control> _raControls = new List<Control>();

        public static AjaxManager Instance
        {
            get
            {
                if (((Page)HttpContext.Current.CurrentHandler).Items["_AjaxManagerInstance"] == null)
                    ((Page)HttpContext.Current.CurrentHandler).Items["_AjaxManagerInstance"] = new AjaxManager();
                return (AjaxManager)((Page)HttpContext.Current.CurrentHandler).Items["_AjaxManagerInstance"];
            }
        }

        public Page CurrentPage
        {
            get { return ((Page)HttpContext.Current.CurrentHandler); }
        }

        public bool IsCallback
        {
            get { return CurrentPage.Request.Params["__RA_CALLBACK"] == "true"; }
        }

        public void InitializeControl(Control ctrl)
        {
            _raControls.Add(ctrl);

            if (IsCallback)
            {
                // This is a Ra Ajax callback, we need to wait until the Page Load 
                // events are finished loading and then find the control which
                // wants to fire an event and do so...
                CurrentPage.LoadComplete += CurrentPage_LoadComplete;
            }
        }

        void CurrentPage_LoadComplete(object sender, EventArgs e)
        {
            // Finding the Control which initiated the request
            string idOfControl = CurrentPage.Request.Params["__RA_CONTROL"];
            Control ctrl = _raControls.Find(
                delegate(Control idx)
                {
                    return idx.ClientID == idOfControl;
                });

            // Getting the name of the event the control raised
            string eventName = CurrentPage.Request.Params["__EVENT_NAME"];

            // Casting the initiated control to the interface expected all Ra Controls to be
            IRaControl raCtrl = ctrl as IRaControl;
            if (ctrl == null)
            {
                throw new ApplicationException("A control which was not a Ra Control initiated a callback, implement the IRaControl interface on the control");
            }
            raCtrl.DispatchEvent(eventName);

            // Handling the PreRender event to do our filtering there
            CurrentPage.PreRender += CurrentPage_PreRender;
        }

        void CurrentPage_PreRender(object sender, EventArgs e)
        {
            // Since you can concatenate filters, we need to "keep track" of the previous one...
            CurrentPage.Response.Filter = new CallbackFilter(CurrentPage.Response.Filter);
        }

        public void IncludeMainRaScript()
        {
            CurrentPage.ClientScript.RegisterClientScriptResource(typeof(AjaxManager), "Ra.Js.JsCore.Ra.js");
        }

        public void IncludeMainControlScripts()
        {
            CurrentPage.ClientScript.RegisterClientScriptResource(typeof(AjaxManager), "Ra.Js.Control.js");
        }

        internal void Render(Stream next)
        {
            TextWriter writer = new StreamWriter(next);
            writer.Write("thomas");
            writer.Flush();
        }
    }
}























