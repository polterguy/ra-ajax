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
    public class EffectFadeOut : Effect
    {
        public EffectFadeOut(Control control, decimal seconds)
			: base(control, seconds)
        { }

		// For chained effects
        public EffectFadeOut()
			: base(null, 0.0M)
        { }

		public override void Render ()
		{
			base.Render ();
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
				tmp.Style["opacity", false] = "0";
				tmp.Style["display", false] = "none";
			}
		}

        public override string RenderChainedOnStart()
        {
            return @"
    this.element.setOpacity(1);
    this.element.style.display = '';
";
        }

        public override string RenderChainedOnFinished()
        {
            return @"
    this.element.style.display = 'none';
    this.element.setOpacity(0);
";
        }

        public override string RenderChainedOnRender()
        {
            return @"
    this.element.setOpacity(1.0 - pos);
";
        }
    }
}
