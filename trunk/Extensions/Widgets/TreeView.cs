/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
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
using System.Collections.Generic;

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:TreeView runat=\"server\"></{0}:TreeView>")]
    public class TreeView : RaWebControl, ASP.INamingContainer
    {
        [Browsable(false)]
        public IEnumerable<TreeViewItem> Items
        {
            get
            {
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is TreeViewItem)
                        yield return idx as TreeViewItem;
                }
            }
        }

        protected override string GetOpeningHTML()
        {
            return string.Format("<ul id=\"{0}\"{1}{2}>",
                ClientID,
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute());
        }

        protected override string GetClosingHTML()
        {
            return "</ul>";
        }
    }
}
