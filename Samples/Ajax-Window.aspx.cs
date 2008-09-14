/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

public partial class AjaxWindow : System.Web.UI.Page
{
    protected void showWindow_Click(object sender, EventArgs e)
    {
		window.Visible = true;
		Effect effect = new EffectFadeIn(window, 400);
		effect.Render();
    }
}
