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

        public EffectSize(Control control, int milliseconds)
			: base(control, milliseconds)
        { }

        public EffectSize(Control control, int milliseconds, int height, int width)
			: base(control, milliseconds)
        {
            _height = height;
            _width = width;
        }

		// For chained effects
        public EffectSize()
			: base(null, 0)
        { }

		// For chained effects
        public EffectSize(int height, int width)
			: base(null, 0)
        {
            _height = height;
            _width = width;
        }

		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
                if (_height != -1)
                    tmp.Style["height", false] = this._height.ToString() + "px";
                if (_width != -1)
                    tmp.Style["width", false] = this._width.ToString() + "px";
			}
		}

        public override string RenderParalledOnStart()
        {
			UpdateStyleCollection();
            return @"
    this.startSize = this.element.getDimensions();
";
        }

        public override string RenderParalledOnFinished()
        {
            string retVal = "";
            if (_height != -1)
                retVal += string.Format(@"
    this.element.setStyle('height',{0}+'px');
",
                    _height);
            if (_width != -1)
                retVal += string.Format(@"
    this.element.setStyle('width',{0}+'px');
",
                    _width);
            return retVal;
        }

        public override string RenderParalledOnRender()
        {
            string retVal = "";
            if (_height != -1)
                retVal += string.Format(@"
    var deltaH = ({0} - this.startSize.height) * pos;
    var newH = parseInt(deltaH + this.startSize.height, 10);
    this.element.setStyle('height',newH + 'px');
",
                    _height);
            if (_width != -1)
                retVal += string.Format(@"
    var deltaW = ({0} - this.startSize.width) * pos;
    var newW = parseInt(deltaW + this.startSize.width, 10);
    this.element.setStyle('width',newW + 'px');
",
                    _width);
            return retVal;
        }
    }
}
