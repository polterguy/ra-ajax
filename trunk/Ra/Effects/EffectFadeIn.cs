/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
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
new Ra.Effect('{0}', {{
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
