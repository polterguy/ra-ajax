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

public partial class RaControlNoViewState : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void testChangeStyle_Click(object sender, EventArgs e)
    {
        verifyNoStylesChanged.Style["width"] = "150px";
    }

    protected void verifyNoStylesChanged_Click(object sender, EventArgs e)
    {
        if (verifyNoStylesChanged.Style["width"] == null)
            verifyNoStylesChanged.Text = "success";
    }
}
