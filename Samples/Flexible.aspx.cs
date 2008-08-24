/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Ra Software AS thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

public partial class Flexible : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btn_MouseOver(object sender, EventArgs e)
    {
        btn.Text = "Mouse Over";
    }

    protected void btn_MouseOut(object sender, EventArgs e)
    {
        btn.Text = "Mouse Out";
    }

    protected void txt_MouseOut(object sender, EventArgs e)
    {
        pnl.Visible = false;
    }

    protected void txt_MouseOver(object sender, EventArgs e)
    {
        pnl.Visible = true;
    }
}
