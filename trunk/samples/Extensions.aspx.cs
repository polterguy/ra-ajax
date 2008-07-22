/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

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
        Effect effect = new EffectFadeOut(calendarDTP, 0.4M);
        effect.Render();
    }

    protected void btnPickDate_Click(object sender, EventArgs e)
    {
        calendarDTP.Visible = true;
        Effect effect = new EffectFadeIn(calendarDTP, 0.4M);
        effect.Render();
    }

    protected void calTab_SelectedValueChanged(object sender, EventArgs e)
    {
        lblCalTab.Text = calTab.Value.ToString("dddd, MMMM dd, yyyy", System.Threading.Thread.CurrentThread.CurrentUICulture);
        Effect effect = new EffectFadeIn(lblCalTab, 0.4M);
        effect.Render();
    }
}
