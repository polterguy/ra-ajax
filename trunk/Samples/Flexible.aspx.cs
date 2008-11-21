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
        }

        protected void pnlMouse_MouseOut(object sender, EventArgs e)
        {
            lblResult.Text = "Mouse out";
        }

        protected void pnlMouse2_MouseOver(object sender, EventArgs e)
        {
            lblResult2.Text = "Mouse over";
        }

        protected void pnlMouse2_MouseOut(object sender, EventArgs e)
        {
            lblResult2.Text = "Mouse out";
        }

        protected void btn_MouseOver(object sender, EventArgs e)
        {
            btn.Text = "Mouse Over";
        }

        protected void btn_MouseOut(object sender, EventArgs e)
        {
            btn.Text = "Mouse Out";
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
