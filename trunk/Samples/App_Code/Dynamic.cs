using System;

public abstract class Dynamic : System.Web.UI.Page
{
    public int First
    {
        get { return ViewState["First"] == null ? 0 : (int)ViewState["First"]; }
        set { ViewState["First"] = value; }
    }

    public int Second
    {
        get { return ViewState["Second"] == null ? 0 : (int)ViewState["Second"]; }
        set { ViewState["Second"] = value; }
    }

    public int Third
    {
        get { return ViewState["Third"] == null ? 0 : (int)ViewState["Third"]; }
        set { ViewState["Third"] = value; }
    }

    public abstract void Change(string from);
}
