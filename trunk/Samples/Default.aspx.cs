/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
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
		string gn = string.IsNullOrEmpty(name.Text.Trim()) ? "stranger" : name.Text; 
        lblResults.Text = "Hello " + gn + " and welcome to the world :)";
        Effect effect = new EffectFadeIn(lblResults, 800);
		effect.Joined.Add(new EffectHighlight());
        effect.Render();
        name.Focus();
        name.Select();
    }

    protected void submit2_Click(object sender, EventArgs e)
    {
        pnl.Style[Styles.backgroundColor] = pnl.Style[Styles.backgroundColor] == "Orange" ? "Yellow" : "Orange";

        // This line of code is actually *VERY* convenient since setting an element's
        // opacity so that it works for all different browsers is a *NIGHTMARE*...!
        // Ra-Ajax completely abstracts away that problem...
        // This works on ALL browser 100% transparently
        pnl.Style[Styles.opacity] = "0.5";
    }
}
