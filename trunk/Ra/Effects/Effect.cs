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
        private Control _control;
        private decimal _seconds;
		
		protected Effect(Control control, decimal seconds)
		{
			_control = control;
			_seconds = seconds;
		}
		
		public Control Control
		{
			get { return _control; }
		}

        public abstract string RenderChainedOnStart();

        public abstract string RenderChainedOnFinished();

        public abstract string RenderChainedOnRender();
		
		private string RenderOnStart(Effect effect)
		{
			string retVal = this.RenderChainedOnStart();
			foreach (Effect idx in Chained)
			{
				retVal += idx.RenderChainedOnStart();
			}
			return retVal;
		}

		private string RenderOnFinished(Effect effect)
		{
			string retVal = this.RenderChainedOnFinished();
			foreach (Effect idx in Chained)
			{
				retVal += idx.RenderChainedOnFinished();
			}
			return retVal;
		}

		private string RenderOnRender(Effect effect)
		{
			string retVal = this.RenderChainedOnRender();
			foreach (Effect idx in Chained)
			{
				retVal += idx.RenderChainedOnRender();
			}
			return retVal;
		}

        public virtual void Render()
        {
			// If the if sentence below kicks in then this is NOT a chained effect rendering
			// which is the only place where it makes sense to have zero seconds and/or no
			// Control to update...
			if (this._control == null || this._seconds == 0.0M)
				throw new ArgumentException("Cannot have an effect which affects no Controls or lasts for zero period");
            string onStart = RenderOnStart(this);
            string onFinished = RenderOnFinished(this);
            string onRender = RenderOnRender(this);
            AjaxManager.Instance.WriterAtBack.WriteLine(@"
Ra.E('{0}', {{
  onStart: function() {{{2}}},
  onFinished: function() {{{3}}},
  onRender: function(pos) {{{4}}},
  duration:{1}
}});", 
                _control.ClientID,
                _seconds.ToString(System.Globalization.CultureInfo.InvariantCulture),
                onStart,
                onFinished,
                onRender);
        }

        private List<Effect> _chained = new List<Effect>();
        public List<Effect> Chained
        {
            get { return _chained; }
        }
    }
}