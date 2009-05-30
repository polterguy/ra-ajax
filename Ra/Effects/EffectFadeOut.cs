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
     * Will fade control's root DOM element out of visibility
     */
    public class EffectFadeOut : Effect
    {
        /**
         * Use this CTOR only if your effects are being Joined. 
         * Expects the main effect to set the Control and Duration properties.
         */
        public EffectFadeOut()
            : base(null, 0)
        { }

        /**
         * CTOR - control to animate and milliseconds to spend executing
         */
        public EffectFadeOut(Control control, int milliseconds)
			: base(control, milliseconds)
        { }

		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
                tmp.Style.SetStyleValueViewStateOnly("opacity", "0");
                tmp.Style.SetStyleValueViewStateOnly("display", "none");
			}
		}

        public override string RenderParalledOnStart()
        {
			UpdateStyleCollection();
            return @"
    this.element.setOpacity(1);
    this.element.setStyle('display','');
";
        }

        public override string RenderParalledOnFinished()
        {
            return @"
    this.element.setStyle('display','none');
    this.element.setOpacity(0);
";
        }

        public override string RenderParalledOnRender()
        {
            return @"
    this.element.setOpacity(1.0 - pos);
";
        }
    }
}
