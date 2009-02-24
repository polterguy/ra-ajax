/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using Ra.Extensions;

namespace Samples
{
    public partial class AjaxMessageBox : System.Web.UI.Page
    {
        protected void showMsgBox1_Click(object sender, EventArgs e)
        {
            MessageBox1.Visible = true;
        }

        protected void showMsgBox2_Click(object sender, EventArgs e)
        {
            MessageBox2.Visible = true;
        }

        protected void showMsgBox3_Click(object sender, EventArgs e)
        {
            MessageBox3.Visible = true;
        }

        protected void showMsgBox4_Click(object sender, EventArgs e)
        {
            MessageBox4.Visible = true;
        }

        protected void showMsgBox5_Click(object sender, EventArgs e)
        {
            MessageBox5.Visible = true;
        }

        protected void MessageBox_Closed(object sender, MessageBox.ClosedEventArgs e)
        {
            switch (e.HowClosed)
            {
                case MessageBox.ClosedHow.OK:
                    lbl.Text = "Closed with OK";
                    break;
                case MessageBox.ClosedHow.Cancel:
                    lbl.Text = "Closed with Cancel";
                    break;
                case MessageBox.ClosedHow.Yes:
                    lbl.Text = "Closed with Yes";
                    break;
                case MessageBox.ClosedHow.No:
                    lbl.Text = "Closed with No";
                    break;
            }
            if (!string.IsNullOrEmpty(e.UserInput))
                lbl.Text += " " + e.UserInput;
        }
    }
}
