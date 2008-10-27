/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace Samples
{
    public partial class AjaxTimer : System.Web.UI.Page
    {
        protected void timer1_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            new EffectSize(pnlTimer, 400, rnd.Next(100, 250), rnd.Next(200, 700)).Render();
            new EffectMove(pnlTimer2, 400, rnd.Next(0, 100), rnd.Next(0, 200)).Render();
            date.Text = DateTime.Now.ToString("dddd, MMM dd, yyyy - HH:mm:ss");
            new EffectFadeIn(date, 400).Render();
        }
    }
}
