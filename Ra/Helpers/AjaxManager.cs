/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
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
using Ra.Core;


[assembly: WebResource("Ra.Js.Ra.js", "text/javascript")]

#if !CONCAT
[assembly: WebResource("Ra.Js.Control.js", "text/javascript")]
[assembly: WebResource("Ra.Js.Behaviors.js", "text/javascript")]
#endif

namespace Ra
{
    /**
     * Singleton class (acces through Instance property) 
     * The "glue" that ties everything together, contains lots of helper metods, though most of them are for
     * extension control developers. Use WriterAtBack to add up e.g. JavaScript in callbacks.
     */
    public sealed class AjaxManager
    {
        private List<RaControl> _raControls = new List<RaControl>();
        private bool _supressFilters;
        private List<string> _scriptIncludes = new List<string>();
        private List<string> _dynamicScriptIncludes = new List<string>();
        private string _redirectUrl;
        private MemoryStream _memStream;
        private HtmlTextWriter _writer;
        private MemoryStream _memStreamBack;
        private HtmlTextWriter _writerBack;
        private string _requestViewState;

        /**
         * Public accessor, only way to access instance of AjaxManager. The AjaxManager is
         * the "glue" that ties everything together. Use this property to retrieve the only
         * existing object of type AjaxManager.
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

        /**
         * Returns true if this is a Ra-Ajax callback. A Ra-Ajax callback is also a "subset" of
         * a normal Postback, which means that if this property is true, then the IsPostBack from 
         * ASP.NET's System.Web.UI.Page will also be true.
         */
        public bool IsCallback
        {
            get { return HttpContext.Current.Request.Params["__RA_CALLBACK"] == "true" && HttpContext.Current.Request.Params["HTTP_X_MICROSOFTAJAX"] == null; }
        }

        // Set this one to true to bypass the Response.Filter logic and toss in your own version of it...
        /**
         * Surpress the rendering filter on the response. WARNING; Unless you completely understand what this
         * property does, then do NOT USE IT! It will shut off all the default Ajax engine since it will
         * make the rendering return to "default" postback rendering. This is only meant for really advanced
         * control developers and also those that wants to handle requests in their own special way.
         */
        public bool SupressAjaxFilters
        {
            get { return _supressFilters; }
            set { _supressFilters = value; }
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
         * Use this method to append you own script and/or HTML or other output which will be executed 
         * on the client when the request returns. Notice though that the JavaScript Ajax engine of
         * Ra-Ajax will expect whatever is being returned to the client to be JavaScript. This means that
         * if you want to return other types of values and objects back to the client you need to wrap this
         * somehow inside of JavaScript by using e.g. JSON or some similar mechanism. Also you should always
         * end your JavaScript statements with semicolon (;) since otherwise other parts of the JavaScript
         * returned probably will fissle.
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

        internal Control FindControl(Control current, string id)
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

        private Control FindControl(string id)
        {
            Control tmpRetVal = FindControl(((Page)HttpContext.Current.CurrentHandler), id);
            if (tmpRetVal != null)
                return tmpRetVal;
            if (((Page)HttpContext.Current.CurrentHandler).Master != null)
                tmpRetVal = FindControl(((Page)HttpContext.Current.CurrentHandler).Master, id);
            return tmpRetVal;
        }

        private Control FindControlClientID(string id)
        {
            Control tmpRetVal = FindControlClientID(((Page)HttpContext.Current.CurrentHandler), id);
            if (tmpRetVal != null)
                return tmpRetVal;
            if (((Page)HttpContext.Current.CurrentHandler).Master != null)
                tmpRetVal = FindControlClientID(((Page)HttpContext.Current.CurrentHandler).Master, id);
            return tmpRetVal;
        }

        /**
         * Recursively traverses all controls on Page and MasterPage and returns the first control with the 
         * given ID casting it to typeof(T).
         */
        public T FindControl<T>(string id) where T : Control
        {
            return (T)FindControl(id);
        }

        /**
         * Recursively traverses all controls starting from given Control and returns the first control with the 
         * given ID casting it to typeof(T)
         */
        public T FindControl<T>(Control childOf, string id) where T : Control
        {
            return (T)FindControl(childOf, id);
        }

        internal T FindControlClientID<T>(string id) where T : Control
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
                if (((Page)HttpContext.Current.CurrentHandler).Request.Params["HTTP_X_MICROSOFTAJAX"] != null)
                    return;

                if (IsCallback)
                {
                    // This is a Ra-Ajax callback, we need to wait until the Page Load 
                    // events are finished loading and then find the control which
                    // wants to fire an event and do so...
                    ((Page)HttpContext.Current.CurrentHandler).LoadComplete += CurrentPage_LoadComplete;

                    // Checking to see if the Filtering logic has been supressed
                    if (!SupressAjaxFilters)
                    {
                        ((Page)HttpContext.Current.CurrentHandler).Response.Filter = new CallbackFilter(((Page)HttpContext.Current.CurrentHandler).Response.Filter);
                    }
                }
                else
                {
                    ((Page)HttpContext.Current.CurrentHandler).LoadComplete += CurrentPage_LoadComplete_NO_AJAX;

                    // Checking to see if the Filtering logic has been supressed
                    if (!SupressAjaxFilters)
                        ((Page)HttpContext.Current.CurrentHandler).Response.Filter = new PostbackFilter(((Page)HttpContext.Current.CurrentHandler).Response.Filter);
                }
            }
        }

        void CurrentPage_LoadComplete_NO_AJAX(object sender, EventArgs e)
        {
            // Turning OFF form values caching...
            // Unless we do this then FireFox will RETAIN its OLD values for form elements
            // including the ViewState which practically will completely break the back
            // button in addition to breaking down all Ajax when clicking back, forward or refresh
            // in FireFox...
            if (((Page)HttpContext.Current.CurrentHandler).Request.Browser.Browser == "Firefox" || ((Page)HttpContext.Current.CurrentHandler).Request.Browser.Browser == "Iceweasel")
                ((Page)HttpContext.Current.CurrentHandler).Form.Attributes.Add("autocomplete", "off");
        }

        private void CurrentPage_LoadComplete(object sender, EventArgs e)
        {
            // Storing request viewstate in order to be able to "diff" 
            // them in rendering of response
            _requestViewState = ((Page)HttpContext.Current.CurrentHandler).Request.Params["__VIEWSTATE"];

            // Finding the Control which initiated the request
            string idOfControl = ((Page)HttpContext.Current.CurrentHandler).Request.Params["__RA_CONTROL"];

            // Setting Response Content-Type to JavaScript
            ((Page)HttpContext.Current.CurrentHandler).Response.ContentType = "text/javascript";

            // Checking to see if this is a "non-Control" callback...
            if (string.IsNullOrEmpty(idOfControl))
            {
                string functionName = ((Page)HttpContext.Current.CurrentHandler).Request.Params["__FUNCTION_NAME"];
                
                if (string.IsNullOrEmpty(functionName))
                    return;

                Control ctrlToCallFor = ((Page)HttpContext.Current.CurrentHandler);
                MethodInfo webMethod = ExtractMethod(functionName, ref ctrlToCallFor);

                if (webMethod == null || webMethod.GetCustomAttributes(typeof(Ra.WebMethod), false).Length == 0)
                    throw new Exception("Cannot call a method without a WebMethod attribute");

                ParameterInfo[] parameters = webMethod.GetParameters();

                object[] args = new object[parameters.Length];
                for (int idx = 0; idx < parameters.Length && ((Page)HttpContext.Current.CurrentHandler).Request.Params["__ARG" + idx] != null; idx++)
                {
                    args[idx] = Convert.ChangeType(((Page)HttpContext.Current.CurrentHandler).Request.Params["__ARG" + idx], parameters[idx].ParameterType);
                }

                object retVal = webMethod.Invoke(ctrlToCallFor, args);
                
                if (retVal != null)
                    WriterAtBack.Write("Ra.Control._methodReturnValue='{0}';", 
                        string.Format(CultureInfo.InvariantCulture, "{0}", retVal).Replace("\\", "\\\\").Replace("'", "\\'"));
                
                return;
            }

            RaControl ctrl = RaControls.Find(
                delegate(RaControl idx)
                {
                    return idx.ClientID == idOfControl;
                });

            // Getting the name of the event the control raised
            string eventName = ((Page)HttpContext.Current.CurrentHandler).Request.Params["__EVENT_NAME"];

            // Casting the initiated control to the interface expected all Ra Controls to be
            IRaControl raCtrl = ctrl as IRaControl;
            if (ctrl == null)
            {
                throw new ApplicationException("A control which was not a Ra Control initiated a callback, implement the IRaControl interface on the control");
            }

            // Dispatching the event to our Ra Control...
            raCtrl.DispatchEvent(eventName);
        }

        private MethodInfo ExtractMethod(string functionName, ref Control ctrlToCallFor)
        {
            MethodInfo webMethod = null;
            if (functionName.IndexOf(".") == -1)
            {
                // No UserControls in the picture here...
                webMethod = ((Page)HttpContext.Current.CurrentHandler).GetType().BaseType.GetMethod(functionName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (webMethod != null)
                {
                    ctrlToCallFor = ((Page)HttpContext.Current.CurrentHandler);
                }
                else
                {
                    // Couldn't find method in Page, looking in MasterPage
                    webMethod = ((Page)HttpContext.Current.CurrentHandler).Master.GetType().BaseType.GetMethod(functionName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    ctrlToCallFor = ((Page)HttpContext.Current.CurrentHandler).Master;
                }
            }
            else
            {
                // A "." means there's a UserControl hosting this method
                string[] entities = functionName.Split('.');
                Control ctrl = null;
                for (int idx = 0; idx < entities.Length - 1; idx++)
                {
                    if (ctrl == null)
                    {
                        ctrl = AjaxManager.Instance.FindControl<UserControl>(entities[idx]);
                    }
                    else
                    {
                        ctrl = AjaxManager.Instance.FindControl<UserControl>(ctrl, entities[idx]);
                    }
                }
                webMethod = ctrl.GetType().BaseType.GetMethod(entities[entities.Length - 1],
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                ctrlToCallFor = ctrl;
            }
            return webMethod;
        }

        internal void IncludeMainRaScript()
        {
			IncludeScriptFromResource("Ra.js");
		}

        internal void IncludeMainControlScripts()
        {
#if !CONCAT
			IncludeScriptFromResource("Control.js");
#endif
        }

		internal void IncludeScriptFromResource(string script)
		{
            if (this.SupressAjaxFilters)
            {
                // Need to explicitly include JS files if filters are surpressed...
                ((Page)HttpContext.Current.CurrentHandler).ClientScript.RegisterClientScriptResource(typeof(AjaxManager), "Ra.Js." + script);
            }
            string resource = ((Page)HttpContext.Current.CurrentHandler).ClientScript.GetWebResourceUrl(typeof(AjaxManager), "Ra.Js." + script);

			if( _scriptIncludes.Exists(
                delegate(string idx)
			    {
				    return idx == resource;
			    }))
			   return;
            _scriptIncludes.Add(resource);
        }

        /**
         * Includes a JavaScript file from a resource with the given resource id. This one 
         * is mostly for Control developers to make sure your script files are being included.
         * Note that Ra-Ajax can include ANY JavaScrip file even in Ajax Callbacks. But in order for
         * this to work you must use the methods for JavaScript file inclusions in the
         * AjaxManager like for instance this method.
         */
        public void IncludeScriptFromResource(Type type, string id)
        {
            if (this.SupressAjaxFilters)
            {
                // Need to explicitly include JS files if filters are surpressed...
                ((Page)HttpContext.Current.CurrentHandler).ClientScript.RegisterClientScriptResource(type, id);
            }
            
            string resource = ((Page)HttpContext.Current.CurrentHandler).ClientScript.GetWebResourceUrl(type, id);

            if (!string.IsNullOrEmpty(resource))
                IncludeScript(resource);
        }

        /**
         * Includes a JavaScript file from a file path. Note that Ra-Ajax can 
         * include ANY JavaScrip file even in Ajax Callbacks. But in order for
         * this to work you must use the methods for JavaScript file inclusions in the
         * AjaxManager like for instance this method.
         */
        public void IncludeScriptFromFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            if (this.SupressAjaxFilters)
            {
                // Need to explicitly include JS files if filters are surpressed...
                ((Page)HttpContext.Current.CurrentHandler).ClientScript.RegisterClientScriptInclude(path.GetHashCode().ToString(), path);
            }

            IncludeScript(path);
        }

        private void IncludeScript(string path)
        {
            if (IsCallback)
            {
                if (!_dynamicScriptIncludes.Contains(path))
                {
                    _dynamicScriptIncludes.Add(path);
                }
            }
            else
            {
                if (!_scriptIncludes.Contains(path))
                {
                    _scriptIncludes.Add(path);
                }
            }
        }

        /**
         * Use to redirect to another page from a Ra-Ajax Callback. Note the default ASP.NET implementation
         * that redirects on Response.Redirect does NOT work in Ra-Ajax callbacks. This method might be used
         * instead of the default Response.Redirect method though.
         */
        public void Redirect(string url)
        {
            if (!IsCallback)
                ((Page)HttpContext.Current.CurrentHandler).Response.Redirect(url);
            else
            {
                ((Page)HttpContext.Current.CurrentHandler).Response.AddHeader("Location", ((Page)HttpContext.Current.CurrentHandler).Response.ApplyAppPathModifier(url));

                // Note that due to w3c standardizing the XHR should TRANSPARENTLY
                // do 301 and 302 redirects we need another mechanism to inform client side that
                // the user code is RE-directing to another page!
                ((Page)HttpContext.Current.CurrentHandler).Response.StatusCode = 278;
                _redirectUrl = url;
            }
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
            AddScriptIncludes(builder);
            AddInitializationScripts(builder);

            // Replacing the </body> element with the client-side object creation scripts for the Ra Controls...
            Regex reg = new Regex("</body>", RegexOptions.IgnoreCase);
            wholePageContent = reg.Replace(wholePageContent, builder.ToString());

            // Now writing everything back to client (or next Filter)
            TextWriter writer = new StreamWriter(next);
            writer.Write(wholePageContent);
            writer.Flush();
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

                AddDynamicScriptIncludes(writer);
                writer.WriteLine(allContent);

                // Retrieving ViewState (+++) changes and returning back to client
                UpdateViewState(content, writer);

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

        private void UpdateViewState(MemoryStream content, TextWriter writer)
        {
            content.Position = 0;
            TextReader reader = new StreamReader(content);
            string wholePageContent = reader.ReadToEnd();

            if (wholePageContent.IndexOf("__VIEWSTATE") != -1)
            {
                int idxOfChange = 0;
                string responseViewState = GetViewState(wholePageContent, "__VIEWSTATE");
                if (!string.IsNullOrEmpty(_requestViewState))
                {
                    for (; idxOfChange < responseViewState.Length && idxOfChange < _requestViewState.Length; idxOfChange++)
                    {
                        if (_requestViewState[idxOfChange] != responseViewState[idxOfChange])
                            break;
                    }
                    if (idxOfChange >= responseViewState.Length)
                        responseViewState = "";
                    else
                        responseViewState = responseViewState.Substring(idxOfChange);
                }
                writer.WriteLine("Ra.$F('__VIEWSTATE', '{0}', {1});", responseViewState, idxOfChange);
            }
            if (wholePageContent.IndexOf("__EVENTVALIDATION") != -1)
                writer.WriteLine("Ra.$F('__EVENTVALIDATION').value = '{0}';", GetViewState(wholePageContent, "__EVENTVALIDATION"));
        }
        
        private void AddInitializationScripts(StringBuilder builder)
        {
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
(function() {
if (window.addEventListener) {
  window.addEventListener('load', RAInitialize, false);
} else {
  window.attachEvent('onload', RAInitialize);
}
})();

");
            _scriptIncludes.ForEach(delegate(string script) {
                builder.AppendFormat("Ra._loadedScripts['{0}']=true;\r\n", script.Replace("&", "&amp;")); 
            });
            builder.Append("</script>");
            builder.Append("</body>");
        }

        private void AddScriptIncludes(StringBuilder builder)
        {
            foreach (string idx in _scriptIncludes)
            {
                string script = idx.Replace("&", "&amp;");
                string scriptInclusion = string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>\r\n", script);
                builder.Append(scriptInclusion);
            }
        }

        private void AddDynamicScriptIncludes(TextWriter writer)
        {
            foreach (string script in _dynamicScriptIncludes)
            {
                writer.WriteLine("Ra.$I('{0}');", script.Replace("&", "&amp;"));
            }
        }
    }
}
