/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using System.Web.UI;
using Ra.Widgets;

namespace Ra.Effects
{
    /**
     * Will make sure control gets focus when run. Useful for effect which fades a control in and afterwards
     * needs to give focus to a specific child control etc. Often you have a control which fades in with
     * an effect but you still want to give focus to a child control of the given control. This is normally
     * not possible using the conventional Focus and Select methods since if the control has display:none
     * which they very often do when you want them to animate in with e.g. EffectFadeIn etc since both
     * Focus and Select will fail if the control is either directly or indirectly in-visible. For such
     * circumstances this effect might come in handy.
     */
    public class EffectFocusAndSelect : Effect
    {
        private bool isRaControl;

        /**
         * CTOR - control to animate and milliseconds to spend executing
         */
        public EffectFocusAndSelect(Control control)
			: base(control, 1)
		{
            isRaControl = control is RaWebControl;
        }

        protected override string RenderParalledOnStart()
        {
            if (isRaControl)
            {
                return @"
    var ctrl = Ra.Control.$(this.element.id);
    ctrl.Focus();
    if( ctrl.Select ) {
      ctrl.Select();
    }
";
            }
            else
            {
                return @"
    this.element.focus();
    if( this.element.select ) {
      this.element.select();
    }
";
            }
        }

        protected override string RenderParalledOnFinished()
        {
            return "";
        }

        protected override string RenderParalledOnRender()
        {
            return "";
        }
    }
}
