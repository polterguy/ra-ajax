using System;
using Castle.ActiveRecord;
using NHibernate.Expression;

namespace Engine.Entities
{
    [ActiveRecord(Table = "Operators")]
    public class Operator : ActiveRecordBase<Operator>
    {
        private int _id;
        private string _username;
        private string _password;

        [Property(Column="UPassword")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [Property(Column = "UUsername")]
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        [PrimaryKey]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public static int GetCount()
        {
            return Count();
        }
    }
}
