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

        public EffectRollDown(Control control, decimal seconds, int toHeight)
			: base(control, seconds)
        {
            _toHeight = toHeight;
        }

		// for chained effects
        public EffectRollDown(int toHeight)
			: base(null, 0.0M)
        {
            _toHeight = toHeight;
        }

		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
				tmp.Style["height", false] = _toHeight.ToString() + "px";
				tmp.Style["display", false] = "";
			}
		}

        public override string RenderChainedOnStart()
        {
			UpdateStyleCollection();
            return @"
    this.element.style.height = '0px';
    this.element.style.display = '';
";
        }

        public override string RenderChainedOnFinished()
        {
            return string.Format(@"
    this.element.style.height = '{0}px';
",
                _toHeight);
        }

        public override string RenderChainedOnRender()
        {
            return string.Format(@"
    this.element.style.height = parseInt({0}*pos) + 'px';
",
                _toHeight);
        }
    }
}
