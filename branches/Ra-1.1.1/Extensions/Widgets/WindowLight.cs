/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

namespace Ra.Extensions
{
    /**
     * Light version of Window, contains far less markup and are less CSS and browser demanding/intensive
     */
    [ASP.ToolboxData("<{0}:WindowLight runat=\"server\"></{0}:WindowLight>")]
    public class WindowLight : Panel, ASP.INamingContainer
    {
		private WEBCTRLS.Panel _pnlHead;
		private Label _lblHead;
		private LinkButton _close;
		private BehaviorDraggable _dragger;

        /**
         * Raised when window is closed by clicking the close icon
         */
        public event EventHandler Closed;

        /**
         * Header text of window
         */
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
			_close = new LinkButton();
			_close.Text = "Close";
			_close.Click += _close_Clicked;
			_pnlHead.Controls.Add(_close);
			this.Controls.AddAt(0, _pnlHead);
			
			// Creating dragger
			_dragger = new BehaviorDraggable();
			_dragger.ID = "dragger";
			this.Controls.Add(_dragger);
        }
		
		protected void _close_Clicked(object sender, EventArgs e)
		{
			this.Visible = false;
            if (Closed != null)
                Closed(this, new EventArgs());
		}
		
		protected override void OnPreRender (EventArgs e)
		{
			_pnlHead.CssClass = CssClass.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0] + "-head";
			_close.CssClass = CssClass.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)[0] + "-close";
			_lblHead.Text = Caption;
			_dragger.Handle = _pnlHead.ClientID;
			base.OnPreRender (e);
		}
    }
}
