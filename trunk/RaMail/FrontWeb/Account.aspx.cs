using System;
using NHibernate.Expression;
using Engine.Entities;

public partial class Account : System.Web.UI.Page
{
    private Engine.Entities.Account _account;

    protected void Page_Load(object sender, EventArgs e)
    {
        _account = Engine.Entities.Account.FindOne(Expression.Eq("ID", Int32.Parse(Request.Params["id"])));
        if (_account.Operator.ID != Operator.Current.ID)
            throw new Exception("Security error");

        if (!IsPostBack)
        {
            header.InnerHtml += " " + _account.Pop3Server;
        }
    }

    protected void checkEmail_Click(object sender, EventArgs e)
    {
        _account.CheckMail();
    }
}
