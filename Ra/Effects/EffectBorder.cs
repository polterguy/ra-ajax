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
    public class EffectBorder : Effect
    {
		private int _borderTo;

        public EffectBorder(Control control, int milliseconds, int borderTo)
			: base(control, milliseconds)
		{
			_borderTo = borderTo;
		}

        public EffectBorder(Control control, int milliseconds)
			: base(control, milliseconds)
		{ }

		// For chained effects
        public EffectBorder()
			: base(null, 0)
		{ }
		
		// For chained effects
        public EffectBorder(int borderTo)
			: base(null, 0)
		{
			_borderTo = borderTo;
		}
		
		public int BorderTo
		{
			get { return _borderTo; }
			set { _borderTo = value; }
		}
		
		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
				tmp.Style["border-style", false] = "dashed";
				tmp.Style["border-width", false] = BorderTo + "px";
			}
		}

        public override string RenderChainedOnStart()
        {
			UpdateStyleCollection();
            return @"
    this.element.style.borderStyle = 'dashed';
    this.element.style.borderWidth = '1px';
";
        }

        public override string RenderChainedOnFinished()
        {
            return string.Format(@"
    this.element.style.borderWidth = '{0}px';
", BorderTo);
        }

        public override string RenderChainedOnRender()
        {
            return string.Format(@"
    var x = parseInt(pos*{0});
    this.element.style.borderWidth = x + 'px';
", BorderTo);
        }
    }
}
