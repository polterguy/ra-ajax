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
    [ASP.ToolboxData("<{0}:Button runat=server />")]
    public class Button : RaWebControl, IRaControl
    {
        public event EventHandler Click;

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

        #endregion

        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        // Override this one to handle events fired on the client-side
        public override void DispatchEvent(string name)
        {
            switch (name)
            {
                case "click":
                    if (Click != null)
                        Click(this, new EventArgs());
                    break;
                default:
                    throw new ApplicationException("Unknown event fired for control");
            }
        }

        // Override this one to create specific initialization script for your widgets
        public override string GetClientSideScript()
        {
            if( Click == null )
                return string.Format("new Ra.Control('{0}');", ClientID);
            else
                return string.Format("new Ra.Control('{0}', {{evts:['click']}});", ClientID);
        }

        // Override this one to create specific HTML for your widgets
        public override string GetHTML()
        {
            string accessKey = string.IsNullOrEmpty(AccessKey) ? "" : string.Format(" accesskey=\"{0}\"", AccessKey);
            return string.Format("<input type=\"button\" id=\"{0}\" value=\"{1}\"{2}{3}{4}{5} />", 
                ClientID,
                Text,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute(),
                accessKey,
                (Enabled ? "" : "disabled=\"disabled\""));
        }

        #endregion
    }
}
