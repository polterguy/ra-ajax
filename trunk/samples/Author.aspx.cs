/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

public partial class Author : System.Web.UI.Page
{
    protected void drop_click(object sender, EventArgs e)
    {
        pnlEmail.Visible = true;
        Effect effect = new EffectFadeIn(pnlEmail, 1.0M);
        effect.Render();
        effect = new EffectRollDown(pnlEmail, 1.0M, 60);
        effect.Render();
    }
}
