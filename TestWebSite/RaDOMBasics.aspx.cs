using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class RaDOMBasics : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["testingForm"] == "testing")
        {
            Response.Clear();
            Response.Write("this worked");
            Response.Flush();
            try
            {
                Response.End();
            }
            catch
            {
                // Do nothing...!
            }
        }
        if (Request.Params["testingForm"] == "testingError")
        {
            throw new Exception("TESTING ERROR HANDLER IN FORM / XHR");
        }
        if (Request.Params["testingXHR"] == "true")
        {
            Response.Clear();
            Response.Write("works");
            Response.Flush();
            try
            {
                Response.End();
            }
            catch
            {
                // Do nothing...!
            }
        }
    }
}
