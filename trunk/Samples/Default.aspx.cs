/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace Samples
{
    public partial class _Default : System.Web.UI.Page
    {
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
        }
    }
}
