using System;
using System.Data;
using System.Configuration;
using System.Web;

public partial class ASPNETControlsIntegration : System.Web.UI.Page
{
    protected void btn_Click(object sender, EventArgs e)
    {
        lbl.Text = txt.Text +
            txtArea.Text +
            ddl.SelectedItem.Value +
            chk1.Checked +
            chk2.Checked +
            rdo1.Checked +
            rdo2.Checked;
    }

    protected void btnASP_Click(object sender, EventArgs e)
    {
        lblASP.Text = txtRa.Text +
            txtAreaRa.Text +
            ddlRa.SelectedItem.Value +
            chk1Ra.Checked +
            chk2Ra.Checked +
            rdo1Ra.Checked +
            rdo2Ra.Checked;
    }
}
