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
    public partial class AjaxTextBox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt.Select();
                txt.Focus();
            }
        }

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
