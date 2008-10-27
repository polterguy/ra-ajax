/*
 * Ra-Ajax, Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * Ra is licensed under an MITish license. The licenses should 
 * exist on disc where you extracted Ra and will be named license.txt
 * 
 */

using System;
using Ra.Widgets;
using System.IO;
using Entity;
using NHibernate.Expression;
using System.Web;

namespace RaWebsite
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Checking remember me feature
                if (Request.Cookies["userId"] != null)
                {
                    HttpCookie rem = Request.Cookies["userId"];
                    string userName = rem.Value;
                    Operator.Login(userName);
                }

                if (Operator.Current != null)
                    userInfoPanel.Visible = true;
                else
                    loginPanel.Visible = true;

                // Morphing in/out the GetFireFox panel
                if (Request.Browser.Browser == "IE")
                {
                    pnlCrappyBrowser.Visible = true;
                    Effect effect2 = new EffectFadeIn(pnlCrappyBrowser, 400);
                    effect2.Render();
                }

                // Iterating through all bloggers showing links to them...
                blogLinksWrapper.Text += "<ul style=\"margin-top:10px;margin-bottom:10px;\">";
                foreach (Operator idx in Operator.FindAll(Order.Asc("Created"), Expression.Eq("IsBlogger", true)))
                {
                    blogLinksWrapper.Text += string.Format("\r\n<li><a href=\"{0}\">Blog of {1}</a></li>",
                        (Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1).Replace("/Forums", "") + idx.Username + ".blogger"),
                        idx.Username);
                }
                blogLinksWrapper.Text += "</ul>";
            }
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            Ra.AjaxManager.Instance.Redirect("~/Login.aspx?ReturnUrl=" + Request.Url.AbsolutePath);
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Response.Cookies["userId"].Value = "__mumbo__jumbo";
            Operator.Logout();
            Ra.AjaxManager.Instance.Redirect(Request.Url.ToString());
        }

        protected void closeIE_Click(object sender, EventArgs e)
        {
            Effect effect = new EffectFadeOut(pnlCrappyBrowser, 400);
            effect.Render();
        }
    }
}
