/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using Ra.Extensions.Widgets;
using Ra.Effects;

public partial class Docs_Controls_RichEdit : System.Web.UI.UserControl
{
    protected void editor_CtrlKey(object sender, RichEdit.CtrlKeysEventArgs e)
    {
        lbl.Text = "Dead key clicked; " + e.Key;
        new EffectHighlight(lbl, 200).Render();
    }
}
