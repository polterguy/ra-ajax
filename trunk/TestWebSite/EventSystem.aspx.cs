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

public partial class EventSystem : System.Web.UI.Page
{
    protected void click_click(object sender, EventArgs e)
    {
        (sender as Ra.Widgets.Label).Text = "success";
    }

    protected void dblClick_dblClick(object sender, EventArgs e)
    {
        (sender as Ra.Widgets.Label).Text = "success";
    }

    protected void keyDown_keyDown(object sender, EventArgs e)
    {
        (sender as Ra.Widgets.Label).Text = "success";
    }
}





















