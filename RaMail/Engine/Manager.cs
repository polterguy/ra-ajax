using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public sealed class Manager
    {
        private static Manager _instance = new Manager();

        public static Manager Instance
        {
            get { return _instance; }
        }

        private Manager()
        { }

        private bool _hasBeenInitialized;
        public void Initialize()
        {
            if (_hasBeenInitialized)
                return;
            _hasBeenInitialized = true;

            Castle.ActiveRecord.ActiveRecordStarter.Initialize(
                Castle.ActiveRecord.Framework.Config.ActiveRecordSectionHandler.Instance,
                new Type[] { 
                    typeof(Entities.Operator),
                    typeof(Entities.Email),
                    typeof(Entities.Account)
                });

            try
            {
                // This one will throw an exception if the schema is incorrect or not created...
                int dummyToCheckIfDataBaseHasSchema = Entities.Operator.GetCount();
            }
            catch
            {
                // Letting ActiveRecord create our schema since the schema was obviously NOT correct
                // or created (warning; this logic might corrupt your database if you do changes to 
                // the schema)
                Castle.ActiveRecord.ActiveRecordStarter.CreateSchema();

                // Creating default operator
                Entities.Operator oper = new Engine.Entities.Operator();
                oper.Username = "admin";
                oper.Password = "admin";
                oper.Save();
            }
        }
    }
}
