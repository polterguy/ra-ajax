using System;
using Castle.ActiveRecord;

namespace Engine.Entities
{
    [ActiveRecord(Table = "ArticleRevisions")]
    public class ArticleRevision : ActiveRecordBase<ArticleRevision>
    {
        private int _id;
        private string _header;
        private string _body;
        private DateTime _created;

        [Property]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
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
