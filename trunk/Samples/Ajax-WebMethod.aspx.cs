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
        [Ra.WebMethod]
        private string foo(string name, int age)
        {
            // As you can see you can combine any server-side logic, including 
            // effects with Ra-Ajax WebMethods ;)
            new EffectHighlight(pnl, 500).
                JoinThese(new EffectBorder(5), new EffectSize(150, 250)).
                Render();
            return string.Format("Hello {0}, in 10 years you will be {1}! :D<br />What time do you make of it, on the server it's {2}", 
                name, 
                age + 10,
                DateTime.Now.ToString("HH:mm"));
        }
    }
}
