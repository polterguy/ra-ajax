/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Ra Software AS thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

public partial class Effects : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectSize(pnl, 0.6M, 300, 300);
        effect.Render();
    }

    protected void btn2_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectFadeOut(pnl, 0.6M);
        effect.Render();
    }

    protected void btn3_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectFadeIn(pnl, 0.6M);
        effect.Render();
    }
}
