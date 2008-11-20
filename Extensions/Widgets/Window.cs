/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
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
    [ASP.PersistChildren(true)]
    [ASP.ParseChildren(true, "Content")]
    public class Window : Panel, ASP.INamingContainer
    {
        public class CreateNavigationalButtonsEvtArgs : EventArgs
        {
            private ASP.Control _ctrl;

            internal CreateNavigationalButtonsEvtArgs(ASP.Control ctrl)
            {
                _ctrl = ctrl;
            }

            public ASP.Control Caption
            {
                get { return _ctrl; }
            }
        }

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
         * Raised when window is moved and dropped at new position
         */
        public event EventHandler Moved;

        /**
         * Raised when window needs "additional navigational buttons" (next to the close button)
         * Called immediately before Close button is created (if it is created)
         */
        public event EventHandler<CreateNavigationalButtonsEvtArgs> CreateNavigationalButtons;

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
         * If true (default value) then Window will have a "Close" icon which makes it possible
         * to close it by clicking.
         */
        [DefaultValue(true)]
        public bool Closable
        {
            get { return ViewState["Closable"] == null ? true : (bool)ViewState["Closable"]; }
            set { ViewState["Closable"] = value; }
        }

        /**
         * If true (default value) then Window can be dragged around and moved.
         */
        [DefaultValue(true)]
        public bool Movable
        {
            get { return ViewState["Movable"] == null ? true : (bool)ViewState["Movable"]; }
            set { ViewState["Movable"] = value; }
        }

        /**
         * Actual content parts of Window
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

        /**
         * The actual "content part" of the Window. This is where your child controls from the markup
         * will be added. If you add up items dynamically to the Window then this is where you should
         * add them up. So instead of using "myWindow.Controls.Add" you should normally use 
         * "myWindow.Content.Add" when adding Controls dynamically to the Window.
         */
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [ASP.PersistenceMode(ASP.PersistenceMode.InnerDefaultProperty)]
        public ASP.ControlCollection Content
        {
            get { return _content.Controls; }
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
            base.Controls.Add(_nw);

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

            if (CreateNavigationalButtons != null)
                CreateNavigationalButtons(this, new CreateNavigationalButtonsEvtArgs(_n));

            if (Closable)
            {
                _close = new LinkButton();
                _close.ID = "XXclose";
                _close.Click += new EventHandler(_close_Click);
                _n.Controls.Add(_close);
            }

            // Middle parts
            _body = new Label();
            _body.Tag = "div";
            _body.ID = "XXbody";
            base.Controls.Add(_body);

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

            if (Movable)
            {
                _dragger = new BehaviorDraggable();
                _dragger.ID = "XXdragger";
                _dragger.Handle = _caption.ClientID;
                _dragger.Dropped += new EventHandler(_dragger_Dropped);
                base.Controls.Add(_dragger);
            }
            else
            {
                _caption.Style["cursor"] = "default";
            }
        }

        private void _dragger_Dropped(object sender, EventArgs e)
        {
            if (Moved != null)
                Moved(this, new EventArgs());
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
            if (Closable)
                _close.CssClass = this.CssClass + "_close";

            // Making sure that all Behaviors are in WINDOW and NOT in "content Panel"
            List<ASP.Control> tmp = new List<System.Web.UI.Control>();
            foreach (ASP.Control idx in _content.Controls)
            {
                if (idx is Behavior)
                    tmp.Add(idx);
            }
            foreach (ASP.Control idx in tmp)
            {
                _content.Controls.Remove(idx);
                Controls.Add(idx);
            }

            // Calling base...
            base.OnPreRender(e);
        }
    }
}
