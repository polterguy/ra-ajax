/*
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the the MIT license which 
 * means you can use it (almost) exactly as you wish in your 
 * own projects and copy and paste as much as you want.
 */

using System;
using Ra.Extensions;
using Ra.Widgets;
using Ra.Extensions.Widgets;
using Ra.Effects;

public partial class Docs_Controls_ResizeHandler : System.Web.UI.UserControl
{
    protected void re_Resized(object sender, ResizeHandler.ResizedEventArgs e)
    {
        lbl.Text = string.Format("New size width {0}px and height {1}px", e.Width, e.Height);
        new EffectHighlight(lbl, 400).Render();
    }
}
