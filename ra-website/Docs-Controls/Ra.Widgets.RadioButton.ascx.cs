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

public partial class Docs_Controls_RadioButton : System.Web.UI.UserControl
{
    protected void CheckedChanged(object sender, EventArgs e)
    {
        if (rdo1.Checked)
            new EffectHighlight(lbl, 400).Render();
        else
            new EffectFadeIn(lbl, 400).Render();
    }
}
