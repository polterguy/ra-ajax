﻿/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;
using Ra.Extensions;

public partial class Docs_Controls_Accordion : System.Web.UI.UserControl
{
    protected void acc_ActiveAccordionViewChanged(object sender, EventArgs e)
    {
        lbl.Text = acc.Views[acc.ActiveAccordionViewIndex].Caption;
    }
}
