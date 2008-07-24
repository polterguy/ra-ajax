using System;
using Engine.Entities;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["idNewUserConfirmation"] != null)
            {
                ConfirmNewUser();
            }
            if (Request.Params["approveUser"] != null)
            {
                ApproveNewUser();
            }
        }
    }

    private void ApproveNewUser()
    {
        if (Operator.Current != null)
        {
            if (Operator.Current.IsAdmin)
            {
                Operator oper = Operator.ConfirmNewUser(Request.Params["idNewUserConfirmation"], Request.Params["seed"]);
                content.InnerHtml = string.Format(@"
User {0} is now approved and can log into the wiki.
", oper.Username);
            }
            else
            {
                content.InnerHtml = "You must be logged in as an administrator to approve new users";
            }
        }
        else
        {
            content.InnerHtml = "You must be logged in as an administrator to approve new users";
        }
    }

    private void ConfirmNewUser()
    {
        Operator oper = Operator.ConfirmNewUser(Request.Params["idNewUserConfirmation"], Request.Params["seed"]);
        content.InnerHtml = string.Format(@"
Welcome as a new user {0}.
<br />
{1}", oper.Username,
                      (oper.AdminApproved ?
                        "You can now login with your username and your password" :
                        "An administrator at this wiki must approve your membership before you can login"));
    }
}
