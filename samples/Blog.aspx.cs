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
using System.IO;

public partial class Blog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // To support File Uploading...
            Form.Enctype = "multipart/form-data";

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
        pnlNewBlog.Style["display"] = "";
        txtHeader.Focus();
        txtHeader.Select();
        Effect effect = new EffectFadeIn(pnlNewBlog, 0.4M);
        effect.Render();
        hidBlogId.Value = "";
    }

    protected void btnImages_Click(object sender, EventArgs e)
    {
        pnlImages.Visible = true;
        Effect effect = new EffectFadeIn(pnlImages, 0.4M);
        effect.Render();

        DataBindImages();
    }

    private void DataBindImages()
    {
        // Databinding images
        string[] files = Directory.GetFiles(Server.MapPath("~/media/UserImages"));
        for (int idx = 0; idx < files.Length; idx++)
        {
            files[idx] = files[idx].Replace(Server.MapPath("~"), Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf("/"))).Replace("\\", "/");
        }
        repImages.DataSource = files;
        repImages.DataBind();
    }

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        uploadImage.SaveAs(Server.MapPath("~/media/UserImages/" + uploadImage.FileName));
        DataBindImages();
        pnlImages.SignalizeReRender();
    }

    protected void btnImagesClose_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectFadeOut(pnlImages, 0.4M);
        effect.Render();
        txtHeader.Focus();
        txtHeader.Select();
    }

    protected void EditBlog(object sender, EventArgs e)
    {
        pnlNewBlog.Visible = true;
        pnlNewBlog.Style["display"] = "";
        txtHeader.Focus();
        txtHeader.Select();
        Effect effect = new EffectFadeIn(pnlNewBlog, 0.4M);
        effect.Render();

        Ra.Widgets.HiddenField hid = (sender as System.Web.UI.Control).Parent.Controls[1] as Ra.Widgets.HiddenField;
        if (hid == null) // Mono doesn't add a Literal Control if you create space between controls...
            hid = (sender as System.Web.UI.Control).Parent.Controls[0] as Ra.Widgets.HiddenField;
        int blogId = Int32.Parse(hid.Value);
        Entity.Blog blog = Entity.Blog.FindOne(Expression.Eq("Id", blogId));
        txtHeader.Text = blog.Header;
        hidBlogId.Value = blog.Id.ToString();
        txtBody.Text = blog.Body;
    }

    protected void DeleteBlog(object sender, EventArgs e)
    {
        Ra.Widgets.HiddenField hid = (sender as System.Web.UI.Control).Parent.Controls[1] as Ra.Widgets.HiddenField;
        if (hid == null) // Mono doesn't add a Literal Control if you create space between controls...
            hid = (sender as System.Web.UI.Control).Parent.Controls[0] as Ra.Widgets.HiddenField;
        int blogId = Int32.Parse(hid.Value);
        Entity.Blog blog = Entity.Blog.FindOne(Expression.Eq("Id", blogId));
        blog.Delete();

        // Re-render blogs
        string bloggerUserName = Request.Params["blogger"];
        if (string.IsNullOrEmpty(bloggerUserName))
            Response.Redirect("Default.aspx", true);
        Operator oper = Operator.FindOne(Expression.Eq("Username", bloggerUserName));
        if (oper == null)
            Response.Redirect("Default.aspx", true);
        DataBindBlogs(oper);
        blogWrapper.SignalizeReRender();
        Effect effect = new EffectFadeIn(blogWrapper, 0.4M);
        effect.Render();
    }

    protected void btnCancelSave_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectFadeOut(pnlNewBlog, 0.4M);
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

        Entity.Blog blog = null;
        if( hidBlogId.Value == "")
        {
            blog = new Entity.Blog();
            blog.Created = DateTime.Now;
        }
        else
        {
            blog = Entity.Blog.FindOne(Expression.Eq("Id", Int32.Parse(hidBlogId.Value)));
        }
        blog.Header = txtHeader.Text;
        blog.Body = txtBody.Text;
        blog.Operator = Operator.Current;
        blog.Save();

        DataBindBlogs(oper);
        blogWrapper.SignalizeReRender();
        Effect effect = new EffectFadeIn(blogWrapper, 0.4M);
        effect.Render();

        effect = new EffectFadeOut(pnlNewBlog, 0.4M);
        effect.Render();
    }
}






























