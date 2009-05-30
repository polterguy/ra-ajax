/*
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the the MIT license which 
 * means you can use it (almost) exactly as you wish in your 
 * own projects and copy and paste as much as you want.
 */

using System;
using Ra.Extensions;
using Ra.Widgets;

public partial class Docs_Controls_DateTimePicker : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (cal.Style[Styles.width] != "170px")
        {
            cal.Style[Styles.width] = "170px";
            cal.Value = DateTime.Now;
        }
    }

    protected void cal_SelectedValueChanged(object sender, EventArgs e)
    {
        lbl.Text = cal.Value.ToString("dddd dd.MMM yyyy HH:mm");
    }
}
