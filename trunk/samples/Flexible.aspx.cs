using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NHibernate.Expression;

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
            autoPnl.SignalizeReRender();
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
