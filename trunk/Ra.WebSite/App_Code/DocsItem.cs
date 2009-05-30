/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;

public class DocsItem
{
    private string _name;
    private string _id;
    private string _kind;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string ID
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Kind
    {
        get { return _kind; }
        set { _kind = value; }
    }

    public DocsItem(string name, string id)
    {
        Name = name;
        ID = id;
    }
}
