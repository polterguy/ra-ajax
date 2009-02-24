/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;

namespace Ra.Widgets
{
    /**
     * HiddenField control, renders &lt;input type="hidden"...
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
            if (!this.IsViewStateEnabled && AjaxManager.Instance.CurrentPage.IsPostBack)
            {
                string value = AjaxManager.Instance.CurrentPage.Request.Params[ClientID];
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
            if (AjaxManager.Instance.CurrentPage.IsPostBack)
            {
                string value = AjaxManager.Instance.CurrentPage.Request.Params[ClientID];
                if (value != Value)
                    Value = value;
            }
        }

        // Override this one to create specific HTML for your widgets
        protected override string GetOpeningHTML()
        {
            return string.Format("<input type=\"hidden\" id=\"{0}\" name=\"{0}\" value=\"{1}\" />",
                ClientID,
                Value);
        }

        #endregion
    }
}
