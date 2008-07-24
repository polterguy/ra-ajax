using System;
using Ra.Widgets;
using Engine.Entities;
using Ra.Extensions;
using NHibernate.Expression;

public partial class AdminMode : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetToInvisible()
    {
        users.Visible = false;
    }

    protected void adminUsers_Click(object sender, EventArgs e)
    {
        users.Visible = true;
        Effect effect = new EffectFadeIn(users, 0.4M);
        effect.Render();
        DataBindUsers();
    }

    private void DataBindUsers()
    {
        repUsers.DataSource = Operator.FindAll();
        repUsers.DataBind();
    }

    protected void UsernameChanged(object sender, EventArgs e)
    {
        InPlaceEdit btn = sender as InPlaceEdit;
        int id = GetId(btn);
        Operator oper = Operator.FindOne(Expression.Eq("Id", id));
        oper.Username = btn.Text;
        oper.Save();
    }

    protected void PasswordChanged(object sender, EventArgs e)
    {
        InPlaceEdit btn = sender as InPlaceEdit;
        int id = GetId(btn);
        Operator oper = Operator.FindOne(Expression.Eq("Id", id));
        oper.Password = btn.Text;
        oper.Save();
    }

    protected void EmailChanged(object sender, EventArgs e)
    {
        InPlaceEdit btn = sender as InPlaceEdit;
        int id = GetId(btn);
        Operator oper = Operator.FindOne(Expression.Eq("Id", id));
        oper.Email = btn.Text;
        oper.Save();
    }

    private int GetId(InPlaceEdit btn)
    {
        HiddenField hid = btn.Parent.Controls[0] as HiddenField;
        if (hid == null)
            hid = btn.Parent.Controls[1] as HiddenField;
        return Int32.Parse(hid.Value);
    }
}
