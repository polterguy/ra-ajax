<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Setting number of viewers to ZERO
        Entity.Operator.ViewersCount = 0;
        
        // Initializing Castle
        Castle.ActiveRecord.ActiveRecordStarter.Initialize(
            Castle.ActiveRecord.Framework.Config.ActiveRecordSectionHandler.Instance,
            new Type[] { 
                typeof(Entity.ForumPost),
                typeof(Entity.Blog),
                typeof(Entity.Todo),
                typeof(Entity.Operator)
            });

        try
        {
            // This one will throw an exception if the schema is incorrect or not created...
            int dummyToCheckIfDataBaseHasSchema = Entity.Operator.GetCount();
        }
        catch
        {
            // Letting ActiveRecord create our schema since the schema was obviously NOT correct
            // or created (warning; this logic might corrupt your database if you do changes to 
            // the schema)
            Castle.ActiveRecord.ActiveRecordStarter.CreateSchema();

            // Creating default operator
            Entity.Operator oper = new Entity.Operator();
            oper.Username = "thomas";
            oper.Pwd = "thomas";
            oper.Email = "polterguy@gmail.com";
            oper.IsAdmin = true;
            oper.Confirmed = true;
            oper.Signature = "Here's your signature";
            oper.IsBlogger = true;
            oper.Created = DateTime.Now;
            oper.Create();

            // Creating default forum post
            Entity.ForumPost defPost = new Entity.ForumPost();
            defPost.Body = "This is a sample forum post";
            defPost.Url = "default-post-for-forums.forum";
            defPost.Created = DateTime.Now;
            defPost.Header = "Default post";
            defPost.Operator = oper;
            defPost.Create();
            
            // Creating default blog
            Entity.Blog blog = new Entity.Blog();
            blog.Operator = oper;
            blog.Created = DateTime.Now;
            blog.Header = "Default blog";
            blog.Url = "default-blog.blog";
            blog.Body = "Howdy, this is a blog";
            blog.Create();
        }
    }
    
    void Application_End(object sender, EventArgs e) 
    {
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
    }

    void Application_EndRequest(object sender, EventArgs e)
    { 
    }

    void Session_Start(object sender, EventArgs e) 
    {
        Entity.Operator.ViewersCount += 1;
    }

    void Session_End(object sender, EventArgs e) 
    {
        Entity.Operator.ViewersCount -= 1;
    }
       
</script>
