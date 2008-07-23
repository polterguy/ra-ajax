using System;
using Engine.Entities;
using System.Web.Security;
using System.Web;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookiePath];
            if (cookie != null)
            {
                string userName = cookie.Value;
                Operator.Login(userName);
            }
        }
    }
}
