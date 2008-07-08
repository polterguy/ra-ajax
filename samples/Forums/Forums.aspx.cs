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

public partial class Forums_Forums : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBindForumPosts();
        }

        // Checking to see if user is logged in
        if (Operator.Current == null)
        {
            pnlLogin.Visible = true;
            pnlLoggedIn.Visible = false;
            if (!IsPostBack)
                username.Focus();
        }
        else
        {
            pnlLogin.Visible = false;
            pnlLoggedIn.Visible = true;
            usernameLoggedIn.Text = Operator.Current.Username;
        }
    }

    private void DataBindForumPosts()
    {
        List<ForumPost> postsToBind = new List<ForumPost>();
        int idxNo = 0;
        foreach (ForumPost idx in ForumPost.FindAll(Order.Desc("Created"), Expression.Eq("ParentPost", 0)))
        {
            if (idxNo > 10)
                break;
            postsToBind.Add(idx);
            idxNo += 1;
        }
        forumPostsRepeater.DataSource = postsToBind;
        forumPostsRepeater.DataBind();
    }

    protected void login_Click(object sender, EventArgs e)
    {
        if (Operator.Login(username.Text, pwd.Text))
        {
            pnlLogin.Visible = false;
            pnlLoggedIn.Visible = true;
            usernameLoggedIn.Text = Operator.Current.Username;
        }
    }

    protected void createNewPost_Click(object sender, EventArgs e)
    {
        pnlNewPost.Visible = true;
        Effect effect = new EffectFadeIn(pnlNewPost, 0.4M);
        effect.Render();
        header.Focus();
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
        post.Save();

        // Removing panel
        Effect effect = new EffectFadeOut(pnlNewPost, 0.4M);
        effect.Render();

        // Re-rendering posts to get the newly added item
        DataBindForumPosts();
        postsWrapper.SignalizeReRender();
    }
}



























