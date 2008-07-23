using System;
using Castle.ActiveRecord;
using NHibernate.Expression;
using System.Web;
using Indy.Sockets;

namespace Engine.Entities
{
    [ActiveRecord(Table="Accounts")]
    public class Account : ActiveRecordBase<Account>
    {
        private int _id;
        private string _pop3server;
        private string _username;
        private string _password;
        private Operator _operator;

        [BelongsTo]
        public Operator Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

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

        [Property]
        public string Pop3Server
        {
            get { return _pop3server; }
            set { _pop3server = value; }
        }

        [PrimaryKey]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public void CheckMail()
        {
            using (POP3 pop = new POP3())
            {
                pop.Username = Username;
                pop.Password = Password;
                pop.Connect(Pop3Server);
                int count = pop.CheckMessages();
                for (int idx = 0; idx < count; idx++)
                {
                    Message msg = new Message();
                    pop.Retrieve(1, msg);
                    Email m = new Email();
                    m.Account = this;
                    m.Body = msg.Body.GetText();
                    m.Header = msg.Subject;
                    m.From = msg.From.Address;
                    m.Created = DateTime.Now;
                    m.Save();
                }
            }
        }
    }
}
