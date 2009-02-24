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
    public partial class EditProfile : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ProfileLoaded && Operator.Current != null)
            {
                changePassword.Text = Operator.Current.Pwd;
                changePasswordConfirm.Text = Operator.Current.Pwd;
                changeNickname.Text = Operator.Current.Signature;
                changeEmail.Text = Operator.Current.Email;

                changeNickname.Focus();
                changeNickname.Select();

                ProfileLoaded = true;
            }
        }

        protected void btnChangeProfile_Click(object sender, EventArgs e)
        {
            if (ValidatePasswordsMatch() && ValidateEmailFormat() && ProfileLoaded)
            {
                Operator.Current.Pwd = changePassword.Text;
                Operator.Current.Signature = changeNickname.Text;
                Operator.Current.Email = changeEmail.Text;
                Operator.Current.Save();

                resultLabel.Text = "Your profile was updated successfully.";
                new EffectFadeIn(resultLabel, 400).Render();
            }
        }

        public bool ProfileLoaded
        {
            get 
            {
                if (Session["ProfileLoaded"] != null)
                    return (bool)Session["ProfileLoaded"];
                return false;
            }
            set
            {
                Session["ProfileLoaded"] = value;
            }
        }

        private bool ValidateEmailFormat()
        {
            bool retVal = !(string.IsNullOrEmpty(changeEmail.Text) ||
                !Regex.IsMatch(changeEmail.Text, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25}$", RegexOptions.Compiled));

            if (!retVal)
            {
                resultLabel.Text = "The email is not in a correct format.";
                new EffectFadeIn(resultLabel, 400).Render();
                changeEmail.Focus();
                changeEmail.Select();
            }
            return retVal;
        }

        private bool ValidatePasswordsMatch()
        {
            bool retVal = changePassword.Text == changePasswordConfirm.Text;
            if (!retVal)
            {
                changePassword.Text = "";
                changePasswordConfirm.Text = "";

                resultLabel.Text = "Passwords didn't match.";
                new EffectFadeIn(resultLabel, 400).Render();

                new EffectHighlight(changePassword, 1000).ChainThese(
                    new EffectTimeout(200),
                    new EffectHighlight(changePassword, 1000)
                ).Render();

                new EffectHighlight(changePasswordConfirm, 1000).ChainThese(
                    new EffectTimeout(200),
                    new EffectHighlight(changePasswordConfirm, 1000)
                ).Render();

                changePassword.Focus();
                changePassword.Select();
            }
            return retVal;
        }
    }
}