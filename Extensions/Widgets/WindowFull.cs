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
using System.Collections.Generic;

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:WindowFull runat=\"server\"></{0}:WindowFull>")]
    public class WindowFull : Panel, ASP.INamingContainer
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
        BehaviorDraggable _dragger;

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

        public override ASP.ControlCollection Controls
        {
            get { return _content.Controls; }
        }

        private void CreateWindowControls()
        {
            // Top parts
            _nw = new Label();
            _nw.Tag = "div";
            _nw.ID = "nw";
            base.Controls.Add(_nw);

            _ne = new Label();
            _ne.Tag = "div";
            _ne.ID = "ne";
            _nw.Controls.Add(_ne);

            _n = new Label();
            _n.Tag = "div";
            _n.ID = "n";
            _n.Text = "&nbsp;";
            _ne.Controls.Add(_n);

            // Middle parts
            _body = new Label();
            _body.Tag = "div";
            _body.ID = "body";
            base.Controls.Add(_body);

            _w = new Label();
            _w.Tag = "div";
            _w.ID = "w";
            _body.Controls.Add(_w);

            _e = new Label();
            _e.Tag = "div";
            _e.ID = "e";
            _w.Controls.Add(_e);

            _content.Tag = "div";
            _content.ID = "content";
            _e.Controls.Add(_content);

            // Bottom parts
            _sw = new Label();
            _sw.Tag = "div";
            _sw.ID = "sw";
            _body.Controls.Add(_sw);

            _se = new Label();
            _se.Tag = "div";
            _se.ID = "se";
            _sw.Controls.Add(_se);

            _s = new Label();
            _s.Tag = "div";
            _s.ID = "s";
            _s.Text = "&nbsp;";
            _se.Controls.Add(_s);

            _dragger = new BehaviorDraggable();
            _dragger.ID = "dragger";
            _dragger.Handle = _n.ClientID;
            base.Controls.Add(_dragger);
        }

        private ASP.ControlCollection GetBaseControls()
        {
            return base.Controls;
        }

        [Browsable(false)]
        public override IEnumerable<Behavior> Behaviors
        {
            get
            {
                ASP.ControlCollection controls = GetBaseControls();
                foreach (ASP.Control idx in controls)
                {
                    if (idx is Behavior)
                        yield return idx as Behavior;
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
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
            base.OnPreRender(e);
        }
    }
}
