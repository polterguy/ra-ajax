/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace Samples
{
    public partial class Flexible : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void pnlMouse_MouseOver(object sender, EventArgs e)
        {
            lblResult.Text = "Mouse over";
            lblResult.Style["color"] = "Green";
            pnlMouse.Style["background-color"] = "#bfb";
        }

        protected void pnlMouse_MouseOut(object sender, EventArgs e)
        {
            lblResult.Text = "Mouse out";
            lblResult.Style["color"] = "Red";
            pnlMouse.Style["background-color"] = "#fbb";
        }

        protected void pnlMouse2_MouseOver(object sender, EventArgs e)
        {
            lblResult2.Text = "Mouse over";
            lblResult2.Style["color"] = "Green";
            pnlMouse2.Style["background-color"] = "#afa";
        }

        protected void pnlMouse2_MouseOut(object sender, EventArgs e)
        {
            lblResult2.Text = "Mouse out";
            lblResult2.Style["color"] = "Red";
            pnlMouse2.Style["background-color"] = "#faa";
        }

        protected void btn_MouseOver(object sender, EventArgs e)
        {
            btn.Text = "Mouse Over";
            btn.Style["color"] = "Green";
        }

        protected void btn_MouseOut(object sender, EventArgs e)
        {
            btn.Text = "Mouse Out";
            btn.Style["color"] = "Red";
        }

        protected void txt_MouseOut(object sender, EventArgs e)
        {
            pnl.Visible = false;
        }

        protected void txt_MouseOver(object sender, EventArgs e)
        {
            pnl.Visible = true;
        }
    }
}
