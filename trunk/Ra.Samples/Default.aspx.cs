/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using Ra.Effects;

public partial class _Default : System.Web.UI.Page
{
    protected void btn_Click(object sender, EventArgs e)
    {
        wnd.Visible = true;
    }

    protected void timer_Tick(object sender, EventArgs e)
    {
        lbl.Text = DateTime.Now.ToString();
        new EffectHighlight(lbl, 500)
            .Render();
    }
}
