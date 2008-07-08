using System;
using Castle.ActiveRecord;
using NHibernate.Expression;

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
        private string _url;

        [Property]
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

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

        public override void Save()
        {
            Url = Header.ToLower();
            int index = 0;
            while (index < Url.Length)
            {
                if (("abcdefghijklmnopqrstuvwxyz0123456789").IndexOf(Url[index]) == -1)
                {
                    Url = Url.Substring(0, index) + "-" + Url.Substring(index + 1);
                }
                index += 1;
            }
            int countOfOldWithSameURL = ForumPost.Count(Expression.Eq("Url", Url));
            if (countOfOldWithSameURL > 0)
                Url += countOfOldWithSameURL.ToString();
            base.Save();
        }
    }
}






























