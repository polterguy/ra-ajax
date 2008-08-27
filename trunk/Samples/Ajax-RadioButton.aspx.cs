/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using System.Threading;

public partial class AjaxRadioButton : System.Web.UI.Page
{
    protected void rdo1_CheckedChanged(object sender, EventArgs e)
    {
        HelloWorld1.Visible = !rdo1.Checked;
        Combining1.Visible = rdo1.Checked;
        pnl.ReRender();
    }
}
