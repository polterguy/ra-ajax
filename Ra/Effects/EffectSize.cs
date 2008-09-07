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
    public class EffectSize : Effect
    {
        private int _height;
        private int _width;

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public EffectSize(Control control, decimal seconds)
			: base(control, seconds)
        { }

        public EffectSize(Control control, decimal seconds, int height, int width)
			: base(control, seconds)
        {
            _height = height;
            _width = width;
        }

		// For chained effects
        public EffectSize()
			: base(null, 0.0M)
        { }

		// For chained effects
        public EffectSize(int height, int width)
			: base(null, 0.0M)
        {
            _height = height;
            _width = width;
        }

		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
				tmp.Style["height", false] = this._height.ToString() + "px";
				tmp.Style["width", false] = this._width.ToString() + "px";
			}
		}

        public override string RenderChainedOnStart()
        {
			UpdateStyleCollection();
            return @"
    this.startH = parseInt(this.element.style.height, 10);
    this.startW = parseInt(this.element.style.width, 10);
";
        }

        public override string RenderChainedOnFinished()
        {
            return string.Format(@"
    this.element.style.height = {0}+'px';
    this.element.style.width = {1}+'px';
",
                _height, _width);
        }

        public override string RenderChainedOnRender()
        {
            return string.Format(@"
    var deltaH = ({0} - this.startH) * pos;
    var newH = parseInt(deltaH + this.startH, 10);
    this.element.style.height = newH + 'px';

    var deltaW = ({1} - this.startW) * pos;
    var newW = parseInt(deltaW + this.startW, 10);
    this.element.style.width = newW + 'px';
",
                _height, _width);
        }
    }
}
