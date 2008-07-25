using System;
using Engine.Entities;
using NHibernate.Expression;

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
                // Create mode...
                headerInPlace.Text = Request.Params["name"];
                tab.ActiveTabViewIndex = 1;
            }
        }
    }
}
