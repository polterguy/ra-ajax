using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Web;

public class People
{
    private string _name;
    private string _address;
    private DateTime _birthday;
    private Guid _id;

    public Guid ID
    {
        get { return _id; }
        set { _id = value; }
    }

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
        _id = Guid.NewGuid();
    }
}

public sealed class PeopleDatabase
{
    public enum Sort { Name, Adr, Birthdate }

    public static Sort Sorting
    {
        get
        {
            if (HttpContext.Current.Session["Sorting"] == null)
                return Sort.Name;
            return (Sort)HttpContext.Current.Session["Sorting"];
        }
        set
        {
            HttpContext.Current.Session["Sorting"] = value;
        }
    }

    public static string Filter
    {
        get
        {
            if (HttpContext.Current.Session["Filter"] == null)
                return "";
            return HttpContext.Current.Session["Filter"].ToString();
        }
        set
        {
            HttpContext.Current.Session["Filter"] = value;
        }
    }

    public static List<People> Database
    {
        get
        {
            if (HttpContext.Current.Session["PeopleDatabase"] == null)
            {
                List<People> tmp = new List<People>();
                tmp.Add(new People("Thomas Hansen", "Norway", new DateTime(1974, 5, 16)));
                tmp.Add(new People("Kariem Ali", "Egypt", new DateTime(1982, 10, 11)));
                tmp.Add(new People("John Doe", "US", new DateTime(1980, 8, 22)));
                tmp.Add(new People("Jane Doe", "Canada", new DateTime(1970, 11, 6)));
                tmp.Add(new People("Bill Gates", "Seattle", new DateTime(1952, 4, 20)));
                tmp.Add(new People("Steve Jobs", "San Fransisco", new DateTime(1954, 3, 1)));
                tmp.Add(new People("Weird Al Jankovich", "Somewhere", new DateTime(1965, 8, 29)));
                tmp.Add(new People("Ola Dunk", "Larvik", new DateTime(1978, 9, 11)));
                tmp.Add(new People("Milton Friedman", "Boston", new DateTime(1928, 2, 4)));
                tmp.Add(new People("John F. Kennedy", "Los Angeles", new DateTime(1918, 4, 9)));
                tmp.Add(new People("Al Capone", "Chicago", new DateTime(1892, 4, 29)));
                tmp.Add(new People("Johhny Johnson", "Stockholm", new DateTime(1983, 5, 3)));
                tmp.Add(new People("Hauk Sigurdson", "Reykjavik", new DateTime(1978, 11, 10)));
                tmp.Add(new People("Severin Suveren", "Scandinavia", new DateTime(1967, 12, 1)));
                HttpContext.Current.Session["PeopleDatabase"] = tmp;
            }
            List<People> original = (List<People>)HttpContext.Current.Session["PeopleDatabase"];
            List<People> retVal = new List<People>(original);
            if (Filter != string.Empty)
            {
                retVal.RemoveAll(
                delegate(People idx)
                {
                    return idx.Address.ToLower().IndexOf(Filter) == -1 && idx.Name.ToLower().IndexOf(Filter) == -1;
                });
            }
            retVal.Sort(
                delegate(People left, People right)
                {
                    switch (Sorting)
                    {
                        case Sort.Name:
                            return left.Name.CompareTo(right.Name);
                        case Sort.Adr:
                            return left.Address.CompareTo(right.Address);
                        case Sort.Birthdate:
                            return left.Birthday.CompareTo(right.Birthday);
                        default:
                            throw new Exception("Undefined sorting column found");
                    }
                });
            return retVal;
        }
    }
}
