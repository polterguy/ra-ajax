/*
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
using Ra.Widgets;
using Ra.Extensions;

namespace Samples
{
    public partial class RSSSample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RSSDatabase.Database.Count == 0)
            {
                RSSDatabase.MakeSureOneExists();
            }
            rep.DataSource = RSSDatabase.Database[0].Items;
            rep.DataBind();
        }

        protected void ShowItems(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            Guid id = new Guid(btn.Xtra);
            wndBlog.Visible = true;
            RSSItem item = RSSDatabase.Database[0].Items.Find(
                delegate(RSSItem idx)
                {
                    return idx.Id == id;
                });
            wndBlog.Caption = item.Header + " - <i>" + item.Date.ToString("dddd dd MMM yy") + "</i>";
            header.Text = item.Header;
            content.Text = item.Body;
        }
    }
}
