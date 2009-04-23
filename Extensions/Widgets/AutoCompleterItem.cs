/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

namespace Ra.Extensions
{
    /**
     * AutoCompleterItem, item control type for AutoCompleter Widget. When the RetrieveAutoCompleterItems
     * of the AutoCompleter widget is raised then this is the type of widget which is expected by
     * you to append into the Control collection of the AutoCompleter. This is basically just a normal
     * Label control which has overridden the Tag property to render as "li" (list elements) instead
     * of as "span" elements.
     */
    public class AutoCompleterItem : Label, ASP.INamingContainer
    {
        protected override void OnInit(EventArgs e)
        {
            Tag = "li";
            base.OnInit(e);
        }
    }
}
