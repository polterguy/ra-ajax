/*
 * Ra Ajax - An Ajax Library for Mono ++
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
    public abstract class Effect
    {
        #region [-- Private Fields --]

        private Control _control;
        private int _milliseconds;
		private bool _sinoidal;
        private List<Effect> _paralleled = new List<Effect>();
        private List<Effect> _chained = new List<Effect>();

        #endregion

        #region [-- Public Properties --]

        public Control Control
        {
            get { return _control; }
            private set { _control = value; }
        }

        public bool Sinoidal
        {
            get { return _sinoidal; }
            set { _sinoidal = value; }
        }

        public List<Effect> Paralleled
        {
            get { return _paralleled; }
        }

        public List<Effect> Chained
        {
            get { return _chained; }
        }

        #endregion

        protected Effect(Control control, int milliseconds)
		{
			_control = control;
			_milliseconds = milliseconds;
        }

        public Effect Chain(Effect chainedEffect)
        {
            this.Chained.Add(chainedEffect);
            return chainedEffect;
        }

        public void ChainThese(params Effect[] chainedEffects)
        {
            for (int i = 1; i < chainedEffects.Length; i++)
                chainedEffects[i - 1].Chained.Add(chainedEffects[i]);
            
            this.Chained.Add(chainedEffects[0]);
        }

        #region [-- Abstract Render Methods for Parallel Effects --]

        public abstract string RenderParalledOnStart();

        public abstract string RenderParalledOnFinished();

        public abstract string RenderParalledOnRender();

        #endregion

        #region [-- Rendering Methods --]

        private string RenderOnStart(Effect effect)
		{
			string retVal = this.RenderParalledOnStart();
			foreach (Effect idx in Paralleled)
			{
				retVal += idx.RenderParalledOnStart();
			}
			return retVal;
		}

		private string RenderOnFinished(Effect effect)
		{
			string retVal = this.RenderParalledOnFinished();
			foreach (Effect idx in Paralleled)
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

            foreach (Effect idxParalleled in Paralleled)
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
			foreach (Effect idx in Paralleled)
			{
				retVal += idx.RenderParalledOnRender();
			}
			return retVal;
		}

        public virtual void Render()
        {
            AjaxManager.Instance.WriterAtBack.WriteLine(RenderImplementation());
        }

        private string RenderImplementation()
        {
            foreach (Effect idx in Paralleled)
            {
                idx.Control = this.Control;
            }

            // If the if sentence below kicks in then this is NOT a chained effect rendering
            // which is the only place where it makes sense to have zero seconds and/or no
            // Control to update...
            if (this._control == null || this._milliseconds == 0)
                throw new ArgumentException("Cannot have an effect which affects no Control or has zero value for Duration property");
            string onStart = RenderOnStart(this);
            string onFinished = RenderOnFinished(this);
            string onRender = RenderOnRender(this);
            return string.Format(@"
Ra.E('{0}', {{
  onStart: function() {{{2}}},
  onFinished: function() {{{3}}},
  onRender: function(pos) {{{4}}},
  duration:{1},
  sinoidal:{5}
}});",
                _control.ClientID,
                _milliseconds.ToString(),
                onStart,
                onFinished,
                onRender,
                this.Sinoidal ? "true" : "false");
        }

        #endregion
    }
}