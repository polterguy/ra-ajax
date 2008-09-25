/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
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
        if (!IsPostBack)
        {
            btn.Focus();
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        btn.Enabled = false;
        btn2.Enabled = true;
        btn2.Focus();
        Effect effect = new EffectSize(pnl, 600, 160, 300);
        effect.Sinoidal = true;
        effect.Paralleled.Add(new EffectHighlight());
        effect.Paralleled.Add(new EffectBorder(5));
        effect.Render();
    }

    protected void btn2_Click(object sender, EventArgs e)
    {
        btn2.Enabled = false;
        btn3.Enabled = true;
        btn3.Focus();
        Effect effect = new EffectFadeOut(pnl, 600);
        effect.Render();
    }

    protected void btn3_Click(object sender, EventArgs e)
    {
        btn3.Enabled = false;
        btn2.Enabled = true;
        btn2.Focus();
        Effect effect = new EffectFadeIn(pnl, 600);
        effect.Render();
    }

    protected void btn4_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectFadeIn(pnl2, 800);
        effect.Paralleled.Add(new EffectRollDown(160));
        effect.Render();
    }
}
