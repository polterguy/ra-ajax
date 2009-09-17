/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Developed by Karel Boek, karel.boek@raskenlund.com, Sep 2009
 */

using System;
using System.Web.UI;
using Ra.Widgets;

namespace Ra.Effects
{
    public class EffectCssClass : Effect
    {
        private readonly string cssClass;

        public EffectCssClass() 
            : base(null, 0)
        {
        }

        public EffectCssClass(Control control, string cssClass) 
            : base(control, 1)
        {
            this.cssClass = cssClass;
        }

        protected override string RenderParalledOnStart()
        {
            return string.Format(@"this.element.className = ''; this.element.addClassName('{0}')", cssClass);
        }

        protected override string RenderParalledOnFinished()
        {
            var ctrl = Control as RaWebControl;

            if (ctrl != null)
                ctrl.CssClass = cssClass;

            return string.Empty;
        }

        protected override string RenderParalledOnRender()
        {
            return string.Empty;
        }

        protected override void ValidateEffect()
        {
            if (Control == null)
                throw new ArgumentException("Cannot have an CssClass effect which affects no Control");
        }
    }
}
