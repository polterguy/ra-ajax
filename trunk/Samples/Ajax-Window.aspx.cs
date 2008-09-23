/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

public partial class AjaxWindow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            window.SurfaceControl.Style["width"] = "400px";
            window.SurfaceControl.Style["height"] = "250px";
            showWindow.Enabled = false;
        }
    }

    protected void window_Closed(object sender, EventArgs e)
    {
        showWindow.Enabled = true;
    }

    protected void showWindow_Click(object sender, EventArgs e)
    {
        showWindow.Enabled = false;
        window.Visible = true;
        Effect effect = new EffectFadeIn(window, 400);
        effect.Render();
    }

    protected void animate_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectHighlight(window.SurfaceControl, 400);
        if (window.SurfaceControl.Style["height"] == "400px")
            effect.Chained.Add(new EffectSize(250, 400));
        else
            effect.Chained.Add(new EffectSize(400, 550));
        effect.Render();
    }
}
