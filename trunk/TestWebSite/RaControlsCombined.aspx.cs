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

    protected void testChangeTextBoxValue_Click(object sender, EventArgs e)
    {
        txtBox.Text = "New text";
    }

    protected void testCallBack_TextChanged(object sender, EventArgs e)
    {
        testCallBack.Text = "After change";
    }

    protected void changeToComplexValue_Click(object sender, EventArgs e)
    {
        txtComplexValue.Text = "&\"'''//\\";
    }

    protected void verifyComplexValue_Click(object sender, EventArgs e)
    {
        if (txtComplexValue.Text == "&\"'''//\\")
        {
            verifyComplexValue.Text = "success";
        }
    }

    protected void testTextArea_Click(object sender, EventArgs e)
    {
        if (textArea.Text == "Text of textarea")
        {
            textArea.Text = "success1";
        }
    }

    protected void testTextArea2_Click(object sender, EventArgs e)
    {
        if (textArea.Text == "success1")
        {
            textArea.Text = "success2";
        }
    }

    protected void testTextArea3_Click(object sender, EventArgs e)
    {
        if (textArea.Text == "changed")
        {
            textArea.Text = "success";
        }
    }
}
