/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
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
using Ra.Behaviors;

namespace Ra.Extensions.Widgets
{
    /**
     * Window control. This is the equivalent of a Window on desktop systems. Have lots of "candy features"
     * like the possibility to set the Window into "Modal" mode. In Modal mode none of the controls behind it
     * on the page can be clicked. Can also be moved around on screen, have support for Caption and a 
     * closing icon to close it. You can also easily create your own "icons" by overriding the 
     * CreateTitleBarControls event. Notice that to make the Window Modal you need to add up the 
     * BehaviorObscurer to the Controls collection of the Window.
     */
    [ASP.ToolboxData("<{0}:Window runat=\"server\"></{0}:Window>")]
    [ASP.PersistChildren(true)]
    [ASP.ParseChildren(true, "Content")]
    public class Window : Panel, ASP.INamingContainer
    {
        /**
         * EventArgs passed to the CreateTitleBarControls event of the Window.
         */
        public class CreateTitleBarControlsEventArgs : EventArgs
        {
            private readonly ASP.Control _ctrl;

            internal CreateTitleBarControlsEventArgs(ASP.Control ctrl)
            {
                _ctrl = ctrl;
            }

            /**
             * This is the Caption control which you can add child controls onto.
             */
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
        readonly Label _content = new Label();
        Label _e;
        Label _sw;
        Label _s;
        Label _se;
        readonly Label _caption = new Label();
        readonly LinkButton _close = new LinkButton();
        readonly BehaviorDraggable _dragger = new BehaviorDraggable();

        /**
         * Raised when window is closed by clicking the close icon.
         */
        public event EventHandler Closed;

        /**
         * Raised when window is moved and dropped at new position.
         */
        public event EventHandler Moved;

        /**
         * Handle this event if you need to create extra controls at the Window TitleBar 
         * (next to the close button) for any reasons. Called immediately before Close button 
         * is created (if it is created).
         */
        public event EventHandler<CreateTitleBarControlsEventArgs> CreateTitleBarControls;

        /**
         * Overridden to provide a sane default value
         */
        [DefaultValue("window")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "window";
                return retVal;
            }
            set
            {
                bool sameClass = value == base.CssClass;
                base.CssClass = value;
                if (!sameClass && _nw != null)
                {
                    // If class has actually *changed* AND we have created the child composition
                    // controls...
                    SetCssClassesAndMore();
                }
            }
        }

        /**
         * Header text of window
         */
        [DefaultValue("")]
        public string Caption
        {
            get
            {
                if (string.IsNullOrEmpty(_caption.Text))
                    _caption.Text = "&nbsp;";
                return _caption.Text;
            }
            set { _caption.Text = value; }
        }

        /**
         * If true (default value) then Window will have a Close icon to close it.
         */
        [DefaultValue(true)]
        public bool Closable
        {
            get { return _close.Visible; }
            set { _close.Visible = value; }
        }

        /**
         * If true (default value) then Window can be dragged around and moved.
         */
        [DefaultValue(true)]
        public bool Movable
        {
            get { return _dragger.Enabled; }
            set
            {
                _dragger.Enabled = value;

                _caption.Style["cursor"] = value ? "" : "default";
            }
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
            // Parsing the CssClass since we're constructing CssClasses for 
            // Child Controls this way...
            string cssClass = this.CssClass;
            if (cssClass.IndexOf(' ') != -1)
                cssClass = cssClass.Split(' ')[0];

            // Top parts
            _nw = new Label {Tag = "div", ID = "XXnw", CssClass = cssClass + "_nw"};
            base.Controls.Add(_nw);

            _ne = new Label {Tag = "div", ID = "XXne", CssClass = cssClass + "_ne"};
            _nw.Controls.Add(_ne);

            _n = new Label {Tag = "div", ID = "XXn", CssClass = cssClass + "_n"};
            _ne.Controls.Add(_n);

            _caption.ID = "XXcaption";
            _caption.CssClass = cssClass + "_title";
            _n.Controls.Add(_caption);

            if (CreateTitleBarControls != null)
                CreateTitleBarControls(this, new CreateTitleBarControlsEventArgs(_n));

            _close.ID = "XXclose";
            _close.Click += new EventHandler(CloseClick);
            _close.CssClass = cssClass + "_close";
            _n.Controls.Add(_close);

            // Middle parts
            _body = new Label {Tag = "div", ID = "XXbody", CssClass = cssClass + "_body"};
            base.Controls.Add(_body);

            _w = new Label {Tag = "div", ID = "XXw", CssClass = cssClass + "_w"};
            _body.Controls.Add(_w);

            _e = new Label {Tag = "div", ID = "XXe", CssClass = cssClass + "_e"};
            _w.Controls.Add(_e);

            _content.Tag = "div";
            _content.ID = "XXcontent";
            _content.CssClass = cssClass + "_content";
            _e.Controls.Add(_content);

            // Bottom parts
            _sw = new Label {Tag = "div", ID = "XXsw", CssClass = cssClass + "_sw"};
            _body.Controls.Add(_sw);

            _se = new Label {Tag = "div", ID = "XXse", CssClass = cssClass + "_se"};
            _sw.Controls.Add(_se);

            _s = new Label {Tag = "div", ID = "XXs", Text = "&nbsp;", CssClass = cssClass + "_s"};
            _se.Controls.Add(_s);

            _dragger.ID = "XXdragger";
            _dragger.Handle = _caption.ClientID;
            _dragger.Dropped += DraggerDropped;
            base.Controls.Add(_dragger);
        }

        private void DraggerDropped(object sender, EventArgs e)
        {
            if (Moved != null)
                Moved(this, new EventArgs());
        }

        private void CloseClick(object sender, EventArgs e)
        {
            this.Visible = false;
            if (Closed != null)
                Closed(this, new EventArgs());
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Making sure we're setting the properties correctly
            _close.Visible = Closable;

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

        private void SetCssClassesAndMore()
        {
            // Setting the CSS classes for all "decoration controls"
            string cssClass = CssClass;
            if (cssClass.IndexOf(' ') != -1)
                cssClass = cssClass.Split(' ')[0];

            _nw.CssClass = cssClass + "_nw";
            _n.CssClass = cssClass + "_n";
            _ne.CssClass = cssClass + "_ne";
            _e.CssClass = cssClass + "_e";
            _se.CssClass = cssClass + "_se";
            _s.CssClass = cssClass + "_s";
            _sw.CssClass = cssClass + "_sw";
            _w.CssClass = cssClass + "_w";
            _content.CssClass = cssClass + "_content";
            _body.CssClass = cssClass + "_body";
            _caption.CssClass = cssClass + "_title";

            // Action buttons
            if (Closable)
                _close.CssClass = cssClass + "_close";
        }
    }
}
