using System;
using Castle.ActiveRecord;
using NHibernate.Expression;

namespace Entity
{
    [ActiveRecord(Table="Blogs")]
    public class Blog : ActiveRecordBase<Blog>
    {
        private int _id;
        private DateTime _created;
        private Operator _operator;
        private string _header;
        private string _body;
        private string _url;

        [Property(Unique=true)]
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        [Property(ColumnType="StringClob")]
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

        [BelongsTo]
        public Operator Operator
        {
            get { return _operator; }
            set { _operator = value; }
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

        public override void Save()
        {
            // Building UNIQUE friendly URL
            Url = Header.ToLower();
            if (Url.Length > 100)
                Url = Url.Substring(0, 100);
            int index = 0;
            while (index < Url.Length)
            {
                if (("abcdefghijklmnopqrstuvwxyz0123456789").IndexOf(Url[index]) == -1)
                {
                    Url = Url.Substring(0, index) + "-" + Url.Substring(index + 1);
                }
                index += 1;
            }
            int countOfOldWithSameURL = ForumPost.Count(Expression.Like("Url", Url + "%.blog"));
            if (countOfOldWithSameURL > 0)
                Url += (countOfOldWithSameURL + 1).ToString();
            Url += ".blog";

            // Replacing CR/LF with <br /> elements...
            Body = Body.Replace("\r\n", "<br />").Replace("\n", "<br />");
            base.Save();
        }
    }
}



























