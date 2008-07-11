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
using System.Xml;

public partial class Blog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Retrieving blogger username
            string bloggerUserName = Request.Params["blogger"];
            if (string.IsNullOrEmpty(bloggerUserName))
                Response.Redirect("Default.aspx", true);
            Operator oper = Operator.FindOne(Expression.Eq("Username", bloggerUserName));
            if (oper == null)
                Response.Redirect("Default.aspx", true);

            if (Request.Params["rss"] == "true")
            {
                // Retrieving last 50 blogs for blogger
                List<Entity.Blog> blogs = new List<Entity.Blog>();
                int idxNo = 0;
                foreach (Entity.Blog idx in Entity.Blog.FindAll(Order.Desc("Created"), Expression.Eq("Operator", oper)))
                {
                    if (idxNo++ > 50)
                        break;
                    blogs.Add(idx);
                }

                // Spitting out RSS back to client...
                //Response.Clear();
                Response.ContentType = "text/xml";

                // Creating RSS
                XmlDocument doc = new XmlDocument();

                // RSS node
                XmlNode rss = doc.CreateNode(XmlNodeType.Element, "rss", "");
                XmlAttribute version = doc.CreateAttribute("version");
                version.Value = "2.0";
                rss.Attributes.Append(version);
                doc.AppendChild(rss);

                // channel node
                XmlNode channel = doc.CreateNode(XmlNodeType.Element, "channel", "");
                rss.AppendChild(channel);

                // Title node
                XmlNode title = doc.CreateNode(XmlNodeType.Element, "title", "");
                XmlNode titleContent = doc.CreateNode(XmlNodeType.Text, null, null);
                titleContent.Value = "Ravings from " + oper.Username;
                title.AppendChild(titleContent);
                channel.AppendChild(title);

                // link node
                XmlNode link = doc.CreateNode(XmlNodeType.Element, "link", "");
                XmlNode linkContent = doc.CreateNode(XmlNodeType.Text, "", "");
                linkContent.Value = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf("?"));
                link.AppendChild(linkContent);
                channel.AppendChild(link);

                // description
                XmlNode description = doc.CreateNode(XmlNodeType.Element, "description", "");
                XmlNode descriptionContent = doc.CreateNode(XmlNodeType.Text, "", "");
                descriptionContent.Value = "Far side software ravings from " + oper.Username;
                description.AppendChild(descriptionContent);
                channel.AppendChild(description);

                // generator
                XmlNode generator = doc.CreateNode(XmlNodeType.Element, "generator", "");
                XmlNode generatorContent = doc.CreateNode(XmlNodeType.Text, "", "");
                generatorContent.Value = "Custom made RSS creator";
                generator.AppendChild(generatorContent);
                channel.AppendChild(generator);

                // Items
                foreach (Entity.Blog idx in blogs)
                {
                    // Item
                    XmlNode item = doc.CreateNode(XmlNodeType.Element, "item", "");

                    // Title
                    XmlNode titleR = doc.CreateNode(XmlNodeType.Element, "title", "");
                    XmlNode titleRContent = doc.CreateNode(XmlNodeType.Text, "", "");
                    titleRContent.Value = idx.Header;
                    titleR.AppendChild(titleRContent);
                    item.AppendChild(titleR);

                    // link
                    XmlNode linkR = doc.CreateNode(XmlNodeType.Element, "link", "");
                    XmlNode linkRContent = doc.CreateNode(XmlNodeType.Text, "", "");
                    linkRContent.Value = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.LastIndexOf("/") + 1) + idx.Url;
                    linkR.AppendChild(linkRContent);
                    item.AppendChild(linkR);

                    // description
                    XmlNode descriptionR = doc.CreateNode(XmlNodeType.Element, "description", "");
                    XmlNode descriptionRContent = doc.CreateNode(XmlNodeType.Text, "", "");
                    descriptionRContent.Value = idx.Body;
                    descriptionR.AppendChild(descriptionRContent);
                    item.AppendChild(descriptionR);

                    // pubDate
                    XmlNode pub = doc.CreateNode(XmlNodeType.Element, "pubDate", "");
                    XmlNode pubContent = doc.CreateNode(XmlNodeType.Text, "", "");
                    pubContent.Value = idx.Created.ToString("ddd, dd MMM yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    pub.AppendChild(pubContent);
                    item.AppendChild(pub);

                    channel.AppendChild(item);
                }

                // Saving to response
                doc.Save(Response.OutputStream);
                try
                {
                    Response.End();
                }
                catch
                {
                    return;
                }
            }

            // To support File Uploading...
            Form.Enctype = "multipart/form-data";

            // To support RSS
            string headerRssLink = 
                string.Format(@"
<link 
    rel=""Alternate"" 
    type=""application/rss+xml"" 
    href=""{0}?rss=true"" 
    title=""Ravings from {1}"" />", 
                Request.Url.AbsolutePath,
                oper.Username);
            System.Web.UI.WebControls.Literal litRss = new System.Web.UI.WebControls.Literal();
            litRss.Text = headerRssLink;
            Header.Controls.Add(litRss);

            btnCreate.Visible = Operator.Current != null && oper.Id == Operator.Current.Id;

            Title = "Blog of " + oper.Username;

            DataBindBlogs(oper);
        }
    }

    private void DataBindBlogs(Operator oper)
    {
        List<Entity.Blog> blogs = new List<Entity.Blog>();
        int idxNo = 0;
        foreach (Entity.Blog idx in Entity.Blog.FindAll(Order.Desc("Created"), Expression.Eq("Operator", oper)))
        {
            if (idxNo++ > 50)
                break;
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
        pnlImages.SignalizeReRender();
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
        txtBody.Text = blog.Body.Replace("<br />", "\r\n");
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






























