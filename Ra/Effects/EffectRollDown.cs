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
    /**
     * Will roll down control from in-visibility to a specified height.
     */
    public class EffectRollDown : Effect
    {
        private int _toHeight;

        /**
         * Use this CTOR only if your effects are being Joined. 
         * Expects the main effect to set the Control and Duration properties.
         * toHeight is the end height of the Control.
         */
        public EffectRollDown(int toHeight)
            : base(null, 0)
        {
            _toHeight = toHeight;
        }

        /**
         * CTOR - control to animate and milliseconds to spend executing in addition to the end height
         * of the Control.
         */
        public EffectRollDown(Control control, int milliseconds, int toHeight)
			: base(control, milliseconds)
        {
            _toHeight = toHeight;
        }

		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
                tmp.Style.SetStyleValueViewStateOnly("height", _toHeight.ToString() + "px");
                tmp.Style.SetStyleValueViewStateOnly("display", "block");
			}
		}

        public override string RenderParalledOnStart()
        {
			UpdateStyleCollection();
            return @"
    this.element.setStyle('height','0px');
    this.element.setStyle('display','block');
";
        }

        public override string RenderParalledOnFinished()
        {
            return string.Format(@"
    this.element.setStyle('height','{0}px');
",
                _toHeight);
        }

        public override string RenderParalledOnRender()
        {
            return string.Format(@"
    this.element.setStyle('height',parseInt({0}*pos) + 'px');
",
                _toHeight);
        }
    }
}
