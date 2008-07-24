/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web.UI;

namespace Ra.Widgets
{
    public class EffectRollDown : Effect
    {
        private Control _control;
        private decimal _seconds;
        private int _toHeight;

        public EffectRollDown(Control control, decimal seconds, int toHeight)
        {
            _control = control;
            _seconds = seconds;
            _toHeight = toHeight;
        }

        public override void Render()
        {
            AjaxManager.Instance.WriterAtBack.WriteLine(@"
Ra.E('{0}', {{
  onStart: function() {{
    this.element.style.height = '0px';
    this.element.style.display = '';
  }},
  onFinished: function() {{
    this.element.style.height = '{2}px';
  }},
  onRender: function(pos) {{
    this.element.style.height = ({2}*pos) + 'px';
  }},
  duration:{1}
}});", _control.ClientID,
     _seconds.ToString(System.Globalization.CultureInfo.InvariantCulture),
     _toHeight);
        }
    }
}
