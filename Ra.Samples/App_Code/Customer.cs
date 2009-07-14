/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using System.Collections.Generic;
using System.Web;

public class Customer
{
    private string _name;
    private string _address;
    private Guid _id;

    public Customer(string name, string adr)
    {
        _name = name;
        _address = adr;
        _id = Guid.NewGuid();
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

    public Guid ID
    {
        get { return _id; }
    }

    public static List<Customer> Customers
    {
        get
        {
            if (HttpContext.Current.Session["__Customers"] == null)
            {
                List<Customer> retVal = new List<Customer>();
                retVal.Add(new Customer("John Doe", "Seattle 64"));
                retVal.Add(new Customer("Jane Doe", "Seattle 64"));
                retVal.Add(new Customer("Peter Janson", "NYC 43"));
                retVal.Add(new Customer("Samuel Jackson", "Hollywood 654"));
                retVal.Add(new Customer("Peter Tosh", "Trenchtown 11"));
                HttpContext.Current.Session["__Customers"] = retVal;
            }
            return HttpContext.Current.Session["__Customers"] as List<Customer>;
        }
    }
}
