/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Helpers;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:Window runat=\"server\"></{0}:Window>")]
    public class Window : Panel
    {
		private WEBCTRLS.Panel _pnlHead;
		private Label _lblHead;
		private BehaviorDraggable _dragger;

        [DefaultValue("")]
        public string Caption
        {
            get { return ViewState["Text"] == null ? "" : (string)ViewState["Text"]; }
            set { ViewState["Text"] = value; }
        }

		protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            // Since we're dependant upon that the ViewState has finished loading before
            // we initialize the ChildControls since how the child controls (and which)
            // child controls are being re-created is dependant upon a ViewState saved value
            // this is the earliest possible time we can reload the ChildControls for the
            // Control
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            CreateWindowControls();
        }

        private void CreateWindowControls()
        {
			// Creating header control
			_pnlHead = new WEBCTRLS.Panel();
			_pnlHead.ID = "head";
			_lblHead = new Label();
			_lblHead.ID = "headCaption";
			_pnlHead.Controls.Add(_lblHead);
			this.Controls.AddAt(0, _pnlHead);
			
			// Creating dragger
			_dragger = new BehaviorDraggable();
			this.Controls.Add(_dragger);
        }
		
		protected override void OnPreRender (EventArgs e)
		{
			_pnlHead.CssClass = CssClass + "-head";
			_lblHead.Text = Caption;
			_dragger.Handle = _pnlHead.ClientID;
			base.OnPreRender (e);
		}
    }
}
