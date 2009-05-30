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

public partial class Docs_Controls_EffectMove : System.Web.UI.UserControl
{
    protected void btn_Click(object sender, EventArgs e)
    {
        if (lbl.Style[Styles.left] == "100px")
            new EffectMove(lbl, 800, 50, 30)
                .Render();
        else
            new EffectMove(lbl, 800, 100, 10)
                .Render();
    }
}
