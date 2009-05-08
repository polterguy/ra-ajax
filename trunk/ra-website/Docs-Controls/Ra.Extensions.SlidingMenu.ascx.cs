/*
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the the MIT license which 
 * means you can use it (almost) exactly as you wish in your 
 * own projects and copy and paste as much as you want.
 */

using System;
using Ra.Extensions;

public partial class Docs_Controls_SlidingMenu : System.Web.UI.UserControl
{
    protected void window_GetChildNodes(object sender, EventArgs e)
    {
        SlidingMenuLevel level = sender as SlidingMenuLevel;
        for (int idx = 0; idx < 5; idx++)
        {
            SlidingMenuItem i = new SlidingMenuItem();
            i.ID = level.ID + idx;
            i.Text = "Window " + idx;
            level.Controls.Add(i);

            SlidingMenuLevel l = new SlidingMenuLevel();
            l.Caption = "Window " + idx;
            l.ID = level.ID + "LL" + idx;
            l.GetChildNodes += window_GetChildNodes;
            i.Controls.Add(l);
        }
    }

    protected void slider_Navigate(object sender, EventArgs e)
    {
        lbl.Text = slider.ActiveLevel == null ? "null" : slider.ActiveLevel;
    }

    protected void slider_ItemClicked(object sender, EventArgs e)
    {
        SlidingMenuItem item = sender as SlidingMenuItem;
        lbl.Text = item.ID;
    }
}
