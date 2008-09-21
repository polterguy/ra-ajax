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

        public EffectRollUp(Control control, int milliseconds, int fromHeight)
			: base(control, milliseconds)
        {
            _fromHeight = fromHeight;
        }

		// For chained effects
        public EffectRollUp(int fromHeight)
			: base(null, 0)
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
    this.element.setStyle('display','');
    this.element.setStyle('height','{0}px)'
",
                _fromHeight);
        }

        public override string RenderChainedOnFinished()
        {
            return @"
    this.element.setStyle('display','none');
    this.element.setStyle('height','0px)'
";
        }

        public override string RenderChainedOnRender()
        {
            return string.Format(@"
    this.element.setStyle('height',((1.0-pos)*{0}) + 'px');
",
                _fromHeight);
        }
    }
}
