/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Ra Software AS thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using NHibernate.Expression;
using Ra.Widgets;

public partial class Todo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBindTodoList();
            if (Entity.Operator.Current == null)
                pnlAdd.Visible = false;
        }
    }

    private void DataBindTodoList()
    {
        if (viewFinished.Checked)
            repTodo.DataSource = 
                Entity.Todo.FindAll(
                    Order.Desc("Created"),
                    Expression.Or(
                        Expression.Like("Header", filter.Text.Trim(), MatchMode.Anywhere),
                        Expression.Like("Body", filter.Text.Trim(), MatchMode.Anywhere)));
        else
            repTodo.DataSource = 
                Entity.Todo.FindAll(
                    Order.Desc("Created"), 
                    Expression.Eq("Finished", false),
                    Expression.Or(
                        Expression.Like("Header", filter.Text.Trim(), MatchMode.Anywhere),
                        Expression.Like("Body", filter.Text.Trim(), MatchMode.Anywhere)));
        repTodo.DataBind();
    }

    protected void save_Click(object sender, EventArgs e)
    {
        Entity.Todo t = new Entity.Todo();
        t.Body = body.Text;
        t.Created = DateTime.Now;
        t.Header = header.Text;
        t.Operator = Entity.Operator.Current;
        t.TypeOfTodo = type.SelectedItem.Text;
        t.Save();
        DataBindTodoList();
        repWrapper.SignalizeReRender();
    }

    protected void viewFinished_CheckedChanged(object sender, EventArgs e)
    {
        DataBindTodoList();
        repWrapper.SignalizeReRender();
    }

    protected void FinishedChecked(object sender, EventArgs e)
    {
        CheckBox ctrl = sender as CheckBox;
        HiddenField hid = ctrl.Parent.Controls[0] as HiddenField;
        if (hid == null)
            hid = ctrl.Parent.Controls[1] as HiddenField;
        Entity.Todo todo = Entity.Todo.FindOne(Expression.Eq("Id", Int32.Parse(hid.Value)));
        todo.Finished = ctrl.Checked;
        todo.Save();
        DataBindTodoList();
        repWrapper.SignalizeReRender();
    }

    protected void ClickDetails(object sender, EventArgs e)
    {
        LinkButton ctrl = sender as LinkButton;
        HiddenField hid = ctrl.Parent.Controls[0] as HiddenField;
        if (hid == null)
            hid = ctrl.Parent.Controls[1] as HiddenField;
        Entity.Todo todo = Entity.Todo.FindOne(Expression.Eq("Id", Int32.Parse(hid.Value)));
        Panel panel = ctrl.Parent.Controls[7] as Panel;
        if (panel == null)
            panel = ctrl.Parent.Controls[8] as Panel;
        panel.Visible = true;
        Effect effect = new EffectFadeIn(panel, 0.4M);
        effect.Render();
        effect = new EffectSize(panel, 0.4M, 150, 350);
        effect.Render();
    }

    protected void filter_KeyUp(object sender, EventArgs e)
    {
        DataBindTodoList();
        repWrapper.SignalizeReRender();
    }
}
