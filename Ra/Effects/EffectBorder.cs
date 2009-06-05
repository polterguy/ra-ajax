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
     * Animates borders of Control element. Will make the borders of the element dashed and
     * change the width of the borders.
     */
    public class EffectBorder : Effect
    {
		private int _borderTo;

        /**
         * Use this CTOR only if your effects are being Joined. 
         * Expects the main effect to set the Control and Duration properties.
         */
        public EffectBorder()
            : base(null, 0)
        { }

        /**
         * CTOR - control to animate and milliseconds to spend executing
         */
        public EffectBorder(Control control, int milliseconds)
            : base(control, milliseconds)
        { }

        /**
         * CTOR - control to animate, milliseconds to spend animating and width of
         * border when effect is finished.
         */
        public EffectBorder(Control control, int milliseconds, int borderTo)
			: base(control, milliseconds)
		{
			_borderTo = borderTo;
		}

        /**
         * Use this CTOR only if your effects are being Joined. 
         * Expects the main effect to set the Control and Duration properties. borderTo
         * is the end width of the border
         */
        public EffectBorder(int borderTo)
			: base(null, 0)
		{
			_borderTo = borderTo;
		}
		
        /**
         * End resulting width of border style
         */
		public int BorderTo
		{
			get { return _borderTo; }
			set { _borderTo = value; }
		}
		
		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
                tmp.Style.SetStyleValueViewStateOnly("border-style", "dashed");
                tmp.Style.SetStyleValueViewStateOnly("border-width", BorderTo + "px");
			}
		}

        protected override string RenderParalledOnStart()
        {
			UpdateStyleCollection();
            return @"
    this.element.setStyle('borderStyle','dashed');
    this.element.setStyle('borderWidth','1px');
";
        }

        protected override string RenderParalledOnFinished()
        {
            return string.Format(@"
    this.element.setStyle('borderWidth','{0}px');
", BorderTo);
        }

        protected override string RenderParalledOnRender()
        {
            return string.Format(@"
    var x = parseInt(pos*{0});
    this.element.setStyle('borderWidth',x + 'px');
", BorderTo);
        }
    }
}
