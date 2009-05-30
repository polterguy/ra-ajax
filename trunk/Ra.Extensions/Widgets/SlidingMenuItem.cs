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
using Ra.Effects;

namespace Ra.Extensions.Widgets
{
    /**
     * This is on item in the SliderMenu. Note that a SliderMenu consists of a SliderMenuLevel which
     * in turn consists of one or more SliderMenuItems which in turn can have one SliderMenuLevel and 
     * so on. This means that the parent of SliderMenuItems MUST be SliderMenuLevel items.
     */
    [ASP.ToolboxData("<{0}:SlidingMenuItem runat=\"server\"></{0}:SlidingMenuItem>")]
    public class SlidingMenuItem : Panel, ASP.INamingContainer
    {
        private Label _button = new Label();
        private Label _childLbl = new Label();

        // TODO: Make publicly available effect...?
        internal class EffectRollOut : Effect
        {
            private bool _reversed;
            private int _noLevels;
            private ASP.Control _bread;
            private bool _allTheWay;

            public EffectRollOut(ASP.Control control, ASP.Control bread, int milliseconds)
                : this(control, bread, milliseconds, false)
            { }

            public EffectRollOut(ASP.Control control, 
                ASP.Control bread, 
                int milliseconds, 
                bool reversed)
                : this(control, bread, milliseconds, reversed, 1)
            { }

            public EffectRollOut(ASP.Control control, 
                ASP.Control bread, 
                int milliseconds, 
                bool reversed, 
                int noLevels)
                : this(control, bread, milliseconds, reversed, noLevels, false)
            { }

            public EffectRollOut(ASP.Control control, 
                ASP.Control bread, 
                int milliseconds, 
                bool reversed, 
                int noLevels, 
                bool allTheWay)
                : base(control, milliseconds)
            {
                _reversed = reversed;
                _noLevels = noLevels;
                _bread = bread;
                _allTheWay = allTheWay;
            }

            private void UpdateStyleCollection()
            {
                // TODO: Implement...?
                //RaWebControl tmp = this.Control as RaWebControl;
            }

            public override string RenderParalledOnStart()
            {
                UpdateStyleCollection();
                if (_reversed)
                {
                    return string.Format(@"
this._bread = Ra.$('{1}');
if( this._bread ) {{
  this._bread.setOpacity(0);
  this._bread.setStyle('display', '');
  var el = this._bread.lastChild;
  if( el ) {{
    var fullWidth = Ra.$(this._bread.parentNode.parentNode.id).getDimensions().width;
    var toMargin = Math.max((el.offsetLeft + el.offsetWidth) - fullWidth, 0);
    this._bread.setStyle('marginLeft', (-toMargin) + 'px');
  }}
  this._bread.style.visibility='';
}}
this._fromWidth = this.element.getDimensions().width * {0};
this._oldMargin = parseInt(this.element.getStyle('marginLeft')) || 0;
", _noLevels, this._bread.ClientID);
                }
                else
                {
                    return string.Format(@"
this._bread = Ra.$('{1}');
if( this._bread ) {{
  this._bread.style.visibility='hidden';
  this._bread.setStyle('display', '');
  var el = this._bread.lastChild;
  this._lastBread = el;
  if( el ) {{
    this._lastBread.setOpacity(0);
    var fullWidth = Ra.$(this._bread.parentNode.parentNode.id).getDimensions().width;
    this._breadToMargin = Math.max((el.offsetLeft + el.offsetWidth) - fullWidth, 0);
    var el2 = el.previousSibling;
    this._breadFromMargin = 0;
    if( el2 && this._breadToMargin > 0) {{
      this._breadFromMargin = Math.max((el2.offsetLeft + el2.offsetWidth) - fullWidth, 0);
      if( this._breadFromMargin > 0 ) {{
        this._bread.setStyle('marginLeft', (-this._breadFromMargin) + 'px');
      }}
    }}
  }}
  this._bread.style.visibility='';
}}
this._fromWidth = this.element.getDimensions().width * {0};
this._oldMargin = parseInt(this.element.getStyle('marginLeft')) || 0;
", _noLevels, _bread.ClientID);
                }
            }

            public override string RenderParalledOnFinished()
            {
                if (_reversed)
                {
                    if (_allTheWay)
                    {
                        return @"
    this.element.setStyle('marginLeft', '0px');
    this._bread.setOpacity(1);
";
                    }
                    else
                    {
                        return @"
    this.element.setStyle('marginLeft',this._oldMargin + this._fromWidth + 'px');
    this._bread.setOpacity(1);
";
                    }
                }
                else
                {
                    return @"
    this.element.setStyle('marginLeft',this._oldMargin - this._fromWidth + 'px');
    if( this._breadToMargin > 0 ) {
      this._bread.setStyle('marginLeft',(-(this._breadToMargin))+'px');
    }
    if( this._lastBread ) {
      this._lastBread.setOpacity(1);
    }
";
                }
            }

            public override string RenderParalledOnRender()
            {
                if (_reversed)
                {
                    if (_allTheWay)
                    {
                        return @"
this.element.setStyle('marginLeft',this._oldMargin - parseInt(pos*this._oldMargin) + 'px');
this._bread.setOpacity(pos);
";
                    }
                    else
                    {
                        return @"
this.element.setStyle('marginLeft',this._oldMargin + parseInt(pos*this._fromWidth) + 'px');
this._bread.setOpacity(pos);
";
                    }
                }
                else
                {
                    return @"
this.element.setStyle('marginLeft',this._oldMargin - parseInt(pos*this._fromWidth) + 'px');
if( this._breadToMargin > 0 ) {
  this._bread.setStyle('marginLeft',(-((pos*(this._breadToMargin - this._breadFromMargin)) + this._breadFromMargin))+'px');
}
if( this._lastBread ) {
  this._lastBread.setOpacity(pos);
}
";
                }
            }
        }

        /**
         * This is the Text of the MenuItem.
         */
        public string Text
        {
            get { return _button.Text; }
            set { _button.Text = value; }
        }

        /**
         * If this one is true then the MenuItem will not trigger a click event. This can be useful
         * if you have other types of controls within the MenuItem which are relying on click interactions
         * like for instance hyperlinks and such that should navigate away from the page. Or if you have
         * CheckBox or other controls that needs to themselves trap the "click" event. Notice though
         * that this will also make it impossible to EXPAND the MenuItem. This means that it this 
         * property should only be true for LEAF menu items. The default value is false.
         */
        public bool NoClick
        {
            get { return ViewState["NoClick"] == null ? false : (bool)ViewState["NoClick"]; }
            set { ViewState["NoClick"] = value; }
        }

        /**
         * Overridden to provide a sane default value. The default CSS class is "item".
         */
        [DefaultValue("sliding-item")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "sliding-item";
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
                foreach (ASP.Control idx in base.Controls)
                {
                    if (idx is SlidingMenuLevel)
                        return false;
                }
                return true;
            }
        }

        /**
         * If you want to have child controls within the item then this is where you are supposed
         * to add those up.
         */
        public RaWebControl Content
        {
            get { return _button; }
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
            _button.ID = "btn";
            _button.CssClass = "sliding-menu-btn";
            if (!NoClick)
            {
                _button.QueueMultipleRequests = false;
                _button.Click += _button_Click;
            }
            Controls.AddAt(0, _button);

            _childLbl.ID = "slLeaf";
            _childLbl.CssClass = "sliding-leaf";
            _childLbl.Text = "&nbsp;";
            _button.Controls.Add(_childLbl);
        }

        private SlidingMenu _root;
        private SlidingMenu Root
        {
            get
            {
                if (_root == null)
                {
                    ASP.Control idx = this.Parent;
                    while (idx != null && !(idx is SlidingMenu))
                        idx = idx.Parent;
                    _root = idx as SlidingMenu;
                }
                return _root;
            }
        }

        private void _button_Click(object sender, EventArgs e)
        {
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

            if (child != null)
            {
                child.Style["display"] = "";
                Root.SetActiveLevel(child);
            }

            // Raising MenuItem clicked event
            Root.RaiseItemClicked(this);

            if (this.IsLeaf)
                return;

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
            Root.BreadCrumb.Style["display"] = "gokk"; // To force a new value to the display property...
            Root.BreadCrumb.Style["display"] = "none";
            new EffectRollOut(rootLevel, Root.BreadCrumb, Root.AnimationDuration)
                .Render();
        }

        protected override void OnPreRender(EventArgs e)
        {
            _childLbl.Visible = !IsLeaf;
            base.OnPreRender(e);
        }
    }
}
