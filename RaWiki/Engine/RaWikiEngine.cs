using System;
using Engine.Entities;

namespace Engine
{
    public sealed class RaWikiEngine
    {
        private static RaWikiEngine _instance = new RaWikiEngine();

        public static RaWikiEngine Instance
        {
            get { return _instance; }
        }

        private bool _hasBeenInitialized;
        public void Initialize()
        {
            if (_hasBeenInitialized)
                return;
            _hasBeenInitialized = true;

            // Initializing Castle
            Castle.ActiveRecord.ActiveRecordStarter.Initialize(
                Castle.ActiveRecord.Framework.Config.ActiveRecordSectionHandler.Instance,
                new Type[] { 
                    typeof(Article),
                    typeof(ArticleRevision),
                    typeof(Operator)
            });

            try
            {
                // This one will throw an exception if the schema is incorrect or not created...
                int dummyToCheckIfDataBaseHasSchema = Operator.GetCount();
            }
            catch
            {
                // Letting ActiveRecord create our schema since the schema was obviously NOT correct
                // or created (warning; this logic might corrupt your database if you do changes to 
                // the schema)
                Castle.ActiveRecord.ActiveRecordStarter.CreateSchema();

                // Creating default operator
                Operator oper = new Operator();
                oper.Username = "admin";
                oper.Password = "admin";
                oper.Confirmed = true;
                oper.AdminApproved = true; // Default user obviously must be auto admin approved...
                oper.IsAdmin = true;
                oper.Created = DateTime.Now;
                oper.SaveAndFlush();

                // Creating default article
                Article a = new Article();
                a.Body = @"
This id the default article created for you by the system, also a default user have been created with 
the username of ""admin"" and the password of ""admin"". Needless to say both should be changed as 
soon as possible.
";
                a.Changed = DateTime.Now;
                a.Created = DateTime.Now;
                a.Header = "Welcome to the Ra Wiki system";
                a.SiteWide = false;
                a.Url = "default";
                ArticleRevision r = new ArticleRevision();
                r.Article = a;
                r.Body = a.Body;
                r.Created = a.Created;
                r.Header = a.Header;
                r.Operator = oper;
                a.Revisions.Add(r);
                a.Save();
                r.Save();
            }
        }
    }
}
