/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Drawing;
using Ra.Widgets;

public partial class AjaxUpdater : System.Web.UI.Page
{
	protected void btn_Click(object sender, EventArgs e)
	{
		System.Threading.Thread.Sleep(2000);
		lbl.Text = "Note though that there was an intentional delay of 2 seconds on "+
			"the server when the request was being sent to make sure you would notice "+
			"the Ajax Updater"; 
	}
}
