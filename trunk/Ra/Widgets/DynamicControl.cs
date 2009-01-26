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
    [ASP.ToolboxData("<{0}:DynamicControl runat=\"server\" />")]
    public class DynamicControl : Panel
    {
        public class DynamicControlEventArgs : EventArgs
        {
            private string _controlKey;

            public string ControlKey
            {
                get { return _controlKey; }
                set { _controlKey = value; }
            }

            internal DynamicControlEventArgs(string controlKey)
            {
                _controlKey = controlKey;
            }
        }

        public event EventHandler<DynamicControlEventArgs> DynamicLoad;

        protected override void OnLoad(EventArgs e)
        {
            LoadDynamicControl();
            base.OnLoad(e);
        }

        private void LoadDynamicControl()
        {
            string typeName = ViewState["_controlKey"] as string;
            if (DynamicLoad != null && typeName != null)
            {
                DynamicControlEventArgs e = new DynamicControlEventArgs(typeName);
                DynamicLoad(this, e);
            }
        }

        public void Initialize(string controlKey)
        {
            Controls.Clear();

            ViewState["_controlKey"] = controlKey;
            LoadDynamicControl();
            ReRender();
        }
    }
}
