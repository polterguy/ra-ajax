/*
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the the MIT license which 
 * means you can use it (almost) exactly as you wish in your 
 * own projects and copy and paste as much as you want.
 */

using System;
using Ra.Extensions;
using Ra.Widgets;

public partial class Docs_Controls_BehaviorDroppable : System.Web.UI.UserControl
{
    protected void Dropped(object sender, EventArgs e)
    {
        lbl.Text = "STOP WASTING MY MONEY...!!";
        lbl2.Text = "Thank you for shopping here :)";
        new EffectMove(pnl, 400, 15, 15).Render();
    }
}
