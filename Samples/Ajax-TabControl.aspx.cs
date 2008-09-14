/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

public partial class AjaxTabControl : System.Web.UI.Page
{
	protected void tabBtn_Click(object sender, EventArgs e)
	{
		tabBtn.Text = "Clicked";
	}
}
