/*
 * Ra-Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using Ra.Extensions;

namespace RaWebsite
{
    public partial class Extensions : System.Web.UI.Page
    {
        protected void lnkTest_Click(object sender, EventArgs e)
        {
            lnkTest.Text = "I was clicked :)";
        }

        protected void calendar_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedDate.Text = calendar.Value.ToString("dddd, MMMM dd, yyyy", System.Threading.Thread.CurrentThread.CurrentUICulture);
        }

        protected void calendarDTP_SelectedValueChanged(object sender, EventArgs e)
        {
            lblDate.Text = calendarDTP.Value.ToString("MMMM dd, yyyy", System.Threading.Thread.CurrentThread.CurrentUICulture);
        }

        protected void calendarDTP_DateClicked(object sender, EventArgs e)
        {
            lblDate.Text = calendarDTP.Value.ToString("MMMM dd, yyyy", System.Threading.Thread.CurrentThread.CurrentUICulture);
            Effect effect = new EffectFadeOut(calendarDTP, 400);
            effect.Render();
        }

        protected void btnPickDate_Click(object sender, EventArgs e)
        {
            calendarDTP.Visible = true;
            Effect effect = new EffectFadeIn(calendarDTP, 400);
            effect.Render();
        }

        protected void calTab_SelectedValueChanged(object sender, EventArgs e)
        {
            lblCalTab.Text = calTab.Value.ToString("dddd, MMMM dd, yyyy", System.Threading.Thread.CurrentThread.CurrentUICulture);
            Effect effect = new EffectFadeIn(lblCalTab, 400);
            effect.Render();

            if (calTab.Value.Date == new DateTime(2008, 7, 3))
            {
                lblCalTab.Text += " <strong>Holiday on planet VENUS ;)</strong>";
            }
            if (calTab.Value.Date == new DateTime(2008, 7, 11))
            {
                lblCalTab.Text += " <strong>Holiday on planet VENUS ;)</strong>";
            }
            if (calTab.Value.Date == new DateTime(2008, 7, 15))
            {
                lblCalTab.Text += " <strong>Holiday on planet VENUS ;)</strong>";
            }
        }

        protected void calTab_RenderDay(object sender, Calendar.RenderDayEventArgs e)
        {
            if (e.Date == new DateTime(2008, 7, 3))
            {
                e.Cell.Attributes.Add("class", "holidayOnVenus");
            }
            if (e.Date == new DateTime(2008, 7, 11))
            {
                e.Cell.Attributes.Add("class", "holidayOnVenus");
            }
            if (e.Date == new DateTime(2008, 7, 15))
            {
                e.Cell.Attributes.Add("class", "holidayOnVenus");
            }
        }
    }
}