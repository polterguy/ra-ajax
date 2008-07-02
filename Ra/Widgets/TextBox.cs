/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc in addition to that 
 * the code also is licensed under a pure GPL license for those that
 * cannot for some reasons obey by rules in the MIT(ish) kind of license.
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
    [ASP.ToolboxData("<{0}:TextBox runat=server />")]
    public class TextBox : RaWebControl, IRaControl
    {
        public enum TextBoxMode
        {
            SingleLine,
            MultiLine,
            Password
        };

        public event EventHandler TextChanged;

        #region [ -- Properties -- ]

        [DefaultValue("")]
        public string Text
        {
            get { return ViewState["Text"] == null ? "" : (string)ViewState["Text"]; }
            set
            {
                ViewState["Text"] = value;
                SetJSONValueString("Value", value);
            }
        }

        [DefaultValue("")]
        public string AccessKey
        {
            get { return ViewState["AccessKey"] == null ? "" : (string)ViewState["AccessKey"]; }
            set
            {
                ViewState["AccessKey"] = value;
                SetJSONValueString("AccessKey", value);
            }
        }

        [DefaultValue(TextBox.TextBoxMode.SingleLine)]
        public TextBoxMode TextMode
        {
            get { return ViewState["TextMode"] == null ? TextBoxMode.SingleLine : (TextBoxMode)ViewState["TextMode"]; }
            set
            {
                ViewState["TextMode"] = value;
                // Cannot change TextMode in Callbacks
                // TODO: Check for callback (and change) and throw exception or something
            }
        }

        #endregion

        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        protected override void OnInit(EventArgs e)
        {
            if (!this.IsViewStateEnabled && AjaxManager.Instance.CurrentPage.IsPostBack)
            {
                string valueOfTextBox = AjaxManager.Instance.CurrentPage.Request.Params[ClientID];
                if (valueOfTextBox != Text)
                    Text = valueOfTextBox;
            }
            base.OnInit(e);
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            if (AjaxManager.Instance.CurrentPage.IsPostBack)
            {
                string valueOfTextBox = AjaxManager.Instance.CurrentPage.Request.Params[ClientID];
                if (valueOfTextBox != Text)
                    Text = valueOfTextBox;
            }
        }

        // Override this one to handle events fired on the client-side
        public override void DispatchEvent(string name)
        {
            //System.Web.UI.WebControls.TextBox box;
            //box.TextMode = System.Web.UI.WebControls.TextBoxMode.
            switch (name)
            {
                case "change":
                    if (TextChanged != null)
                        TextChanged(this, new EventArgs());
                    break;
                default:
                    throw new ApplicationException("Unknown event fired for control");
            }
        }

        // Override this one to create specific initialization script for your widgets
        public override string GetClientSideScript()
        {
            if (TextChanged == null)
                return string.Format("new Ra.Control('{0}');", ClientID);
            else
                return string.Format("new Ra.Control('{0}', {{evts:['change']}});", ClientID);
        }

        // Override this one to create specific HTML for your widgets
        public override string GetHTML()
        {
            string accessKey = string.IsNullOrEmpty(AccessKey) ? "" : string.Format(" accesskey=\"{0}\"", AccessKey);
            if (TextMode == TextBoxMode.MultiLine)
            {
                return string.Format("<textarea id=\"{0}\"{2}{3}{4}>{1}</textarea>",
                    ClientID,
                    Text.Replace("\\", "\\\\").Replace("'", "\\'"),
                    GetCssClassHTMLFormatedAttribute(),
                    GetStyleHTMLFormatedAttribute(),
                    accessKey);
            }
            else
            {
                return string.Format("<input type=\"{5}\" id=\"{0}\" value=\"{1}\"{2}{3}{4} />",
                    ClientID,
                    Text.Replace("\\", "\\\\").Replace("'", "\\'"),
                    GetCssClassHTMLFormatedAttribute(),
                    GetStyleHTMLFormatedAttribute(),
                    accessKey,
                    (TextMode == TextBoxMode.SingleLine ? "text" : "password"));
            }
        }

        #endregion
    }
}
