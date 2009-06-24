/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Net;

public class RSS
{
    private string _url;
    private string _title;
    private string _webLink;
    private List<RSSItem> _items = new List<RSSItem>();
    private Guid _id = Guid.NewGuid();

    public RSS(string url)
    {
        Url = url;
    }

    public Guid Id
    {
        get { return _id; }
    }

    public string Url
    {
        get { return _url; }
        set { _url = value; Inititalize(); }
    }

    public string Title
    {
        get { return _title; }
    }

    public string WebLink
    {
        get { return _webLink; }
    }

    public List<RSSItem> Items
    {
        get { return _items; }
    }

    private void Inititalize()
    {
        XmlDocument doc = new XmlDocument();
        HttpWebRequest req = HttpWebRequest.Create(_url) as HttpWebRequest;
        HttpWebResponse res = req.GetResponse() as HttpWebResponse;
        doc.Load(res.GetResponseStream());

        _title = doc.DocumentElement.SelectNodes("channel/title")[0].InnerText;
        _webLink = doc.DocumentElement.SelectNodes("channel/link")[0].InnerText;

        XmlNodeList list = doc.DocumentElement.SelectNodes("channel/item");
        foreach (XmlElement idx in list)
        {
            string title = idx.SelectNodes("title")[0].InnerText;
            DateTime date = DateTime.Parse(idx.SelectNodes("pubDate")[0].InnerText.Replace(" +0000", ""));
            string body = idx.SelectNodes("description")[0].InnerText;
            string url = "";
            if (idx.SelectNodes("guid").Count > 0)
                url = idx.SelectNodes("guid")[0].InnerText;
            else
                url = idx.SelectNodes("link")[0].InnerText;
            Items.Add(new RSSItem(title, body, date, url));
        }
    }
}

public class RSSItem
{
    private string _header;
    private string _body;
    private string _url;
    private DateTime _date;
    private Guid _id = Guid.NewGuid();

    public RSSItem(string header, string body, DateTime date, string url)
    {
        _header = header;
        _body = body;
        _date = date;
        _url = url;
    }

    public Guid Id
    {
        get { return _id; }
    }

    public string Url
    {
        get { return _url; }
        set { _url = value; }
    }

    public string Header
    {
        get { return _header; }
        set { _header = value; }
    }

    public string Body
    {
        get { return _body; }
        set { _body = value; }
    }

    public DateTime Date
    {
        get { return _date; }
        set { _date = value; }
    }
}

public sealed class RSSDatabase
{
    public static List<RSS> Database
    {
        get
        {
            if (HttpContext.Current.Application["RSSDatabase"] == null)
            {
                List<RSS> tmp = new List<RSS>();
                tmp.Add(new RSS("http://ra-ajax.org/thomas.blogger?rss=true"));
                tmp.Add(new RSS("http://ra-ajax.org/Kariem.blogger?rss=true"));
                tmp.Add(new RSS("http://ra-ajax.org/Rick.blogger?rss=true"));
                HttpContext.Current.Application["RSSDatabase"] = tmp;
            }
            return (List<RSS>)HttpContext.Current.Application["RSSDatabase"];
        }
    }

    public static RSSItem FindItem(Guid id)
    {
        foreach (RSS idx in Database)
        {
            RSSItem tmp = idx.Items.Find(
                delegate(RSSItem i)
                {
                    return i.Id == id;
                });
            if (tmp != null)
                return tmp;
        }
        return null;
    }

    public static void MakeSureOneExists()
    {
        if (HttpContext.Current.Application["RSSDatabase"] == null)
        {
            List<RSS> tmp = new List<RSS>();
            tmp.Add(new RSS("http://ra-ajax.org/thomas.blogger?rss=true"));
            HttpContext.Current.Application["RSSDatabase"] = tmp;
        }
    }
}
