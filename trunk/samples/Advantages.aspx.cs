using System;
using Ra.Widgets;

public partial class Advantages : System.Web.UI.Page
{
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        string results = "You have chosen; ";
        foreach (CheckBox idx in new CheckBox[] { chk1, chk2, chk3, chk4 })
        {
            if (idx.Checked)
                results += idx.Text + ", ";
        }
        lblResults.Text = results;
    }
}
