using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web;
using System.IO;
using Ra.Widgets;
using System.Text;
using System.Text.RegularExpressions;

[assembly: WebResource("Ra.Js.JsCore.Ra.js", "text/javascript")]
[assembly: WebResource("Ra.Js.Control.js", "text/javascript")]

namespace Ra
{
    public sealed class AjaxManager
    {
        public static AjaxManager Instance
        {
            get
            {
                if (((Page)HttpContext.Current.CurrentHandler).Items["_AjaxManagerInstance"] == null)
                    ((Page)HttpContext.Current.CurrentHandler).Items["_AjaxManagerInstance"] = new AjaxManager();
                return (AjaxManager)((Page)HttpContext.Current.CurrentHandler).Items["_AjaxManagerInstance"];
            }
        }

        private List<RaControl> _raControls = new List<RaControl>();

        public List<RaControl> RaControls
        {
            get { return _raControls; }
        }

        public Page CurrentPage
        {
            get { return ((Page)HttpContext.Current.CurrentHandler); }
        }

        public bool IsCallback
        {
            get { return CurrentPage.Request.Params["__RA_CALLBACK"] == "true"; }
        }

        public void InitializeControl(RaControl ctrl)
        {
            _raControls.Add(ctrl);
            if (IsCallback)
            {
                // This is a Ra Ajax callback, we need to wait until the Page Load 
                // events are finished loading and then find the control which
                // wants to fire an event and do so...
                CurrentPage.LoadComplete += CurrentPage_LoadComplete;
            }
            else
            {
                // We STILL need a FILTER on the Response object
                CurrentPage.Response.Filter = new PostbackFilter(CurrentPage.Response.Filter);
            }
        }

        void CurrentPage_LoadComplete(object sender, EventArgs e)
        {
            // Finding the Control which initiated the request
            string idOfControl = CurrentPage.Request.Params["__RA_CONTROL"];
            RaControl ctrl = _raControls.Find(
                delegate(RaControl idx)
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

        internal void RenderCallback(Stream next)
        {
            TextWriter writer = new StreamWriter(next);
            writer.Write("thomas");
            writer.Flush();
        }

        internal void RenderPostback(Stream next, MemoryStream content)
        {
            // First reading the WHOLE page content into memory since we need
            // to add up the "register control" script for all of our visible controls
            content.Position = 0;
            TextReader reader = new StreamReader(content);
            string wholePageContent = reader.ReadToEnd();

            // Stringbuilder to hold our "register script" parts...
            StringBuilder builder = new StringBuilder();
            builder.Append("<script type=\"text/javascript\">");

            // Now replacing the parts BELOW the </body> element to give room 
            // for our "register control" scripts...
            foreach (RaControl idx in RaControls)
            {
                if (idx.Phase == RaControl.RenderingPhase.MadeVisibleThisRequest || idx.Phase == RaControl.RenderingPhase.RenderHtml)
                    builder.Append(idx.GetClientSideScript());

            }

            // Adding script closing element
            builder.Append("</script></body>");

            //wholePageContent = wholePageContent.Replace("</body>", "</body>" + builder.ToString());
            Regex reg = new Regex("</body>", RegexOptions.IgnoreCase);
            wholePageContent = reg.Replace(wholePageContent, builder.ToString());

            // Now writing everything back to client (or next Filter)
            TextWriter writer = new StreamWriter(next);
            writer.Write(wholePageContent);
            writer.Flush();
        }
    }
}























