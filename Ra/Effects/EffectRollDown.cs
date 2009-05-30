/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web.UI;
using Ra.Widgets;

namespace Ra.Effects
{
    /**
     * Will roll down control from in-visibility to its "needed height to fully display". Note that this
     * effect does not play nicely together with neither padding nor margin styles or CSS values. If margin
     * is needed on a Widget which you want to animate through the RollDown (or RollUp) effect you must add
     * an extra DOM element within the control which you put the actual margin or padding into. Also the
     * Widget which you run this effect upon should have overflow:hidde as the style value or CSS value)
     * since otherwise it'll "jump" when running and create visually non-appealing artifacts. This is the opposite
     * of the EffectRollUp.
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
