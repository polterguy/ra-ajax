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

        protected override string RenderImplementation()
        {
            foreach (Effect idx in Joined)
            {
                idx.Control = this.Control;
            }

            ValidateEffect();
            string onStart = RenderOnStart(this);
            string onFinished = RenderOnFinished(this);
            string onRender = RenderOnRender(this);
            string extraOptions = RenderExtraOptions();
            return string.Format(@"
Ra.E('{0}', {{
  xtra: {{{6}}}
  onStart: function() {{{2}}},
  onFinished: function() {{{3}}},
  onRender: function(pos) {{{4}}},
  duration:{1},
  transition:'{5}'
}});",
                _controls[0].ClientID,
                Milliseconds.ToString(),
                onStart,
                onFinished,
                onRender,
                this.TransitionType,
                extraOptions);
        }

        private string RenderOnStart(Effect effect)
        {
            string retVal = this.RenderParalledOnStart();
            foreach (Effect idx in Joined)
            {
                retVal += idx.RenderParalledOnStart();
            }
            return retVal;
        }

        private string RenderOnFinished(Effect effect)
        {
            string retVal = this.RenderParalledOnFinished();
            foreach (Effect idx in Joined)
            {
                retVal += idx.RenderParalledOnFinished();
            }
            retVal += RenderChained();
            return retVal;
        }


        private string RenderChained()
        {
            string retVal = string.Empty;
            foreach (Effect idx in Chained)
            {
                retVal += idx.RenderImplementation();
            }

            foreach (Effect idxParalleled in Joined)
            {
                foreach (Effect idxChained in idxParalleled.Chained)
                {
                    retVal += idxChained.RenderImplementation();
                }
            }
            return retVal;
        }

        private string RenderOnRender(Effect effect)
        {
            string retVal = this.RenderParalledOnRender();
            foreach (Effect idx in Joined)
            {
                retVal += idx.RenderParalledOnRender();
            }
            return retVal;
        }

        private string RenderExtraOptions()
        {
            string retVal = string.Empty;

            bool first = true;
            foreach (Control control in _controls)
            {
                if (first)
                {
                    retVal += string.Format("'{0}'", control.ClientID);
                    first = false;
                }
                else
                {
                    retVal += string.Format(",'{0}'", control.ClientID);
                }
            }

            return retVal;
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