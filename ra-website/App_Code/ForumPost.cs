using System;
using Castle.ActiveRecord;
using NHibernate.Expression;
using System.Text.RegularExpressions;
using System.Web;
using System.Collections.Generic;

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

        // Note that all reference documentation on ActiveRecord says we should use NTEXT but doesn't mention
        // that MySQL's name for the same DbType is "TEXT" and NOT NTEXT...!
        // NTEXT as SqlType will throw exception when using MySQL
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
            if (Id == 0)
            {
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
                Url = Url.Trim('-');
                bool found = true;
                while (found)
                {
                    found = false;
                    if (Url.IndexOf("--") != -1)
                    {
                        Url = Url.Replace("--", "-");
                        found = true;
                    }
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

                // Sending email to operators
                List<Entity.Operator> opers = new List<Operator>();
                opers.AddRange(Entity.Operator.FindAll(Expression.Eq("IsAdmin", true)));
                string baseUrl = HttpContext.Current.Request.Url.ToString();
                baseUrl = baseUrl.Substring(0, baseUrl.LastIndexOf("/") + 1);
                if (ParentPost != 0)
                {
                    ForumPost post = ForumPost.FindFirst(Expression.Eq("Id", ParentPost));
                    if (!post.Operator.IsAdmin)
                        opers.Add(post.Operator);
                    baseUrl += post.Url;
                }
                else
                    baseUrl += Url;
                System.Threading.ParameterizedThreadStart start = new System.Threading.ParameterizedThreadStart(this.SendNotificationEmail);
                System.Threading.Thread tr = new System.Threading.Thread(start);
                tr.Start(new object[] { opers, baseUrl });
            }

            // Replacing CR/LF with <br /> elements...
            Body = Body.Replace("\r\n", "<br />").Replace("\n", "<br />");
            base.Save();
        }

        private void SendNotificationEmail(object pars)
        {
            try
            {
                object[] arrs = pars as object[];
                List<Operator> opers = arrs[0] as List<Operator>;
                string baseUrl = arrs[1] as string;
                foreach (Operator idx in opers)
                {
                    idx.SendEmail("New Forum post at ra-ajax.org",
                        string.Format(@"A new post has been posted at; {0} named {1}",
                        baseUrl,
                        Header));
                }
            }
            catch (Exception)
            {
                ; // Do nothing to not shut down server...!
                // TODO: Log and send me an alert email or something?
            }
        }
    }
}






























