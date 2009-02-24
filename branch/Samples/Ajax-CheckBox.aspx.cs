/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace Samples
{
    public partial class AjaxCheckBox : System.Web.UI.Page
    {
        protected void checkedchanged(object sender, EventArgs e)
        {
            (sender as CheckBox).Text = "checked changed " + (sender as CheckBox).Checked;
            new EffectHighlight(sender as CheckBox, 500)
                .Render();
        }
    }
}
