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
    public partial class AjaxDropDownList : System.Web.UI.Page
    {
        protected void selectedchanged(object sender, EventArgs e)
        {
            lblResults2.Text = list.SelectedItem.Text;
        }

        protected void mouseout(object sender, EventArgs e)
        {
            lblResults.Text = "mouse out";
        }

        protected void mouseover(object sender, EventArgs e)
        {
            lblResults.Text = "mouse over";
        }
    }
}
