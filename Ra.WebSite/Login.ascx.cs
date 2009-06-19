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
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using Ra.Widgets;
using Ra.Effects;

namespace RaWebsiteUserControl
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