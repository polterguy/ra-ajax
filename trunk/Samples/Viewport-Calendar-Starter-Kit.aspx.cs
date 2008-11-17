﻿/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using Ra.Extensions;

namespace Samples
{
    public partial class CalendarStarter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calendarStart.Value = DateTime.Now.Date;
                calendarEnd.Value = DateTime.Now.Date;
            }
        }

        protected void menu_MenuItemSelected(object sender, EventArgs e)
        {
        }

        protected void resizer_Resized(object sender, ResizeHandler.ResizedEventArgs e)
        {
            int width = Math.Max(e.Width - 359, 400);
            int height = Math.Max(e.Height - 101, 200);
            int heightLeft = Math.Max(e.Height - 260, 50);
            pnlBottomLeft.Style["height"] = heightLeft.ToString() + "px";
            wndRight.Style["width"] = width.ToString() + "px";
            pnlRight.Style["height"] = height.ToString() + "px";
        }
    }
}