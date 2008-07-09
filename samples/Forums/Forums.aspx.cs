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
            string newUserRegistration = Request.Params["idNewUser"];
            if (newUserRegistration != null)
            {
                Operator oper = Operator.FindOne(
                    Expression.Eq("Username", newUserRegistration),
                    Expression.Eq("Confirmed", false));
                if (oper != null)
                {
                    oper.Confirmed = true;
                    oper.Save();
                    pnlConfirmRegistration.Visible = true;
                    newUserWelcome.InnerHtml = string.Format("Welcome as a new user {0}", oper.Username);
                    Operator.Login(oper.Username, oper.Pwd);
                }
            }
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

    protected void register_Click(object sender, EventArgs e)
    {
        if (Operator.Login(username.Text, pwd.Text))
        {
            pnlLogin.Visible = false;
            pnlLoggedIn.Visible = true;
            usernameLoggedIn.Text = Operator.Current.Username;
        }
        else
        {
            pnlRegister.Visible = true;
            pnlLogin.Visible = false;
            newUsername.Text = username.Text;
            newUsername.Focus();
            newUsername.Select();
            newPassword.Text = pwd.Text;
            newPasswordRepeat.Text = pwd.Text;
        }
    }

    protected void finishRegister_Click(object sender, EventArgs e)
    {
        if (newPassword.Text != newPasswordRepeat.Text)
        {
            newPassword.Text = "";
            newPasswordRepeat.Text = "";
            return;
        }
        else
        {
            Operator oper = new Operator();
            oper.Email = newEmail.Text;
            oper.Pwd = newPassword.Text;
            oper.Username = newUsername.Text;
            oper.Save();
            oper.SendEmail(
                "Please confirm registration", 
                string.Format(
@"This message was automatically sent from the forums at http://ra-ajax.org due to registering a new user.
If you where not the one registering at Ra-Ajax then please just ignore this message or delete it.

To confirm your registration and activate your user account please go to;
{0}?idNewUser={1}

Have a nice day :)", 
                Request.Url.ToString(),
                oper.Username));
            pnlRegister.Visible = false;
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



























