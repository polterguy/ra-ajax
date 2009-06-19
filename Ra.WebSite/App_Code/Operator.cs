/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;
using Castle.ActiveRecord;
using System.Web;
using NHibernate.Expression;
using System.Configuration;
using System.Net.Mail;
using System.Net;

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
            get { return HttpContext.Current.Session["__CurrentOperator"] as Operator; }
        }

        public static bool Login(string username, string password)
        {
            Operator oper = Operator.FindOne(
                Expression.Eq("Username", username), 
                Expression.Eq("Pwd", password),
                Expression.Eq("Confirmed", true));
            HttpContext.Current.Session["__CurrentOperator"] = oper;
            return oper != null;
        }

        public void SendEmail(string subject, string body)
        {
            MailMessage msg = new MailMessage();
            msg.Subject = subject;
            msg.Body = body;
            msg.To.Add(new MailAddress(this.Email));
            msg.From = new MailAddress(ConfigurationManager.AppSettings["fromEmailAddress"]);
            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"]);
            smtp.Port = Int32.Parse(ConfigurationManager.AppSettings["smtpServerPort"]);
            
            string userName = ConfigurationManager.AppSettings["smtpServerUserName"];
            if (!string.IsNullOrEmpty(userName))
            {
                smtp.Credentials = new NetworkCredential(userName,
                    ConfigurationManager.AppSettings["smtpServerPassword"]);
            }
            smtp.Send(msg);
        }

        public override void Save()
        {
            if (Id == 0)
                Created = DateTime.Now;
            base.Save();
        }

        public static void Logout()
        {
            HttpContext.Current.Session["__CurrentOperator"] = null;
        }

        public static void Login(string username)
        {
            if (username == "__mumbo__jumbo")
                return;
            Operator oper = Operator.FindOne(
                Expression.Eq("Username", username));
            HttpContext.Current.Session["__CurrentOperator"] = oper;
        }
    }
}



























