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
        private int _milliseconds;
		private bool _sinoidal;
		
		protected Effect(Control control, int milliseconds)
		{
			_control = control;
			_milliseconds = milliseconds;
		}
		
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

        public abstract string RenderParalledOnStart();

        public abstract string RenderParalledOnFinished();

        public abstract string RenderParalledOnRender();
		
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
			// If the if sentence below kicks in then this is NOT a chained effect rendering
			// which is the only place where it makes sense to have zero seconds and/or no
			// Control to update...
			foreach (Effect idx in Paralleled)
			{
				idx.Control = this.Control;
			}
			if (this._control == null || this._milliseconds == 0)
				throw new ArgumentException("Cannot have an effect which affects no Controls or lasts for zero period");
            string onStart = RenderOnStart(this);
            string onFinished = RenderOnFinished(this);
            string onRender = RenderOnRender(this);
            AjaxManager.Instance.WriterAtBack.WriteLine(@"
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

        private List<Effect> _paralleled = new List<Effect>();
        public List<Effect> Paralleled
        {
            get { return _paralleled; }
        }

        private List<Effect> _chained = new List<Effect>();
        public List<Effect> Chained
        {
            get { return _chained; }
        }
    }
}