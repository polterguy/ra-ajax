/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using Ra.Builder;
using Ra.Effects;

public partial class _Default : System.Web.UI.Page
{
    protected void acc_ActiveAccordionViewChanged(object sender, EventArgs e)
    {
        acc1.Caption = "Hello";
        acc2.Caption = "...world...!";
    }
}
