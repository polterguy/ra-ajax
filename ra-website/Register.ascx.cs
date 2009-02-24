using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using Ra.Widgets;
using System.Text.RegularExpressions;
using NHibernate.Expression;

namespace RaWebsite
{
    public partial class Register : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            newUsername.Focus();
        }

        protected void finishRegister_Click(object sender, EventArgs e)
        {
            if (ValidateUsername() && ValidatePasswordsMatch() && ValidateEmailFormat())
            {
                finishRegister.Enabled = false;
                Operator oper = new Operator();
                oper.Email = newEmail.Text;
                oper.Pwd = newPassword.Text;
                oper.Username = newUsername.Text;

                oper.Save();
                oper.SendEmail(
                    "Confirm your Registration at Ra-Ajax.org",
                    string.Format(
    @"This email was automatically sent from http://ra-ajax.org. If you did not register there, please delete this email.

To confirm your registration and activate your account please visit {0}?NewUser={1}

Your username is: {1}
Your password is: {2}

We hope you enjoy using Ra-Ajax",
                    "http://ra-ajax.org/Default.aspx",
                    oper.Username,
                    oper.Pwd));
                
                new EffectFadeOut(registerationTable, 400).ChainThese(
                    new EffectFadeIn(registerationFinishedTable, 300)
                ).Render();
            }
        }

        private bool ValidateEmailFormat()
        {
            bool retVal = !(string.IsNullOrEmpty(newEmail.Text) ||
                !Regex.IsMatch(newEmail.Text, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25}$", RegexOptions.Compiled));

            if (!retVal)
            {
                resultLabel.Text = "The email is not in a correct format.";
                new EffectFadeIn(resultLabel, 400).Render();
                newEmail.Focus();
                newEmail.Select();
            }
            return retVal;
        }

        private bool ValidatePasswordsMatch()
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

                newPassword.Focus();
                newPassword.Select();
            }
            return retVal;
        }

        private bool ValidateUsername()
        {
            bool retVal = !string.IsNullOrEmpty(newUsername.Text) &&
                Operator.FindOne(Expression.Eq("Username", newUsername.Text)) == null;

            if (string.IsNullOrEmpty(newUsername.Text))
            {
                resultLabel.Text = "Please enter a username.";
                new EffectFadeIn(resultLabel, 400).Render();

                new EffectHighlight(newUsername, 1000).ChainThese(
                    new EffectTimeout(200),
                    new EffectHighlight(newUsername, 1000)
                ).Render();

                newUsername.Focus();
            }
            else if (Operator.FindOne(Expression.Eq("Username", newUsername.Text)) != null)
            {
                resultLabel.Text = "This username is already used.";
                new EffectFadeIn(resultLabel, 400).Render();
                newUsername.Focus();
                newUsername.Select();
            }
            return retVal;
        }
    }
}