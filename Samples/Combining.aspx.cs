/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace Samples
{
    public partial class Combining : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                calendar.Value = DateTime.Now;
        }

        protected void uc_Saved(object sender, EventArgs e)
        {
            lblResults.Text = string.Format("Hello {0}, I see you're {1} years old :)", uc.Name, uc.Age);
        }

        protected void calendar_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedDate.Text = calendar.Value.ToString("dddd, MMMM dd, yyyy", System.Threading.Thread.CurrentThread.CurrentUICulture);
        }
    }
}