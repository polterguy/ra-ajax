/*
 * Ra-Ajax - An Ajax Library for Mono ++
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
    /**
     * Base class for all effects
     */
    public abstract class Effect
    {
        /**
         * Defines how the Effect should handle the duration
         */
        public enum Transition
        {
            /**
             * Effect will execute linearly
             */
            Linear,

            /**
             * Most of the Effect will be executed in the last. Starting slowly and picking up speed towards the end
             */
            Accelerating,

            /**
             * Most of the Effect will be executed in the first parts. Starting at a high speed and loosing speed
             * towards the end.
             */
            Explosive,
        };

        #region [-- Private Fields --]

        private Control _control;
        private int _milliseconds;
		private Transition _type = Transition.Explosive;
        private List<Effect> _joined = new List<Effect>();
        private List<Effect> _chained = new List<Effect>();

        #endregion

        #region [-- Public Properties --]

        /**
         * Control to run effect on, doesn't have to be a Ra-Control
         */
        public Control Control
        {
            get { return _control; }
            private set { _control = value; }
        }

        /**
         * Number of milliseconds effect will spend rendering
         */
        public int Milliseconds
        {
            get { return _milliseconds; }
        }

        /**
         * Type of transition
         */
        public Transition TransitionType
        {
            get { return _type; }
            set { _type = value; }
        }

        /**
         * List of all the joined effects. A joined effect is an effect which will execute at the same
         * time as the original effect. Requires that the joined effects are being run on the same Control.
         */
        public List<Effect> Joined
        {
            get { return _joined; }
        }

        /**
         * List of chained effects. A chained effect is an effect which will start immediately afterwards
         * the effect itszelf has finished excution.
         */
        public List<Effect> Chained
        {
            get { return _chained; }
        }

        #endregion

        /**
         * When creating new extension effects use this constructor in your own effect to initialize the
         * base class properties
         */
        protected Effect(Control control, int milliseconds)
		{
			_control = control;
			_milliseconds = milliseconds;
        }

        /**
         * Takes a params list of effects which will be Chained
         */
        public Effect ChainThese(params Effect[] chainedEffects)
        {
            if (chainedEffects.Length == 0)
                return this;

            for (int i = 1; i < chainedEffects.Length; i++)
                chainedEffects[i - 1].Chained.Add(chainedEffects[i]);
            
            _chained.Add(chainedEffects[0]);

            return this;
        }

        /**
         * Takes a params list of effects which will be Joined
         */
        public Effect JoinThese(params Effect[] joinedEffects)
        {
            _joined.AddRange(joinedEffects);
            return this;
        }

        #region [-- Abstract Render Methods for Parallel Effects --]

        /**
         * Implementation of the body rendering of the OnStart logic
         */
        public abstract string RenderParalledOnStart();

        /**
         * Implementation of the body rendering of the OnFinished logic
         */
        public abstract string RenderParalledOnFinished();

        /**
         * Implementation of the body rendering of the OnRender logic
         */
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

        /**
         * Renders the effect into the response stream.
         */
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

        /**
         * Override this one to validate the parameters of your effect. Will be called before rendering occurs
         * and is expected to throw an exception if parameters are illegal or wrong.
         */
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