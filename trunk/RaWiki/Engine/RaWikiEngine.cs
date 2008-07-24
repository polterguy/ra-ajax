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

                Operator oper = new Operator();
                oper.Username = "admin";
                oper.Password = "admin";
                oper.Save();
            }
        }
    }
}
