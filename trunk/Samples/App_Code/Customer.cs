using System;
using System.Collections.Generic;
using System.Web;

public class Customer
{
    private string _name;
    private string _address;
    private int _id;

    public Customer(string name, string adr, int id)
    {
        _name = name;
        _address = adr;
        _id = id;
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

    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }

    public static List<Customer> Customers
    {
        get
        {
            if (HttpContext.Current.Session["__Customers"] == null)
            {
                List<Customer> retVal = new List<Customer>();
                retVal.Add(new Customer("John Doe", "Seattle 64", 1));
                retVal.Add(new Customer("Jane Doe", "Seattle 64", 2));
                retVal.Add(new Customer("Peter Janson", "NYC 43", 3));
                retVal.Add(new Customer("Samuel Jackson", "Hollywood 654", 4));
                retVal.Add(new Customer("Peter Tosh", "Trenchtown 11", 5));
                HttpContext.Current.Session["__Customers"] = retVal;
            }
            return HttpContext.Current.Session["__Customers"] as List<Customer>;
        }
    }
}
