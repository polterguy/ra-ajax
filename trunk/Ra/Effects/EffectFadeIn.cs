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
    public class EffectFadeIn : Effect
    {
        public EffectFadeIn(Control control, decimal seconds)
        {
            _control = control;
            _seconds = seconds;
        }

        public override string RenderChainedOnStart()
        {
            return @"
    this.element.setOpacity(0);
    this.element.style.display = '';
";
        }

        public override string RenderChainedOnFinished()
        {
            return @"
    this.element.setOpacity(1);
";
        }

        public override string RenderChainedOnRender()
        {
            return @"
    this.element.setOpacity(pos);
";
        }
    }
}
