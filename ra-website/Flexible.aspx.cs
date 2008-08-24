/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using NHibernate.Expression;
using System.Web.UI.WebControls;
using Ra.Widgets;

public partial class Flexible : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            txt.Focus();
    }

    protected void txt_KeyUp(object sender, EventArgs e)
    {
        lbl.Text = txt.Text;
        Effect effect = new EffectFadeIn(lbl, 0.4M);
        effect.Render();
    }

    protected void autoTxt_KeyUp(object sender, EventArgs e)
    {
        if (autoTxt.Text.Trim().Length == 0)
        {
            autoPnl.Visible = false;
        }
        else
        {
            autoPnl.Visible = true;
            autoPnl.Controls.Clear();
            autoPnl.ReRender();
            int idxNo = 0;
            Entity.Blog[] blogs = Entity.Blog.FindAll(Expression.Like("Header", autoTxt.Text, MatchMode.Anywhere));
            foreach (Entity.Blog idx in blogs)
            {
                Literal lit = new Literal();
                if (idxNo == 0)
                {
                    lit.Text += "<ul>";
                }
                lit.Text += string.Format("<li><a href=\"{0}\">{1}</a></li>", idx.Url, idx.Header);
                if (idxNo == blogs.Length)
                    lit.Text += "</ul>";
                autoPnl.Controls.Add(lit);
                idxNo += 1;
            }
        }
    }
}
