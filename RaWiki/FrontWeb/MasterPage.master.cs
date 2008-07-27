/*
 * Ra Ajax, Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * Ra is licensed under an MITish license. The licenses should 
 * exist on disc where you extracted Ra and will be named license.txt
 * 
 */

using System;
using Engine.Entities;
using Ra.Widgets;
using NHibernate.Expression;
using Castle.ActiveRecord.Queries;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Operator.Current == null)
            {
                username.Focus();
            }
            else
            {
                loginPnl.Visible = false;
                welcome.Text += Operator.Current.Username;
                welcomePnl.Visible = true;
                search.Focus();
            }
        }
    }

    protected void search_KeyUp(object sender, EventArgs e)
    {
        if (search.Text.Trim().Length == 0)
        {
            searchResults.Visible = false;
        }
        else
        {
            searchResults.Visible = true;
            searchResults.Controls.Clear();
            searchResults.SignalizeReRender();
            int idxNo = 0;
            SimpleQuery<Article> q = new SimpleQuery<Article>(@"
from Article a where a.Header like '%" + search.Text + "%'");
            Article[] articles = q.Execute();
            foreach (Article idx in articles)
            {
                System.Web.UI.WebControls.Literal lit = new System.Web.UI.WebControls.Literal();
                if (idxNo == 0)
                {
                    lit.Text += "<ul>";
                }
                lit.Text += string.Format("<li><a href=\"{0}.wiki\">{1}</a></li>", idx.Url, idx.Header);
                if (idxNo == articles.Length)
                    lit.Text += "</ul>";
                searchResults.Controls.Add(lit);
                idxNo += 1;
            }
        }
    }

    protected void loginBtn_Click(object sender, EventArgs e)
    {
        errLogin.Text = "";
        if (Operator.Login(username.Text, password.Text))
        {
            Effect effect = new EffectFadeOut(loginPnl, 0.4M);
            effect.Render();
            welcome.Text += Operator.Current.Username;
            welcomePnl.Visible = true;
            effect = new EffectFadeIn(welcomePnl, 0.4M);
            effect.Render();
        }
        else
        {
            errLogin.Text = "Wrong username/password";
            Effect effect = new EffectFadeIn(errLogin, 0.4M);
            effect.Render();
        }
    }

    protected void logout_Click(object sender, EventArgs e)
    {
        Operator.Logout();
        Effect effect = new EffectFadeOut(welcomePnl, 0.4M);
        effect.Render();
        welcome.Text = "Welcome ";
        effect = new EffectFadeIn(loginPnl, 0.4M);
        loginPnl.Style["display"] = ""; // Otherwise focus won't work...
        effect.Render();
        username.Text = "";
        password.Text = "";
        username.Focus();
        loginPnl.Visible = true;
    }

    protected void register_Click(object sender, EventArgs e)
    {
        errLogin.Text = "";
        Effect effect = new EffectFadeOut(loginPnl, 0.4M);
        effect.Render();
        registerPnl.Visible = true;
        effect = new EffectFadeIn(registerPnl, 0.4M);
        effect.Render();
        regUsername.Focus();
    }

    protected void regBtn_Click(object sender, EventArgs e)
    {
        Operator n = new Operator();
        n.Password = regPassword.Text;
        n.Username = regUsername.Text;
        n.Email = regEmail.Text;
        n.Register();
        Effect effect = new EffectFadeOut(registerPnl, 0.4M);
        effect.Render();
        effect = new EffectFadeIn(loginPnl, 0.4M);
        effect.Render();
        errLogin.Text = "A confirmation email has been sent to your email, you must confirm your email by clicking this email before you can login to Ra Wiki";
        loginPnl.Style["display"] = "";
        username.Focus();
    }
}
