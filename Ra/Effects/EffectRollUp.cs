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
    public class EffectRollUp : Effect
    {
        private Control _control;
        private decimal _seconds;
        private int _fromHeight;

        public EffectRollUp(Control control, decimal seconds, int fromHeight)
        {
            _control = control;
            _seconds = seconds;
            _fromHeight = fromHeight;
        }

        public override void Render()
        {
            AjaxManager.Instance.WriterAtBack.WriteLine(@"
Ra.E('{0}', {{
  onStart: function() {{
    this.element.style.display = '';
    this.element.style.height = '{2}px'
  }},
  onFinished: function() {{
    this.element.style.display = 'none';
  }},
  onRender: function(pos) {{
    this.element.style.height = ((1.0-pos)*{2}) + 'px';
  }},
  duration:{1}
}});", _control.ClientID,
     _seconds.ToString(System.Globalization.CultureInfo.InvariantCulture),
     _fromHeight);
        }
    }
}
