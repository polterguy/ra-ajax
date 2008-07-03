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

    protected void btnSetChkVisible_Click(object sender, EventArgs e)
    {
        chkSetVisible.Visible = true;
    }

    protected void chkSetVisible_CheckedChanged(object sender, EventArgs e)
    {
        btnSetChkVisible.Text = "Checkbox clicked";
    }

    protected void btnToggleChk_Click(object sender, EventArgs e)
    {
        chkToggle.Visible = !chkToggle.Visible;
    }

    protected void btnChangeChkStyle_Click(object sender, EventArgs e)
    {
        chkChangeStyle.Style["color"] = "Red";
        chkChangeStyle.Style["width"] = "400px";
        chkChangeStyle.Style["height"] = "200px";
        chkChangeStyle.Style["display"] = "block";
    }

    protected void btnToggleStyle_Click(object sender, EventArgs e)
    {
        if (chkToggleStyle.Style["color"] == "Red")
        {
            chkToggleStyle.Style["color"] = "Yellow";
            chkToggleStyle.Style["width"] = "100px";
        }
        else
        {
            chkToggleStyle.Style["color"] = "Red";
            chkToggleStyle.Style["width"] = "200px";
        }
    }

    protected void chkAccKey_CheckedChanged(object sender, EventArgs e)
    {
        chkAccKey.Text = "It was accessed...";
    }
}
