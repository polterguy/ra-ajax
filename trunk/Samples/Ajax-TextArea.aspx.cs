/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace Samples
{
    public partial class AjaxTextArea : System.Web.UI.Page
    {
        protected void btn_Click(object sender, EventArgs e)
        {
            lbl.Text = txt.Text;
            txt.Select();
            txt.Focus();
        }

        protected void txt_Focused(object sender, EventArgs e)
        {
            txt.Select();
        }
    }
}
