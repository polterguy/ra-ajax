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
            private string _key;

            public string Key
            {
                get { return _key; }
                set { _key = value; }
            }

            internal DynamicControlEventArgs(string key)
            {
                _key = key;
            }
        }

        public event EventHandler<DynamicControlEventArgs> LoadControls;

        protected override void OnLoad(EventArgs e)
        {
            LoadDynamicControl();
            base.OnLoad(e);
        }

        private void LoadDynamicControl()
        {
            string typeName = ViewState["_key"] as string;
            if (LoadControls != null && typeName != null)
            {
                DynamicControlEventArgs e = new DynamicControlEventArgs(typeName);
                LoadControls(this, e);
            }
        }

        public void ReLoadControls(string key)
        {
            Controls.Clear();

            ViewState["_key"] = key;
            LoadDynamicControl();
            ReRender();
        }
    }
}
