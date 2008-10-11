/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using ASPCTRLS = System.Web.UI;
using Ra.Widgets;
using Ra.Extensions;
using System.Drawing;

namespace Samples
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void menu_MenuItemSelected(object sender, EventArgs e)
        {
            lbl.Text = (sender as MenuItem).ID + " was selected";
            new EffectHighlight(lbl, 200).Render();
        }
    }
}
