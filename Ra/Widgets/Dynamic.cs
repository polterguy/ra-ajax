/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;
using System.ComponentModel;
using ASP = System.Web.UI;
using WEBCTRLS = System.Web.UI.WebControls;

namespace Ra.Widgets
{
    /**
     * The Dynamic Control is very useful for having portions of your page dynamically rendered with
     * different control collections. By utilizing this control it is very easy to create support for
     * dynamically loaded controls in your own applications. If used correctly this control also
     * have the capacity to significantly reduce the amount of ViewState in your applications. This is 
     * especially true for "single website Ajax Applications" where you have tons of features and controls
     * on the same page. The basic logic of the control is that it will internally keep a reference to
     * a "key" which you submit to it and then call an event handler with that key in which you decide 
     * which Control collection to load and stuff into the Controls collection of the Dynamic Control.
     * Then when an Ajax Callback (or postback) occurs it will re-call that same Event Handler with
     * the same key to give the impression of statefulness.
     */
    [ASP.ToolboxData("<{0}:Dynamic runat=\"server\" />")]
    public class Dynamic : Panel
    {
        private string _key;
        private object _extra;

        /**
         * This is the EventArgs which will be passed into the Reload event of the Dynamic Control.
         * You must ALWAYS load the EXACT same Controls when the same Key is passed in.
         */
        public class ReloadEventArgs : EventArgs
        {
            private string _key;
            private object _extra;
            private bool _firstReload;

            public string Key
            {
                get { return _key; }
            }

            public object Extra
            {
                get { return _extra; }
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
                    
                    base.LoadControlState(controlSatate[2]);
                }
            }
        }

        protected override object SaveControlState()
        {
            object[] controlState = new object[3];
            controlState[0] = _key;
            controlState[1] = _extra;
            controlState[2] = base.SaveControlState();
            return controlState;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack)
            {
                LoadDynamicControl(false);
            }
            base.OnLoad(e);
        }

        private void LoadDynamicControl(bool firstReload)
        {
            if (Reload != null && _key != null)
            {
                ReloadEventArgs e = new ReloadEventArgs(_key, _extra, firstReload);
                Reload(this, e);
            }
        }

        public void LoadControls()
        {
            LoadControls(string.Empty, null);
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
            LoadDynamicControl(true);
            ReRender();
        }

        public void ClearControls()
        {
            Controls.Clear();
            
            _key = null;
            _extra = null;
            ReRender();
        }
    }
}
