/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using Ra.Extensions;
using Ra;

public partial class Docs_Controls_WebMethod : System.Web.UI.UserControl
{
    [WebMethod]
    private string foo(string name)
    {
        return "Hello '" + 
            name + 
            "' on the server it's " + 
            DateTime.Now.ToString("HH:mm") + 
            " now...";
    }
}
