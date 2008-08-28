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
        {
            _control = control;
            _seconds = seconds;
            _toHeight = toHeight;
        }

        public override string RenderChainedOnStart()
        {
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
