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
    public class EffectFadeOut : Effect
    {
        private Control _control;
        private decimal _seconds;

        public EffectFadeOut(Control control, decimal seconds)
        {
            _control = control;
            _seconds = seconds;
        }

        public override void Render()
        {
            AjaxManager.Instance.WriterAtBack.WriteLine(@"
Ra.E('{0}', {{
  onStart: function() {{
    this.element.setOpacity(1);
    this.element.style.display = '';
  }},
  onFinished: function() {{
    this.element.style.display = 'none';
  }},
  onRender: function(pos) {{
    this.element.setOpacity(1.0 - pos);
  }},
  duration:{1}
}});", _control.ClientID,
     _seconds.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }
    }
}
