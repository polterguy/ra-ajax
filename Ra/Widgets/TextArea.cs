/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
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
    [ASP.ToolboxData("<{0}:TextBox runat=server />")]
    public class TextArea : RaWebControl, IRaControl
    {
        public event EventHandler TextChanged;

        #region [ -- Properties -- ]

        [DefaultValue("")]
        public string Text
        {
            get { return ViewState["Text"] == null ? "" : (string)ViewState["Text"]; }
            set
            {
                if (value != Text)
                    SetJSONValueString("Value", value);
                ViewState["Text"] = value;
            }
        }

        [DefaultValue("")]
        public string AccessKey
        {
            get { return ViewState["AccessKey"] == null ? "" : (string)ViewState["AccessKey"]; }
            set
            {
                if (value != AccessKey)
                    SetJSONValueString("AccessKey", value);
                ViewState["AccessKey"] = value;
            }
        }

        [DefaultValue(true)]
        public bool Enabled
        {
            get { return ViewState["Enabled"] == null ? true : (bool)ViewState["Enabled"]; }
            set
            {
                if (value != Enabled)
                    SetJSONGenericValue("disabled", (value ? "" : "disabled"));
                ViewState["Enabled"] = value;
            }
        }

        [DefaultValue(20)]
        public int Columns
        {
            get { return ViewState["Columns"] == null ? 20 : (int)ViewState["Columns"]; }
            set
            {
                if (value != Columns)
                    SetJSONGenericValue("cols", value.ToString());
                ViewState["Columns"] = value;
            }
        }

        [DefaultValue(2)]
        public int Rows
        {
            get { return ViewState["Rows"] == null ? 3 : (int)ViewState["Rows"]; }
            set
            {
                if (value != Rows)
                    SetJSONGenericValue("rows", value.ToString());
                ViewState["Rows"] = value;
            }
        }

        #endregion

        private bool _hasSetSelect;
        public void Select()
        {
            _hasSetSelect = true;
            if (AjaxManager.Instance.IsCallback)
            {
                SetJSONValueString("Select", "");
            }
        }

        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        protected override void OnInit(EventArgs e)
        {
            // Since if ViewState is DISABLED we will NEVER come into LoadViewState we need to
            // have the same logic in OnInit since we really should modify the Text value to
            // the postback value BEFORE Page_Load event is fired...
            if (Enabled && !this.IsViewStateEnabled && AjaxManager.Instance.CurrentPage.IsPostBack)
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

            // Since if ViewState is DISABLED we will NEVER come into this bugger we need to
            // have the same logic in OnInit since we really should modify the Text value to
            // the postback value BEFORE Page_Load event is fired...
            if (Enabled && AjaxManager.Instance.CurrentPage.IsPostBack)
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
            {
                if (_hasSetFocus || _hasSetSelect)
                {
                    string options = "";
                    if (_hasSetFocus)
                        options += "focus:true";
                    if (_hasSetSelect)
                    {
                        if (options.Length > 0)
                            options += ",";
                        options += "select:true";
                    }
                    return string.Format("Ra.C('{0}',{{{1}}});", ClientID, options);
                }
                else
                {
                    return string.Format("Ra.C('{0}');", ClientID);
                }
            }
            else
            {
                if (_hasSetFocus || _hasSetSelect)
                {
                    string options = "";
                    if (_hasSetFocus)
                        options += "focus:true";
                    if (_hasSetSelect)
                    {
                        if (options.Length > 0)
                            options += ",";
                        options += "select:true";
                    }
                    return string.Format("Ra.C('{0}',{{evts:[['change']],{1}}});", ClientID, options);
                }
                else
                {
                    return string.Format("Ra.C('{0}',{{evts:[['change']]}});", ClientID);
                }
            }
        }

        // Override this one to create specific HTML for your widgets
        public override string GetHTML()
        {
            string accessKey = string.IsNullOrEmpty(AccessKey) ? "" : string.Format(" accesskey=\"{0}\"", AccessKey);
            return string.Format("<textarea id=\"{0}\" name=\"{0}\" rows=\"{5}\" cols=\"{6}\"{2}{3}{4}{7}>{1}</textarea>",
                ClientID,
                Text,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute(),
                accessKey,
                Rows,
                Columns,
                (Enabled ? "" : " disabled=\"disabled\""));
        }

        #endregion
    }
}
