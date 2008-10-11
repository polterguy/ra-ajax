/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web.UI;
using System.ComponentModel;
using System.Collections.Generic;

namespace Ra.Widgets
{
    /**
     * class for "visual" Ajax Controls. Mostly all Ajax Controls in Ra-Ajax inherits from this class
     * instead of the RaControl class since this class implements logic for the Style property collection.
     */
    public abstract class RaWebControl : RaControl, IAttributeAccessor, IRaControl
    {
        private StyleCollection _styles;

        /**
         * Raised when control is clicked
         */
        public event EventHandler Click;

        /**
         * Raised when mouse is over the control, opposite of MouseOut
         */
        public event EventHandler MouseOver;

        /**
         * Raised when mouse is leaving the control, opposite of MouseOver
         */
        public event EventHandler MouseOut;

        // Only purpose is to instantiate the _styles field with the this as the parameter
        public RaWebControl()
        {
            _styles = new StyleCollection(this);
        }

        public virtual void DispatchEvent(string name)
        {
            switch (name)
            {
                case "click":
                    if (Click != null)
                        Click(this, new EventArgs());
                    break;
                case "mouseover":
                    if (MouseOver != null)
                        MouseOver(this, new EventArgs());
                    break;
                case "mouseout":
                    if (MouseOut != null)
                        MouseOut(this, new EventArgs());
                    break;
                default:
                    throw new ApplicationException("Unknown event fired for control");
            }
        }

        protected override string GetEventsRegisterScript()
        {
            string evts = string.Empty;
            if (Click != null)
                evts += "['click', true]";
            if (MouseOver != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['mouseover']";
            }
            if (MouseOut != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['mouseout']";
            }
            return evts;
        }

        #region [ -- Overridden Base Class methods -- ]

        // Overridden to start tracking on the Style collection
        protected override void TrackViewState()
        {
            base.TrackViewState();
            Style.TrackViewState();
        }

        // Overridden to save the ViewState for the Style collection
        protected override object SaveViewState()
        {
            object[] content = new object[2];

            // This order we must remember for the LoadViewState logic ;)
            content[0] = Style.SaveViewState();
            content[1] = base.SaveViewState();
            return content;
        }

        // Overridden to reload the ViewState for the Style collection
        protected override void LoadViewState(object savedState)
        {
            object[] content = savedState as object[];

            Style.LoadViewState(content[0] as string);

            // When we save the ViewState we save the base class object as the second instance in the array...
            base.LoadViewState(content[1]);
        }

        #endregion

        #region [ -- Properties -- ]

        /**
         * CSS class name for the root HTML element. Default value is "" and will not render the class 
         * attribute on the HTML element.
         */
        [DefaultValue("")]
        public string CssClass
        {
            get { return ViewState["CssClass"] == null ? "" : (string)ViewState["CssClass"]; }
            set
            {
                if (value != CssClass)
                    SetJSONValueString("CssClass", value);
                ViewState["CssClass"] = value;
            }
        }

        /**
         * Collection of style-values, maps to the style attribute on the root HTML element
         */
        public StyleCollection Style
        {
            get { return _styles; }
        }

        #endregion

        #region IAttributeAccessor Members

        public string GetAttribute(string key)
        {
            // TODO: Implement...
            return null;
        }

        public void SetAttribute(string key, string value)
        {
            if (key.ToLower() == "style")
            {
                string[] styles = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string idx in styles)
                {
                    string[] keyValue = idx.Split(':');
                    Style[keyValue[0]] = keyValue[1];
                }
            }
        }

        protected virtual string GetStyleHTMLFormatedAttribute()
        {
            string style = Style.ToString(true);
            if (!string.IsNullOrEmpty(style))
                style = string.Format(" style=\"{0}\"", style);
            return style;
        }

        protected virtual string GetCssClassHTMLFormatedAttribute()
        {
            string cssClass = string.IsNullOrEmpty(CssClass) ? "" : " class=\"" + CssClass + "\"";
            return cssClass;
        }

        #endregion
    }
}
