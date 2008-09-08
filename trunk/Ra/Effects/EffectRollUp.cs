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
    public class EffectRollUp : Effect
    {
        private int _fromHeight;

        public EffectRollUp(Control control, decimal seconds, int fromHeight)
			: base(control, seconds)
        {
            _fromHeight = fromHeight;
        }

		// For chained effects
        public EffectRollUp(int fromHeight)
			: base(null, 0.0M)
        {
            _fromHeight = fromHeight;
        }

		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
				tmp.Style["height", false] = "0px";
				tmp.Style["display", false] = "none";
			}
		}

        public override string RenderChainedOnStart()
        {
			UpdateStyleCollection();
            return string.Format(@"
    this.element.style.display = '';
    this.element.style.height = '{0}px'
",
                _fromHeight);
        }

        public override string RenderChainedOnFinished()
        {
            return @"
    this.element.style.display = 'none';
    this.element.style.height = '0px'
";
        }

        public override string RenderChainedOnRender()
        {
            return string.Format(@"
    this.element.style.height = ((1.0-pos)*{0}) + 'px';
",
                _fromHeight);
        }
    }
}
