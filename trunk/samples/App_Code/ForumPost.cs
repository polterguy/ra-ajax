using System;
using Castle.ActiveRecord;

namespace Entity
{
    [ActiveRecord(Table="ForumPosts")]
    public class ForumPost : ActiveRecordBase<ForumPost>
    {
        private int _id;
        private DateTime _created;
        private string _header;
        private string _body;
        private Operator _operator;

        [BelongsTo]
        public Operator Operator
        {
            get { return _operator; }
            set { _operator = value; }
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

        [Property]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
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
