/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using Ra.Extensions;

public partial class Docs_Controls_BehaviorDraggable : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        dragger.Bounds = new System.Drawing.Rectangle(0, 0, 450, 250);
    }

    protected void toggleBehavior_Click(object sender, EventArgs e)
    {
        dragger.Enabled = !dragger.Enabled;
        toggleBehavior.Text = string.Format(
            "{0} Behavior", dragger.Enabled ? "Disable" : "Enable");
    }
}
