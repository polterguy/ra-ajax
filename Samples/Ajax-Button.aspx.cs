/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

public partial class AjaxButton : System.Web.UI.Page
{
    protected void click(object sender, EventArgs e)
    {
        (sender as Button).Text = "clicked";
        (sender as Button).Style["background-color"] = "Yellow";
    }

    protected void mouseout(object sender, EventArgs e)
    {
        (sender as Button).Text = "mouseout";
    }

    protected void mouseover(object sender, EventArgs e)
    {
        (sender as Button).Text = "mouseover";
    }
}
