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

public class Activity
{
    private string _header;
    private string _body;
    private DateTime _when;
    private Guid _id;

    public Activity(string header, string body, DateTime when)
    {
        _header = header;
        _body = body;
        _when = when;
        _id = Guid.NewGuid();
    }

    public Guid ID
    {
        get { return _id; }
        set { _id = value; }
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
                tmp.Add(new Activity("Deliver project", "Delivering the current project to the customer and talk about futures", DateTime.Now.AddDays(1)));
                tmp.Add(new Activity("Meeting", "Meeting with the rest of the team - organize my vacation - delegate responsibilities...", DateTime.Now.AddDays(3)));
                tmp.Add(new Activity("Vacation", "Starting my vacation", DateTime.Now.AddDays(7)));
                tmp.Add(new Activity("Back from Vacation", "Coming back from my vacation", DateTime.Now.AddDays(21)));
                tmp.Add(new Activity("New project", "Starting our new Scrum project", DateTime.Now.AddDays(24)));
                tmp.Add(new Activity("First Sprint meeting", "First Sprint in new Scrum project", DateTime.Now.AddDays(26)));
                HttpContext.Current.Session["ActivitiesDatabase"] = tmp;
            }
            return (List<Activity>)HttpContext.Current.Session["ActivitiesDatabase"];
        }
    }
}
