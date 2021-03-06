﻿/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Widgets;
using HTML = System.Web.UI.HtmlControls;
using Ra.Builder;

namespace Ra.Extensions.Widgets
{
    /**
     * A nicer button with skinning capablities, support for adding buttons and lots of other 
     * "candy goodies". In all other regards it's pretty similar to the normal Ra.Widgets.Button
     * control. This one will render as the HTML button element though and not as an input type="button"
     * element.
     */
    [ASP.ToolboxData("<{0}:ExtButton runat=server />")]
    public class ExtButton : RaWebControl
    {
        /**
         * The text that is displayed within the button, default value is string.Empty.
         */
        [DefaultValue("")]
        public string Text
        {
            get { return ViewState["Text"] == null ? "" : (string)ViewState["Text"]; }
            set
            {
                if (value != Text)
                    SetJSONValueString("Text", value);
                ViewState["Text"] = value;
            }
        }

        /**
         * The keyboard shortcut for clicking the button. Most browsers implements
         * some type of keyboard shortcut logic like for instance FireFox allows
         * form elements to be triggered by combining the AccessKey value (single character)
         * together with ALT and SHIFT. Meaning if you have e.g. "H" as keyboard shortcut
         * you can click this button by doing ALT+SHIFT+H on your keyboard. The combinations
         * to effectuate the keyboard shortcuts however vary from browsers to browsers.
         */
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

        /**
         * If false then the button is disabled, otherwise it is enabled. A disabled button cannot be
         * clicked and will not raise events on the server when clicked.
         */
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

        /**
         * Overridden to provide a sane default value. The default value of this property is "button".
         */
        [DefaultValue("ra-button")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "ra-button";
                return retVal;
            }
            set { base.CssClass = value; }
        }

        protected override string GetClientSideScriptOptions()
        {
            string retVal = base.GetClientSideScriptOptions();
            if (!string.IsNullOrEmpty(retVal))
                retVal += ",";
            retVal += string.Format("label:'{0}_LBL'", ClientID);
            return retVal;
        }

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element el = builder.CreateElement("button"))
            {
                AddAttributes(el);
                using (Element bRight = builder.CreateElement("span"))
                {
                    bRight.AddAttribute("class", "ra-button-right");
                    using (Element bLeft = builder.CreateElement("span"))
                    {
                        bLeft.AddAttribute("class", "ra-button-left");
                        using (Element bCenter = builder.CreateElement("span"))
                        {
                            bCenter.AddAttribute("class", "ra-button-center");
                            using (Element content = builder.CreateElement("span"))
                            {
                                content.AddAttribute("class", "ra-button-content");
                                content.AddAttribute("id", ClientID + "_LBL");
                                content.Write(Text);
                            }
                        }
                    }
                }
            }
        }

        protected override void AddAttributes(Element el)
        {
            el.AddAttribute("type", "button");
            if (!string.IsNullOrEmpty(AccessKey))
                el.AddAttribute("accesskey", AccessKey);
            if (!Enabled)
                el.AddAttribute("disabled", "disabled");
            base.AddAttributes(el);
        }
    }
}
