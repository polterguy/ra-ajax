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
using ASP = System.Web.UI;

namespace Ra.Extensions
{
    /**
     * DateTimePicker Ajax Control. Inherits from Calendar and handles 
     * the CreateExtraControlsAtBottom event to create hour and minute controls
     */
    [ASP.ToolboxData("<{0}:DateTimePicker runat=server />")]
    public class DateTimePicker : Calendar, ASP.INamingContainer
    {
        private TextBox _hour = new TextBox();
        private TextBox _minutes = new TextBox();

        protected override void OnInit(EventArgs e)
        {
            this.CreateExtraControlsAtBottom += new EventHandler<CreateExtraControlsAtBottomEvtArgs>(DateTimePicker_CreateExtraControlsAtBottom);
            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            _hour.Text = this.Value.ToString("HH");
            _minutes.Text = this.Value.ToString("mm");
            base.OnPreRender(e);

            _caption.Text += " - " + this.Value.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture);
        }

        void DateTimePicker_CreateExtraControlsAtBottom(object sender, Calendar.CreateExtraControlsAtBottomEvtArgs e)
        {
            Panel lit = new Panel();
            lit.ID = "time";

            _hour.ID = "hour";
            _hour.CssClass = "hour";
            _hour.TextChanged += _hour_TextChanged;
            lit.Controls.Add(_hour);

            _minutes.ID = "minutes";
            _minutes.CssClass = "minutes";
            _minutes.TextChanged += _minutes_TextChanged;
            lit.Controls.Add(_minutes);

            e.Controls.Add(lit);
        }

        void _minutes_TextChanged(object sender, EventArgs e)
        {
            DateTime d = this.Value;
            int nMinute = 1;
            Int32.TryParse(_minutes.Text, out nMinute);
            this.Value = new DateTime(d.Year, d.Month, d.Day, d.Hour, nMinute, 1);
        }

        void _hour_TextChanged(object sender, EventArgs e)
        {
            DateTime d = this.Value;
            int nHour = 1;
            Int32.TryParse(_hour.Text, out nHour);
            this.Value = new DateTime(d.Year, d.Month, d.Day, nHour, d.Minute, 1);
        }
    }
}
