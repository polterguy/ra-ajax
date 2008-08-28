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
        protected Control _control;
        protected decimal _seconds;

        public abstract string RenderChainedOnStart();

        public abstract string RenderChainedOnFinished();

        public abstract string RenderChainedOnRender();

        public virtual void Render()
        {
            string onStart = RenderChainedOnStart();
            string onFinished = RenderChainedOnFinished();
            string onRender = RenderChainedOnRender();
            foreach (Effect idx in Chained)
            {
                onStart += idx.RenderChainedOnStart();
                onFinished += idx.RenderChainedOnFinished();
                onRender += idx.RenderChainedOnRender();
            }
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