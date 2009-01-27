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

        public class LoadControlsEventArgs : EventArgs
        {
            private string _key;

            public string Key
            {
                get { return _key; }
                set { _key = value; }
            }

            internal LoadControlsEventArgs(string key)
            {
                _key = key;
            }
        }

        public event EventHandler<LoadControlsEventArgs> LoadControls;

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
                LoadDynamicControl();
            }
            base.OnLoad(e);
        }

        private void LoadDynamicControl()
        {
            if (LoadControls != null && !string.IsNullOrEmpty(_key))
            {
                LoadControlsEventArgs e = new LoadControlsEventArgs(_key);
                LoadControls(this, e);
            }
        }

        public void ReLoadControls(string key)
        {
            Controls.Clear();

            _key = key;
            LoadDynamicControl();
            ReRender();
        }
    }
}
