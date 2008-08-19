/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Ra Software AS thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

public partial class Timer : System.Web.UI.Page
{
    protected void timer_Tick(object sender, EventArgs e)
    {
        Random rnd = new Random();
        Effect effect = new EffectSize(pnlTimer, 0.4M, rnd.Next(100, 250), rnd.Next(200, 700));
        effect.Render();
        effect = new EffectMove(pnlTimer2, 0.4M, rnd.Next(0, 100), rnd.Next(0, 200));
        effect.Render();
        date.Text = DateTime.Now.ToString("dddd, MMM dd, yyyy - HH:mm:ss");
        effect = new EffectFadeIn(date, 0.4M);
        effect.Render();
    }
}
