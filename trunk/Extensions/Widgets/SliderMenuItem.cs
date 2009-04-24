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
using System.Collections.Generic;

namespace Ra.Extensions
{
    /**
     */
    [ASP.ToolboxData("<{0}:SliderMenuItem runat=\"server\"></{0}:SliderMenuItem>")]
    public class SliderMenuItem : Panel, ASP.INamingContainer
    {
        private LinkButton _button = new LinkButton();
        private Label _childLbl = new Label();

        internal class EffectRollOut : Effect
        {
            private bool _reversed;

            public EffectRollOut(ASP.Control control, int milliseconds)
                : base(control, milliseconds)
            { }

            public EffectRollOut(ASP.Control control, int milliseconds, bool reversed)
                : base(control, milliseconds)
            {
                _reversed = reversed;
            }

            private void UpdateStyleCollection()
            {
                //RaWebControl tmp = this.Control as RaWebControl;
            }

            public override string RenderParalledOnStart()
            {
                UpdateStyleCollection();
                return @"
    this._fromWidth = this.element.getDimensions().width;
    this._oldMargin = parseInt(this.element.getStyle('marginLeft')) || 0;
";
            }

            public override string RenderParalledOnFinished()
            {
                if (_reversed)
                {
                    return @"
    var val = this._oldMargin + this._fromWidth + 'px';
    this.element.setStyle('marginLeft',val);
";
                }
                else
                {
                    return @"
    var val = this._oldMargin - this._fromWidth + 'px';
    this.element.setStyle('marginLeft',val);
";
                }
            }

            public override string RenderParalledOnRender()
            {
                if (_reversed)
                {
                    return @"
var val = this._oldMargin + parseInt(pos*this._fromWidth) + 'px';
this.element.setStyle('marginLeft',val);";
                }
                else
                {
                    return @"
var val = this._oldMargin - parseInt(pos*this._fromWidth) + 'px';
this.element.setStyle('marginLeft',val);";
                }
            }
        }

        public string Text
        {
            get { return _button.Text; }
            set { _button.Text = value; }
        }

        /**
         * Overridden to provide a sane default value
         */
        [DefaultValue("item")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "item";
                return retVal;
            }
            set { base.CssClass = value; }
        }

        public bool IsLeaf
        {
            get
            {
                if (ViewState["IsLeaf"] == null)
                    return false;
                return (bool)ViewState["IsLeaf"];
            }
            set { ViewState["IsLeaf"] = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            Tag = "li";
            EnsureChildControls();
            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            CreateMenuControls();
        }

        private void CreateMenuControls()
        {
            _childLbl.ID = "leaf";
            _childLbl.CssClass = "leaf";
            _childLbl.Text = "&nbsp;";
            _button.Controls.Add(_childLbl);

            _button.ID = "btn";
            _button.CssClass = "menu-btn";
            _button.Click += _button_Click;
            this.Controls.AddAt(0, _button);
        }

        private SliderMenu Root
        {
            get
            {
                ASP.Control idx = this.Parent;
                while (idx != null && !(idx is SliderMenu))
                    idx = idx.Parent;
                return idx as SliderMenu;
            }
        }

        void _button_Click(object sender, EventArgs e)
        {
            // Raising MenuItem clicked event
            Root.RaiseItemClicked(this);

            if (this.IsLeaf)
                return;

            // Finding child SliderMenuLevel control
            SliderMenuLevel child = null;
            foreach (ASP.Control idx in Controls)
            {
                if (idx is SliderMenuLevel)
                {
                    child = idx as SliderMenuLevel;
                    break;
                }
            }

            child.Style["display"] = "";
            Root.SetActiveLevel(child);
            Root.SetAllChildrenNonVisible(Root);
            ASP.Control idxFromThis = child;
            while (idxFromThis != null && !(idxFromThis is SliderMenu))
            {
                if (idxFromThis is SliderMenuLevel)
                {
                    (idxFromThis as SliderMenuLevel).Style["display"] = "";
                }
                idxFromThis = idxFromThis.Parent;
            }

            // Animating Menu levels...
            ASP.Control rootLevel = null;
            foreach (ASP.Control idx in Root.Controls)
            {
                if (idx is SliderMenuLevel)
                {
                    rootLevel = idx;
                    break;
                }
            }
            new EffectRollOut(rootLevel, 800)
                .Render();
        }

        protected override void OnPreRender(EventArgs e)
        {
            _childLbl.Visible = !IsLeaf;
            base.OnPreRender(e);
        }
    }
}
