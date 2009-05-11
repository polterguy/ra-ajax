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

public partial class Docs_Controls_BehaviorUpdater : System.Web.UI.UserControl
{
    protected void sel_SelectedIndexChanged(object semder, EventArgs e)
    {
        if (sel.SelectedItem.Value != "nothing")
            dyn.LoadControls(sel.SelectedItem.Value);
    }

    protected void dyn_Reload(object sender, Dynamic.ReloadEventArgs e)
    {
        string fileName = "~/Docs-Controls/" + e.Key;
        System.Web.UI.Control ctrl = Page.LoadControl(fileName);
        dyn.Controls.Add(ctrl);
    }
}
