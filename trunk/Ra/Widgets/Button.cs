/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
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

        public event EventHandler Blur;

        public event EventHandler Focused;

        public event EventHandler MouseOver;

        public event EventHandler MouseOut;

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
                case "mouseover":
                    if (MouseOver != null)
                        MouseOver(this, new EventArgs());
                    break;
                case "mouseout":
                    if (MouseOut != null)
                        MouseOut(this, new EventArgs());
                    break;
                case "blur":
                    if (Blur != null)
                        Blur(this, new EventArgs());
                    break;
                case "focus":
                    if (Focused != null)
                        Focused(this, new EventArgs());
                    break;
                default:
                    throw new ApplicationException("Unknown event fired for control");
            }
        }

        // Override this one to create specific initialization script for your widgets
        private bool _scriptRetrieved;
        public override string GetClientSideScript()
        {
            if (_scriptRetrieved)
                return "";
            _scriptRetrieved = true;
            string evts = "";
            if (Click != null)
                evts += "['click']";
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
            if (Blur != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['blur']";
            }
            if (Focused != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['focus']";
            }
			string behaviors = GetBehaviorRegisterScript();
            if (evts.Length == 0)
            {
                if (_hasSetFocus)
                {
                    string options = "focus:true" + (behaviors == string.Empty ? "" : "," + behaviors);
                    return string.Format("\r\nRa.C('{0}',{{{1}}});", 
					    ClientID, 
					    options);
                }
                else
                {
                    return string.Format("\r\nRa.C('{0}');", ClientID);
                }
            }
            else
            {
                if (_hasSetFocus)
                {
                    string options = "";
                    if (_hasSetFocus)
                        options += "focus:true";
                    return string.Format("Ra.C('{0}',{{evts:[{2}],{1}}});", ClientID, options, evts);
                }
                else
                {
                    return string.Format("Ra.C('{0}',{{evts:[{1}]}});", ClientID, evts);
                }
            }
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
