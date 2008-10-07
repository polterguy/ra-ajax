/*
 * Ra-Ajax - An Ajax Library for Mono ++
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
     * Will roll up control from visibility to in-visibility.
     */
    public class EffectRollUp : Effect
    {
        /**
         * Use this CTOR only if your effects are being Joined. 
         * Expects the main effect to set the Control and Duration properties.
         */
        public EffectRollUp()
            : base(null, 0)
        { }

        /**
         * CTOR - control to animate and milliseconds to spend executing
         */
        public EffectRollUp(Control control, int milliseconds)
			: base(control, milliseconds)
        { }

		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
                tmp.Style.SetStyleValueViewStateOnly("height", "");
                tmp.Style.SetStyleValueViewStateOnly("display", "none");
			}
		}

        public override string RenderParalledOnStart()
        {
			UpdateStyleCollection();
            return @"
    this.element.setStyle('display','');
    this._fromHeight = this.element.getDimensions().height;
    this._overflow = this.element.getStyle('overflow');
    this.element.setStyle('overflow','hidden');
";
        }

        public override string RenderParalledOnFinished()
        {
            return @"
    this.element.setStyle('display','none');
    this.element.setStyle('height','');
    this.element.setStyle('overflow',this._overflow);
";
        }

        public override string RenderParalledOnRender()
        {
            return "this.element.setStyle('height',((1.0-pos)*this._fromHeight) + 'px');";
        }
    }
}
