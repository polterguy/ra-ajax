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

        public virtual void Render()
        {
            AjaxManager.Instance.WriterAtBack.WriteLine(RenderImplementation());
        }

        private string RenderImplementation()
        {
            foreach (Effect idx in Joined)
            {
                idx.Control = this.Control;
            }

            ValidateEffect();
            string onStart = RenderOnStart(this);
            string onFinished = RenderOnFinished(this);
            string onRender = RenderOnRender(this);
            return string.Format(@"
Ra.E('{0}', {{
  onStart: function() {{{2}}},
  onFinished: function() {{{3}}},
  onRender: function(pos) {{{4}}},
  duration:{1},
  transition:'{5}'
}});",
                (_control == null ? null : _control.ClientID),
                _milliseconds.ToString(),
                onStart,
                onFinished,
                onRender,
                this.TransitionType);
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