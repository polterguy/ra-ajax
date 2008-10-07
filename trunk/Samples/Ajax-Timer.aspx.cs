/*
 * Ra-Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace Samples
{
    public partial class AjaxTimer : System.Web.UI.Page
    {
        protected void timer_Tick(object sender, EventArgs e)
        {
            lbl.Text = DateTime.Now.ToString("dddd, dd MM - yy : HH:mm:ss");
            Effect effect = new EffectHighlight(lbl, 300);
            effect.Render();
        }
    }
}
