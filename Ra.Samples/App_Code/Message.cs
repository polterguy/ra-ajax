using System;
using System.Collections.Generic;
using System.Web;

public class Message
{
    private string _content;

    public Message(string content)
    {
        _content = content;
    }

    public string Content
    {
        get { return _content; }
        set { _content = value; }
    }

    public static List<Message> Messages
    {
        get
        {
            if (HttpContext.Current.Application["__Messages"] == null)
            {
                List<Message> retVal = new List<Message>();
                retVal.Add(new Message("Here is a sample chat message"));
                HttpContext.Current.Application["__Messages"] = retVal;
            }
            return HttpContext.Current.Application["__Messages"] as List<Message>;
        }
    }
}
