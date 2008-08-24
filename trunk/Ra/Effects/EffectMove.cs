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
    public class EffectMove : Effect
    {
        private Control _control;
        private decimal _seconds;
        private int _left;
        private int _top;

        public int Top
        {
            get { return _top; }
            set { _top = value; }
        }

        public int Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public EffectMove(Control control, decimal seconds)
        {
            _control = control;
            _seconds = seconds;
        }

        public EffectMove(Control control, decimal seconds, int left, int top)
        {
            _control = control;
            _seconds = seconds;
            _left = left;
            _top = top;
        }

        public override void Render()
        {
            AjaxManager.Instance.WriterAtBack.WriteLine(@"
Ra.E('{0}', {{
  onStart: function() {{
    this.startL = parseInt(this.element.style.left, 10);
    this.startT = parseInt(this.element.style.top, 10);
  }},
  onFinished: function() {{
    this.element.style.left = {2}+'px';
    this.element.style.top = {3}+'px';
  }},
  onRender: function(pos) {{
    var deltaL = ({2} - this.startL) * pos;
    var newL = parseInt(deltaL + this.startL, 10);
    this.element.style.left = newL + 'px';

    var deltaT = ({3} - this.startT) * pos;
    var newT = parseInt(deltaT + this.startT, 10);
    this.element.style.top = newT + 'px';
  }},
  duration:{1}
}});", _control.ClientID,
     _seconds.ToString(System.Globalization.CultureInfo.InvariantCulture),
     _left,
     _top);
        }
    }
}
