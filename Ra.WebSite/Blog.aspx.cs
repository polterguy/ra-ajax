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
using Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using Ra.Widgets;
using System.IO;
using System.Xml;
using Ra.Effects;

namespace RaWebsite
{
    public partial class Blog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieving blogger username
                string bloggerUserName = Request.Params["blogger"];
                if (string.IsNullOrEmpty(bloggerUserName))
                    Response.Redirect("~", true);
                Operator oper = Operator.FindOne(Expression.Eq("Username", bloggerUserName));
                if (oper == null)
                    Response.Redirect("~", true);

                if (Request.Params["rss"] == "true")
                    CreateRSS(oper);

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

                CurrentBlogPage = 0;
                DataBindBlogs(CurrentBlogPage);
            }
        }

        private void CreateRSS(Operator oper)
        {
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
            int idxNoBlog = 0;
            foreach (Entity.Blog idx in Blogs)
            {
                if (idxNoBlog >= 10)
                    break;
                idxNoBlog += 1;

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

        private List<Entity.Blog> Blogs
        {
            get 
            {
                List<Entity.Blog> blogs = new List<Entity.Blog>();
                Operator oper = Operator.FindOne(Expression.Eq("Username", Request.Params["blogger"]));
                foreach (Entity.Blog idx in Entity.Blog.FindAll(Order.Desc("Created"), Expression.Eq("Operator", oper)))
                {
                    blogs.Add(idx);
                }
                return blogs;
            }
        }

        private int CurrentBlogPage
        {
            get 
            {
                if (Session["CurrentBlogPage"] != null)
                    return (int)Session["CurrentBlogPage"];
                return 0;
            }
            set 
            {
                Session["CurrentBlogPage"] = value;
            }
        }

        private void DataBindBlogs(int page)
        {
            System.Web.UI.WebControls.PagedDataSource pagedBlogs = new System.Web.UI.WebControls.PagedDataSource();
            pagedBlogs.AllowPaging = true;
            pagedBlogs.DataSource = Blogs;
            pagedBlogs.PageSize = 5;
            pagedBlogs.CurrentPageIndex = page;
            
            repBlogs.DataSource = pagedBlogs;
            repBlogs.DataBind();
            
            newerPosts.Visible = !pagedBlogs.IsFirstPage;
            olderPosts.Visible = !pagedBlogs.IsLastPage;
        }

        protected void newerPosts_Click(object sender, EventArgs e)
        {
            DataBindBlogs(--CurrentBlogPage);
            blogWrapper.ReRender();
        }

        protected void olderPosts_Click(object sender, EventArgs e)
        {
            DataBindBlogs(++CurrentBlogPage);
            blogWrapper.ReRender();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            pnlNewBlog.Visible = true;
            pnlNewBlog.Style["display"] = "";
            txtHeader.Focus();
            txtHeader.Select();
            new EffectFadeIn(pnlNewBlog, 400).Render();
            hidBlogId.Value = "";
        }

        protected void btnImages_Click(object sender, EventArgs e)
        {
            pnlImages.Visible = true;
            new EffectFadeIn(pnlImages, 400).Render();

            DataBindImages();
            pnlImages.ReRender();
        }

        private void DataBindImages()
        {
            // Databinding images
            string[] files = Directory.GetFiles(Server.MapPath("~/media/UserImages"));

            Array.Sort(files, delegate(string left, string right) {
                return File.GetCreationTime(right).CompareTo(File.GetCreationTime(left));
            });

            for (int idx = 0; idx < 20 && idx < files.Length; idx++)
            {
                files[idx] = files[idx]
                    .Replace(
                        Server.MapPath("~"), 
                        Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf("/") + 1))
                    .Replace("\\", "/");
            }
            repImages.DataSource = files;
            repImages.DataBind();
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            uploadImage.SaveAs(Server.MapPath("~/media/UserImages/" + uploadImage.FileName));
            DataBindImages();
            pnlImages.ReRender();
        }

        protected void EditBlog(object sender, EventArgs e)
        {
            pnlNewBlog.Visible = true;
            pnlNewBlog.Style["display"] = "";
            txtHeader.Focus();
            txtHeader.Select();
            new EffectFadeIn(pnlNewBlog, 400).Render();

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
            Session["blogIdToDelete"] = Int32.Parse(hid.Value);
            confirmDeleteWindow.Visible = true;
        }

        private void DeleteBlogPost(int blogId)
        {
            if (blogId <= -1)
                return;
            Entity.Blog blog = Entity.Blog.FindOne(Expression.Eq("Id", blogId));
            blog.Delete();

            // Re-render blogs
            string bloggerUserName = Request.Params["blogger"];
            if (string.IsNullOrEmpty(bloggerUserName))
                Response.Redirect("~", true);
            Operator oper = Operator.FindOne(Expression.Eq("Username", bloggerUserName));
            if (oper == null)
                Response.Redirect("~", true);
            DataBindBlogs(CurrentBlogPage);
            blogWrapper.ReRender();
            new EffectFadeIn(blogWrapper, 400).Render();
        }

        protected void cancelDeleteButton_Click(object sender, EventArgs e)
        {
            confirmDeleteWindow.Visible = false;
        }

        protected void confirmDeleteButton_Click(object sender, EventArgs e)
        {
            DeleteBlogPost((int)Session["blogIdToDelete"]);
            Session["blogIdToDelete"] = -1;
            confirmDeleteWindow.Visible = false;
        }

        protected void btnCancelSave_Click(object sender, EventArgs e)
        {
            new EffectFadeOut(pnlNewBlog, 400).Render();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string bloggerUserName = Request.Params["blogger"];
            if (string.IsNullOrEmpty(bloggerUserName))
                Response.Redirect("~", true);
            Operator oper = Operator.FindOne(Expression.Eq("Username", bloggerUserName));
            if (oper == null)
                Response.Redirect("~", true);

            Entity.Blog blog = null;
            if (hidBlogId.Value == "")
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

            CurrentBlogPage = 0;
            DataBindBlogs(CurrentBlogPage);
            blogWrapper.ReRender();
            
            new EffectFadeOut(pnlNewBlog, 400).ChainThese(
                new EffectFadeIn(blogWrapper, 400)
            ).Render();
        }
    }
}






























