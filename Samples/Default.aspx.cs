/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Ra Software AS thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            name.Focus();
            name.Select();
        }
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        lblResults.Text = "Hello " + name.Text + " and welcome to the world :)";
        Effect effect = new EffectFadeIn(lblResults, 0.8M);
        effect.Render();
        name.Focus();
        name.Select();
    }

    protected void submit2_Click(object sender, EventArgs e)
    {
        pnl.Style["background-color"] = pnl.Style["background-color"] == "Orange" ? "Yellow" : "Orange";
    }
}
