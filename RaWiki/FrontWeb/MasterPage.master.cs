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
using System.Configuration;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (ConfigurationSettings.AppSettings["contentLicense"])
            {
                case "by-nc-sa":
                    contentLicense.NavigateUrl = "http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode";
                    contentLicense.Text = "Creative Commons - Attribution Non-commercial Share Alike";
                    break;
                case "by-sa":
                    contentLicense.NavigateUrl = "http://creativecommons.org/licenses/by-sa/3.0/legalcode";
                    contentLicense.Text = "Creative Commons - Attribution Share Alike";
                    break;
                case "GFDL":
                    contentLicense.NavigateUrl = "http://www.gnu.org/licenses/fdl.txt";
                    contentLicense.Text = "GNU Free Documentation License";
                    break;
                default:
                    throw new ArgumentException("Invalid license found in configuration file");
            }

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

            // Creating the links to the last changed articles...
            CreateLastChanges();
            CreateSiteWideLinks();
        }
    }

    private void CreateSiteWideLinks()
    {
        int idxNo = 0;
        string url = Request.Url.ToString();
        url = url.Substring(0, url.LastIndexOf("/") + 1);
        foreach (Article idx in Article.FindAll(Expression.Eq("SiteWide", true)))
        {
            if (idxNo == 0)
                startingPoint.Text += "<ul class=\"links\"><li>{Start Here}</li>";
            startingPoint.Text += string.Format("<li><a href=\"{0}\">{1}</a></li>",
                (idx.Url == "default" ? url : idx.Url + ".wiki"),
                idx.Header);
            idxNo += 1;
        }
        if (idxNo > 0)
            startingPoint.Text += "</ul>";
    }

    private void CreateLastChanges()
    {
        int idxNo = 0;
        string url = Request.Url.ToString();
        url = url.Substring(0, url.LastIndexOf("/") + 1);
        foreach (Article idx in Article.FindAll(Order.Desc("Changed")))
        {
            if (idxNo == 0)
                lastArticles.Text += "<ul class=\"links\"><li>{Last Changes}</li>";
            lastArticles.Text += string.Format("<li><a href=\"{0}\">{1}</a></li>",
                (idx.Url == "default" ? url : idx.Url + ".wiki"), 
                idx.Header);
            idxNo += 1;
            if (idxNo > 50)
                break;
        }
        if (idxNo > 0)
            lastArticles.Text += "</ul>";
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
            string url = Request.Url.ToString();
            url = url.Substring(0, url.LastIndexOf("/") + 1);
            foreach (Article idx in articles)
            {
                System.Web.UI.WebControls.Literal lit = new System.Web.UI.WebControls.Literal();
                if (idxNo == 0)
                {
                    lit.Text += "<ul>";
                }
                lit.Text += string.Format("<li><a href=\"{0}\">{1}</a></li>",
                    (idx.Url == "default" ? url : idx.Url + ".wiki"), 
                    idx.Header);
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
            search.Focus();
        }
        else
        {
            errLogin.Text = "Couldn't log you in, either wrong username/password combination or your user is yet noe confirmed or approved...";
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
