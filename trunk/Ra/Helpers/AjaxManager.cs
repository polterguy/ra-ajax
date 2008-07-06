/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

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
        private bool _supressFilters;

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

        // Set this one to true to bypass the Response.Filter logic and toss in your own version of it...
        public bool SupressAjaxFilters
        {
            get { return _supressFilters; }
            set { _supressFilters = value; }
        }

        public void InitializeControl(RaControl ctrl)
        {
            // We store all Ra Controls in a list for easily access later down the road...
            RaControls.Add(ctrl);

            // Making sure we only run the initialization logic ONCE...!!
            if (RaControls.Count == 1)
            {
                if (IsCallback)
                {
                    // This is a Ra Ajax callback, we need to wait until the Page Load 
                    // events are finished loading and then find the control which
                    // wants to fire an event and do so...
                    CurrentPage.LoadComplete += CurrentPage_LoadComplete;

                    // Checking to see if the Filtering logic has been supressed
                    if (!SupressAjaxFilters)
                        CurrentPage.Response.Filter = new CallbackFilter(CurrentPage.Response.Filter);
                }
                else
                {
                    // Checking to see if the Filtering logic has been supressed
                    if (!SupressAjaxFilters)
                        CurrentPage.Response.Filter = new PostbackFilter(CurrentPage.Response.Filter);
                }
            }
        }

        // This is the place where we're dispatching RA events.
        // We should only be here is a Ra Control has created an Ajax Callback
        void CurrentPage_LoadComplete(object sender, EventArgs e)
        {
            // Finding the Control which initiated the request
            string idOfControl = CurrentPage.Request.Params["__RA_CONTROL"];
            
            // Checking to see if this is a "non-Control" callback...
            if (idOfControl == null)
                return;

            RaControl ctrl = RaControls.Find(
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

            // Dispatching the event to our Ra Control...
            raCtrl.DispatchEvent(eventName);
        }

        public void IncludeMainRaScript()
        {
            CurrentPage.ClientScript.RegisterClientScriptResource(typeof(AjaxManager), "Ra.Js.JsCore.Ra.js");
        }

        public void IncludeMainControlScripts()
        {
            CurrentPage.ClientScript.RegisterClientScriptResource(typeof(AjaxManager), "Ra.Js.Control.js");
        }

        private MemoryStream _memStream;
        private HtmlTextWriter _writer;
        public HtmlTextWriter Writer
        {
            get
            {
                if (_writer == null)
                {
                    _memStream = new MemoryStream();
                    TextWriter tw = new StreamWriter(_memStream);
                    _writer = new HtmlTextWriter(tw);
                }
                return _writer;
            }
        }

        private MemoryStream _memStreamBack;
        private HtmlTextWriter _writerBack;
        public HtmlTextWriter WriterAtBack
        {
            get
            {
                if (_writerBack == null)
                {
                    _memStreamBack = new MemoryStream();
                    TextWriter tw = new StreamWriter(_memStreamBack);
                    _writerBack = new HtmlTextWriter(tw);
                }
                return _writerBack;
            }
        }

        // We only come here if this is a Ra Ajax Callback (IsCallback == true)
        // We don't really care about the HTML rendered by the page here.
        // We just short-circut the whole HTML rendering phase here and only render
        // back changes to the client
        internal void RenderCallback(Stream next, MemoryStream content)
        {
            Writer.Flush();
            _memStream.Flush();
            _memStream.Position = 0;
            TextReader readerContent = new StreamReader(_memStream);
            string allContent = readerContent.ReadToEnd();
            TextWriter writer = new StreamWriter(next);
            writer.WriteLine(allContent);

            // Retrieving ViewState (+++) changes and returning back to client
            content.Position = 0;
            TextReader reader = new StreamReader(content);
            string wholePageContent = reader.ReadToEnd();

            if (wholePageContent.IndexOf("__VIEWSTATE") != -1)
                writer.WriteLine("Ra.$('__VIEWSTATE').value = '{0}';", GetViewState(wholePageContent, "__VIEWSTATE"));
            if (wholePageContent.IndexOf("__EVENTVALIDATION") != -1)
                writer.WriteLine("Ra.$('__EVENTVALIDATION').value = '{0}';", GetViewState(wholePageContent, "__EVENTVALIDATION"));

            WriterAtBack.Flush();
            _memStreamBack.Flush();
            _memStreamBack.Position = 0;
            readerContent = new StreamReader(_memStreamBack);
            string allContentAtBack = readerContent.ReadToEnd();
            writer.WriteLine(allContentAtBack);

            writer.Flush();
        }

        private string GetViewState(string wholePageContent, string searchString)
        {
            string viewStateStart = string.Format("<input type=\"hidden\" name=\"{0}\" id=\"{0}\" value=\"", searchString);
            string viewStateValue = GetHiddenInputValue(wholePageContent, viewStateStart);
            return viewStateValue;
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

            Writer.Flush();
            _memStream.Flush();
            _memStream.Position = 0;
            TextReader readerContent = new StreamReader(_memStream);
            string allContent = readerContent.ReadToEnd();
            builder.Append(allContent);

            WriterAtBack.Flush();
            _memStreamBack.Flush();
            _memStreamBack.Position = 0;
            readerContent = new StreamReader(_memStreamBack);
            string allContentAtBack = readerContent.ReadToEnd();
            builder.Append(allContentAtBack);

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























