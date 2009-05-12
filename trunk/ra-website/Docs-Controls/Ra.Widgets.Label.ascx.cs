/*
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the the MIT license which 
 * means you can use it (almost) exactly as you wish in your 
 * own projects and copy and paste as much as you want.
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
