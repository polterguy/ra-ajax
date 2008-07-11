using System;
using Castle.ActiveRecord;
using NHibernate.Expression;
using System.Text.RegularExpressions;

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
        private int _parentPost;
        private int _noReplies = -1;

        public int NoReplies
        {
            get
            {
                if (_noReplies == -1)
                {
                    _noReplies = Count(Expression.Eq("ParentPost", Id));
                }
                return _noReplies;
            }
        }

        [Property]
        public int ParentPost
        {
            get { return _parentPost; }
            set { _parentPost = value; }
        }

        [Property(Unique=true)]
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

        [Property(ColumnType = "StringClob", SqlType = "TEXT")]
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        [Property(Length=150)]
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

        public static int GetCount(params ICriterion[] criteria)
        {
            return Count(criteria);
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
            int countOfOldWithSameURL = ForumPost.Count(Expression.Like("Url", Url + "%.forum"));
            if (countOfOldWithSameURL > 0)
                Url += (countOfOldWithSameURL + 1).ToString();
            Url += ".forum";

            // Replacing URLs with a href links...
            Body = Regex.Replace(
                " " + Body,
                "(?<spaceChar>\\s+)(?<linkType>http://|https://)(?<link>\\S+)", 
                "${spaceChar}<a href=\"${linkType}${link}\" rel=\"nofollow\">${link}</a>").Trim(); 

            // Replacing CR/LF with <br /> elements...
            Body = Body.Replace("\r\n", "<br />").Replace("\n", "<br />");
            base.Save();
        }
    }
}






























