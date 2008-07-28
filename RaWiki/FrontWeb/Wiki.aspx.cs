using System;
using Engine.Entities;
using NHibernate.Expression;
using Ra.Widgets;
using Ra;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Engine.Utilities;

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
                Title = _article.Header;
                header_read.InnerText = _article.Header;
                content.Text = FormatContent(_article.Body);
                content.Text += string.Format(@"
<p><em>Last changed; {0}</em></p>",
                    _article.Changed.ToString("dddd, dd MMM, yyyy", System.Globalization.CultureInfo.InvariantCulture));
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
            {
                cssClass = "non-existant";
                url += ".wiki?name=" + name;
            }
            else
            {
                url += ".wiki";
            }
            string urlElement = string.Format(@"<a{3} class=""{2}"" href=""{0}"">{1}</a>",
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
            // Normal "view mode"
            if (headerInPlace.Text.Length > 0 || richedit.Text.Length > 0)
            {
                header_read.InnerText = headerInPlace.Text;
                content.Text = FormatContent(richedit.Text);
                warning.Visible = true;
                warning.Text = "Remember to SAVE your articles when editing!";
            }
        }
        else if (tab.ActiveTabViewIndex == 1)
        {
            // Edit mode

            // Verifying Operator is logged in, confirmed and so on...
            if (Operator.Current != null && Operator.Current.Confirmed && Operator.Current.AdminApproved)
            {
                headerInPlace.Text = header_read.InnerText;
                if (_article != null && richedit.Text == "")
                    richedit.Text = _article.Body;
                warning.Visible = false;
            }
            else
            {
                tab.SetActiveTabViewIndex(0);
                warning.Text = "You must be logged in with a confirmed and approved user to edit wikis.";
                warning.Visible = true;
            }
        }
        else if (tab.ActiveTabViewIndex == 2)
        {
            // What links here
            warning.Visible = false;
            if (_article != null)
            {
                Article[] articles = Article.FindAll(Expression.Like("Body", "%[" + _article.Url + "%]%", MatchMode.Anywhere));
                repLinks.DataSource = articles;
                repLinks.DataBind();
            }
        }
        else if (tab.ActiveTabViewIndex == 3)
        {
            // Revisions
            if (_article != null)
            {
                // Sorting them in last in first out order
                List<ArticleRevision> revs = GetRevisions();
                repRevisions.DataSource = revs;
                repRevisions.DataBind();
            }
        }
    }

    private List<ArticleRevision> GetRevisions()
    {
        List<ArticleRevision> revs = new List<ArticleRevision>(_article.Revisions);
        revs.Sort(
            delegate(ArticleRevision left, ArticleRevision right)
            {
                return right.Created.CompareTo(left.Created);
            });
        return revs;
    }

    protected void RevisionClicked(object sender, EventArgs e)
    {
        // Finding Id of revision
        LinkButton btn = sender as LinkButton;
        HiddenField hid = btn.Parent.Controls[0] as HiddenField;
        if (hid == null)
            hid = btn.Parent.Controls[1] as HiddenField;
        int id = Int32.Parse(hid.Value);

        // Retrieving revision texts

        // Current revision
        List<ArticleRevision> revs = GetRevisions();
        string articleFirstContent = revs.Find(
            delegate(ArticleRevision rev)
            {
                return rev.Id == id;
            }).Body;

        // Older revision
        bool foundCurrent = false;
        string articleLastContent = "";
        ArticleRevision next = revs.Find(
            delegate(ArticleRevision rev)
            {
                if (foundCurrent)
                    return true;
                if (rev.Id == id)
                    foundCurrent = true;
                return false;
            });
        if (next != null)
            articleLastContent = next.Body;

        // Removing HTML formatting
        articleFirstContent = Regex.Replace(
            articleFirstContent,
            "(?<html><{1}[^>]+>{1})",
            "").Trim();

        articleLastContent = Regex.Replace(
            articleLastContent,
            "(?<html><{1}[^>]+>{1})",
            "").Trim();

        // Getting diff
        string diffContent = GetDiff(articleFirstContent, articleLastContent);

        litRevisions.Text = "<br/><br/><h2>Additions and deletions for revisions</h2><p><em>Note that all formatting is removed meaning that if there was only formatting changes to the revision no changes will show. Additions are green and deletions are red</em></p>" + diffContent;

        revView.SignalizeReRender();
    }

    protected void RevertToRevision(object sender, EventArgs e)
    {
        // Finding Id of revision
        Button btn = sender as Button;
        HiddenField hid = btn.Parent.Controls[0] as HiddenField;
        if (hid == null)
            hid = btn.Parent.Controls[1] as HiddenField;
        int id = Int32.Parse(hid.Value);

        // Retrieving revision texts

        // Current revision
        List<ArticleRevision> revs = GetRevisions();
        ArticleRevision revision = revs.Find(
            delegate(ArticleRevision rev)
            {
                return rev.Id == id;
            });
        ArticleRevision nRev = new ArticleRevision();
        nRev.Article = revision.Article;
        nRev.Body = revision.Body;
        nRev.Header = revision.Header;
        nRev.Operator = Operator.Current;
        nRev.Created = DateTime.Now;
        nRev.Article.Body = nRev.Body;
        nRev.Article.Header = nRev.Header;
        nRev.Save();
        nRev.Article.Save();

        string url = Request.Params["id"];
        _article = Article.FindOne(Expression.Eq("Url", url));
        header_read.InnerText = _article.Header;
        content.Text = FormatContent(_article.Body);

        revs = GetRevisions();
        repRevisions.DataSource = revs;
        repRevisions.DataBind();
        repRevisionsWrapper.SignalizeReRender();
    }

    private static string GetDiff(string articleFirstContent, string articleLastContent)
    {
        string diffContent = "";
        int[] a_codes = DiffCharCodes(articleFirstContent, false);
        int[] b_codes = DiffCharCodes(articleLastContent, false);
        Diff.Item[] diffs = Diff.DiffInt(a_codes, b_codes);

        int pos = 0;
        for (int n = 0; n < diffs.Length; n++)
        {
            Diff.Item it = diffs[n];

            // write unchanged chars
            while ((pos < it.StartB) && (pos < articleLastContent.Length))
            {
                diffContent += articleLastContent[pos];
                pos++;
            }

            // write deleted chars
            if (it.deletedA > 0)
            {
                diffContent += "<span class=\"cd\">";
                for (int m = 0; m < it.deletedA; m++)
                {
                    diffContent += articleFirstContent[it.StartA + m];
                }
                diffContent += "</span>";
            }

            // write inserted chars
            if (pos < it.StartB + it.insertedB)
            {
                diffContent += "<span class=\"ci\">";
                while (pos < it.StartB + it.insertedB)
                {
                    diffContent += articleLastContent[pos];
                    pos++;
                }
                diffContent += "</span>";
            }
        }

        // write rest of unchanged chars
        while (pos < articleLastContent.Length)
        {
            diffContent += articleLastContent[pos];
            pos++;
        }
        return diffContent;
    }

    private static int[] DiffCharCodes(string content, bool ignoreCase)
    {
        if (ignoreCase)
            content = content.ToUpperInvariant();

        int[] codes = new int[content.Length];

        for (int n = 0; n < content.Length; n++)
            codes[n] = (int)content[n];

        return codes;
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

    protected void formattingDDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        string content = richedit.Selection;
        if (string.IsNullOrEmpty(content))
            content = "content";
        switch (formattingDDL.SelectedItem.Value)
        {
            case "h2":
                richedit.Selection = "<h2>" + content + "</h2>";
                break;
            case "h3":
                richedit.Selection = "<h3>" + content + "</h3>";
                break;
            case "paragraph":
                richedit.Selection = "<p>" + content + "</p>";
                break;
            case "quote":
                richedit.Selection = "<blockquote>" + content + "</blockquote>";
                break;
        }
        formattingDDL.SelectedItem = formattingDDL.Items[0];
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

    protected void keepSessionTimer_Tick(object sender, EventArgs e)
    {
        // Just s dummy timer to make sure we NEVER get session timeout when
        // editing a wiki...!
        // Therefor we have a dummy timer on the Edit TabView whos only
        // purpose is to "tick" the server to update the session every
        // 120 seconds...
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
