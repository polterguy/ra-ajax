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
        private List<Effect> _joined = new List<Effect>();
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

        public List<Effect> Joined
        {
            get { return _joined; }
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

        public Effect ChainThese(params Effect[] chainedEffects)
        {
            for (int i = 1; i < chainedEffects.Length; i++)
                chainedEffects[i - 1].Chained.Add(chainedEffects[i]);
            
            _chained.Add(chainedEffects[0]);

            return this;
        }

        public Effect JoinThese(params Effect[] joinedEffects)
        {
            _joined.AddRange(joinedEffects);
            return this;
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
  sinoidal:{5}
}});",
                (_control == null ? null : _control.ClientID),
                _milliseconds.ToString(),
                onStart,
                onFinished,
                onRender,
                this.Sinoidal ? "true" : "false");
        }

        protected virtual void ValidateEffect()
        {
            // If the if sentence below kicks in then this is NOT a chained effect rendering
            // which is the only place where it makes sense to have zero seconds and/or no
            // Control to update...
            if (this._control == null || this._milliseconds == 0)
                throw new ArgumentException("Cannot have an effect which affects no Control or has zero value for Duration property");
        }

        #endregion
    }
}