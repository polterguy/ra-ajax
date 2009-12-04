/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using Ra.Effects;

public partial class Docs_Controls_Timer : System.Web.UI.UserControl
{
    protected void timer_Tick(object sender, EventArgs e)
    {
        lbl.Text = DateTime.Now.ToString("dddd dd.MMM yyyy - HH:mm:ss");
        new EffectHighlight(lbl, 200).Render();
        Random rnd = new Random();
        new EffectMove(lbl, 400, rnd.Next(0, 350), rnd.Next(0, 250)).Render();
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        timer.Enabled = !timer.Enabled;
        lbl.Text = timer.Enabled ? "Timer enabled..." : "DISABLED";
        timer.Duration = new Random().Next(100, 1500);
    }
}
