/*
 * Ra-Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using System.Threading;

namespace Samples
{
    public partial class AjaxPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt.Focus();
                txt.Select();
            }
        }

        protected void lnk_Click(object sender, EventArgs e)
        {
            pnl.Style["background-color"] = pnl.Style["background-color"] == "Yellow" ? "Orange" : "Yellow";
        }

        protected void txt_KeyUp(object sender, EventArgs e)
        {
            string color = "";
            switch (txt.Text.Length % 5)
            {
                case 0:
                    color = "#000";
                    break;
                case 1:
                    color = "#0e0";
                    break;
                case 2:
                    color = "#e00";
                    break;
                case 3:
                    color = "#00e";
                    break;
                case 4:
                    color = "#e0e";
                    break;
            }
            pnl.Style["color"] = color;
            pnl.Style["background-color"] = "White";
        }
    }
}
