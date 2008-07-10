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
            oper.Username = "admin";
            oper.Pwd = "admin";
            oper.Email = "someone@somewhere.com";
            oper.IsAdmin = true;
            oper.Confirmed = true;
            oper.Signature = "Here's your signature";
            oper.Create();

            // Creating default forum post
            Entity.ForumPost defPost = new Entity.ForumPost();
            defPost.Body = "This is a sample forum post";
            defPost.Url = "default-post-for-forums.forum";
            defPost.Created = DateTime.Now;
            defPost.Header = "Default post";
            defPost.Operator = oper;
            defPost.Create();
        }
    }
    
    void Application_End(object sender, EventArgs e) 
    {
    }
        
    void Application_Error(object sender, EventArgs e) 
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
