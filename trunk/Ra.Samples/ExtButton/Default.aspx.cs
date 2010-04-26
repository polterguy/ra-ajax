using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExtButton_Default : System.Web.UI.Page
{
    protected void extbtn1_Click(object sender, EventArgs e)
    {
        if (extbtn1.CssClass.Contains(" ra-button-selected"))
        {
            extbtn1.CssClass = "ra-button";
        }
        else
        {
            extbtn1.CssClass = "ra-button ra-button-selected";
        }
    }

    protected void extbtn2_Click(object sender, EventArgs e)
    {
        if (extbtn2.Dir == "")
        {
            extbtn2.Dir = "rtl";
            extbtn2.Text = "راع بتوون";
        }
        else
        {
            extbtn2.Dir = "";
            extbtn2.Text = "Ra Button";
        }
    }
}