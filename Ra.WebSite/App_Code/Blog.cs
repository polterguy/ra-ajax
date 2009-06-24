/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using Castle.ActiveRecord;
using NHibernate.Expression;
using System.Web;

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
            // Checking to see if this is FIRST saving and if it is create a new friendly URL...
            if (Id == 0)
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
                int countOfOldWithSameURL = Blog.Count(Expression.Like("Url", Url + "%.blog"));
                if (countOfOldWithSameURL > 0)
                    Url += (countOfOldWithSameURL + 1).ToString();
                Url += ".blog";
                                
                // Replacing CR/LF with <br /> elements...
                Body = Body.Replace("\r\n", "<br />").Replace("\n", "<br />");
                base.Save();
            }
            else
            {
                // Replacing CR/LF with <br /> elements...
                Body = Body.Replace("\r\n", "<br />").Replace("\n", "<br />");
                base.Save();
            }
        }
    }
}



























