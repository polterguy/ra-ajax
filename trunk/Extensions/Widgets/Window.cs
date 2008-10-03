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
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace Ra.Extensions
{
    /**
     * window control, Panel with extra capabilities
     */
    [ASP.ToolboxData("<{0}:Window runat=\"server\"></{0}:Window>")]
    public class Window : Panel, ASP.INamingContainer
    {
        Label _nw;
        Label _n;
        Label _ne;
        Label _w;
        Label _body;
        Label _content = new Label();
        Label _e;
        Label _sw;
        Label _s;
        Label _se;
        Label _caption;
        LinkButton _close;
        BehaviorDraggable _dragger;

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
            get { return ViewState["Caption"] == null ? "" : (string)ViewState["Caption"]; }
            set { ViewState["Caption"] = value; }
        }

        /**
         * This is the actual inner control where the child controls are being rendered (re-arranged in PreRender)
         */
        public Label SurfaceControl
        {
            get { return _content; }
        }

        protected override void OnInit(EventArgs e)
        {
            EnsureChildControls();
            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            CreateWindowControls();
        }

        private void CreateWindowControls()
        {
            // Top parts
            _nw = new Label();
            _nw.Tag = "div";
            _nw.ID = "XXnw";
            Controls.Add(_nw);

            _ne = new Label();
            _ne.Tag = "div";
            _ne.ID = "XXne";
            _nw.Controls.Add(_ne);

            _n = new Label();
            _n.Tag = "div";
            _n.ID = "XXn";
            _ne.Controls.Add(_n);

            _caption = new Label();
            _caption.ID = "XXcaption";
            _n.Controls.Add(_caption);

            _close = new LinkButton();
            _close.ID = "XXclose";
            _close.Click += new EventHandler(_close_Click);
            _n.Controls.Add(_close);

            // Middle parts
            _body = new Label();
            _body.Tag = "div";
            _body.ID = "XXbody";
            Controls.Add(_body);

            _w = new Label();
            _w.Tag = "div";
            _w.ID = "XXw";
            _body.Controls.Add(_w);

            _e = new Label();
            _e.Tag = "div";
            _e.ID = "XXe";
            _w.Controls.Add(_e);

            _content.Tag = "div";
            _content.ID = "XXcontent";
            _e.Controls.Add(_content);

            // Bottom parts
            _sw = new Label();
            _sw.Tag = "div";
            _sw.ID = "XXsw";
            _body.Controls.Add(_sw);

            _se = new Label();
            _se.Tag = "div";
            _se.ID = "XXse";
            _sw.Controls.Add(_se);

            _s = new Label();
            _s.Tag = "div";
            _s.ID = "XXs";
            _s.Text = "&nbsp;";
            _se.Controls.Add(_s);

            _dragger = new BehaviorDraggable();
            _dragger.ID = "XXdragger";
            _dragger.Handle = _caption.ClientID;
            Controls.Add(_dragger);

            // Moving controls to where they SHOULD be...
            // This time to get the ViewState right...
            ReArrangeControls();
        }

        private void _close_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            if (Closed != null)
                Closed(this, new EventArgs());
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Setting the CSS classes for all "decoration controls"
            _nw.CssClass = this.CssClass + "_nw";
            _n.CssClass = this.CssClass + "_n";
            _ne.CssClass = this.CssClass + "_ne";
            _e.CssClass = this.CssClass + "_e";
            _se.CssClass = this.CssClass + "_se";
            _s.CssClass = this.CssClass + "_s";
            _sw.CssClass = this.CssClass + "_sw";
            _w.CssClass = this.CssClass + "_w";
            _content.CssClass = this.CssClass + "_content";
            _body.CssClass = this.CssClass + "_body";
            _caption.CssClass = this.CssClass + "_title";

            // Making sure our Caption is displayed correctly
            _caption.Text = Caption;

            // Action buttons
            _close.CssClass = this.CssClass + "_close";

            // Moving controls to where they SHOULD be...
            ReArrangeControls();

            // Calling base...
            base.OnPreRender(e);
        }

        private void ReArrangeControls()
        {
            // Moving all controls where they SHOULD be
            List<ASP.Control> controls = new List<ASP.Control>();
            foreach (ASP.Control idx in Controls)
            {
                if (string.IsNullOrEmpty(idx.ID) || idx.ID.Substring(0, 2) != "XX")
                    controls.Add(idx);
            }
            foreach (ASP.Control idx in controls)
            {
                Controls.Remove(idx);
                _content.Controls.Add(idx);
            }
        }
    }
}
