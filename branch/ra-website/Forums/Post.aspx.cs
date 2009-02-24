/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Entity;
using NHibernate.Expression;
using Ra.Widgets;

namespace RaWebsite
{
    public partial class Forums_Post : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieving post...
                string idOfPost = Request.Params["id"] + ".forum";
                ForumPost post = ForumPost.FindOne(Expression.Eq("Url", idOfPost), Expression.Eq("ParentPost", 0));
                if (post == null)
                    Response.Redirect("Forums.aspx", true);
                headerParent.InnerHtml = post.Header;
                dateParent.InnerHtml = post.Created.ToString("d.MMM yy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                contentParent.InnerHtml = post.Body;
                signatureParent.InnerHtml = post.Operator.Signature;
                operatorInfo.InnerHtml =
                    string.Format("Posted by {0} who has {1} posts",
                        post.Operator.Username,
                        post.Operator.NumberOfPosts);
                this.Title = post.Header;
                header.Text = "Re: " + post.Header;

                // Binding replies...
                DataBindReplies(post);
                if (Operator.Current != null)
                {
                    pnlReply.Visible = true;
                }
                else
                {
                    pnlReply.Visible = false;
                }
            }
        }

        private void DataBindReplies(ForumPost post)
        {
            repReplies.DataSource = ForumPost.FindAll(Expression.Eq("ParentPost", post.Id));
            repReplies.DataBind();
        }

        protected void newSubmit_Click(object sender, EventArgs e)
        {
            // Simple valdation
            if (header.Text.Length < 5)
                return;

            // Creating new post
            ForumPost post = new ForumPost();
            post.Body = body.Text;
            post.Created = DateTime.Now;
            post.Header = header.Text;
            post.Operator = Operator.Current;

            string idOfPost = Request.Params["id"] + ".forum";
            ForumPost parent = ForumPost.FindOne(Expression.Eq("Url", idOfPost));
            post.ParentPost = parent.Id;

            post.Save();

            // Flashing panel
            Effect effect = new EffectFadeIn(pnlReply, 400);
            effect.Render();

            effect = new EffectHighlight(postsWrapper, 400);
            effect.Render();
            body.Text = "";

            // Re-rendering posts to get the newly added item
            DataBindReplies(parent);
            postsWrapper.ReRender();
        }
    }
}
