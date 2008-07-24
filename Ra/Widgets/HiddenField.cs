/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Helpers;

namespace Ra.Widgets
{
    [DefaultProperty("Text")]
    [ASP.ToolboxData("<{0}:HiddenField runat=server />")]
    public class HiddenField : RaControl, IRaControl
    {
        #region [ -- Properties -- ]

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
        public override string GetHTML()
        {
            return string.Format("<input type=\"hidden\" id=\"{0}\" name=\"{0}\" value=\"{1}\" />",
                ClientID,
                Value);
        }

        #endregion
    }
}
