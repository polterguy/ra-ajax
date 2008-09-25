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
        // Resetting element back to base
        pnl2.Style["border"] = "solid 1px black";
        pnl2.Style["width"] = "100px";
        pnl2.Style["height"] = "100px";

        // Running a whole bunch of effects which are chained...
        Effect effect = new EffectFadeIn(pnl2, 1000);
        effect.Chained.Add(new EffectHighlight(pnl2, 1000));
        effect.Chained[0].Chained.Add(new EffectSize(pnl2, 500, -1, 500));
        effect.Chained[0].Chained[0].Chained.Add(new EffectSize(pnl2, 500, 150, -1));
        effect.Chained[0].Chained[0].Chained[0].Chained.Add(new EffectBorder(pnl2, 500, 5));
        effect.Chained[0].Chained[0].Chained[0].Chained[0].Chained.Add(new EffectHighlight(pnl2, 500));
        effect.Render();
    }
}
