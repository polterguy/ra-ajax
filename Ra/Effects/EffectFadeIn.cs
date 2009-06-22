/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;
using System.Web.UI;
using Ra.Widgets;

namespace Ra.Effects
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

        public override void Render()
        {
            if (!Control.Visible)
                Control.Visible = true;
            base.Render();
        }
		
		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
                tmp.Style.SetStyleValueViewStateOnly("opacity", "1.0");
                tmp.Style.SetStyleValueViewStateOnly("display", "");
			}
		}

        protected override string RenderParalledOnStart()
        {
			UpdateStyleCollection();
            return @"
    this.element.setOpacity(0);
    this.element.setStyle('display','');
";
        }

        protected override string RenderParalledOnFinished()
        {
            return @"
    this.element.setOpacity(1);
";
        }

        protected override string RenderParalledOnRender()
        {
            return @"
    this.element.setOpacity(pos);
";
        }
    }
}
