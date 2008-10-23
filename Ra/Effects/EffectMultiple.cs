/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Ra.Widgets
{
    public class EffectMultiple : Effect
    {
        Control[] _controls;

        public EffectMultiple(int milliseconds, params Control[] controls) 
            : base(null, milliseconds)
        {
            _controls = controls;
        }

        public override void Render()
        {
            
        }

        protected override void ValidateEffect()
        {
            if (Milliseconds < 1)
                throw new Exception("You must specify a duration for the effect.");
            if (_controls == null || _controls.Length == 0)
                throw new Exception("You must specify at least one control to apply the effect to.");

            foreach (Control control in _controls)
            {
                if (control == null)
                    throw new Exception("You can not have a null control as part of the controls.");
            }


        }

        public override string RenderParalledOnStart()
        {
            
        }

        public override string RenderParalledOnFinished()
        {
            
        }

        public override string RenderParalledOnRender()
        {
            
        }
    }
}