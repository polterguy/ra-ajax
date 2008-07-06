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
using Ra.Widgets;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtFirstName.Focus();
        txtFirstName.Select();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        pnlResults.Visible = true;
        if (txtFirstName.Text.Trim() == "" && txtSurname.Text.Trim() == "")
        {
            lblResults.Text = "Hello stranger, don't you want to give me your name?";
        }
        else
        {
            lblResults.Text = string.Format("Hello {0} {1}, and welcome to this website",
                txtFirstName.Text,
                txtSurname.Text);
        }
        Effect effect = new EffectFadeIn(pnlResults, 2.0M);
        effect.Render();
    }
}
