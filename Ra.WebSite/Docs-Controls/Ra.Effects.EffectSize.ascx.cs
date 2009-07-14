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

public partial class Docs_Controls_EffectSize : System.Web.UI.UserControl
{
    protected void btn_Click(object sender, EventArgs e)
    {
        if (lbl.Style[Styles.width] == "300px")
            new EffectSize(lbl, 800, 250, 50)
                .Render();
        else
            new EffectSize(lbl, 800, 50, 300)
                .Render();
    }
}
