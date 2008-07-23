using System;
using Castle.ActiveRecord;
using System.Web;
using NHibernate.Expression;
using System.Configuration;
using System.Web.Mail;

namespace Entity
{
    [ActiveRecord(Table="Operators")]
    public class Operator : ActiveRecordBase<Operator>
    {
        private int _id;
        private string _userName;
        private string _pwd;
        private string _email;
        private bool _confirmed;
        private bool _isAdmin;
        private string _signature;
        private bool _isBlogger;
        private DateTime _created;
        private static int _viewersCount;

        public static int ViewersCount
        {
            get { return _viewersCount; }
            set { _viewersCount = value; }
        }

        [Property]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        [Property]
        public bool IsBlogger
        {
            get { return _isBlogger; }
            set { _isBlogger = value; }
        }

        [Property]
        public string Signature
        {
            get { return _signature; }
            set { _signature = value; }
        }

        [Property]
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }

        [Property]
        public bool Confirmed
        {
            get { return _confirmed; }
            set { _confirmed = value; }
        }

        [Property]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [Property]
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        [Property(Unique=true)]
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

        public void SendEmail(string subject, string body)
        {
            MailMessage msg = new MailMessage();
            msg.Subject = subject;
            msg.Body = body;
            msg.To = this.Email;
            msg.From = ConfigurationSettings.AppSettings["fromEmailAddress"];
            SmtpMail.SmtpServer = ConfigurationSettings.AppSettings["smtpServer"];
            SmtpMail.Send(msg);
        }

        public int NumberOfPosts
        {
            get
            {
                return ForumPost.GetCount(Expression.Eq("Operator", this));
            }
        }

        public override void Save()
        {
            if (Id == 0)
                Created = DateTime.Now;
            base.Save();
        }

        public static void Login(string username)
        {
            Operator oper = Operator.FindOne(
                Expression.Eq("Username", username));
            HttpContext.Current.Session["__CurrentOperator"] = oper;
        }
    }
}



























