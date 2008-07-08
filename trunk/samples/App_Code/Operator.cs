using System;
using Castle.ActiveRecord;
using System.Web;
using NHibernate.Expression;

namespace Entity
{
    [ActiveRecord(Table="Operators")]
    public class Operator : ActiveRecordBase<Operator>
    {
        private int _id;
        private string _userName;
        private string _pwd;

        [Property]
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        [Property]
        public string Username
        {
            get { return _userName; }
            set { _userName = value; }
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
            get
            {
                return HttpContext.Current.Session["__CurrentOperator"] as Operator;
            }
        }

        public static bool Login(string username, string password)
        {
            Operator oper = Operator.FindOne(
                Expression.Eq("Username", username), 
                Expression.Eq("Pwd", password));
            HttpContext.Current.Session["__CurrentOperator"] = oper;
            return oper != null;
        }
    }
}



























