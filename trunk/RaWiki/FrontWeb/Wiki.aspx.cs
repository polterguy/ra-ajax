using System;
using Engine.Entities;
using NHibernate.Expression;
using Ra.Widgets;
using Ra;
using System.Text.RegularExpressions;

public partial class Wiki : System.Web.UI.Page
{
    private Article _article;

    protected override void OnInit(EventArgs e)
    {
        Operator.LoggedIn += new EventHandler(Operator_LoggedIn);
        Operator.LoggedOut += new EventHandler(Operator_LoggedOut);
        base.OnInit(e);
    }

    void Operator_LoggedOut(object sender, EventArgs e)
    {
        delete.Visible = Operator.Current != null && Operator.Current.IsAdmin;
    }

    void Operator_LoggedIn(object sender, EventArgs e)
    {
        delete.Visible = Operator.Current != null && Operator.Current.IsAdmin;
    }

    protected override void OnPreRender(EventArgs e)
    {
        // Since they are STATIC Event Handlers on Operator type we need to REMOVE them again...!
        Operator.LoggedIn -= new EventHandler(Operator_LoggedIn);
        Operator.LoggedOut -= new EventHandler(Operator_LoggedOut);
        base.OnPreRender(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string url = Request.Params["id"];
        _article = Article.FindOne(Expression.Eq("Url", url));
        if (!IsPostBack)
        {
            if (_article == null)
            {
                // Checking to see if user is LOGGED IN and if NOT redirect since we
                // don't allo editing of articles for users not logged in
                if (Operator.Current == null)
                    Response.Redirect("~");

                // Create mode...
                headerInPlace.Text = Request.Params["name"];
                tab.ActiveTabViewIndex = 1;
                richedit.Text = "<p>Just edit this content directly.<br />And save it when you feel for it.<br />";
                richedit.Text += "Use the toolbar at the top for formatting of your text<br />";
                richedit.Text += "Note that the Ra RichEdit works in such a way that you must FIRST create your text and THEN choose your formatting options...</p>";
            }
            else
            {
                header_read.InnerText = _article.Header;
                content.Text = FormatContent(_article.Body);
            }
        }
        delete.Visible = Operator.Current != null && Operator.Current.IsAdmin;
    }

    protected void delete_Click(object sender, EventArgs e)
    {
        _article.Delete();
        string url = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf("/"));
        AjaxManager.Instance.Redirect(url);
    }

    private string FormatContent(string content)
    {
        foreach (Match idx in Regex.Matches(content, "(?<wikiLinks>\\[[^\\]]+\\])"))
        {
            string[] values = idx.Value.Trim('[', ']').Split('|');
            string url = values[0].Trim();
            string name = values.Length > 1 ? values[1].Trim() : values[0].Trim();
            string cssClass = "exists";
            if (Article.FindAll(Expression.Eq("Url", url)).Length == 0)
                cssClass = "non-existant";
            string urlElement = string.Format(@"<a{3} class=""{2}"" href=""{0}.wiki"">{1}</a>",
            url,
            name,
            cssClass,
            (cssClass == "exists" ? "" : " rel=\"nofollow\""));
            content = content.Replace(idx.Value, urlElement);
        }
        return content;
    }

    protected void tab_ActiveTabViewChanged(object sender, EventArgs e)
    {
        if (tab.ActiveTabViewIndex == 0)
        {
            header_read.InnerText = headerInPlace.Text;
            content.Text = FormatContent(richedit.Text);
            warning.Visible = true;
            warning.Text = "Remember to SAVE your articles when editing!";
        }
        else if (tab.ActiveTabViewIndex == 1)
        {
            // Verifying Operator is logged in, confirmed and so on...
            if (Operator.Current != null && Operator.Current.Confirmed && Operator.Current.AdminApproved)
            {
                headerInPlace.Text = header_read.InnerText;
                if (_article != null && richedit.Text == "")
                    richedit.Text = _article.Body;
            }
            else
            {
                tab.SetActiveTabViewIndex(0);
                warning.Text += "You must be logged in with a confirmed and approved user to edit wikis.";
            }
            warning.Visible = false;
        }
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

    protected void cancelLnk_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectFadeOut(pnlLnk, 0.4M);
        effect.Render();
    }

    protected void save_Click(object sender, EventArgs e)
    {
        if (_article == null)
        {
            // New article
            _article = new Article();
            _article.Created = DateTime.Now;
            string url = Request.Params["id"];
            _article.Url = url;
            _article.Header = headerInPlace.Text;
            _article.Body = richedit.Text;

            // Creating "content" for 1st revision
            ArticleRevision r = new ArticleRevision();
            r.Body = richedit.Text;
            r.Created = DateTime.Now;
            r.Header = headerInPlace.Text;
            r.Save();

            _article.Revisions.Add(r);
            _article.Save();
        }
        else
        {
            // Creating "content" for revision
            ArticleRevision r = new ArticleRevision();
            r.Body = richedit.Text;
            r.Created = DateTime.Now;
            r.Header = headerInPlace.Text;
            r.Save();

            _article.Revisions.Add(r);
            _article.Header = headerInPlace.Text;
            _article.Body = richedit.Text;
            _article.Save();
        }

        // Going to "preview mode"
        tab.SetActiveTabViewIndex(0);
        header_read.InnerText = headerInPlace.Text;
        content.Text = FormatContent(richedit.Text);
    }

    protected void template_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (template.SelectedItem.Value)
        {
            case "normal":
                richedit.Text = @"
<p><em>This is a small ingress which basically is to serve as an ""intro"" for your article.</em></p>
<h2>Paragraph header</h2>
<p>This is a paragraph</p>
<h2>Paragraph other header</h2>
<p>And this is another paragraph. Notice that in the Ra Wiki you really don't want to have an H1 element since the
article name will automatically become the H1 and all documents should have one and ONLY one H1 element.
So the above header(s) are H2 elements to serve as ""sub-headers"".</p>
<p>And by constraining the user options for the Rich Editing a bit we're actually able to construct perfectly valid XHTML markup!</p>
";
                break;
            case "list":
                richedit.Text = @"
<p><em>This is a small ingress which basically is to serve as an ""intro"" for your article.</em></p>
<ul>
    <li>First list item</li>
    <li>Second list item</li>
    <li>Third list item</li>
</ul>
<p>Explanation to above list</p>
<ol>
    <li>First list item</li>
    <li>Second list item</li>
    <li>Third list item</li>
</ol>
<p>Explanation to above list</p>
";
                break;
        }
        template.SelectedItem = template.Items[0];
    }
}
