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
        if (Request.Params["id"] != null)
        {
            Response.Clear();
            switch (Request.Params["id"])
            {
                case "0":
                    Response.Write("s");
                    break;
                case "1":
                    Response.Write("u");
                    break;
                case "2":
                    Response.Write("c");
                    break;
                case "3":
                    Response.Write("c");
                    break;
                case "4":
                    Response.Write("e");
                    break;
                case "5":
                    Response.Write("s");
                    break;
                case "6":
                    Response.Write("s");
                    break;
            }
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
        if (Request.Params["testingForm"] == "testingParamsMultiple")
        {
            if (Request.Params["testCheckBox"] == "on" &&
                Request.Params["testCheckBox2"] == null &&
                Request.Params["testRadio"] == "on" &&
                Request.Params["testRadio2"] == null &&
                Request.Params["testButton"] == null &&
                Request.Params["testHidden"] == "testing value &&& $$ ££__//" &&
                Request.Params["testPwd"] == "testing password" &&
                Request.Params["testShouldnt"] == null &&
                Request.Params["testSelect"] == "sel2")
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
        }
        if (Request.Params["testingForm"] == "testingParams1")
        {
            if (Request.Params["testingInput"] == "testing input for form")
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
        }
        if (Request.Params["testingForm"] == "testingParams2")
        {
            if (Request.Params["testingInput2"] == "testing input for form with $€//@#£&&&__{[]} funny")
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
