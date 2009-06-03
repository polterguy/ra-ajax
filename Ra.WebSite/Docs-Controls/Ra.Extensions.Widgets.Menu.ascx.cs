/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Extensions;
using Ra.Extensions.Widgets;

public partial class Docs_Controls_Menu : System.Web.UI.UserControl
{
    protected void ItemSelected(object sender, EventArgs e)
    {
        MenuItem item = sender as MenuItem;
        lbl.Text = item.Text;
    }
}
