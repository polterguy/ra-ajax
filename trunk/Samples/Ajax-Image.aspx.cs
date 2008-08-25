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
    protected void btn_Click(object sender, EventArgs e)
    {
        img.ImageUrl = 
            img.ImageUrl == "media/Pooh_Shepard_1926.png" ?
                "media/180px-Winniethepooh.png" : 
                "media/Pooh_Shepard_1926.png";
    }
}
