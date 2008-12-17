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
    public partial class CRUD : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                firstname.Focus();
            }
        }

        protected void TextBoxCheckForValue(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt.Text.Trim().Length > 0)
            {
                txt.Style["background-color"] = "#dfd";
            }
            else
            {
                txt.Style["background-color"] = "#fdd";
            }
        }

        protected void TextAreaCheckForValue(object sender, EventArgs e)
        {
            TextArea txt = sender as TextArea;
            if (txt.Text.Trim().Length > 0)
            {
                txt.Style["background-color"] = "#dfd";
            }
            else
            {
                txt.Style["background-color"] = "#fdd";
            }
        }

        protected void whereTo_CheckedChanged(object sender, EventArgs e)
        {
            domestic.Style["background-color"] = "#dfd";
            abroad.Style["background-color"] = "#dfd";
        }

        protected void step2_Click(object sender, EventArgs e)
        {
            acc2.SetActive();
        }

        protected void step3_Click(object sender, EventArgs e)
        {
            acc3.SetActive();
        }

        protected void accordion_ActiveAccordionViewChanged(object sender, EventArgs e)
        {
            if (acc3.IsActive())
            {
                fullName.Text = lastname.Text + ", " + firstname.Text;
                fromStart.Text = start.Value.ToString("dddd dd. MMMM yyyy");
                fromEnd.Text = end.Value.ToString("dddd dd. MMMM yyyy");
                fromAdr.Text = address.Text + ", " + zip.Text;
                fromPos.Text = position.Text;
                fromCountry.Text = country.SelectedItem.Text;
                fromDomestic.Text = domestic.Checked ? "domestic" : "abroad";
                fromTxt.Text = purpose.Text;
            }
        }
    }
}
