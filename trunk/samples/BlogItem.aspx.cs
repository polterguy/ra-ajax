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
            string blogId = Request.Params["id"];
            if (string.IsNullOrEmpty(blogId))
                Response.Redirect("Default.aspx", true);
            Entity.Blog blog = Entity.Blog.FindOne(Expression.Eq("Url", blogId + ".blog"));
            if (blog == null)
                Response.Redirect("Default.aspx", true);
            header.InnerHtml = blog.Header;
            body.InnerHtml = blog.Body;
            date.InnerHtml = blog.Created.ToString("dd.MMM yy");
            Title = blog.Header;
        }
    }
}
