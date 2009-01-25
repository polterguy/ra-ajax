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
            private string _TypeName;
            private ASP.Control _control;

            public string TypeName
            {
                get { return _TypeName; }
                set { _TypeName = value; }
            }

            public ASP.Control Control
            {
                get { return _control; }
                set { _control = value; }
            }

            internal DynamicControlEventArgs(string typeName)
            {
                _TypeName = typeName;
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
            string typeName = ViewState["_typeName"] as string;
            if (DynamicLoad != null && typeName != null)
            {
                DynamicControlEventArgs e = new DynamicControlEventArgs(typeName);
                DynamicLoad(this, e);
                Controls.Add(e.Control);
            }
        }

        public void SetControl(string typeName)
        {
            Controls.Clear();

            ViewState["_typeName"] = typeName;
            LoadDynamicControl();
            ReRender();
        }
    }
}
