/*
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the the MIT license which 
 * means you can use it (almost) exactly as you wish in your 
 * own projects and copy and paste as much as you want.
 */

using System;
using Ra.Extensions;
using Ra.Widgets;
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
}
