/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web.UI;

namespace Ra.Widgets
{
    public class EffectMove : Effect
    {
        private int _left;
        private int _top;

        public int Top
        {
            get { return _top; }
            set { _top = value; }
        }

        public int Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public EffectMove(Control control, int milliseconds)
			: base(control, milliseconds)
        { }

        public EffectMove(Control control, int milliseconds, int left, int top)
			: base(control, milliseconds)
        {
            _left = left;
            _top = top;
        }

		// For chained effects
        public EffectMove()
			: base(null, 0)
        { }

		// For chained effects
        public EffectMove(int left, int top)
			: base(null, 0)
        {
            _left = left;
            _top = top;
        }

		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
				tmp.Style["left", false] = _left.ToString() + "px";
				tmp.Style["top", false] = _top.ToString() + "px";
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
