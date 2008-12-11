/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace RaWebsite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void btn_Click(object sender, EventArgs e)
        {
            btn.Text = "Hello world";
            new EffectSize(btn, 500, 20, 55)
                .ChainThese(new EffectFadeOut(btn, 500)
                        .ChainThese(new EffectFadeIn(btn, 500)
                            .ChainThese(new EffectSize(btn, 200, 80, 500)
                                .ChainThese(new EffectHighlight(btn, 500))))).Render();
        }
    }
}
