using System;

public class DocsItem
{
    private string _name;
    private string _id;

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

    public DocsItem(string name, string id)
    {
        Name = name;
        ID = id;
    }
}
