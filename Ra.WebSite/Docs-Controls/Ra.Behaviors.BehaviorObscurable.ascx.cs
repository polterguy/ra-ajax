/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;
using Ra.Extensions;
using Ra.Widgets;
using Ra.Effects;

public partial class Docs_Controls_BehaviorObscurable : System.Web.UI.UserControl
{
    protected void btn_Click(object sender, EventArgs e)
    {
        btn2.Visible = true;
        if (lblInfo.Visible)
            new EffectRollUp(lblInfo, 400).Render();
    }

    protected void btn2_Click(object sender, EventArgs e)
    {
        btn2.Visible = false;
        lblInfo.Visible = true;
        lblInfo.Text = "If you look at the code you will see that " +
            "the button which is being shown have a higher z-index " +
            "then all other controls on the page. Then the Obscurer " +
            "is being added to the body element with a z-index of one " +
            "lower then the control it obscures. And there is the whole " +
            " magic. Of course normally you would never use this for " +
            "a button, but rather panels, Windows and such more '" +
            "advanced' controls.";
        lblInfo.Style[Styles.display] = "none";
        new EffectRollDown(lblInfo, 400).Render();
    }
}
