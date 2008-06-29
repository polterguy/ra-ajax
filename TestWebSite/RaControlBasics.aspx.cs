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

public partial class RaControlBasics : System.Web.UI.Page
{
    protected void testCallback_Clicked(object sender, EventArgs e)
    {
        setInVisible.Visible = false;
    }

    protected void testCallbackSetButtonVisible_Clicked(object sender, EventArgs e)
    {
        setVisible.Visible = true;
    }
}
