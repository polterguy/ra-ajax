using System;
using Engine.Entities;

public partial class _Default : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        Operator.LoggedIn += new EventHandler(Operator_LoggedIn);
        Operator.LoggedOut += new EventHandler(Operator_LoggedOut);
        base.OnInit(e);
    }

    protected override void OnPreRender(EventArgs e)
    {
        // Since they are STATIC Event Handlers on Operator type we need to REMOVE them again...!
        Operator.LoggedIn -= new EventHandler(Operator_LoggedIn);
        Operator.LoggedOut -= new EventHandler(Operator_LoggedOut);
        base.OnPreRender(e);
    }

    void Operator_LoggedOut(object sender, EventArgs e)
    {
        adminMode.Visible = false;
        adminWrapper.Visible = false;
        AdminMode1.SetToInvisible();
    }

    private void Operator_LoggedIn(object sender, EventArgs e)
    {
        if (Operator.Current.IsAdmin)
            adminMode.Visible = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Operator.Current != null)
                Operator_LoggedIn(this, new EventArgs());
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

    protected void adminMode_Click(object sender, EventArgs e)
    {
        adminWrapper.Visible = true;
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
