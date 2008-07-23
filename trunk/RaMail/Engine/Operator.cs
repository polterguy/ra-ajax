using System;
using Castle.ActiveRecord;
using NHibernate.Expression;
using System.Web;

namespace Engine.Entities
{
    [ActiveRecord(Table="Operators")]
    public class Operator : ActiveRecordBase<Operator>
    {
        private int _id;
        private string _username;
        private string _password;

        [Property(Column = "UPwd")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [Property(Column="UName")]
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        [PrimaryKey]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public static Operator Current
        {
            get { return HttpContext.Current.Session["__operator"] as Operator; }
            set { HttpContext.Current.Session["__operator"] = value; }
        }

        public static int GetCount()
        {
            return Count();
        }

        public static bool Login(string username, string password)
        {
            Operator oper = Operator.FindOne(
                Expression.Eq("Username", username), 
                Expression.Eq("Password", password));
            Current = oper;
            return oper != null;
        }
    }
}
