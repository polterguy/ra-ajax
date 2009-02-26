using System;
using System.Web.UI.WebControls.WebParts;

public partial class ChartDataCollector : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            first.Focus();
        }
    }

    protected void save_Click(object sender, EventArgs e)
    {
        try
        {
            (Page as Dynamic).First = Int32.Parse(first.Text);
            (Page as Dynamic).Second = Int32.Parse(second.Text);
            (Page as Dynamic).Third = Int32.Parse(third.Text);
            (Page as Dynamic).Change("ChartDataCollector.ascx");
        }
        catch (Exception)
        {
            err.Text = "Make sure you type in *NUMBERS*...!";
            first.Select();
            first.Focus();
        }
    }
}
