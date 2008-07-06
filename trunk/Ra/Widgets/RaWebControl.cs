/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web.UI;
using System.ComponentModel;
using System.Collections.Generic;
using Ra.Helpers;

namespace Ra.Widgets
{
    public abstract class RaWebControl : RaControl, IAttributeAccessor
    {
        StyleCollection _styles;

        // Need default CTOR impl. to make sure we get to create the style collection with 
        // the pointer to the this widget...
        public RaWebControl()
        {
            _styles = new StyleCollection(this);
        }

        #region [ -- Overridden Base Class methods -- ]

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

            Style.LoadViewState(content[0]);

            // When we save the ViewState we save the base class object as the second instance in the array...
            base.LoadViewState(content[1]);
        }

        #endregion

        #region [ -- Properties -- ]

        [DefaultValue("")]
        public string CssClass
        {
            get { return ViewState["CssClass"] == null ? "" : (string)ViewState["CssClass"]; }
            set { ViewState["CssClass"] = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        public StyleCollection Style
        {
            get { return _styles; }
        }

        #endregion

        #region IAttributeAccessor Members

        public string GetAttribute(string key)
        {
            if (key.ToLower() == "style")
            {
                // TODO: Implement...!!
                return "color:Red;";
            }
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
            string style = Style.ToString();
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
