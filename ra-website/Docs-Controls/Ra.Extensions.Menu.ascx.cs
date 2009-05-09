﻿/*
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the the MIT license which 
 * means you can use it (almost) exactly as you wish in your 
 * own projects and copy and paste as much as you want.
 */

using System;
using Ra.Extensions;

public partial class Docs_Controls_Menu : System.Web.UI.UserControl
{
    protected void ItemSelected(object sender, EventArgs e)
    {
        MenuItem item = sender as MenuItem;
        lbl.Text = item.Text;
    }
}
