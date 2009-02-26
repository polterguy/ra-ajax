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
    public partial class AjaxHiddenField : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt.Focus();
                txt.Select();
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            hid.Value = txt.Text;
            txt.Text = "";
        }

        protected void retrieveValue_Click(object sender, EventArgs e)
        {
            txt.Text = hid.Value;
        }
    }
}
