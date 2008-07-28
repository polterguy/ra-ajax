using System;
using Castle.ActiveRecord;
using System.Collections.Generic;
using NHibernate.Expression;
using System.Collections;

namespace Engine.Entities
{
    [ActiveRecord(Table = "Articles")]
    public class Article : ActiveRecordBase<Article>
    {
        private int _id;
        private string _url;
        private DateTime _created;
        private IList _revisions = new ArrayList();
        private string _header;
        private string _body;
        private DateTime _changed;

        [Property]
        public DateTime Changed
        {
            get { return _changed; }
            set { _changed = value; }
        }

        [HasMany(typeof(ArticleRevision), Cascade=ManyRelationCascadeEnum.All)]
        public IList Revisions
        {
            get { return _revisions; }
            set { _revisions = value; }
        }

        [Property]
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        [Property(Unique=true)]
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        [PrimaryKey]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [Property]
        public string Header
        {
            get
            {
                if (string.IsNullOrEmpty(_header))
                    return "[null]";
                return _header;
            }
            set { _header = value; }
        }

        [Property(ColumnType = "StringClob", SqlType = "TEXT")]
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        public static int GetCount()
        {
            return Count();
        }

        public static string CreateUniqueFriendlyURL(string from)
        {
            // All URLs are lowercase
            string url = from.ToLower();

            // Not longer than 100 characters long
            if (url.Length > 100)
                url = url.Substring(0, 100);

            // Checking for anything else than alphanumeric characters
            int index = 0;
            while (index < url.Length)
            {
                if (("abcdefghijklmnopqrstuvwxyz0123456789").IndexOf(url[index]) == -1)
                {
                    url = url.Substring(0, index) + "-" + url.Substring(index + 1);
                }
                index += 1;
            }

            // Trimming away excessive leadin and trailing "-"
            url = url.Trim('-');

            // Trimming away all DOUBLE, TRIPLE and so on "--" from Url
            while (true)
            {
                if (url.IndexOf("--") == -1)
                    break;
                url = url.Replace("--", "-");
            }

            // Checking uniqueness, and if not unique appending a number to the URL
            // Note this also works with DELETED articles meaning it'll pick the FIRST available number to append
            // if an article in the middle of the "list of same article URLs" was deleted
            List<Article> articlesWithSameName = new List<Article>(
                Article.FindAll(Expression.Like("Url", url, MatchMode.Start)));
            int idxFree = 1;
            if (articlesWithSameName != null && articlesWithSameName.Count != 0)
            {
                foreach (Article idx in articlesWithSameName)
                {
                    if (idxFree == 1)
                    {
                        idxFree += 1;
                        continue;
                    }
                    if (url + idxFree.ToString() == idx.Url)
                    {
                        idxFree += 1;
                        continue;
                    }

                    // If none of the above ifs kicked in we've got a unique number for our article
                    break;
                }
                url += idxFree.ToString();
            }
            return url;
        }

        public override void Save()
        {
            Changed = DateTime.Now;
            base.Save();
        }
    }
}
