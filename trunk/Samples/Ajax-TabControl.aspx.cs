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
    public partial class AjaxTabControl : System.Web.UI.Page
    {
        protected void tabBtn_Click(object sender, EventArgs e)
        {
            tabBtn.Text = "Clicked";
            tab1.Enabled = !tab1.Enabled;
            tab2.Caption = "I changed";
            tab3.Visible = !tab3.Visible;
        }
    }
}
