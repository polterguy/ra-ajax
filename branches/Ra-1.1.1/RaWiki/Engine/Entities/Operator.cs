using System;
using Castle.ActiveRecord;
using NHibernate.Expression;
using System.Web;
using System.Web.Mail;
using System.Configuration;

namespace Engine.Entities
{
    [ActiveRecord(Table = "Operators")]
    public class Operator : ActiveRecordBase<Operator>
    {
        private int _id;
        private string _username;
        private string _password;
        private bool _confirmed;
        private bool _isAdmin;
        private string _email;
        private bool _adminApproved;
        private DateTime _created;

        public static event EventHandler LoggedIn;

        public static event EventHandler LoggedOut;

        [Property]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        [Property]
        public bool AdminApproved
        {
            get { return _adminApproved; }
            set { _adminApproved = value; }
        }

        [Property]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [Property]
        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }

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

        [Property(Column = "UUsername", Unique=true)]
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
            get { return HttpContext.Current.Session == null ? null : HttpContext.Current.Session["__CurrentOperator"] as Operator; }
        }

        public static bool Login(string username, string password)
        {
            Operator oper = Operator.FindOne(
                Expression.Eq("Username", username),
                Expression.Eq("Password", password),
                Expression.Eq("AdminApproved", true),
                Expression.Eq("Confirmed", true));
            HttpContext.Current.Session["__CurrentOperator"] = oper;

            if (oper != null)
                if (LoggedIn != null)
                    LoggedIn(typeof(Operator), new EventArgs());

            return oper != null;
        }

        public static void Login(string username)
        {
            Operator oper = Operator.FindOne(
                Expression.Eq("Username", username));
            HttpContext.Current.Session["__CurrentOperator"] = oper;
            if (oper != null)
                if (LoggedIn != null)
                    LoggedIn(typeof(Operator), new EventArgs());
        }

        public static void Logout()
        {
            HttpContext.Current.Session["__CurrentOperator"] = null;
            if (LoggedOut != null)
                LoggedOut(typeof(Operator), new EventArgs());
        }

        public void Register()
        {
            this.Save();
            string confirmUrl = HttpContext.Current.Request.Url.ToString();
            confirmUrl = confirmUrl.Substring(0, confirmUrl.LastIndexOf("/") + 1) + "Admin.aspx";
            string header = string.Format(@"Welcome as a new Ra Wiki user {0}", Username);
            string body = string.Format(
@"This message was automatically sent from the forums at {0} due to registering a new user.
If you where not the one registering at Ra-Ajax then please just ignore this message or delete it.

To confirm your registration and activate your user account please go to;
{2}?idNewUserConfirmation={0}&seed={3}

Your username is; {0}
Your password is; {1}

Please keep this email for future references since it contains your username and password for your account.

Note that when you create an article and other users edit it you will get a notification email so that you can track
the articles you create automagically. Also when you edit articles in the Ra Wiki all administrators in the system
will get notification emails. Both of these settings are configurable however and MIGHT be turned OFF for the specific
wiki you're registering at.

Ra Wiki is created with Ra Ajax which can be found at http://ra-ajax.org while Ra Wiki itself can be found at http://ra-wiki.org

Have a nice day :)
", 
                Username,
                Password,
                confirmUrl,
                (Username + Password).GetHashCode());
            SendEmail(header, body);
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

        public static Operator ApproveNewUser(string username, string seed)
        {
            if (Operator.Current == null || !Operator.Current.IsAdmin)
            {
                throw new ArgumentException("You must be logged in as an ADMIN to approve new users");
            }
            Operator oper = Operator.FindOne(Expression.Eq("Username", username));
            if (oper == null)
                throw new ArgumentException("No such user");
            if (seed != (oper.Username + oper.Password).GetHashCode().ToString())
                throw new ArgumentException("Incorrect seed");
            if (oper.AdminApproved)
                return oper;

            oper.AdminApproved = true;
            oper.Save();
            string confirmUrl = HttpContext.Current.Request.Url.ToString();
            confirmUrl = confirmUrl.Substring(0, confirmUrl.LastIndexOf("/") + 1);
            oper.SendEmail("You have been approved at the Ra Wiki", string.Format(@"
Admin {0} have approved you for login at the Ra Wiki, welcome :)
You can now start editing and creating articles at {1}", 
                Operator.Current.Username,
                confirmUrl));
            return oper;
        }

        public static Operator ConfirmNewUser(string username, string seed)
        {
            Operator oper = Operator.FindOne(Expression.Eq("Username", username));
            if (oper == null)
                throw new ArgumentException("No such user");
            if( seed != (oper.Username + oper.Password).GetHashCode().ToString())
                throw new ArgumentException("Incorrect seed");

            // User was already confirmed, returning so that we don't keep on sending admins new confirm emails....
            if (oper.Confirmed)
                return oper;
            oper.Confirmed = true;
            bool autoApproved = ConfigurationSettings.AppSettings["autoApproveNewUsers"] == "true";
            if (autoApproved)
            {
                oper.AdminApproved = true;
            }
            else
            {
                string confirmUrl = HttpContext.Current.Request.Url.ToString();
                confirmUrl = confirmUrl.Substring(0, confirmUrl.LastIndexOf("/") + 1) + "Admin.aspx";
                foreach (Operator idx in Operator.FindAll(Expression.Eq("IsAdmin", true)))
                {
                    string subject = string.Format("Please approve {0} for login at the Ra Wiki", oper.Username);
                    string body = string.Format(@"
To approve user {0} to login to your wiki please click this link; 
{1}?approveUser={0}&seed={2}
", 
                        oper.Username,
                        confirmUrl,
                        (oper.Username + oper.Password).GetHashCode());
                    idx.SendEmail(subject, body);
                }
            }
            oper.Save();
            return oper;
        }

        public override void Save()
        {
            if (Id == 0)
                Created = DateTime.Now;
            base.Save();
        }
    }
}
