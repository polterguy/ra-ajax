/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Ra Software AS thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web.UI;

namespace Ra.Widgets
{
    public class EffectSize : Effect
    {
        private Control _control;
        private decimal _seconds;
        private int _height;
        private int _width;

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public EffectSize(Control control, decimal seconds)
        {
            _control = control;
            _seconds = seconds;
        }

        public EffectSize(Control control, decimal seconds, int height, int width)
        {
            _control = control;
            _seconds = seconds;
            _height = height;
            _width = width;
        }

        public override void Render()
        {
            AjaxManager.Instance.WriterAtBack.WriteLine(@"
Ra.E('{0}', {{
  onStart: function() {{
    this.startH = parseInt(this.element.style.height, 10);
    this.startW = parseInt(this.element.style.width, 10);
  }},
  onFinished: function() {{
    this.element.style.height = {2}+'px';
    this.element.style.width = {3}+'px';
  }},
  onRender: function(pos) {{
    var deltaH = ({2} - this.startH) * pos;
    var newH = parseInt(deltaH + this.startH, 10);
    this.element.style.height = newH + 'px';

    var deltaW = ({3} - this.startW) * pos;
    var newW = parseInt(deltaW + this.startW, 10);
    this.element.style.width = newW + 'px';
  }},
  duration:{1}
}});", _control.ClientID,
     _seconds.ToString(System.Globalization.CultureInfo.InvariantCulture),
     _height,
     _width);
        }
    }
}
