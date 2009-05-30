using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using Ra.Widgets;

namespace RaWebsite
{
    public partial class Login : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            username.Focus();
        }

        protected void login_Click(object sender, EventArgs e)
        {
            if (Operator.Login(username.Text, pwd.Text))
            {
                if (remember.Checked)
                {
                    HttpCookie rem = new HttpCookie("userId", username.Text);
                    rem.Expires = DateTime.Now.AddMonths(24);
                    Response.Cookies.Add(rem);
                }
                Ra.AjaxManager.Instance.Redirect(Request.Url.ToString());
            }
            else
            {
                resultLabel.Text = "Couldn't log you in, please try again.";
                username.Focus();
                username.Select();
                new EffectFadeIn(resultLabel, 400).Render();
            }
        }
    }
}