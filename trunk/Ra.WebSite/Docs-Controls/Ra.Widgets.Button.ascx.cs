/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using Ra.Extensions;
using System.Threading;
using Ra.Widgets;
using Ra.Effects;

public partial class Docs_Controls_BehaviorUpdater : System.Web.UI.UserControl
{
    protected void btn_Click(object sender, EventArgs e)
    {
        btn.Text = "I was clicked...";
        if (btn.Style[Styles.width] == "200px")
        {
            new EffectSize(btn, 400, 30, 120).Render();
        }
        else
        {
            new EffectSize(btn, 400, 80, 200).Render();
        }
    }
}
