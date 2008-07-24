/*
 * Ra Ajax, Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * Ra is licensed under an MITish license. The licenses should 
 * exist on disc where you extracted Ra and will be named license.txt
 * 
 */

using System;
using Engine.Entities;
using Ra.Widgets;

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
            }
        }
    }

    protected void loginBtn_Click(object sender, EventArgs e)
    {
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
        loginPnl.Style["display"] = "";
        effect.Render();
        username.Text = "";
        password.Text = "";
        username.Focus();
    }
}
