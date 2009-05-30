using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Ra;

public partial class NestedMethods2 : System.Web.UI.UserControl
{
    [WebMethod]
    private string foo(int intVal, string stringVal)
    {
        return "" + intVal + stringVal;
    }
}
