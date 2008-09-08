/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

public partial class Behaviors : System.Web.UI.Page
{
    protected void dragger_Dropped(object sender, EventArgs e)
    {
		lbl.Text = string.Format("Label dragged and dropped to {0}, {1}", lbl.Style["left"], lbl.Style["top"]);
		Effect effect = new EffectFadeIn(lbl, 0.4M);
		effect.Render();
    }

    protected void dragger2_Dropped(object sender, EventArgs e)
    {
		lbl2.Text = string.Format("Label dragged and dropped to {0}, {1}", lbl2.Style["left"], lbl2.Style["top"]);
		Effect effect = new EffectFadeIn(lbl2, 0.4M);
		effect.Render();
    }
}
