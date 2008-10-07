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
     * Will roll down control from in-visibility to a specified height.
     */
    public class EffectRollDown : Effect
    {
        /**
         * Use this CTOR only if your effects are being Joined. 
         * Expects the main effect to set the Control and Duration properties.
         */
        public EffectRollDown()
            : base(null, 0)
        { }

        /**
         * CTOR - control to animate and milliseconds to spend executing in addition to the end height
         * of the Control.
         */
        public EffectRollDown(Control control, int milliseconds)
			: base(control, milliseconds)
        { }

		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
                tmp.Style.SetStyleValueViewStateOnly("display", "block");
                tmp.Style.SetStyleValueViewStateOnly("height", "");
            }
		}

        public override string RenderParalledOnStart()
        {
			UpdateStyleCollection();
            return @"
    this._toHeight = this.element.getDimensions().height;
    this.element.setStyle('height','0px');
    this.element.setStyle('display','block');
    this._overflow = this.element.getStyle('overflow');
    this.element.setStyle('overflow','hidden');
";
        }

        public override string RenderParalledOnFinished()
        {
            return @"
    this.element.setStyle('height', '');
    this.element.setStyle('overflow',this._overflow);
";
        }

        public override string RenderParalledOnRender()
        {
            return @"
    this.element.setStyle('height', parseInt(this._toHeight*pos) + 'px');
";
        }
    }
}
