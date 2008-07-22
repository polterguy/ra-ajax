/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Entity;
using NHibernate.Expression;

public partial class BlogItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Finding "this" blog
            string blogId = Request.Params["id"];
            if (string.IsNullOrEmpty(blogId))
                Response.Redirect("~", true);
            Entity.Blog blog = Entity.Blog.FindOne(Expression.Eq("Url", blogId + ".blog"));
            if (blog == null)
                Response.Redirect("~", true);
            header.InnerHtml = blog.Header;
            body.InnerHtml = blog.Body;
            date.InnerHtml = blog.Created.ToString("MMMM dd, yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Title = blog.Header;

            // To support RSS
            string headerRssLink =
                string.Format(@"
<link 
    rel=""Alternate"" 
    type=""application/rss+xml"" 
    href=""{0}?rss=true"" 
    title=""Ravings from {1}"" />",
                Request.Url.AbsolutePath.Substring(0, Request.Url.AbsolutePath.LastIndexOf("/") + 1) + blog.Operator.Username + ".blogger",
                blog.Operator.Username);
            System.Web.UI.WebControls.Literal litRss = new System.Web.UI.WebControls.Literal();
            litRss.Text = headerRssLink;
            Header.Controls.Add(litRss);

            // Finding previous blog
            Entity.Blog previousBlog = Entity.Blog.FindFirst(Order.Desc("Id"), Expression.Lt("Id", blog.Id));
            if (previousBlog != null)
            {
                previous.HRef = previousBlog.Url;
                previous.InnerHtml += " - " + previousBlog.Header;
            }
            else
                previous.Visible = false;

            // Finding next blog
            Entity.Blog nextBlog = Entity.Blog.FindFirst(Order.Asc("Id"), Expression.Gt("Id", blog.Id));
            if (nextBlog != null)
            {
                next.HRef = nextBlog.Url;
                next.InnerHtml = nextBlog.Header + " - " + next.InnerHtml;
            }
            else
                next.Visible = false;
        }
    }
}
