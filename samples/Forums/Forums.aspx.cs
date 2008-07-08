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

public partial class Forums_Forums : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<ForumPost> postsToBind = new List<ForumPost>();
            int idxNo = 0;
            foreach (ForumPost idx in ForumPost.FindAll(Order.Desc("Created")))
            {
                if (idxNo > 10)
                    break;
                postsToBind.Add(idx);
                idxNo += 1;
            }
            forumPostsRepeater.DataSource = postsToBind;
            forumPostsRepeater.DataBind();
        }

        // Checking to see if user is logged in
        if (Operator.Current == null)
        {
            pnlLogin.Visible = true;
            username.Focus();
        }
        else
        {
            pnlLogin.Visible = false;
        }
    }

    protected void login_Click(object sender, EventArgs e)
    {
        if (Operator.Login(username.Text, pwd.Text))
        {
            pnlLogin.Visible = false;
        }
    }
}
