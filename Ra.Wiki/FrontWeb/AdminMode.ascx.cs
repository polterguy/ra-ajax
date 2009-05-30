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
        nUsername.Focus();
    }

    protected void nCreate_Click(object sender, EventArgs e)
    {
        Operator o = new Operator();
        o.AdminApproved = true;
        o.Confirmed = true;
        o.Created = DateTime.Now;
        o.Email = nEmail.Text;
        o.IsAdmin = nIsAdmin.Checked;
        o.Password = nPassword.Text;
        o.Username = nUsername.Text;
        o.Save();
        DataBindUsers();
        repUsersWrapper.ReRender();
        nUsername.Text = "";
        nPassword.Text = "";
        nEmail.Text = "";
        nIsAdmin.Checked = false;
        nUsername.Focus();
    }

    protected void closeUsers_Click(object sender, EventArgs e)
    {
        users.Visible = false;
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

    protected void IsAdminChanged(object sender, EventArgs e)
    {
        CheckBox btn = sender as CheckBox;
        int id = GetId(btn);
        Operator oper = Operator.FindOne(Expression.Eq("Id", id));
        oper.IsAdmin = btn.Checked;
        oper.Save();
    }

    protected void ConfirmedChanged(object sender, EventArgs e)
    {
        CheckBox btn = sender as CheckBox;
        int id = GetId(btn);
        Operator oper = Operator.FindOne(Expression.Eq("Id", id));
        oper.Confirmed = btn.Checked;
        oper.Save();
    }

    protected void AdminApprovedChanged(object sender, EventArgs e)
    {
        CheckBox btn = sender as CheckBox;
        int id = GetId(btn);
        Operator oper = Operator.FindOne(Expression.Eq("Id", id));
        oper.AdminApproved = btn.Checked;
        oper.Save();
    }
    
    protected void DeleteUser(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        int id = GetId(btn);
        Operator oper = Operator.FindOne(Expression.Eq("Id", id));
        oper.Delete();
        users.ReRender();
        DataBindUsers();
    }

    private int GetId(System.Web.UI.Control btn)
    {
        HiddenField hid = btn.Parent.Controls[0] as HiddenField;
        if (hid == null)
            hid = btn.Parent.Controls[1] as HiddenField;
        return Int32.Parse(hid.Value);
    }
}
