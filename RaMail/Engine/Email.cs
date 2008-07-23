using System;
using Castle.ActiveRecord;
using NHibernate.Expression;
using System.Web;

namespace Engine.Entities
{
    [ActiveRecord(Table="Emails")]
    public class Email : ActiveRecordBase<Email>
    {
        private int _id;
        private string _header;
        private string _body;
        private string _from;
        private Account _account;
        private DateTime _created;

        [Property]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        [BelongsTo]
        public Account Account
        {
            get { return _account; }
            set { _account = value; }
        }

        [Property(Column="FromEmail")]
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        [Property]
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        [Property]
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        [PrimaryKey]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
