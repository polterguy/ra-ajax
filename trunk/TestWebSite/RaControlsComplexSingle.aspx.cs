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

public partial class RaControlsComplexSingle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        chk.Text = "New text";
    }

    protected void setChkToInvisible_Click(object sender, EventArgs e)
    {
        chkSetInVisible.Visible = false;
    }
}
