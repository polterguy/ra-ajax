/*
 * Ra-Ajax - An Ajax Library for Mono ++
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
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFirstName.Focus();
            txtFirstName.Select();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            pnlResults.Visible = true;
            if (txtFirstName.Text.Trim() == "" && txtSurname.Text.Trim() == "")
            {
                lblResults.Text = "Hello stranger, don't you want to give me your name?";
            }
            else
            {
                lblResults.Text = string.Format("Hello {0} {1}, and welcome to this website",
                    txtFirstName.Text,
                    txtSurname.Text);
            }
            Effect effect = new EffectFadeIn(pnlResults, 2000);
            effect.Render();
        }
    }
}
