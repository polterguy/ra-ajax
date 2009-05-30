/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Extensions;
using Ra.Widgets;
using Ra.Effects;

public partial class Docs_Controls_InPlaceEdit : System.Web.UI.UserControl
{
    protected void edit_TextChanged(object sender, EventArgs e)
    {
        lbl.Text = edit.Text;
        new EffectHighlight(lbl, 400).Render();
    }
}
