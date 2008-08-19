/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Ra Software AS thomas@ra-ajax.org
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

public partial class Forums_Forums : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Checking to see if this is a "confirm user" request
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

            // Databinding forum posts
            DataBindForumPosts();

            // Retrieving information about database
            informationLabel.Text = 
                    string.Format("{0} registered users have posted {1} posts into these forums. There are currently {2} people online browsing these forums.",
                        Operator.GetCount(),
                        ForumPost.GetCount(),
                        Operator.ViewersCount);
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

    private int Page
    {
        get
        {
            if (ViewState["Page"] == null)
                return 0;
            return (int)ViewState["Page"];
        }
        set
        {
            ViewState["Page"] = value;
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
            if (idxNo < Page)
            {
                idxNo += 1;
                continue;
            }
            if (idxNo >= Page + 5) // We only show the 50 last ones...
                break;
            postsToBind.Add(idx);
            idxNo += 1;
        }
        if (postsToBind.Count == 0)
            next.Visible = false;
        else
            next.Visible = true;
        if (Page == 0)
            previous.Visible = false;
        else
            previous.Visible = true;
        forumPostsRepeater.DataSource = postsToBind;
        forumPostsRepeater.DataBind();
    }

    protected void previous_Click(object sender, EventArgs e)
    {
        Page = Math.Max(0, Page - 5);
        DataBindForumPosts();
        postsWrapper.SignalizeReRender();
    }

    protected void next_Click(object sender, EventArgs e)
    {
        Page += 5;
        DataBindForumPosts();
        postsWrapper.SignalizeReRender();
    }

    protected void login_Click(object sender, EventArgs e)
    {
        if (Operator.Login(username.Text, pwd.Text))
        {
            pnlLogin.Visible = false;
            pnlLoggedIn.Visible = true;
            usernameLoggedIn.Text = Operator.Current.Username;
            if (remember.Checked)
            {
                HttpCookie rem = new HttpCookie("userId", username.Text);
                rem.Expires = DateTime.Now.AddMonths(24);
                Response.Cookies.Add(rem);
            }
        }
        else
        {
            lblError.Text = "Couldn't log you in";
            Effect effect = new EffectFadeIn(lblError, 0.4M);
            username.Focus();
            username.Select();
            effect.Render();
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
            pnlRegister.Style["display"] = "";
            newUsername.Focus();
            newUsername.Select();

            // Fading in/out panels...
            Effect effect = new EffectFadeOut(pnlLogin, 0.6M);
            effect.Render();
            effect = new EffectFadeIn(pnlRegister, 0.6M);
            effect.Render();

            newUsername.Text = username.Text;
            newUsername.Focus();
            newUsername.Select();
            newPassword.Text = pwd.Text;
            newPasswordRepeat.Text = pwd.Text;
        }
    }

    protected void btnCancelRegistration_Click(object sender, EventArgs e)
    {
        pnlLogin.Visible = true;

        // Fading in/out panels...
        Effect effect = new EffectFadeIn(pnlLogin, 0.6M);
        effect.Render();
        effect = new EffectFadeOut(pnlRegister, 0.6M);
        effect.Render();
        pnlLogin.Style["display"] = "";
        username.Focus();
        username.Select();
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

Your username is; {1}
Your password is; {2}

Have a nice day :)", 
                Request.Url.ToString(),
                oper.Username,
                oper.Pwd));
            pnlRegister.Visible = false;
        }
    }

    protected void createNewPost_Click(object sender, EventArgs e)
    {
        lblErrorPost.Text = "";
        pnlNewPost.Visible = true;
        Effect effect = new EffectFadeIn(pnlNewPost, 0.4M);
        effect.Render();
        pnlNewPost.Style["display"] = "";
        header.Focus();
        header.Select();
    }

    protected void btnChangeProfile_Click(object sender, EventArgs e)
    {
        if (changePassword.Text != changePasswordConfirm.Text)
        {
            lblErrorProfile.Text = "Passwords didn't match";
            Effect effect2 = new EffectFadeIn(lblErrorProfile, 0.4M);
            effect2.Render();
            return;
        }
        Effect effect = new EffectFadeOut(pnlProfile, 0.4M);
        effect.Render();

        Operator.Current.Pwd = changePassword.Text;
        Operator.Current.Signature = changeSignature.Text;
        Operator.Current.Email = changeEmail.Text;
        Operator.Current.Save();
    }

    protected void btnCancelSavingProfile_Click(object sender, EventArgs e)
    {
        lblErrorProfile.Text = "";
        Effect effect = new EffectFadeOut(pnlProfile, 0.4M);
        effect.Render();
    }

    protected void profile_Click(object sender, EventArgs e)
    {
        pnlProfile.Visible = true;
        pnlProfile.Style["display"] = "";
        Effect effect = new EffectFadeIn(pnlProfile, 0.4M);
        effect.Render();
        changePassword.Text = Operator.Current.Pwd;
        changePassword.Focus();
        changePassword.Select();
        changePasswordConfirm.Text = Operator.Current.Pwd;
        changeSignature.Text = Operator.Current.Signature;
        changeEmail.Text = Operator.Current.Email;
    }

    protected void newPostCancel_Click(object sender, EventArgs e)
    {
        // Removing panel
        Effect effect = new EffectFadeOut(pnlNewPost, 0.4M);
        effect.Render();
    }

    protected void search_KeyUp(object sender, EventArgs e)
    {
        Page = 0;
        DataBindForumPosts();
        postsWrapper.SignalizeReRender();
    }

    protected void newSubmit_Click(object sender, EventArgs e)
    {
        // Simple valdation
        if (header.Text.Length < 5)
        {
            lblErrorPost.Text = "Header of post is too short";
            Effect effect2 = new EffectFadeIn(lblErrorPost, 0.4M);
            effect2.Render();
            return;
        }

        // Creating new post
        ForumPost post = new ForumPost();
        post.Body = body.Text;
        post.Created = DateTime.Now;
        post.Header = header.Text;
        post.Operator = Operator.Current;
        post.Body += string.Format(@"
-- 
<em> {0} </em>", Operator.Current.Signature);
        post.Save();

        // Removing panel
        Effect effect = new EffectFadeOut(pnlNewPost, 0.4M);
        effect.Render();

        // Re-rendering posts to get the newly added item
        DataBindForumPosts();
        postsWrapper.SignalizeReRender();
    }
}



























