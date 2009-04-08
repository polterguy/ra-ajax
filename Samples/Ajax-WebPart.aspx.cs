/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace Samples
{
    public partial class WebPart : System.Web.UI.Page
    {
        protected void btn_Click(object sender, EventArgs e)
        {
            webpart.Visible = true;
            webpart.Style[Styles.display] = "none";
            new EffectFadeOut(btn, 200)
                .ChainThese(new EffectFadeIn(webpart, 400))
                .Render();
        }

        protected void webpart_Closed(object sender, EventArgs e)
        {
            btn.Visible = true;
            new EffectFadeIn(btn, 200).Render();
        }
    }
}
