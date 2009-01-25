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
            private string _dynamicControlTypeName;

            public string DynamicControlTypeName
            {
              get { return _dynamicControlTypeName; }
              set { _dynamicControlTypeName = value; }
            }

            internal DynamicControlEventArgs(string dynamicControlTypeName)
            {
                _dynamicControlTypeName = dynamicControlTypeName;
            }
        }

        public event EventHandler<DynamicControlEventArgs> DynamicLoad;

        protected override void OnLoad(EventArgs e)
        {
            if (DynamicLoad != null && ViewState["_dynamicControlTypeName"] != null)
                DynamicLoad(this, new DynamicControlEventArgs(ViewState["_dynamicControlTypeName"].ToString()));

            base.OnLoad(e);
        }

        [Browsable(false)]
        public ASP.Control Control
        {
            set
            {
                if (value == null)
                    return;

                Controls.Clear();

                ViewState["_dynamicControlTypeName"] = value.GetType().FullName;
                Controls.Add(value);
                ReRender();
            }
        }
    }
}
