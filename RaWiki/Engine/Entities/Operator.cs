using System;
using Castle.ActiveRecord;
using NHibernate.Expression;
using System.Web;

namespace Engine.Entities
{
    [ActiveRecord(Table = "Operators")]
    public class Operator : ActiveRecordBase<Operator>
    {
        private int _id;
        private string _username;
        private string _password;
        private bool _confirmed;

        [Property(Column = "UConfirmed")]
        public bool Confirmed
        {
            get { return _confirmed; }
            set { _confirmed = value; }
        }

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

        public static Operator Current
        {
            get { return HttpContext.Current.Session["__CurrentOperator"] as Operator; }
        }

        public static bool Login(string username, string password)
        {
            Operator oper = Operator.FindOne(
                Expression.Eq("Username", username),
                Expression.Eq("Password", password),
                Expression.Eq("Confirmed", true));
            HttpContext.Current.Session["__CurrentOperator"] = oper;
            return oper != null;
        }

        public static void Login(string username)
        {
            Operator oper = Operator.FindOne(
                Expression.Eq("Username", username));
            HttpContext.Current.Session["__CurrentOperator"] = oper;
        }

        public static void Logout()
        {
            HttpContext.Current.Session["__CurrentOperator"] = null;
        }
    }
}
