/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using System.IO;
using Ra.Builder;

namespace Ra.Widgets
{
    /**
     * Panel control. Think of this as the "collection of other controls" in Ra-Ajax. Though most controls
     * in Ra-Ajax can actually contain other controls, this is the prefered one to use when you need a "wrapper"
     * to "host" other controls.It also have a very useful feature in the forms of the ReRender method which
     * will mke the Panel transform into an "UpdatePanel" and actually do Partial Rendering when called. This
     * feature is highly useful coupled with other 3rd party (non Ra-Ajax) controls or even when you need to
     * Ajaxify conventional ASP.NET Controls like a GridView or a Repeater.
     */
    [DefaultProperty("CssClass")]
    [ASP.ToolboxData("<{0}:Panel runat=server></{0}:Panel>")]
    public class Panel : RaWebControl, ASP.INamingContainer
    {
        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        /**
         * The HTML tag used to render the label, defaults to div
         */
        [DefaultValue("div")]
        public string Tag
        {
            get { return ViewState["Tag"] == null ? "div" : (string)ViewState["Tag"]; }
            set { ViewState["Tag"] = value; }
        }

        /**
         * The button that will be clicked if user clicks enter (Carriage Return) within the panel 
         * somehow. E.g. by having focus in TextBox and clicking Enter...
         */
        [DefaultValue(null)]
        public string DefaultWidget
        {
            get { return ViewState["DefaultWidget"] == null ? null : (string)ViewState["DefaultWidget"]; }
            set { ViewState["DefaultWidget"] = value; }
        }

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element el = builder.CreateElement(Tag))
            {
                AddAttributes(el);
                RenderChildren(builder.Writer as System.Web.UI.HtmlTextWriter);
            }
        }

        protected override string GetClientSideScriptOptions()
        {
            string retVal = base.GetClientSideScriptOptions();
            if (!string.IsNullOrEmpty(DefaultWidget))
            {
                if (!string.IsNullOrEmpty(retVal))
                    retVal += ",";
                retVal += string.Format("def_wdg:'{0}'", AjaxManager.Instance.FindControl(this, DefaultWidget).ClientID);
            }
            return retVal;
        }

        #endregion
	}
}
