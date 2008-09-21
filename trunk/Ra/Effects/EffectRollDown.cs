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
    public class EffectRollDown : Effect
    {
        private int _toHeight;

        public EffectRollDown(Control control, int milliseconds, int toHeight)
			: base(control, milliseconds)
        {
            _toHeight = toHeight;
        }

		// for chained effects
        public EffectRollDown(int toHeight)
			: base(null, 0)
        {
            _toHeight = toHeight;
        }

		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
				tmp.Style["height", false] = _toHeight.ToString() + "px";
				tmp.Style["display", false] = "block";
			}
		}

        public override string RenderChainedOnStart()
        {
			UpdateStyleCollection();
            return @"
    this.element.setStyle('height','0px');
    this.element.setStyle('display','block');
";
        }

        public override string RenderChainedOnFinished()
        {
            return string.Format(@"
    this.element.setStyle('height','{0}px');
",
                _toHeight);
        }

        public override string RenderChainedOnRender()
        {
            return string.Format(@"
    this.element.setStyle('height',parseInt({0}*pos) + 'px');
",
                _toHeight);
        }
    }
}
