/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Drawing;
using Ra.Widgets;

namespace Samples
{
    public partial class AjaxUpdater : System.Web.UI.Page
    {
        protected void btn_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            lbl.Text = "Note though that there was an intentional delay of 2 seconds on " +
                "the server when the request was being sent to make sure you would notice " +
                "the Ajax Updater";
            new EffectHighlight(lbl, 400).Render();
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            lbl.Text = "This request was too fast to trigger updater " +
                "since updater waits for 0.5 seconds before kicking in";
            new EffectHighlight(lbl, 400).Render();
        }

        protected void btn3_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(600);
            lbl.Text = "Did you notice how only 'parts' of the effect was running?";
            new EffectHighlight(lbl, 400).Render();
        }
    }
}
