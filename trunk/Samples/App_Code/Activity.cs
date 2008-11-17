using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Web;

public class Activity
{
    private string _header;
    private string _body;
    private DateTime _when;

    public Activity(string header, string body, DateTime when)
    {
        _header = header;
        _body = body;
        _when = when;
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

    public DateTime When
    {
        get { return _when; }
        set { _when = value; }
    }
}

public sealed class ActivitiesDatabase
{
    public static List<Activity> Database
    {
        get
        {
            if (HttpContext.Current.Session["ActivitiesDatabase"] == null)
            {
                List<Activity> tmp = new List<Activity>();
                tmp.Add(new Activity("Meeting", "Meeting with the rest of the team", DateTime.Now.AddDays(3)));
                tmp.Add(new Activity("Sprint meeting", "Sprint talk with the rest of the team", DateTime.Now.AddDays(1)));
                tmp.Add(new Activity("Vacation", "Starting my vacation", DateTime.Now.AddDays(7)));
                tmp.Add(new Activity("Back from Vacation", "Coming back from my vacation", DateTime.Now.AddDays(21)));
                HttpContext.Current.Session["ActivitiesDatabase"] = tmp;
            }
            return (List<Activity>)HttpContext.Current.Session["ActivitiesDatabase"];
        }
    }
}
