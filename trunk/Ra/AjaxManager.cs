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
                // Though we only add this Event Handler ONCE
                if (_raControls.Count == 1)
                    CurrentPage.LoadComplete += CurrentPage_LoadComplete;
            }
            else
            {
                // We STILL need a FILTER on the Response object
                // Though we only add this filter ONCE...!!
                if (_raControls.Count == 1)
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

        // We only come here if this is a Ra Ajax Callback (IsCallback == true)
        // We don't really care about the HTML rendered by the page here.
        // We just short-circut the whole HTML rendering phase here and only render
        // back changes to the client
        internal void RenderCallback(Stream next, MemoryStream content)
        {
            TextWriter writer = new StreamWriter(next);
            foreach (RaControl idx in RaControls)
            {
                switch (idx.Phase)
                {
                    case RaControl.RenderingPhase.Destroy:
                        writer.WriteLine("Ra.Control.$('{0}').destroy();", idx.ClientID);
                        break;
                    case RaControl.RenderingPhase.Invisible:
                        // Do nothing...
                        break;
                    case RaControl.RenderingPhase.MadeVisibleThisRequest:
                        // Rendering replace logic and register script logic
                        writer.WriteLine("Ra.$('{0}').replace('{1}');", 
                            idx.ClientID, 
                            idx.GetHTML().Replace("\\", "\\\\").Replace("'", "\\'"));
                        writer.WriteLine(idx.GetClientSideScript());
                        break;
                    case RaControl.RenderingPhase.PropertyChanges:
                        // Render JSON changes
                        break;
                    case RaControl.RenderingPhase.RenderHtml:
                        // Should NOT be possible...!
                        break;
                }
            }

            // Retrieving ViewState changes and returning back to client
            content.Position = 0;
            TextReader reader = new StreamReader(content);
            string wholePageContent = reader.ReadToEnd();
            string viewStateStart = "<input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\" value=\"";
            string viewStateValue = GetHiddenInputValue(wholePageContent, viewStateStart);
            writer.WriteLine("Ra.$('__VIEWSTATE').value = '{0}';", viewStateValue);
            
            writer.Flush();
        }

        private string GetHiddenInputValue(string html, string marker)
        {
            string value = null;
            int i = html.IndexOf(marker);
            if (i != -1)
            {
                value = html.Substring(i + marker.Length);
                value = value.Substring(0, value.IndexOf('\"'));
            }
            return value;
        }

        // Here we are if this is a POSTBACK or the initial rendering of the page... (Page.IsPostBack == false)
        // The content parameter is a stream containing the entire page's HTML output
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

            // Replacing the </body> element with the client-side object creation scripts for the Ra Controls...
            Regex reg = new Regex("</body>", RegexOptions.IgnoreCase);
            wholePageContent = reg.Replace(wholePageContent, builder.ToString());

            // Now writing everything back to client (or next Filter)
            TextWriter writer = new StreamWriter(next);
            writer.Write(wholePageContent);
            writer.Flush();
        }
    }
}























