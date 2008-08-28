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
        {
            _control = control;
            _seconds = seconds;
            _fromHeight = fromHeight;
        }

        public override string RenderChainedOnStart()
        {
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
