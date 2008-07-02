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
    [ASP.ParseChildren(true, "Items")]
    [ASP.ToolboxData("<{0}:DropDownList runat=server />")]
    public class DropDownList : RaWebControl, IRaControl
    {
        private ListItemCollection _listItems;

        public event EventHandler SelectedIndexChanged;

        public DropDownList()
        {
            _listItems = new ListItemCollection(this);
        }

        #region [ -- Properties -- ]

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

        [ASP.PersistenceMode(ASP.PersistenceMode.InnerDefaultProperty)]
        public ListItemCollection Items
        {
            get { return _listItems; }
        }

        #endregion

        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        // Override this one to handle events fired on the client-side
        public override void DispatchEvent(string name)
        {
            switch (name)
            {
                case "change":
                    if (SelectedIndexChanged != null)
                        SelectedIndexChanged(this, new EventArgs());
                    break;
                default:
                    throw new ApplicationException("Unknown event fired for control");
            }
        }

        // Override this one to create specific initialization script for your widgets
        public override string GetClientSideScript()
        {
            if (SelectedIndexChanged == null)
                return string.Format("new Ra.Control('{0}');", ClientID);
            else
                return string.Format("new Ra.Control('{0}', {{evts:['change']}});", ClientID);
        }

        // Override this one to create specific HTML for your widgets
        public override string GetHTML()
        {
            string accessKey = string.IsNullOrEmpty(AccessKey) ? "" : string.Format(" accesskey=\"{0}\"", AccessKey);
            return string.Format("<select id=\"{0}\"{1}{2}{3}>{4}</select>", 
                ClientID,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute(),
                accessKey,
                GetHTMLForOptions());
        }

        private string GetHTMLForOptions()
        {
            string retVal = "";
            foreach (ListItem idx in Items)
            {
                retVal += string.Format("<option value=\"{0}\"{2}{3}>{1}</option>",
                    idx.Value,
                    idx.Text,
                    (idx.Enabled ? "" : "disabled=\"disabled\""),
                    (idx.Selected ? "selected=\"selected\"" : ""));
            }
            return retVal;
        }

        #endregion
    }
}
































