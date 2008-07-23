using System;
using Engine.Entities;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Login1.Focus();
        }
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        if (Operator.Login(Login1.UserName, Login1.Password))
        {
            e.Authenticated = true;
            FormsAuthentication.RedirectFromLoginPage(Operator.Current.Username, Login1.RememberMeSet);
        }
    }
}
