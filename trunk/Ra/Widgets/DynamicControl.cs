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

            public string TypeName
            {
                get { return _TypeName; }
                set { _TypeName = value; }
            }

            internal DynamicControlEventArgs(string typeName)
            {
                _dynamicControlTypeName = typeName;
            }
        }

        public event EventHandler<DynamicControlEventArgs> DynamicLoad;

        protected override void OnLoad(EventArgs e)
        {
            string typeName = ViewState["_typeName"] as string;
            if (DynamicLoad != null && typeName != null)
                DynamicLoad(this, new DynamicControlEventArgs(typeName));

            base.OnLoad(e);
        }

        public void SetControl(ASP.Control ctrl, string typeName)
        {
            Controls.Clear();

            if (value == null)
                return;

            ViewState["_typeName"] = typeName;
            Controls.Add(ctrl);
            ReRender();
        }
    }
}
