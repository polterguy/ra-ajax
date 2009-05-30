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

public partial class Docs_Controls_SelectList : System.Web.UI.UserControl
{
    protected void SelectChanged(object sender, EventArgs e)
    {
        if (sel.SelectedItem.Value == "Highlight")
            new EffectHighlight(lbl, 400).Render();
        else
            new EffectFadeIn(lbl, 400).Render();
    }
}
