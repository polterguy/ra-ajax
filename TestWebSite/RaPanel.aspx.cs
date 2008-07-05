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

public partial class RaPanel : System.Web.UI.Page
{
    protected void btnMakeVisible_Click(object sender, EventArgs e)
    {
        pnlInvisible.Visible = true;
    }

    protected void btnMakeINVisible_Click(object sender, EventArgs e)
    {
        pnlVisible.Visible = false;
    }

    protected void btnToggle_Click(object sender, EventArgs e)
    {
        pnlToggle.Visible = !pnlToggle.Visible;
    }

    protected void btnTest_Click(object sender, EventArgs e)
    {
        btnTest.Text = "clicked";
    }

    protected void setPnlVisible_Click(object sender, EventArgs e)
    {
        pnlControlsINVisible.Visible = !pnlControlsINVisible.Visible;
        btnTestINVisible.Text = "should click";
    }

    protected void btnTestINVisible_Click(object sender, EventArgs e)
    {
        btnTestINVisible.Text = "clicked";
    }

    protected void btnRec1_Click(object sender, EventArgs e)
    {
        btnRec1.Text = "clicked";
    }

    protected void btnShowPnlRec_Click(object sender, EventArgs e)
    {
        if (pnlRec1.Visible)
            pnlRec2.Visible = true;
        else
            pnlRec1.Visible = true;
    }

    protected void btnShowPnlRec2_Click(object sender, EventArgs e)
    {
        pnlRec3.Visible = true;
    }

    protected void btnRec2_Click(object sender, EventArgs e)
    {
        btnRec2.Text = "clicked";
    }
}
