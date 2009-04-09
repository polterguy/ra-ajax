/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using Ra;
using System;
using Ra.Widgets;
using System.Web.UI;

namespace Samples
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                new EffectFadeIn(thumbsWrapper, 500)
                    .Render();
                foreach (Control idx in new Control[] { thumbs1, thumbs2, thumbs3, thumbs4, thumbs5 })
                {
                AjaxManager.Instance.WriterAtBack.Write(@"
Ra.$('{0}').observe('mouseover', function() {{
  RaFadeIn('{0}');
}});

Ra.$('{0}').observe('mouseout', function() {{
  RaFadeOut('{0}');
}});
", idx.ClientID);
                }
            }
        }
    }
}
