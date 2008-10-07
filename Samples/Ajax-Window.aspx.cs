/*
 * Ra-Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace Samples
{
    public partial class AjaxWindow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

        protected void btnOpen_Click(object sender, EventArgs e)
        {
            window2.Visible = true;
            Effect effect = new EffectFadeIn(window2, 300);
            effect.Render();
        }

        protected void animate_Click(object sender, EventArgs e)
        {
            // Notes to users!
            // Because of IE's broken box model we need to animate the height 
            // on the "SurfaceControl" (content of Window) and the width on
            // the Window itself.
            // IE won't display the Window correct (*sigh*) if it doesn't
            // have an explicit width...

            // First we're resizing the Window
            Effect effect;
            if (window.Style["width"] == "550px")
                effect = new EffectSize(window, 300, -1, 400);
            else
                effect = new EffectSize(window, 300, -1, 550);
            effect.Render();

            // Then we're resizing the SurfaceControl
            effect = new EffectHighlight(window.SurfaceControl, 300);

            // Then we're "flashing" the Surface Control (content parts of Window)
            effect.Joined.Add(new EffectSize((window.SurfaceControl.Style["height"] == "400px" ? 300 : 400), -1));
            effect.Render();
        }
    }
}
