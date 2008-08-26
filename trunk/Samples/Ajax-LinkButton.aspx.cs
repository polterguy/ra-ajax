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

public partial class AjaxLinkButton : System.Web.UI.Page
{
    protected void btn_Click(object sender, EventArgs e)
    {
        btn.Text = "I was CLICKED";
    }

    protected void lnk1_Click(object sender, EventArgs e)
    {
        lnk1.Text = "I was CLICKED";
        lnk2.Text = "-- watch me as, ";
        Thread.Sleep(5000);
    }

    protected void lnk2_Click(object sender, EventArgs e)
    {
        lnk2.Text += "I am CLICKED --";
    }
}
