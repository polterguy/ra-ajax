﻿/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Extensions;
using System.Threading;
using Ra.Widgets;

public partial class Docs_Controls_Label : System.Web.UI.UserControl
{
    protected void MouseOver(object sender, EventArgs e)
    {
        lbl1.Text = "Welcome :)";
    }

    protected void MouseOut(object sender, EventArgs e)
    {
        lbl1.Text = "Goodbye... :(";
    }
}
