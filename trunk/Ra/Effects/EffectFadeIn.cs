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
     * Will fade control's root DOM element in to visibility
     */
    public class EffectFadeIn : Effect
    {
        /**
         * CTOR - control to animate and milliseconds to spend executing
         */
        public EffectFadeIn(Control control, int milliseconds)
			: base(control, milliseconds)
		{ }

        /**
         * Use this CTOR only if your effects are being Joined. 
         * Expects the main effect to set the Control and Duration properties.
         */
        public EffectFadeIn()
			: base(null, 0)
		{ }
		
		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
                tmp.Style.SetStyleValueViewStateOnly("opacity", "1.0");
                tmp.Style.SetStyleValueViewStateOnly("display", "block");
			}
		}

        public override string RenderParalledOnStart()
        {
			UpdateStyleCollection();
            return @"
    this.element.setOpacity(0);
    this.element.setStyle('display','block');
";
        }

        public override string RenderParalledOnFinished()
        {
            return @"
    this.element.setOpacity(1);
";
        }

        public override string RenderParalledOnRender()
        {
            return @"
    this.element.setOpacity(pos);
";
        }
    }
}
