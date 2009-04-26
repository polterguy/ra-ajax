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
     * This is on item in the SliderMenu. Note that a SliderMenu consists of a SliderMenuLevel which
     * in turn consists of one or more SliderMenuItems which in turn can have one SliderMenuLevel and 
     * so on. This means that the parent of SliderMenuItems MUST be SliderMenuLevel items.
     */
    [ASP.ToolboxData("<{0}:SlidingMenuItem runat=\"server\"></{0}:SlidingMenuItem>")]
    public class SlidingMenuItem : Panel, ASP.INamingContainer
    {
        private LinkButton _button = new LinkButton();
        private Label _childLbl = new Label();

        // TODO: Make publicly available effect...?
        internal class EffectRollOut : Effect
        {
            private bool _reversed;
            private int _noLevels;

            public EffectRollOut(ASP.Control control, int milliseconds)
                : this(control, milliseconds, false)
            { }

            public EffectRollOut(ASP.Control control, int milliseconds, bool reversed)
                : this(control, milliseconds, reversed, 1)
            {
                _reversed = reversed;
            }

            public EffectRollOut(ASP.Control control, int milliseconds, bool reversed, int noLevels)
                : base(control, milliseconds)
            {
                _reversed = reversed;
                _noLevels = noLevels;
            }

            private void UpdateStyleCollection()
            {
                // TODO: Implement...?
                //RaWebControl tmp = this.Control as RaWebControl;
            }

            public override string RenderParalledOnStart()
            {
                UpdateStyleCollection();
                return string.Format(@"
    this._fromWidth = this.element.getDimensions().width * {0};
    this._oldMargin = parseInt(this.element.getStyle('marginLeft')) || 0;
", _noLevels);
            }

            public override string RenderParalledOnFinished()
            {
                if (_reversed)
                {
                    return @"
    this.element.setStyle('marginLeft',this._oldMargin + this._fromWidth + 'px');
";
                }
                else
                {
                    return @"
    this.element.setStyle('marginLeft',this._oldMargin - this._fromWidth + 'px');
";
                }
            }

            public override string RenderParalledOnRender()
            {
                if (_reversed)
                {
                    return @"
this.element.setStyle('marginLeft',this._oldMargin + parseInt(pos*this._fromWidth) + 'px');";
                }
                else
                {
                    return @"
this.element.setStyle('marginLeft',this._oldMargin - parseInt(pos*this._fromWidth) + 'px');";
                }
            }
        }

        public string Text
        {
            get { return _button.Text; }
            set { _button.Text = value; }
        }

        /**
         * Overridden to provide a sane default value. The default CSS class is "item".
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

        /**
         * Readonly property, will be true if SliderMenuItem have no SliderMenuLevel control. Which
         * basically is the definition of a "leaf node".
         */
        public bool IsLeaf
        {
            get
            {
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is SlidingMenuLevel)
                        return false;
                }
                return true;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            Tag = "li";
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // We MUST call the EnsureChildControls AFTER the ControlState has been loaded
            // since we're depending on some value from ControlState in order to correctly
            // instantiate the composition controls
            EnsureChildControls();
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

        private SlidingMenu Root
        {
            get
            {
                ASP.Control idx = this.Parent;
                while (idx != null && !(idx is SlidingMenu))
                    idx = idx.Parent;
                return idx as SlidingMenu;
            }
        }

        private void _button_Click(object sender, EventArgs e)
        {
            // Raising MenuItem clicked event
            Root.RaiseItemClicked(this);

            if (this.IsLeaf)
                return;

            // Finding child SliderMenuLevel control
            SlidingMenuLevel child = null;
            foreach (ASP.Control idx in Controls)
            {
                if (idx is SlidingMenuLevel)
                {
                    child = idx as SlidingMenuLevel;
                    break;
                }
            }

            child.Style["display"] = "";
            Root.SetActiveLevel(child);
            if (child.EnsureChildNodes())
                child.ReRender();
            Root.SetAllChildrenNonVisible(Root);
            ASP.Control idxFromThis = child;
            while (idxFromThis != null && !(idxFromThis is SlidingMenu))
            {
                if (idxFromThis is SlidingMenuLevel)
                {
                    (idxFromThis as SlidingMenuLevel).Style["display"] = "";
                }
                idxFromThis = idxFromThis.Parent;
            }

            // Animating Menu levels...
            ASP.Control rootLevel = null;
            foreach (ASP.Control idx in Root.Controls)
            {
                if (idx is SlidingMenuLevel)
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
