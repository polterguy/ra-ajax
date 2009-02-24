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
    public class Dynamic : Panel
    {
        private string _key;

        public class ReloadEventArgs : EventArgs
        {
            private string _key;
            private object _extra;

            public string Key
            {
                get { return _key; }
                set { _key = value; }
            }

            public object Extra
            {
                get { return _extra; }
                set { _extra = value; }
            }

            internal ReloadEventArgs(string key, object extra)
            {
                _key = key;
                _extra = extra;
            }
        }

        public event EventHandler<ReloadEventArgs> Reload;

        protected override void LoadControlState(object savedState)
        {
            if (savedState != null)
            {
                object[] controlSatate = savedState as object[];

                if (controlSatate != null)
                {
                    if (controlSatate[0] != null && controlSatate[0].GetType() == typeof(string))
                        _key = controlSatate[0].ToString();
                    
                    base.LoadControlState(controlSatate[1]);
                }
            }
        }

        protected override object SaveControlState()
        {
            object[] controlState = new object[2];
            controlState[0] = _key;
            controlState[1] = base.SaveControlState();
            return controlState;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack)
            {
                LoadDynamicControl(null);
            }
            base.OnLoad(e);
        }

        private void LoadDynamicControl(object extra)
        {
            if (Reload != null && !string.IsNullOrEmpty(_key))
            {
                ReloadEventArgs e = new ReloadEventArgs(_key, extra);
                Reload(this, e);
            }
        }

        public void LoadControls(string key)
        {
            LoadControls(key, null);
        }

        public void LoadControls(string key, object extra)
        {
            Controls.Clear();

            _key = key;
            LoadDynamicControl(extra);
            ReRender();
        }
    }
}
