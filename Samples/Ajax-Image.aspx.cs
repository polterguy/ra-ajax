/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

public partial class AjaxImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Image1.Visible = false;
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        img.ImageUrl = 
            img.ImageUrl == "media/flower1.jpg" ?
                "media/flower2.jpg" : 
                "media/flower1.jpg";
    }

    // If you can get to call this method you will get $100 :)
    public void HackApplication()
    {
        Image1.Visible = true;
    }
}
