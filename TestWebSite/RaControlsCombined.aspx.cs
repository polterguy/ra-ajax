using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class RaControlsCombined : System.Web.UI.Page
{
    protected void textChangeLabelValue_Click(object sender, EventArgs e)
    {
        testChangeValue.Text = "New value";
    }

    protected void accessKeyButton_Click(object sender, EventArgs e)
    {
        accessKeyButton.Text = "clicked";
    }
}
