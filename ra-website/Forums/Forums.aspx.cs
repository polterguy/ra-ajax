/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using Ra.Widgets;
using System.Web;

namespace RaWebsite
{
    public partial class Forums_Forums : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Databinding forum posts
                DataBindForumPosts();

                // Retrieving information about the forums
                informationLabel.Text = string.Format(informationLabel.Text, 
                    Operator.GetCount(), ForumPost.GetCount(), Operator.ViewersCount);

                if (Operator.Current != null)
                    newPostPanel.Visible = true;
            }
        }

        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                    return 0;
                return (int)ViewState["CurrentPage"];
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        private void DataBindForumPosts()
        {
            List<ForumPost> postsToBind = new List<ForumPost>();
            int idxNo = 0;
            ForumPost[] posts = null;
            if (search.Text.Trim() == "")
                posts = ForumPost.FindAll(Order.Desc("Created"), Expression.Eq("ParentPost", 0));
            else
                posts = ForumPost.FindAll(Order.Desc("Created"),
                    Expression.Eq("ParentPost", 0),
                    Expression.Or(
                        Expression.Like("Header", search.Text.Trim(), MatchMode.Anywhere),
                        Expression.Like("Body", search.Text.Trim(), MatchMode.Anywhere)));
            foreach (ForumPost idx in posts)
            {
                if (idxNo < CurrentPage)
                {
                    idxNo += 1;
                    continue;
                }
                if (idxNo >= CurrentPage + 5)
                    break;
                postsToBind.Add(idx);
                idxNo += 1;
            }
            if (postsToBind.Count <= 5 || postsToBind.Count == 0)
                next.Visible = false;
            else
                next.Visible = true;
            if (CurrentPage == 0)
                previous.Visible = false;
            else
                previous.Visible = true;
            forumPostsRepeater.DataSource = postsToBind;
            forumPostsRepeater.DataBind();
        }

        protected void previous_Click(object sender, EventArgs e)
        {
            CurrentPage = Math.Max(0, CurrentPage - 5);
            DataBindForumPosts();
            postsWrapper.ReRender();
        }

        protected void next_Click(object sender, EventArgs e)
        {
            CurrentPage += 5;
            DataBindForumPosts();
            postsWrapper.ReRender();
        }

        protected void addNewPostButton_Click(object sender, EventArgs e)
        {
            lblErrorPost.Text = "";
            pnlNewPost.Visible = true;
            new EffectFadeIn(pnlNewPost, 400).Render();
            pnlNewPost.Style["display"] = "";
            header.Focus();
            header.Select();
        }

        protected void newPostCancel_Click(object sender, EventArgs e)
        {
            // Removing panel
            pnlNewPost.Visible = false;
        }

        protected void search_KeyUp(object sender, EventArgs e)
        {
            CurrentPage = 0;
            DataBindForumPosts();
            postsWrapper.ReRender();
        }

        protected void newSubmit_Click(object sender, EventArgs e)
        {
            // Simple valdation
            if (header.Text.Length < 5)
            {
                lblErrorPost.Text = "Header of post is too short";
                Effect effect2 = new EffectFadeIn(lblErrorPost, 400);
                effect2.Render();
                return;
            }

            // Creating new post
            ForumPost post = new ForumPost();
            post.Body = body.Text;
            post.Created = DateTime.Now;
            post.Header = header.Text;
            post.Operator = Operator.Current;
            post.Save();

            // Removing panel
            pnlNewPost.Visible = false;

            // Re-rendering posts to get the newly added item
            DataBindForumPosts();
            postsWrapper.ReRender();
        }
    }
}



























