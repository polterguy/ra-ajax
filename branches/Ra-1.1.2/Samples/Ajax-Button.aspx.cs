/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace Samples
{
    public partial class AjaxButton : System.Web.UI.Page
    {
        protected void click(object sender, EventArgs e)
        {
            (sender as Button).Text = "clicked";
            (sender as Button).Style["background-color"] = "Yellow";
            new EffectSize(sender as Button, 500, 15, 25).ChainThese(
                new EffectSize(sender as Button, 500, 100, 250))
            .Render();
        }
    }
}
