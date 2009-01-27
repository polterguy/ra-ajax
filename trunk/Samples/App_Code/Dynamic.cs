using System;

public abstract class Dynamic : System.Web.UI.Page
{
    public int First
    {
        get { return (int)ViewState["First"]; }
        set { ViewState["First"] = value; }
    }

    public int Second
    {
        get { return (int)ViewState["Second"]; }
        set { ViewState["Second"] = value; }
    }

    public int Third
    {
        get { return (int)ViewState["Third"]; }
        set { ViewState["Third"] = value; }
    }

    public abstract void Change(string from);
}
