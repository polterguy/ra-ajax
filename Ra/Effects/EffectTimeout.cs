/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web.UI;

namespace Ra.Effects
{
    /**
     * Empty effect or a "place holder of time" effect. Only purpose is to make a specified duration
     * of time ellapse. Don't need a specified Control to animate upon. Useful if you create advanced
     * effect chains and want to have a "pause" between the end of one effect and the starting of anoter effect.
     * Or if you want to have "delayed effects" which doesn't occur before some n seconds after the Ajax request
     * returns from the server.
     */
    public class EffectTimeout : Effect
    {
        /**
         * CTOR - taking number of milliseconds for the timeout
         */
        public EffectTimeout(int milliseconds)
            : base(null, milliseconds)
        { }

        // Overridden to remove "must have Control" validation in base implementation
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
