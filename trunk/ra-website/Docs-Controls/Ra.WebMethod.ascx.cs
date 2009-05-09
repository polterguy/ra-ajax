/*
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the the MIT license which 
 * means you can use it (almost) exactly as you wish in your 
 * own projects and copy and paste as much as you want.
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
