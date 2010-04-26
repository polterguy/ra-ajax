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
using HTML = System.Web.UI.HtmlControls;
using Ra.Behaviors;
using Ra.Builder;

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
        readonly Panel _content = new Panel();
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
         * Overridden to provide a sane default value
         */
        [DefaultValue("ra-window")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "ra-window";
                return retVal;
            }
            set { base.CssClass = value; }
        }

        /**
         * Header text of window
         */
        [DefaultValue("")]
        public string Caption
        {
            get { return _caption.Text; }
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
                _caption.Style["cursor"] = value ? "move" : "default";
            }
        }

        /**
         * Actual content parts of Window
         */
        public Panel ContentControl
        {
            get { return _content; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            EnsureChildControls();
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
            _caption.ID = "capt";
            _caption.CssClass = "ra-window-caption";
            Controls.Add(_caption);

            _close.ID = "close";
            _close.CssClass = "ra-window-close";
            _close.Text = "&nbsp;";
            _close.Click += CloseClick;
            Controls.Add(_close);

            _content.ID = "cont";
            _content.CssClass = "ra-window-content";
            Controls.Add(_content);

            _dragger.ID = "dragger";
            _dragger.Dropped += DraggerDropped;
            _dragger.Handle = _caption.ClientID;
            Controls.Add(_dragger);
        }

        private void DraggerDropped(object sender, EventArgs e)
        {
            if (Moved != null)
                Moved(this, new EventArgs());
        }

        private void CloseClick(object sender, EventArgs e)
        {
            Visible = false;
            if (Closed != null)
                Closed(this, new EventArgs());
        }

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element el = builder.CreateElement(Tag))
            {
                AddAttributes(el);
                using (Element nw = builder.CreateElement("div"))
                {
                    nw.AddAttribute("class", "ra-window-nw");
                    using (Element ne = builder.CreateElement("div"))
                    {
                        ne.AddAttribute("class", "ra-window-ne");
                        using (Element n = builder.CreateElement("div"))
                        {
                            n.AddAttribute("class", "ra-window-n");
                            _caption.RenderControl(builder.Writer);
                            _close.RenderControl(builder.Writer);
                        }
                    }
                }
                using (Element w = builder.CreateElement("div"))
                {
                    w.AddAttribute("class", "ra-window-w");
                    using (Element e = builder.CreateElement("div"))
                    {
                        e.AddAttribute("class", "ra-window-e");
                        _content.RenderControl(builder.Writer);
                    }
                }
                using (Element nw = builder.CreateElement("div"))
                {
                    nw.AddAttribute("class", "ra-window-sw");
                    using (Element ne = builder.CreateElement("div"))
                    {
                        ne.AddAttribute("class", "ra-window-se");
                        using (Element n = builder.CreateElement("div"))
                        {
                            n.AddAttribute("class", "ra-window-s");
                        }
                    }
                }
            }
        }
    }
}
