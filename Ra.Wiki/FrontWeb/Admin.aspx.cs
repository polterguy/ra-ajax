using System;
using Engine.Entities;
using Ra;

public partial class Admin : System.Web.UI.Page
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
        createArticle.Visible = false;
    }

    private void Operator_LoggedIn(object sender, EventArgs e)
    {
        if (Operator.Current.IsAdmin)
            adminMode.Visible = true;
        createArticle.Visible = true;
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

    protected void createArticle_Click(object sender, EventArgs e)
    {
        createArticlePnl.Visible = true;
        nArticleName.Focus();
        nArticleName.Select();
        nArticleName.Text = "Type in name of article here";
    }

    protected void createArticleBtn_Click(object sender, EventArgs e)
    {
        string url = Article.CreateUniqueFriendlyURL(nArticleName.Text);
        url = url + ".wiki?name=" + Server.UrlEncode(nArticleName.Text);
        AjaxManager.Instance.Redirect(url);
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
                Operator oper = Operator.ApproveNewUser(Request.Params["approveUser"], Request.Params["seed"]);
                content.InnerHtml = string.Format(@"
User {0} is now approved and can log into the wiki.
", oper.Username);
            }
            else
            {
                content.InnerHtml = "You must be logged in as an administrator to approve new users.";
            }
        }
        else
        {
            content.InnerHtml = "You must be logged in as an administrator to approve new users, <strong>just log in with your admin account and REFRESH this page to approve the user</strong>.";
        }
    }

    private void ConfirmNewUser()
    {
        Operator oper = Operator.ConfirmNewUser(Request.Params["idNewUserConfirmation"], Request.Params["seed"]);
        content.InnerHtml = string.Format(@"
<strong>Welcome as a new user {0}</strong>.
<br />
{1}", oper.Username,
                      (oper.AdminApproved ?
                        "You can now login with your username and your password" :
                        "<strong>An administrator at this wiki must approve your membership before you can login</strong>"));
    }
}
