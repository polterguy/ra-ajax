/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web.UI;

namespace Ra.Widgets
{
    public class EffectFadeIn : Effect
    {
        private Control _control;
        private decimal _seconds;

        public EffectFadeIn(Control control, decimal seconds)
        {
            _control = control;
            _seconds = seconds;
        }

        public override void Render()
        {
            AjaxManager.Instance.WriterAtBack.WriteLine(@"
Ra.E('{0}', {{
  onStart: function() {{
    this.element.setOpacity(0);
    this.element.style.display = '';
  }},
  onFinished: function() {{
    this.element.setOpacity(1);
  }},
  onRender: function(pos) {{
    this.element.setOpacity(pos);
  }},
  duration:{1}
}});", _control.ClientID,
     _seconds.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }
    }
}
