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
    protected void btn_Click(object semder, EventArgs e)
    {
        Thread.Sleep(2000);
        pnl.Visible = true;
        pnl.Style[Styles.display] = "none";
        new EffectRollDown(pnl, 400).Render();
    }
}
