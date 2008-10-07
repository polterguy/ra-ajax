/*
 * Ra-Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace Samples
{
    public partial class AjaxLabel : System.Web.UI.Page
    {
        protected void btn_Click(object sender, EventArgs e)
        {
            lbl.Text = "The button was clicked :)";
            lbl.Style["font-weight"] = lbl.Style["font-weight"] == "bold" ? "normal" : "bold";
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            lbl2.Visible = true;
        }
    }
}
