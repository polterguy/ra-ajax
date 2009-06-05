/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web.UI;
using Ra.Widgets;

namespace Ra.Effects
{
    /**
     * Animates a Control from its current size to a new specified size. Uses pixel values.
     */
    public class EffectSize : Effect
    {
        private int _height;
        private int _width;

        /**
         * Use this CTOR only if your effects are being Joined. 
         * Expects the main effect to set the Control and Duration properties.
         */
        public EffectSize()
            : base(null, 0)
        { }

        /**
         * CTOR - control to animate and milliseconds to spend executing
         */
        public EffectSize(Control control, int milliseconds)
            : base(control, milliseconds)
        { }

        /**
         * CTOR - control to animate and milliseconds to spend executing in addition to end size
         * of Control
         */
        public EffectSize(Control control, int milliseconds, int height, int width)
            : base(control, milliseconds)
        {
            _height = height;
            _width = width;
        }

        /**
         * Use this CTOR only if your effects are being Joined. 
         * Expects the main effect to set the Control and Duration properties.
         * height and width is new size of Control.
         */
        public EffectSize(int height, int width)
            : base(null, 0)
        {
            _height = height;
            _width = width;
        }

        /**
         * New width of Control
         */
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /**
         * New height of Control
         */
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
                if (_height != -1)
                    tmp.Style.SetStyleValueViewStateOnly("height", this._height.ToString() + "px");
                if (_width != -1)
                    tmp.Style.SetStyleValueViewStateOnly("width", this._width.ToString() + "px");
			}
		}

        protected override string RenderParalledOnStart()
        {
			UpdateStyleCollection();
            return @"
    this.startSize = this.element.getDimensions();
";
        }

        protected override string RenderParalledOnFinished()
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

        protected override string RenderParalledOnRender()
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
