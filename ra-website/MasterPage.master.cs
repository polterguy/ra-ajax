/*
 * Ra-Ajax, Copyright 2008 - 2009 - Thomas Hansen polterguy@gmail.com
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

                // Checking to see if this is a "confirm user" request
                string newUserRegistration = Request.Params["NewUser"];

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
                        newUserWelcome.Text = string.Format("  Welcome to Ra-Ajax {0}, your user account is now confiremed.", oper.Username);
                    }
                }

                if (Operator.Current != null)
                    userInfoPanel.Visible = true;
                else
                    loginPanel.Visible = true;

                if (Request.Browser.Browser == "IE" && Request.Cookies["displayIEMessage"] == null)
                {
                    pnlCrappyBrowser.Visible = true;
                    new EffectFadeIn(pnlCrappyBrowser, 400).Render();
                }

                // Iterating through all bloggers showing links to them...
                blogLinksWrapper.Text += "<ul>";
                foreach (Operator idx in Operator.FindAll(Order.Asc("Created"), Expression.Eq("IsBlogger", true)))
                {
                    blogLinksWrapper.Text += string.Format("\r\n<li><a href=\"{0}\" style=\"text-transform:capitalize;\">{1}</a></li>",
                        (Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf('/') + 1).Replace("/Forums", "") + idx.Username + ".blogger"),
                        idx.Username);
                }
                blogLinksWrapper.Text += "</ul>";
            }
            else if (pnlConfirmRegistration.Visible)
            {
                pnlConfirmRegistration.Visible = false;
            }
        }

        protected void pnlConfirmRegistration_Click(object sender, EventArgs e)
        {
            new EffectFadeOut(pnlConfirmRegistration, 400).Render();
        }

        protected void loginButton_Click(object sender, EventArgs e)
        {
            userWindow.Caption = "Enter your login credentials";
            userDyanmicPanel.LoadControls("Login.ascx");
            userWindow.Visible = true;
        }

        protected void registerButton_Click(object sender, EventArgs e)
        {
            userWindow.Caption = "Register";
            userDyanmicPanel.LoadControls("Register.ascx");
            userWindow.Visible = true;
        }

        protected void editProfileButton_Click(object sender, EventArgs e)
        {
            userWindow.Caption = "Update Your Profile";
            userDyanmicPanel.LoadControls("EditProfile.ascx", true);
            userWindow.Visible = true;
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            Response.Cookies["userId"].Value = "__mumbo__jumbo";
            Operator.Logout();
            Ra.AjaxManager.Instance.Redirect(Request.Url.ToString());
        }

        protected void closeIE_Click(object sender, EventArgs e)
        {
            new EffectFadeOut(pnlCrappyBrowser, 400).Render();

            HttpCookie ieMessage = new HttpCookie("displayIEMessage", "false");
            ieMessage.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(ieMessage);
        }

        protected void userDyanmicPanel_Reload(object sender, Dynamic.ReloadEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Key) && File.Exists(Server.MapPath("~/" + e.Key)))
            {
                System.Web.UI.Control control = LoadControl(e.Key);
                if (e.Key == "EditProfile.ascx")
                    ((RaWebsite.EditProfile)control).ProfileLoaded = !e.FirstReload;
                userDyanmicPanel.Controls.Add(control);
            }
        }
    }
}
