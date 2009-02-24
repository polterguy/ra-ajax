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
    public partial class AjaxImageButton : System.Web.UI.Page
    {
        protected void imgBtn_Click(object sender, EventArgs e)
        {
            lblResults.Text = "You clicked the Image Button, notice how we changed the AlternateText (alt) on the ImageButton";
            imgBtn.AlternateText = "This buttons has been clicked";
        }
    }
}
