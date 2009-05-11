﻿/*
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the the MIT license which 
 * means you can use it (almost) exactly as you wish in your 
 * own projects and copy and paste as much as you want.
 */

using System;
using Ra.Extensions;

public partial class Docs_Controls_BehaviorDraggable : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        dragger.Bounds = new System.Drawing.Rectangle(0, 0, 450, 250);
    }
}