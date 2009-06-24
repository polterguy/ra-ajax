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
