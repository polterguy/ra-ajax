/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
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
using System.Reflection;
using System.Globalization;


[assembly: WebResource("Ra.Js.Ra.js", "text/javascript")]
[assembly: WebResource("Ra.Js.Control.js", "text/javascript")]
[assembly: WebResource("Ra.Js.Behaviors.js", "text/javascript")]

namespace Ra
{
    /**
     * Singleton class (acces through Instance property) 
     * The "glue" that ties everything together, contains lots of helper metods, though most of them are for
     * extension control developers.
     */
    public sealed class AjaxManager
    {
        private List<RaControl> _raControls = new List<RaControl>();
        private bool _supressFilters;
        private List<string> _scriptIncludes = new List<string>();
        private string _redirectUrl;
        private MemoryStream _memStream;
        private HtmlTextWriter _writer;
        private MemoryStream _memStreamBack;
        private HtmlTextWriter _writerBack;

        /**
         * Public accessor, only way to access instance of class
         */
        public static AjaxManager Instance
        {
            get
            {
                if (((Page)HttpContext.Current.CurrentHandler).Items["_AjaxManagerInstance"] == null)
                    ((Page)HttpContext.Current.CurrentHandler).Items["_AjaxManagerInstance"] = new AjaxManager();
                return (AjaxManager)((Page)HttpContext.Current.CurrentHandler).Items["_AjaxManagerInstance"];
            }
        }

        private List<RaControl> RaControls
        {
            get { return _raControls; }
        }

        // TODO: Check to see if we can delete CurrentPage...!
        /** 
         * Returns the active page object, of the request
         */
        public Page CurrentPage
        {
            get { return ((Page)HttpContext.Current.CurrentHandler); }
        }

        /**
         * Returns true if this is a Ra-Ajax callback. A Ra-Ajax callback is also a "subset" of
         * a normal Postback, which means if this is true, also the IsPostBack from ASP.NET will
         * be true
         */
        public bool IsCallback
        {
            get { return CurrentPage.Request.Params["__RA_CALLBACK"] == "true"; }
        }

        // Set this one to true to bypass the Response.Filter logic and toss in your own version of it...
        /**
         * Surpress the rendering filter on the response. WARNING; Unless you completely understand what this
         * property does, then do NOT USE IT! It will shut off all the default Ajax engine since it will
         * make the rendering return to "default" postback rendering.
         */
        public bool SupressAjaxFilters
        {
            get { return _supressFilters; }
            set { _supressFilters = value; }
        }

        private Control FindControl(Control current, string id)
        {
            if (current.ID == id)
                return current;
            foreach (Control idx in current.Controls)
            {
                Control retVal = FindControl(idx, id);
                if (retVal != null)
                    return retVal;
            }
            return null;
        }

        private Control FindControlClientID(Control current, string id)
        {
            if (current.ClientID == id)
                return current;
            foreach (Control idx in current.Controls)
            {
                Control retVal = FindControlClientID(idx, id);
                if (retVal != null)
                    return retVal;
            }
            return null;
        }

        /**
         * Recursively traverses all controls on Page and MasterPage and returns the first control with the 
         * given ID
         */
        public Control FindControl(string id)
        {
            Control tmpRetVal = FindControl(CurrentPage, id);
            if (tmpRetVal != null)
                return tmpRetVal;
            if (CurrentPage.Master != null)
                tmpRetVal = FindControl(CurrentPage.Master, id);
            return tmpRetVal;
        }

        /**
         * Recursively traverses all controls on Page and MasterPage and returns the first control with the 
         * given ClientID
         */
        public Control FindControlClientID(string id)
        {
            Control tmpRetVal = FindControlClientID(CurrentPage, id);
            if (tmpRetVal != null)
                return tmpRetVal;
            if (CurrentPage.Master != null)
                tmpRetVal = FindControlClientID(CurrentPage.Master, id);
            return tmpRetVal;
        }

        /**
         * Recursively traverses all controls on Page and MasterPage and returns the first control with the 
         * given ID casting it to typeof(T)
         */
        public T FindControl<T>(string id) where T : Control
        {
            return (T)FindControl(id);
        }

        /**
         * Recursively traverses all controls on Page and MasterPage and returns the first control with the 
         * given ClientID casting it to typeof(T)
         */
        public T FindControlClientID<T>(string id) where T : Control
        {
            return (T)FindControlClientID(id);
        }

        internal void InitializeControl(RaControl ctrl)
        {
            // We store all Ra Controls in a list for easily access later down the road...
            RaControls.Add(ctrl);

            // Making sure we only run the initialization logic ONCE...!!
            if (RaControls.Count == 1)
            {
                if (IsCallback)
                {
                    // This is a Ra-Ajax callback, we need to wait until the Page Load 
                    // events are finished loading and then find the control which
                    // wants to fire an event and do so...
                    CurrentPage.LoadComplete += CurrentPage_LoadComplete;

                    // Checking to see if the Filtering logic has been supressed
                    if (!SupressAjaxFilters)
                    {
                        CurrentPage.Response.Filter = new CallbackFilter(CurrentPage.Response.Filter);
                    }
                }
                else
                {
                    // Checking to see if the Filtering logic has been supressed
                    if (!SupressAjaxFilters)
                        CurrentPage.Response.Filter = new PostbackFilter(CurrentPage.Response.Filter);
                }
            }
        }

        private void CurrentPage_LoadComplete(object sender, EventArgs e)
        {
            // Finding the Control which initiated the request
            string idOfControl = CurrentPage.Request.Params["__RA_CONTROL"];
            
            // Checking to see if this is a "non-Control" callback...
            if (string.IsNullOrEmpty(idOfControl))
            {
                string functionName = CurrentPage.Request.Params["__FUNCTION_NAME"];
                
                if (string.IsNullOrEmpty(functionName))
                    return;

                MethodInfo webMethod = 
                    CurrentPage.GetType().BaseType.GetMethod(functionName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (webMethod == null || webMethod.GetCustomAttributes(typeof(Ra.WebMethod), false).Length == 0)
                    throw new Exception("Cannot call a method without a WebMethod attribute");

                ParameterInfo[] parameters = webMethod.GetParameters();

                object[] args = new object[parameters.Length];
                for (int idx = 0; idx < parameters.Length && CurrentPage.Request.Params["__ARG" + idx] != null; idx++)
                {
                    args[idx] = Convert.ChangeType(CurrentPage.Request.Params["__ARG" + idx], parameters[idx].ParameterType);
                }

                object retVal = webMethod.Invoke(CurrentPage, args);
                
                if (retVal != null)
                    WriterAtBack.Write("Ra.Control._methodReturnValue='{0}';", string.Format(CultureInfo.InvariantCulture, "{0}", retVal));
                
                return;
            }

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

        internal void IncludeMainRaScript()
        {
			IncludeScriptFromResource("Ra.js");
		}

        internal void IncludeMainControlScripts()
        {
			IncludeScriptFromResource("Control.js");
        }

		internal void IncludeScriptFromResource(string script)
		{
            if (this.SupressAjaxFilters)
            {
                // Need to explicitly include JS files if filters are surpressed...
                CurrentPage.ClientScript.RegisterClientScriptResource(typeof(AjaxManager), "Ra.Js." + script);
            }
            string resource = CurrentPage.ClientScript.GetWebResourceUrl(typeof(AjaxManager), "Ra.Js." + script);

			if( _scriptIncludes.Exists(
                delegate(string idx)
			    {
				    return idx == resource;
			    }))
			   return;
            _scriptIncludes.Add(resource);
        }

        /**
         * Includes a JavaScript file from a resource with the given resource if
         */
        public void IncludeScriptFromResource(Type type, string id)
        {
            if (this.SupressAjaxFilters)
            {
                // Need to explicitly include JS files if filters are surpressed...
                CurrentPage.ClientScript.RegisterClientScriptResource(type, id);
            }
            string resource = CurrentPage.ClientScript.GetWebResourceUrl(type, id);
            _scriptIncludes.Add(resource);
        }

        /**
         * Use to redirect to another page from a Ra-Ajax Callback. Note the default ASP.NET implementation
         * that redirects on Response.Redirect does NOT work in Ra-Ajax callbacks.
         */
        public void Redirect(string url)
        {
            if (!IsCallback)
                CurrentPage.Response.Redirect(url);
            else
            {
                if (url == "~" || url == "~/")
                {
                    CurrentPage.Response.AddHeader("Location", CurrentPage.Request.ApplicationPath);
                }
                else if (url.StartsWith("~"))
                {
                    CurrentPage.Response.AddHeader("Location", url.Replace("~", CurrentPage.Request.ApplicationPath));
                }
                else
                {
                    CurrentPage.Response.AddHeader("Location", url);
                }
                // Note that ue to w3c standardizing the XHR should TRANSPARENTLY
                // do 301 and 302 redirects we need another mechanism to inform client side that
                // the user code is RE-directing to another page!
                CurrentPage.Response.StatusCode = 278;
                _redirectUrl = url;
            }
        }

        internal HtmlTextWriter Writer
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

        // TODO: Why is this an HTMLTextWriter?
        /**
         * Use this method to append you own script which will be executed on the client when the request returns
         */
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

        // We only come here if this is a Ra-Ajax Callback (IsCallback == true)
        // We don't really care about the HTML rendered by the page here.
        // We just short-circut the whole HTML rendering phase here and only render
        // back changes to the client
        internal void RenderCallback(Stream next, MemoryStream content)
        {
            if (string.IsNullOrEmpty(_redirectUrl))
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
                    writer.WriteLine("Ra.$F('__VIEWSTATE').value = '{0}';", GetViewState(wholePageContent, "__VIEWSTATE"));
                if (wholePageContent.IndexOf("__EVENTVALIDATION") != -1)
                    writer.WriteLine("Ra.$F('__EVENTVALIDATION').value = '{0}';", GetViewState(wholePageContent, "__EVENTVALIDATION"));

                WriterAtBack.Flush();
                _memStreamBack.Flush();
                _memStreamBack.Position = 0;
                readerContent = new StreamReader(_memStreamBack);
                string allContentAtBack = readerContent.ReadToEnd();
                writer.WriteLine(allContentAtBack);

                writer.Flush();
            }
            else
            {
                ;// Do nothing, headers changed in setter
            }
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
            foreach (string idx in _scriptIncludes)
            {
                string script = idx.Replace("&", "&amp;");
                string scriptInclusion = string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>\r\n", script);
                builder.Append(scriptInclusion);
            }
            builder.Append("<script type=\"text/javascript\">");
			builder.Append(@"
function RAInitialize() {
");

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
			builder.Append(@"
}
if (window.addEventListener) {
  window.addEventListener('load', RAInitialize, false);
} else {
  window.attachEvent('onload', RAInitialize);
}

");
            builder.Append("</script>");
            builder.Append("</body>");

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























