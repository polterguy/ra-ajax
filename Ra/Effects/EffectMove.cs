/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web.UI;

namespace Ra.Widgets
{
    /**
     * Will move control to specified location in absolute pixel position
     */
    public class EffectMove : Effect
    {
        private int _left;
        private int _top;

        /**
         * Use this CTOR only if your effects are being Joined. 
         * Expects the main effect to set the Control and Duration properties.
         */
        public EffectMove()
            : base(null, 0)
        { }

        /**
         * CTOR - control to animate and milliseconds to spend executing
         */
        public EffectMove(Control control, int milliseconds)
            : base(control, milliseconds)
        { }

        /**
         * CTOR - control to animate and milliseconds to spend executing in addition to the
         * end top and left properties.
         */
        public EffectMove(Control control, int milliseconds, int left, int top)
            : base(control, milliseconds)
        {
            _left = left;
            _top = top;
        }

        /**
         * CTOR - the end top and left properties. Use only on Joined effects.
         */
        public EffectMove(int left, int top)
            : base(null, 0)
        {
            _left = left;
            _top = top;
        }

        /**
         * End top position of Control
         */
        public int Top
        {
            get { return _top; }
            set { _top = value; }
        }

        /**
         * End left position of Control
         */
        public int Left
        {
            get { return _left; }
            set { _left = value; }
        }

		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
                tmp.Style.SetStyleValueViewStateOnly("left", _left.ToString() + "px");
                tmp.Style.SetStyleValueViewStateOnly("top", _top.ToString() + "px");
			}
		}

        public override string RenderParalledOnStart()
        {
			UpdateStyleCollection();
            return @"
    this.startL = parseInt(this.element.getStyle('left'), 10);
    this.startT = parseInt(this.element.getStyle('top'), 10);
";
        }

        public override string RenderParalledOnFinished()
        {
            return string.Format(@"
    this.element.setStyle('left',{0}+'px');
    this.element.setStyle('top',{1}+'px');
",
                _left, _top);
        }

        public override string RenderParalledOnRender()
        {
            return string.Format(@"
    var deltaL = ({0} - this.startL) * pos;
    var newL = parseInt(deltaL + this.startL, 10);
    this.element.setStyle('left',newL + 'px');

    var deltaT = ({1} - this.startT) * pos;
    var newT = parseInt(deltaT + this.startT, 10);
    this.element.setStyle('top',newT + 'px');
",
                _left, _top);
        }
    }
}
