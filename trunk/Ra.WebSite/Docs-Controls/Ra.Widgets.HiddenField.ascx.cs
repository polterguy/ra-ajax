/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using Ra.Extensions;
using System.Threading;
using Ra.Widgets;

public partial class Docs_Controls_HiddenField : System.Web.UI.UserControl
{
    protected void btn_Click(object sender, EventArgs e)
    {
        hid.Value = DateTime.Now.ToString();
    }

    protected void btn2_Click(object sender, EventArgs e)
    {
        btn2.Text = hid.Value;
    }
}
