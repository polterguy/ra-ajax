/*
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the the MIT license which 
 * means you can use it (almost) exactly as you wish in your 
 * own projects and copy and paste as much as you want.
 */

using System;
using Ra.Extensions;
using System.Threading;
using Ra.Widgets;
using Ra.Effects;

public partial class Docs_Controls_Panel : System.Web.UI.UserControl
{
    protected void lnk_Click(object sender, EventArgs e)
    {
        new EffectSize(pnl, 400, 150, 300).Render();
    }
}
