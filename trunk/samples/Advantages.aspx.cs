/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

public partial class Advantages : System.Web.UI.Page
{
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        string results = "You have chosen; ";
        foreach (CheckBox idx in new CheckBox[] { chk1, chk2, chk3, chk4 })
        {
            if (idx.Checked)
                results += idx.Text + ", ";
        }
        lblResults.Text = results;
    }

    protected void rdo_CheckedChanged(object sender, EventArgs e)
    {
        if (rdo1.Checked)
        {
            lblEndResults.Text = "Thank you for shopping here";
            panelResults.Style["background-color"] = "Green";
        }
        else
        {
            lblEndResults.Text = "FBI is on its way";
            panelResults.Style["background-color"] = "Red";
        }
        Effect effect = new EffectRollDown(panelResults, 1.0M, 200);
        effect.Render();
    }
}
