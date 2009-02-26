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
        private object _extra;
        private bool _firstReload;

        public class ReloadEventArgs : EventArgs
        {
            private string _key;
            private object _extra;
            private bool _firstReload;

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
            
            public bool FirstReload
            {
                get { return _firstReload; }
            }

            internal ReloadEventArgs(string key, object extra, bool firstReload)
            {
                _key = key;
                _extra = extra;
                _firstReload = firstReload;
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
                    if (controlSatate[1] != null)
                        _extra = controlSatate[1];
                    if (controlSatate[2] != null && controlSatate[2].GetType() == typeof(bool))
                        _firstReload = (bool)controlSatate[2];
                    
                    base.LoadControlState(controlSatate[3]);
                }
            }
        }

        protected override object SaveControlState()
        {
            object[] controlState = new object[4];
            controlState[0] = _key;
            controlState[1] = _extra;
            controlState[2] = _firstReload;
            controlState[3] = base.SaveControlState();
            return controlState;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack)
            {
                LoadDynamicControl();
            }
            base.OnLoad(e);
        }

        private void LoadDynamicControl()
        {
            if (Reload != null && !string.IsNullOrEmpty(_key))
            {
                ReloadEventArgs e = new ReloadEventArgs(_key, _extra, _firstReload);
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
            _extra = extra;
            _firstReload = true;
            LoadDynamicControl();
            _firstReload = false;
            ReRender();
        }

        public void ClearControls()
        {
            Controls.Clear();
            
            _key = string.Empty;
            _extra = null;
            ReRender();
        }
    }
}
