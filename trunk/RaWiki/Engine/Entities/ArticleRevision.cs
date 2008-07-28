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
        private Article _article;
        private Operator _operator;

        [BelongsTo]
        public Operator Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        [BelongsTo]
        public Article Article
        {
            get { return _article; }
            set { _article = value; }
        }

        [Property]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        [Property(ColumnType = "StringClob", SqlType = "TEXT")]
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

        public override void Save()
        {
            Operator = Engine.Entities.Operator.Current;
            base.Save();
        }
    }
}
