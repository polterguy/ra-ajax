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
new Ra.Effect('{0}', {{
  onStart: function() {{
    this.element.setOpacity(1);
    this.element.style.display = '';
  }},
  onFinished: function() {{
    this.element.style.display = 'none';
    this.element.setOpacity(1);
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
