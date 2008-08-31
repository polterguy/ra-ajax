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

        public EffectMove(Control control, decimal seconds)
			: base(control, seconds)
        { }

        public EffectMove(Control control, decimal seconds, int left, int top)
			: base(control, seconds)
        {
            _left = left;
            _top = top;
        }

		// For chained effects
        public EffectMove()
			: base(null, 0.0M)
        { }

		// For chained effects
        public EffectMove(int left, int top)
			: base(null, 0.0M)
        {
            _left = left;
            _top = top;
        }

        public override string RenderChainedOnStart()
        {
            return @"
    this.startL = parseInt(this.element.style.left, 10);
    this.startT = parseInt(this.element.style.top, 10);
";
        }

        public override string RenderChainedOnFinished()
        {
            return string.Format(@"
    this.element.style.left = {0}+'px';
    this.element.style.top = {1}+'px';
",
                _left, _top);
        }

        public override string RenderChainedOnRender()
        {
            return string.Format(@"
    var deltaL = ({0} - this.startL) * pos;
    var newL = parseInt(deltaL + this.startL, 10);
    this.element.style.left = newL + 'px';

    var deltaT = ({1} - this.startT) * pos;
    var newT = parseInt(deltaT + this.startT, 10);
    this.element.style.top = newT + 'px';
",
                _left, _top);
        }
    }
}
