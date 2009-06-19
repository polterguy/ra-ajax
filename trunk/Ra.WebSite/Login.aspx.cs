/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;
using Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using Ra.Widgets;
using System.Web;
using System.Text.RegularExpressions;
using Ra.Effects;

namespace RaWebsite
{
    public partial class Login : System.Web.UI.Page
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

                if (Request.Params["register"] != null)
                    Register();

                if (Request.Params["editProfile"] != null && Operator.Current != null)
                    EditProfile();
            }

            // Checking to see if user is logged in
            if (Request.Params["register"] == null && Request.Params["editProfile"] == null && Operator.Current == null)
            {
                pnlLogin.Visible = true;
                if (!IsPostBack)
                    username.Focus();
            }
        }

        protected void login_Click(object sender, EventArgs e)
        {
            if (Operator.Login(username.Text, pwd.Text))
            {
                pnlLogin.Visible = false;
                if (remember.Checked)
                {
                    HttpCookie rem = new HttpCookie("userId", username.Text);
                    rem.Expires = DateTime.Now.AddMonths(24);
                    Response.Cookies.Add(rem);
                }
                if (!string.IsNullOrEmpty(Request.Params["ReturnUrl"]))
                    Ra.AjaxManager.Instance.Redirect(Request.Params["ReturnUrl"]);
                else
                    Ra.AjaxManager.Instance.Redirect("Default.aspx");
            }
            else
            {
                resultLabel.Text = "Couldn't log you in";
                username.Focus();
                username.Select();
                new EffectFadeIn(resultLabel, 400).Render();
            }
        }

        protected void register_Click(object sender, EventArgs e)
        {
            Register();
        }

        protected void finishRegister_Click(object sender, EventArgs e)
        {
            if (!ValidatePasswordMatch())
                return;
            else if (!ValidateEmailFormat())
                return;
            else if (!ValidateUsername())
                return;
            else
            {
                finishRegister.Enabled = false;
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

To confirm your registration and activate your user account please go to:
{0}?idNewUser={1}

Your username is: {1}
Your password is: {2}

Have a nice day :)",
                    Request.Url.ToString().Remove(Request.Url.ToString().IndexOf(Request.Url.Query)),
                    oper.Username,
                    oper.Pwd));
                new EffectFadeOut(pnlRegister, 400).Render();
                resultLabel.Text = "Thanks for registering. A message has been sent to your email to comfirm registration. We hope you enjoy using Ra-Ajax.";
                new EffectFadeIn(resultLabel, 400).Render();
            }
        }

        private bool ValidateEmailFormat()
        {
            bool retVal = !(string.IsNullOrEmpty(newEmail.Text) ||
                !Regex.IsMatch(newEmail.Text, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25}$", RegexOptions.Compiled));

            if (!retVal)
            {
                resultLabel.Text = "The Email is not in a correct format.";
                new EffectFadeIn(resultLabel, 400).Render();
                newEmail.Focus();
            }
            return retVal;
        }

        private bool ValidatePasswordMatch()
        {
            bool retVal = newPassword.Text == newPasswordRepeat.Text;
            if (!retVal)
            {
                newPassword.Text = "";
                newPasswordRepeat.Text = "";

                resultLabel.Text = "Passwords didn't match.";
                new EffectFadeIn(resultLabel, 400).Render();

                new EffectHighlight(newPassword, 1000).ChainThese(
                    new EffectTimeout(200),
                    new EffectHighlight(newPassword, 1000)
                ).Render();

                new EffectHighlight(newPasswordRepeat, 1000).ChainThese(
                    new EffectTimeout(200),
                    new EffectHighlight(newPasswordRepeat, 1000)
                ).Render();

                newPassword.Style["background-color"] = "#f66";
                newPasswordRepeat.Style["background-color"] = "#f66";
                newPassword.Focus();
            }
            return retVal;
        }

        private bool ValidateUsername()
        {
            bool retVal = !string.IsNullOrEmpty(newUsername.Text) &&
                Operator.FindOne(Expression.Eq("Username", newUsername.Text)) == null;

            if (string.IsNullOrEmpty(newUsername.Text))
            {
                resultLabel.Text = "Please enter a Username.";
                new EffectFadeIn(resultLabel, 400).Render();

                new EffectHighlight(newUsername, 1000).ChainThese(
                    new EffectTimeout(200),
                    new EffectHighlight(newUsername, 1000)
                ).Render();

                newUsername.Style["background-color"] = "#f66";
                newUsername.Focus();
            }
            else if (Operator.FindOne(Expression.Eq("Username", newUsername.Text)) != null)
            {
                resultLabel.Text = "This Username already exists, please choose a different Username.";
                new EffectFadeIn(resultLabel, 400).Render();
                newUsername.Focus();
            }
            return retVal;
        }

        protected void btnChangeProfile_Click(object sender, EventArgs e)
        {
            if (changePassword.Text != changePasswordConfirm.Text)
            {
                changePassword.Text = "";
                changePasswordConfirm.Text = "";
                resultLabel.Text = "Passwords didn't match.";
                new EffectFadeIn(resultLabel, 400).Render();
                changePassword.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(changeEmail.Text) ||
                !Regex.IsMatch(changeEmail.Text, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25}$", RegexOptions.Compiled))
            {
                resultLabel.Text = "The Email is not in a correct format.";
                new EffectFadeIn(resultLabel, 400).Render();
                changeEmail.Focus();
                return;
            }
            btnChangeProfile.Enabled = false;
            Operator.Current.Pwd = changePassword.Text;
            Operator.Current.Signature = changeSignature.Text;
            Operator.Current.Email = changeEmail.Text;
            Operator.Current.Save();
            
            new EffectFadeOut(pnlProfile, 400).Render();
            resultLabel.Text = "Your profile was updated successfully.";
            new EffectFadeIn(resultLabel, 400).Render();
        }

        private void Register()
        {
            if (Operator.Login(username.Text, pwd.Text))
            {
                pnlLogin.Visible = false;
            }
            else
            {
                pnlRegister.Visible = true;
                pnlRegister.Style["display"] = "";
                newUsername.Focus();
                newUsername.Select();

                // Fading in/out panels...
                Effect effect = new EffectFadeOut(pnlLogin, 600);
                effect.Render();
                effect = new EffectFadeIn(pnlRegister, 600);
                effect.Render();

                newUsername.Text = username.Text;
                newUsername.Focus();
                newUsername.Select();
                newPassword.Text = pwd.Text;
                newPasswordRepeat.Text = pwd.Text;
            }
        }

        private void EditProfile()
        {
            pnlProfile.Visible = true;
            pnlProfile.Style["display"] = "";
            new EffectFadeIn(pnlProfile, 400).Render();

            changePassword.Text = Operator.Current.Pwd;
            changePassword.Focus();
            changePassword.Select();
            changePasswordConfirm.Text = Operator.Current.Pwd;
            changeSignature.Text = Operator.Current.Signature;
            changeEmail.Text = Operator.Current.Email;
        }
    }
}



























