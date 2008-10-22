/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace Samples
{
    public partial class AjaxWebMethod : System.Web.UI.Page
    {
        private string foo(int intValue, string stringValue)
        {
            return "Hello from server with " + intValue + " and " + stringValue;
        }
    }
}
