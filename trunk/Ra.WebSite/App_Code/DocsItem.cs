/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;

public class DocsItem
{
    private string _name;
    private string _id;
    private string _kind;
    private string _params;
    private string _returns;

    public string Params
    {
        get { return _params; }
        set { _params = value; }
    }

    public string Returns
    {
        get { return _returns; }
        set { _returns = value; }
    }

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

    public DocsItem(string name, string id, string kind, string signature)
    {
        Name = name;
        ID = id;
        Kind = kind;
        if (!string.IsNullOrEmpty(signature))
        {
            Returns = signature.Substring(0, signature.IndexOf(name));
            if (Kind == "function" || Kind == "ctor")
                Params = signature.Substring(signature.IndexOf("("));
        }
    }
}
