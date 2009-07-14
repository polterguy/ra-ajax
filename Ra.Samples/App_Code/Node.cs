/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using System.Web;
using System.Collections.Generic;


namespace Entities
{
    public class Node
    {
        private List<Node> _children = new List<Node>();

        public Node(string title, string body, DateTime created, string username, params Node[] children)
        {
            Title = title;
            ID = Guid.NewGuid();
            Body = body;
            Created = created;
            Username = username;
            Children.AddRange(children);
        }

        public List<Node> Children
        {
            get { return _children; }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private Guid _id;
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _body;
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        private DateTime _created;
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public static List<Node> Nodes
        {
            get
            {
                if (HttpContext.Current.Application["__Nodes"] == null)
                {
                    HttpContext.Current.Application["__Nodes"] = CreateNodes();
                }
                return (List<Node>)HttpContext.Current.Application["__Nodes"];
            }
        }

        private static Node Find(Node from, Guid id)
        {
            if (from.ID == id)
                return from;
            foreach (Node idx in from.Children)
            {
                Node tmp = Find(idx, id);
                if (tmp != null)
                    return tmp;
            }
            return null;
        }

        public static Node Find(Guid id)
        {
            foreach (Node idx in Nodes)
            {
                Node tmp = Find(idx, id);
                if (tmp != null)
                    return tmp;
            }
            return null;
        }

        private static List<Node> CreateNodes()
        {
            List<Node> retVal = new List<Node>();
            retVal.Add(
                new Node(
                    "This is a questions",
                    "I am wondering about the meaning of life",
                    new DateTime(2007, 11, 28),
                    "Thomas Hansen",
                    new Node(
                        "And this is the answer to the above question",
                        "The thing about questions is that normally the 'Meaning of life question' is too hard to answer without asking Thomas for help... ;)<br/>Submit your question at http://stacked.ra-ajax.org and he'll probably be able to help you ;)",
                        new DateTime(2008, 01, 11),
                        "Average Joe")));
            retVal.Add(
                new Node(
                    "A wonder about something",
                    "What is the purpose of this codeproject forum viewer?",
                    new DateTime(2008, 02, 08),
                    "Some Guy",
                    new Node(
                        "The purpose is to do cool stuff",
                        "With it you can easily create a forum like the forum over at the codeproject.com website",
                        new DateTime(2008, 03, 02),
                        "Creator of forum"),
                    new Node(
                        "Yes, cool stuff indeed...!",
                        "Though if you think you need a forum, you mostly DON'T - try out http://stacked.ra-ajax.org instead which mostly is able to solve your 'forums needs'...",
                        new DateTime(2008, 03, 02),
                        "Creator of forum",
                            new Node(
                            "REALLY, REALLY long answer with LOTS of HTML",
                            @"
<h1>LONG answer...!!</h1>
<p>
    This is just an example of that it is possible to put 
<span style=""color:Red;"">H</span>
<span style=""color:Green;"">T</span>
<span style=""color:Purple;"">M</span>
<span style=""color:Orange;"">L</span>
    inside of your 'Body content'.
</p>
<p>
    Even paragraphs and <a href=""http://stacked.ra-ajax.org"">links to Stacked</a> etc...
</p>
<h2>And even HEADERS...</h2>
<p>
    <em>as you can see...</em>
</p>
",
                            DateTime.Now.AddHours(-3),
                            "Creator of forum")
                        ),
                    new Node(
                        "To see forum postings in a relational manner?",
                        "Yoohooooooo........!!",
                        DateTime.Now.AddMinutes(-3),
                        "Some other dude...")
                        ));
            retVal.Add(
                new Node(
                    "How do I do xxx",
                    "I am having troubles with doing xxx with this forum...",
                    new DateTime(2008, 05, 11),
                    "Some user of Forum",
                    new Node(
                        "You should ask your question at Stacked",
                        "http://stacked.ra-ajax.org",
                        DateTime.Now.AddDays(-3),
                        "Thomas Hansen")));
            retVal.Add(
                new Node(
                    "Will this forum work for all browsers?",
                    "I am wondering if this forum will work with e.g. the iPhone?",
                    new DateTime(2008, 07, 11),
                    "Another user",
                    new Node(
                        "Sure...!",
                        "Ra-Ajax is built with the w3c standards as HOLY, that means that except for some troubles with IE everything built with Ra-Ajax will mostly work with every single browser on the planet! Including the Safari on the iPhone.",
                        new DateTime(2008, 07, 12),
                        "Thomas Hansen",
                        new Node(
                            "I can am dead sure of it...",
                            "But you can easily test it yourself :)",
                            new DateTime(2008, 07, 14),
                            "Ra-Ajax RULES...!"),
                        new Node("I too am pretty sure about it",
                            "Though always the best thing to do is to verify it yourself by testing, yes...",
                            new DateTime(2008, 07, 15),
                            "Some other user"))));
            return retVal;
        }
    }
}