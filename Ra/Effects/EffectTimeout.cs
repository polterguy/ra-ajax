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
    public class EffectTimeout : Effect
    {
        public EffectTimeout(int milliseconds)
            : base(null, milliseconds)
        { }

        protected override void ValidateEffect()
        { }

        public override string RenderParalledOnStart()
        {
            return string.Empty;
        }

        public override string RenderParalledOnFinished()
        {
            return string.Empty;
        }

        public override string RenderParalledOnRender()
        {
            return string.Empty;
        }
    }
}
