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
    public partial class AjaxCalendar : System.Web.UI.Page
    {
        protected void calTab_SelectedValueChanged(object sender, EventArgs e)
        {
            lbl.Text = calTab.Value.ToString("dddd, dd.MMM yy");
        }
    }
}
