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
using System.Collections.Generic;
using Ra.Widgets;

public partial class Blog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string bloggerUserName = Request.Params["blogger"];
            if (string.IsNullOrEmpty(bloggerUserName))
                Response.Redirect("Default.aspx", true);
            Operator oper = Operator.FindOne(Expression.Eq("Username", bloggerUserName));
            if (oper == null)
                Response.Redirect("Default.aspx", true);

            btnCreate.Visible = Operator.Current != null && oper.Id == Operator.Current.Id;

            header.InnerHtml += oper.Username;
            Title = "Blog of " + oper.Username;

            DataBindBlogs(oper);
        }
    }

    private void DataBindBlogs(Operator oper)
    {
        List<Entity.Blog> blogs = new List<Entity.Blog>();
        foreach (Entity.Blog idx in Entity.Blog.FindAll(Order.Desc("Created"), Expression.Eq("Operator", oper)))
        {
            blogs.Add(idx);
        }
        repBlogs.DataSource = blogs;
        repBlogs.DataBind();
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        pnlNewBlog.Visible = true;
        Effect effect = new EffectFadeIn(pnlNewBlog, 0.4M);
        effect.Render();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string bloggerUserName = Request.Params["blogger"];
        if (string.IsNullOrEmpty(bloggerUserName))
            Response.Redirect("Default.aspx", true);
        Operator oper = Operator.FindOne(Expression.Eq("Username", bloggerUserName));
        if (oper == null)
            Response.Redirect("Default.aspx", true);

        Entity.Blog blog = new Entity.Blog();
        blog.Header = txtHeader.Text;
        blog.Body = txtBody.Text;
        blog.Operator = Operator.Current;
        blog.Created = DateTime.Now;
        blog.Save();

        DataBindBlogs(oper);
        blogWrapper.SignalizeReRender();
        Effect effect = new EffectFadeIn(blogWrapper, 0.4M);
        effect.Render();

        effect = new EffectFadeOut(pnlNewBlog, 0.4M);
        effect.Render();
    }
}






























