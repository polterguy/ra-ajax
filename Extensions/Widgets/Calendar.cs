/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace Ra.Extensions
{
    /**
     * calendar widget for choosing dates
     */
    [ASP.ToolboxData("<{0}:Calendar runat=server />")]
    public class Calendar : Panel, ASP.INamingContainer
    {
        /**
         * EventArgs class for the RenderDay Event
         */
        public class RenderDayEventArgs : EventArgs
        {
            private DateTime _date;
            private HTML.HtmlTableCell _cell;

            /**
             * Cell which renders the given Date
             */
            public HTML.HtmlTableCell Cell
            {
                get { return _cell; }
            }

            /**
             * Date which are currently rendered
             */
            public DateTime Date
            {
                get { return _date; }
            }

            internal RenderDayEventArgs(DateTime date, HTML.HtmlTableCell cell)
            {
                _date = date;
                _cell = cell;
            }
        }

        Label _nw;
        Label _n;
        Label _ne;
        Label _w;
        Label _body;
        Label _content;
        Label _e;
        Label _sw;
        Label _s;
        Label _se;

        /**
         * Raised when Value is changed by user. Can be raised by chaning month and year contrary
         * to the DateClicked event
         */
        public event EventHandler SelectedValueChanged;

        /**
         * Raised when a specific date is clicked, this evnt will not be raised when
         * year or month is changed, only when a specific date is clicked or the "Today" date
         * is clicked
         */
        public event EventHandler DateClicked;

        /**
         * Called once for every date which is rendered within the current month if defined
         */
        public event EventHandler<RenderDayEventArgs> RenderDay;

        /**
         * Selected value of calendar
         */
        public DateTime Value
        {
            get { return ViewState["Value"] == null ? DateTime.MinValue : (DateTime)ViewState["Value"]; }
            set
            {
                ViewState["Value"] = value;
            }
        }

        /**
         * Defines the leftmost weekday of the calendar
         */
        [DefaultValue(DayOfWeek.Monday)]
        public DayOfWeek StartsOn
        {
            get { return ViewState["StartsOn"] == null ? DayOfWeek.Monday : (DayOfWeek)ViewState["StartsOn"]; }
            set
            {
                ViewState["StartsOn"] = value;
            }
        }

        private LinkButton SelectedValueBtn
        {
            get { return ViewState["SelectedValueBtn"] == null ? null : (LinkButton)FindControl((string)ViewState["SelectedValueBtn"]); }
            set
            {
                ViewState["SelectedValueBtn"] = value.ID;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Since we're dependant upon that the ViewState has finished loading before
            // we initialize the ChildControls since how the child controls (and which)
            // child controls are being re-created is dependant upon a ViewState saved value
            // this is the earliest possible time we can reload the ChildControls for the
            // Control
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            CreateWindowControls();
            CreateCalendarControls();
            Controls.Add(_nw);
            Controls.Add(_body);
        }

        private void CreateWindowControls()
        {
            // Top parts
            _nw = new Label();
            _nw.Tag = "div";
            _nw.ID = "XXnw";

            _ne = new Label();
            _ne.Tag = "div";
            _ne.ID = "XXne";
            _nw.Controls.Add(_ne);

            _n = new Label();
            _n.Tag = "div";
            _n.ID = "XXn";
            _ne.Controls.Add(_n);

            // Middle parts
            _body = new Label();
            _body.Tag = "div";
            _body.ID = "XXbody";

            _w = new Label();
            _w.Tag = "div";
            _w.ID = "XXw";
            _body.Controls.Add(_w);

            _e = new Label();
            _e.Tag = "div";
            _e.ID = "XXe";
            _w.Controls.Add(_e);

            _content = new Label();
            _content.Tag = "div";
            _content.ID = "XXcontent";
            _e.Controls.Add(_content);

            // Bottom parts
            _sw = new Label();
            _sw.Tag = "div";
            _sw.ID = "XXsw";
            _body.Controls.Add(_sw);

            _se = new Label();
            _se.Tag = "div";
            _se.ID = "XXse";
            _sw.Controls.Add(_se);

            _s = new Label();
            _s.Tag = "div";
            _s.ID = "XXs";
            _s.Text = "&nbsp;";
            _se.Controls.Add(_s);
        }

        private void CreateCalendarControls()
        {
            // Finding date to start on
            DateTime idxDate = FindStartDate();

            // Creating table to wrap the whole thing inside of
            HTML.HtmlTable tbl = new HTML.HtmlTable();
            tbl.EnableViewState = false;
            tbl.ID = "tbl_" + Value.ToString("dd_MM_yyyy", System.Globalization.CultureInfo.InvariantCulture);

            // Creating the header row which contains the Year and Month DropDownLists
            CreateYearMonthPicker(tbl);

            // Creating Weekdays at top of calendar
            idxDate = CreateWeekDaysAtTop(idxDate, tbl);

            // Looping through creating childcontrols
            while (true)
            {
                HTML.HtmlTableRow row = new HTML.HtmlTableRow();
                row.EnableViewState = false;
                row.ID = idxDate.ToString("MM_dd", System.Globalization.CultureInfo.InvariantCulture) + Value.ToString("dd_MM_yyyy", System.Globalization.CultureInfo.InvariantCulture);

                idxDate = CreateWeekNumberCell(idxDate, row);

                // Creating week cells for actual dates...
                for (int idx = 0; idx < 7; idx++)
                {
                    idxDate = CreateOneDayCell(idxDate, row);
                }
                tbl.Rows.Add(row);
                if (idxDate.Month != Value.Month)
                    break;
            }

            CreateTodayButton(tbl);

            // Rooting the Table as the LAST thing we do...!
            _content.Controls.Add(tbl);
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Setting the CSS classes for all "decoration controls"
            _nw.CssClass = this.CssClass + "_nw";
            _n.CssClass = this.CssClass + "_n";
            _ne.CssClass = this.CssClass + "_ne";
            _e.CssClass = this.CssClass + "_e";
            _se.CssClass = this.CssClass + "_se";
            _s.CssClass = this.CssClass + "_s";
            _sw.CssClass = this.CssClass + "_sw";
            _w.CssClass = this.CssClass + "_w";
            _content.CssClass = this.CssClass + "_content";
            _body.CssClass = this.CssClass + "_body";

            // Calling base...
            base.OnPreRender(e);
        }

        private DateTime FindStartDate()
        {
            DateTime idxDate = new DateTime(Value.Year, Value.Month, 1);
            while (idxDate.DayOfWeek != StartsOn)
            {
                idxDate = idxDate.AddDays(-1);
            }
            return idxDate;
        }

        private void CreateTodayButton(HTML.HtmlTable tbl)
        {
            HTML.HtmlTableRow bottomRow = new HTML.HtmlTableRow();
            bottomRow.EnableViewState = false;
            bottomRow.ID = "stat_" + Value.ToString("dd_MM_yyyy", System.Globalization.CultureInfo.InvariantCulture);
            HTML.HtmlTableCell bottomCell = new HTML.HtmlTableCell();
            bottomCell.EnableViewState = false;
            bottomCell.Style["text-align"] = "center";
            bottomCell.ID = "bottC_" + Value.ToString("dd_MM_yyyy", System.Globalization.CultureInfo.InvariantCulture);
            bottomCell.ColSpan = 8;
            LinkButton today = new LinkButton();
            today.Text = DateTime.Now.ToString("MMMM d, yyyy", System.Threading.Thread.CurrentThread.CurrentUICulture);
            today.Click += new EventHandler(today_Click);
            today.EnableViewState = false;
            bottomCell.Controls.Add(today);
            bottomRow.Cells.Add(bottomCell);
            tbl.Rows.Add(bottomRow);
        }

        private DateTime CreateOneDayCell(DateTime idxDate, HTML.HtmlTableRow row)
        {
            HTML.HtmlTableCell cell = new HTML.HtmlTableCell();
            cell.EnableViewState = false;
            cell.ID = idxDate.ToString("cell_MM_dd", System.Globalization.CultureInfo.InvariantCulture) + Value.ToString("dd_MM_yyyy", System.Globalization.CultureInfo.InvariantCulture);

            // Creating actual LinkButton which is the "clickable part" of the calendar days
            LinkButton btn = new LinkButton();
            btn.Text = idxDate.Day.ToString();
            btn.ID = idxDate.ToString("yyyy_MM_dd", System.Globalization.CultureInfo.InvariantCulture);
            if (idxDate == Value.Date)
            {
                btn.CssClass = "selected";
                SelectedValueBtn = btn;
            }
            else if (idxDate.Month != Value.Month)
                btn.CssClass = "offMonth";
            btn.Click += new EventHandler(btn_Click);
            btn.EnableViewState = false;
            cell.Controls.Add(btn);

            if (RenderDay != null)
                RenderDay(this, new RenderDayEventArgs(idxDate.Date, cell));

            row.Cells.Add(cell);

            idxDate = idxDate.AddDays(1);
            return idxDate;
        }

        private DateTime CreateWeekNumberCell(DateTime idxDate, HTML.HtmlTableRow row)
        {
            // Creating week number cell
            // Calculates according to ISO-8601
            HTML.HtmlTableCell week = new HTML.HtmlTableCell();
            week.EnableViewState = false;
            week.Attributes.Add("class", "weekno");
            week.InnerHtml = GetWeekNumber(idxDate).ToString();
            row.Cells.Add(week);
            return idxDate;
        }

        private static DateTime CreateWeekDaysAtTop(DateTime idxDate, HTML.HtmlTable tbl)
        {
            HTML.HtmlTableRow rowDays = new HTML.HtmlTableRow();
            rowDays.EnableViewState = false;
            rowDays.Cells.Add(new HTML.HtmlTableCell());
            rowDays.Attributes.Add("class", "weekdays");
            DateTime idxWeekDay = idxDate;
            for (int idx = 0; idx < 7; idx++)
            {
                HTML.HtmlTableCell cell = new HTML.HtmlTableCell();
                cell.EnableViewState = false;
                cell.InnerHtml = idxWeekDay.ToString("ddd", System.Threading.Thread.CurrentThread.CurrentUICulture);
                rowDays.Cells.Add(cell);
                idxWeekDay = idxWeekDay.AddDays(1);
            }
            tbl.Rows.Add(rowDays);
            return idxDate;
        }

        private int GetWeekNumber(DateTime dt)
        {
            int year = dt.Year;
            DateTime week1;
            if (dt >= new DateTime(year, 12, 29))
            {
                week1 = GetWeekOneDate(year + 1);
                if (dt < week1)
                {
                    week1 = GetWeekOneDate(year);
                }
            }
            else
            {
                week1 = GetWeekOneDate(year);
                if (dt < week1)
                {
                    week1 = GetWeekOneDate(--year);
                }
            }
            return ((dt - week1).Days / 7 + 1);
        }

        private DateTime GetWeekOneDate(int year)
        {
            DateTime date = new DateTime(year, 1, 4);
            int dayNum = (int)date.DayOfWeek; // 0==Sunday, 6==Saturday
            if (dayNum == 0)
                dayNum = 7;
            return date.AddDays(1 - dayNum);
        }

        private void CreateYearMonthPicker(HTML.HtmlTable tbl)
        {
            // Creating header row (with month and year picker)
            HTML.HtmlTableRow headerRow = new HTML.HtmlTableRow();
            headerRow.EnableViewState = false;
            headerRow.ID = "head_" + Value.ToString("dd_MM_yyyy", System.Globalization.CultureInfo.InvariantCulture);
            HTML.HtmlTableCell headerCell = new HTML.HtmlTableCell();
            headerCell.EnableViewState = false;
            headerCell.Style["text-align"] = "center";
            headerCell.ID = "headC_" + Value.ToString("dd_MM_yyyy", System.Globalization.CultureInfo.InvariantCulture);
            headerCell.ColSpan = 8;

            // Year SelectList
            CreateYearPicker(headerCell);

            // Month SelectList
            CreateMonthPicker(headerCell);

            headerRow.Cells.Add(headerCell);
            tbl.Rows.Add(headerRow);
        }

        private void CreateMonthPicker(HTML.HtmlTableCell headerCell)
        {
            SelectList month = new SelectList();
            month.EnableViewState = false;
            month.ID = "month_" + Value.ToString("dd_MM_yyyy", System.Globalization.CultureInfo.InvariantCulture);
            for (int idxMonth = 1; idxMonth < 13; idxMonth++)
            {
                ListItem idxItem = new ListItem();
                idxItem.Text = new DateTime(Value.Year, idxMonth, 1).ToString("MMM", System.Threading.Thread.CurrentThread.CurrentUICulture);
                idxItem.Value = idxMonth.ToString();
                if (idxMonth == Value.Month)
                    idxItem.Selected = true;
                month.Items.Add(idxItem);
            }
            month.SelectedIndexChanged += new EventHandler(month_SelectedIndexChanged);
            headerCell.Controls.Add(month);
        }

        private void CreateYearPicker(HTML.HtmlTableCell headerCell)
        {
            SelectList year = new SelectList();
            year.EnableViewState = false;
            year.ID = "year_" + Value.ToString("dd_MM_yyyy", System.Globalization.CultureInfo.InvariantCulture);
            for (int idxYear = Value.Year - 25; idxYear < Value.Year + 25; idxYear++)
            {
                ListItem idxItem = new ListItem();
                idxItem.Text = idxYear.ToString();
                idxItem.Value = idxYear.ToString();
                if (idxYear == Value.Year)
                    idxItem.Selected = true;
                year.Items.Add(idxItem);
            }
            year.SelectedIndexChanged += new EventHandler(year_SelectedIndexChanged);
            headerCell.Controls.Add(year);
        }

        private void today_Click(object sender, EventArgs e)
        {
            Value = DateTime.Now.Date;
            _content.ReRender();
            _content.Controls.Clear();
            CreateCalendarControls();
            if (SelectedValueChanged != null)
                SelectedValueChanged(this, new EventArgs());
            if (DateClicked != null)
                DateClicked(this, new EventArgs());
        }

        private void month_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectList drop = sender as SelectList;
            int newMonth = Int32.Parse(drop.SelectedItem.Value);
            DateTime newValue = new DateTime(Value.Year, newMonth, Math.Min(28, Value.Day));
            if (Value.Date != newValue.Date)
            {
                Value = newValue;
                _content.ReRender();
                _content.Controls.Clear();
                CreateCalendarControls();
                if (SelectedValueChanged != null)
                    SelectedValueChanged(this, new EventArgs());
            }
        }

        private void year_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectList drop = sender as SelectList;
            int newYear = Int32.Parse(drop.SelectedItem.Value);
            DateTime newValue = new DateTime(newYear, Value.Month, Math.Min(28, Value.Day));
            if (Value.Date != newValue.Date)
            {
                Value = newValue;
                _content.ReRender();
                _content.Controls.Clear();
                CreateCalendarControls();
                if (SelectedValueChanged != null)
                    SelectedValueChanged(this, new EventArgs());
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            DateTime newValue = DateTime.ParseExact(button.ID, "yyyy_MM_dd", System.Globalization.CultureInfo.InvariantCulture);
            if (Value.Date != newValue.Date)
            {
                Value = newValue;
                _content.ReRender();
                _content.Controls.Clear();
                CreateCalendarControls();
                if (SelectedValueChanged != null)
                    SelectedValueChanged(this, new EventArgs());
            }
            if (DateClicked != null)
                DateClicked(this, new EventArgs());
        }
    }
}
