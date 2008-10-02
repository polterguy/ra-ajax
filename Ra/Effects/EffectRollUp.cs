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
    /**
     * Will roll up control from visibility to in-visibility.
     */
    public class EffectRollUp : Effect
    {
        private int _fromHeight;

        /**
         * Use this CTOR only if your effects are being Joined. 
         * Expects the main effect to set the Control and Duration properties.
         * fromHeight is the starting height of the Control.
         */
        public EffectRollUp(int fromHeight)
            : base(null, 0)
        {
            _fromHeight = fromHeight;
        }

        /**
         * CTOR - control to animate and milliseconds to spend executing in addition to the starting height
         * of the Control.
         */
        public EffectRollUp(Control control, int milliseconds, int fromHeight)
			: base(control, milliseconds)
        {
            _fromHeight = fromHeight;
        }

		private void UpdateStyleCollection()
		{
			RaWebControl tmp = this.Control as RaWebControl;
			if (tmp != null)
			{
                tmp.Style.SetStyleValueViewStateOnly("height", "0px");
                tmp.Style.SetStyleValueViewStateOnly("display", "none");
			}
		}

        public override string RenderParalledOnStart()
        {
			UpdateStyleCollection();
            return string.Format(@"
    this.element.setStyle('display','');
    this.element.setStyle('height','{0}px');
",
                _fromHeight);
        }

        public override string RenderParalledOnFinished()
        {
            return @"
    this.element.setStyle('display','none');
    this.element.setStyle('height','0px');
";
        }

        public override string RenderParalledOnRender()
        {
            return string.Format(@"
    this.element.setStyle('height',((1.0-pos)*{0}) + 'px');
",
                _fromHeight);
        }
    }
}
