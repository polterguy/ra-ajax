using System;
using Engine.Entities;
using NHibernate.Expression;
using Ra.Widgets;

public partial class Wiki : System.Web.UI.Page
{
    private Article _article;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string url = Request.Params["id"];
            _article = Article.FindOne(Expression.Eq("Url", url));
            if (_article == null)
            {
                // Checking to see if user is LOGGED IN and if NOT redirect since we
                // don't allo editing of articles for users not logged in
                if (Operator.Current == null)
                    Response.Redirect("~");

                // Create mode...
                headerInPlace.Text = Request.Params["name"];
                tab.ActiveTabViewIndex = 1;
                richedit.Text = "Just edit this content directly.<br />And save it when you feel for it.<br />";
                richedit.Text += "Use the toolbar at the top for formatting of your text<br />";
                richedit.Text += "Note that the Ra RichEdit works in such a way that you must FIRST create your text and THEN choose your formatting options...";
            }
        }
    }

    protected void richedit_KeyUp(object sender, EventArgs e)
    {
        dummy.Text = richedit.Text;
        dummy.Text += "\r\n\r\n";
        dummy.Text += richedit.Selection;
    }

    protected void bold_Click(object sender, EventArgs e)
    {
        string content = richedit.Selection;
        if (string.IsNullOrEmpty(content))
            content = "&nbsp;";
        richedit.Selection = "<strong>" + content + "</strong>";
    }

    protected void italic_Click(object sender, EventArgs e)
    {
        string content = richedit.Selection;
        if (string.IsNullOrEmpty(content))
            content = "&nbsp;";
        richedit.Selection = "<em>" + content + "</em>";
    }

    protected void underscore_Click(object sender, EventArgs e)
    {
        string content = richedit.Selection;
        if (string.IsNullOrEmpty(content))
            content = "&nbsp;";
        richedit.Selection = "<span style=\"text-decoration:underline;\">" + content + "</span>";
    }

    protected void strike_Click(object sender, EventArgs e)
    {
        string content = richedit.Selection;
        if (string.IsNullOrEmpty(content))
            content = "&nbsp;";
        richedit.Selection = "<span style=\"text-decoration:line-through;\">" + content + "</span>";
    }

    protected void bullets_Click(object sender, EventArgs e)
    {
        string content = richedit.Selection;
        if (string.IsNullOrEmpty(content))
            content = "content";
        richedit.Selection = "<ul><li>" + content + "</li></ul>";
    }

    protected void numbers_Click(object sender, EventArgs e)
    {
        string content = richedit.Selection;
        if (string.IsNullOrEmpty(content))
            content = "content";
        richedit.Selection = "<ol><li>" + content + "</li></ol>";
    }

    protected void link_Click(object sender, EventArgs e)
    {
        pnlLnk.Visible = true;
        Effect effect = new EffectFadeIn(pnlLnk, 0.4M);
        effect.Render();
        pnlLnk.Style["display"] = "";
        txtLnk.Focus();
        txtLnk.Select();
        txtLnk.Text = "http://";
    }

    protected void createLnk_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectFadeOut(pnlLnk, 0.4M);
        effect.Render();
        richedit.Selection = string.Format("<a href=\"{0}\" title=\"{1}\" rel=\"nofollow\">{2}</a>",
            txtLnk.Text,
            title.Text,
            richedit.Selection);
    }
}
