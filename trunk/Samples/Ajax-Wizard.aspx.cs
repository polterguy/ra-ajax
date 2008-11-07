/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using Ra.Extensions;

namespace Samples
{
    public partial class AjaxWizard : System.Web.UI.Page
    {
        protected void wizard_CreateNavigationalButtons(object sender, Window.CreateNavigationalButtonsEvtArgs e)
        {
            LinkButton prev = new LinkButton();
            prev.ID = "prev";
            prev.Click += prev_Click;
            prev.CssClass = wizard.CssClass + "_prev";
            e.Caption.Controls.Add(prev);

            LinkButton next = new LinkButton();
            next.ID = "next";
            next.Click += next_Click;
            next.CssClass = wizard.CssClass + "_next";
            e.Caption.Controls.Add(next);
        }

        private int WizardPane
        {
            get { return ViewState["WizardPane"] == null ? 0 : (int)ViewState["WizardPane"]; }
            set { ViewState["WizardPane"] = value; }
        }

        protected void lnk_Click(object sender, EventArgs e)
        {
            (sender as LinkButton).Text = "Clicked...!! ;)";
        }

        void prev_Click(object sender, EventArgs e)
        {
            WizardPane -= 1;
            switch (WizardPane)
            {
                case 0:
                    new EffectMove(wizardContent, 200, 0, 0)
                        .JoinThese(new EffectFadeIn())
                        .Render();
                    break;
                case 1:
                    new EffectMove(wizardContent, 200, -400, 0)
                        .JoinThese(new EffectFadeIn())
                        .Render();
                    break;
                default:
                    WizardPane = 0;
                    break;
            }
        }

        void next_Click(object sender, EventArgs e)
        {
            WizardPane += 1;
            switch (WizardPane)
            {
                case 1:
                    new EffectMove(wizardContent, 200, -400, 0)
                        .JoinThese(new EffectFadeIn())
                        .Render();
                    break;
                case 2:
                    new EffectMove(wizardContent, 200, -800, 0)
                        .JoinThese(new EffectFadeIn())
                        .Render();
                    break;
                default:
                    WizardPane = 2;
                    break;
            }
        }
    }
}
