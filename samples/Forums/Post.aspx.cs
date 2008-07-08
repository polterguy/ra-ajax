using System;
using Entity;
using NHibernate.Expression;
using Ra.Widgets;

public partial class Forums_Post : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Retrieving post...
            string idOfPost = Request.Params["id"] + ".forum";
            ForumPost post = ForumPost.FindOne(Expression.Eq("Url", idOfPost));
            headerParent.InnerHtml = post.Header;
            dateParent.InnerHtml = post.Created.ToString("dd.MMM yy - HH:mm");
            contentParent.InnerHtml = post.Body;
            this.Title = post.Header;
            header.Text = "Re: " + post.Header;
            header.Focus();
            header.Select();

            // Binding replies...
            DataBindReplies(post);
            if (Operator.Current != null)
            {
                pnlReply.Visible = true;
            }
            else
            {
                pnlReply.Visible = false;
            }
        }
    }

    private void DataBindReplies(ForumPost post)
    {
        repReplies.DataSource = ForumPost.FindAll(Expression.Eq("ParentPost", post.Id));
        repReplies.DataBind();
    }

    protected void newSubmit_Click(object sender, EventArgs e)
    {
        // Simple valdation
        if (header.Text.Length < 5)
            return;

        // Creating new post
        ForumPost post = new ForumPost();
        post.Body = body.Text.Replace("\r\n", "<br />").Replace("\n", "<br />");
        post.Created = DateTime.Now;
        post.Header = header.Text;
        post.Operator = Operator.Current;

        string idOfPost = Request.Params["id"] + ".forum";
        ForumPost parent = ForumPost.FindOne(Expression.Eq("Url", idOfPost));
        post.ParentPost = parent.Id;
        
        post.Save();

        // Flashing panel
        Effect effect = new EffectFadeIn(pnlReply, 0.4M);
        effect.Render();

        effect = new EffectFadeIn(postsWrapper, 0.4M);
        effect.Render();
        body.Focus();
        body.Select();

        // Re-rendering posts to get the newly added item
        DataBindReplies(parent);
        postsWrapper.SignalizeReRender();
    }
}
