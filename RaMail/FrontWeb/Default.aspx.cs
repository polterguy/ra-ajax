using System;
using Engine.Entities;
using NHibernate.Expression;
using System.Web.Security;
using Ra.Widgets;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Operator.Current != null)
            {
                DataBindAccounts();
                header.InnerHtml += " " + Operator.Current.Username;
            }
        }
    }

    private void DataBindAccounts()
    {
        repPop.DataSource = Engine.Entities.Account.FindAll(Expression.Eq("Operator", Operator.Current));
        repPop.DataBind();
    }

    protected void logout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
    }

    protected void create_Click(object sender, EventArgs e)
    {
        newAccount.Visible = true;
        Effect effect = new EffectFadeIn(newAccount, 0.4M);
        effect.Render();
        actServer.Focus();
    }

    protected void createBtn_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectFadeOut(newAccount, 0.4M);
        effect.Render();
        Engine.Entities.Account act = new Engine.Entities.Account();
        act.Pop3Server = actServer.Text;
        act.Username = actUsername.Text;
        act.Password = actPwd.Text;
        act.Operator = Operator.Current;
        act.Save();
        DataBindAccounts();
        repPopWrapper.SignalizeReRender();
    }
}
