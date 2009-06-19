/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using System.Web.UI;
using System.Web;
using Ra.Builder;

namespace Ra.Widgets
{
    /**
     * HiddenField control. Useful for storing values into your page which you need to access later down
     * the road but don't need to actually display to the user. Alternatives to this control is to directly
     * us the ViewState.
     */
    [DefaultProperty("Text")]
    [ASP.ToolboxData("<{0}:HiddenField runat=server />")]
    public class HiddenField : RaControl
    {
        #region [ -- Properties -- ]

        /**
         * Value property of the hiddenfield
         */
        [DefaultValue("")]
        public string Value
        {
            get { return ViewState["Value"] == null ? "" : (string)ViewState["Value"]; }
            set
            {
                if (value != Value)
                    SetJSONValueString("Value", value);
                ViewState["Value"] = value;
            }
        }

        #endregion

        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        protected override void OnInit(EventArgs e)
        {
            // Since if ViewState is DISABLED we will NEVER come into LoadViewState we need to
            // have the same logic in OnInit since we really should modify the Text value to
            // the postback value BEFORE Page_Load event is fired...
            if (!this.IsViewStateEnabled && ((Page)HttpContext.Current.CurrentHandler).IsPostBack)
            {
                string value = ((Page)HttpContext.Current.CurrentHandler).Request.Params[ClientID];
                if (value != Value)
                    Value = value;
            }
            base.OnInit(e);
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            // Since if ViewState is DISABLED we will NEVER come into this bugger we need to
            // have the same logic in OnInit since we really should modify the Text value to
            // the postback value BEFORE Page_Load event is fired...
            if (((Page)HttpContext.Current.CurrentHandler).IsPostBack)
            {
                string value = ((Page)HttpContext.Current.CurrentHandler).Request.Params[ClientID];
                if (value != Value)
                    Value = value;
            }
        }

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element el = builder.CreateElement("input"))
            {
                AddAttributes(el);
            }
        }

        protected override void AddAttributes(Element el)
        {
            el.AddAttribute("type", "hidden");
            el.AddAttribute("name", ClientID);
            el.AddAttribute("value", Value);
            base.AddAttributes(el);
        }

        #endregion
    }
}
