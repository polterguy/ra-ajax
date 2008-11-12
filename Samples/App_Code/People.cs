using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Web;

public class People
{
    private string _name;
    private string _address;
    private DateTime _birthday;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    
    public string Address
    {
        get { return _address; }
        set { _address = value; }
    }

    public DateTime Birthday
    {
        get { return _birthday; }
        set { _birthday = value; }
    }

    public People(string name, string address, DateTime birthday)
    {
        _name = name;
        _address = address;
        _birthday = birthday;
    }
}

public sealed class PeopleDatabase
{
    public static List<People> Database
    {
        get
        {
            if (((Page)HttpContext.Current.CurrentHandler).Items["PeopleDatabase"] == null)
            {
                List<People> tmp = new List<People>();
                tmp.Add(new People("Thomas Hansen", "Norway", new DateTime(1974, 5, 16)));
                tmp.Add(new People("Kariem Ali", "Egypt", new DateTime(1982, 10, 11)));
                tmp.Add(new People("John Doe", "US", new DateTime(1980, 8, 22)));
                tmp.Add(new People("Jane Doe", "Canada", new DateTime(1970, 11, 6)));
                tmp.Add(new People("Bill Gates", "Seattle", new DateTime(1952, 4, 20)));
                tmp.Add(new People("Steve Jobs", "San Fransisco", new DateTime(1954, 3, 1)));
                tmp.Add(new People("Weird Al Jankovich", "Somewhere", new DateTime(1965, 8, 29)));
                ((Page)HttpContext.Current.CurrentHandler).Items["PeopleDatabase"] = tmp;
            }
            return (List<People>)(((Page)HttpContext.Current.CurrentHandler).Items["PeopleDatabase"]);
        }
    }
}
