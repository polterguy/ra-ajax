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
}
