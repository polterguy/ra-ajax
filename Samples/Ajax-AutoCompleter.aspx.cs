/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using Ra.Extensions;

namespace Samples
{
    public partial class AjaxAutoCompleter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                auto.Focus();
            }
        }

        protected void auto_RetrieveAutoCompleterItems(object sender, AutoCompleter.RetrieveAutoCompleterItemsEventArgs e)
        {
            // Checking to see if we're getting an "empty" query...
            if (e.Query.Length == 0)
                return;

            // Creating some "random" results...
            for (int idx = 0; idx < Math.Max(2, 20 - e.Query.Length); idx++)
            {
                AutoCompleterItem item = new AutoCompleterItem();
                item.Text = string.Format(e.Query + "-Item-" + idx);
                item.CssClass = auto.CssClass + "-item";
                e.Controls.Add(item);
            }
        }
    }
}
