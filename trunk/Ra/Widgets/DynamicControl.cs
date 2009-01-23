/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using ASP = System.Web.UI;
using WEBCTRLS = System.Web.UI.WebControls;

namespace Ra.Widgets
{
    [ASP.ToolboxData("<{0}:Dynamic runat=\"server\" />")]
    public class DynamicControl : Panel
    {
        public class DynamicControlEventArgs : EventArgs
        {
            private string _dynamicControlID;

            public string DynamicControlID
            {
              get { return _dynamicControlID; }
              set { _dynamicControlID = value; }
            }

            internal DynamicControlEventArgs(string dynamicControlID)
            {
                _dynamicControlID = dynamicControlID;
            }
        }

        public event EventHandler<DynamicControlEventArgs> DynamicLoad;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string dynamicControlID = ViewState["_dynamicControlID"] != null ? 
                ViewState["_dynamicControlID"].ToString() : string.Empty;

            if (DynamicLoad != null)
                DynamicLoad(this, new DynamicControlEventArgs(dynamicControlID));
        }

        public void AddControl(ASP.Control control)
        {
            if (control == null)
                return;

            if (Controls.Contains(control))
                Controls.Remove(control);

            Controls.Add(control);
            ReRender();
            ViewState["_dynamicControlID"] = control.ID;
        }

        public void RemoveControl(ASP.Control control)
        {
            ViewState["_dynamicControlID"] = null;

            if (control == null)
                return;
            if (Controls.Contains(control))
                Controls.Remove(control);
        }
    }
}
