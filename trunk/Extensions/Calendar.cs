/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Helpers;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:Calendar runat=server />")]
    public class Calendar : RaWebControl, IRaControl, ASP.INamingContainer
    {
        public event EventHandler SelectedValueChanged;

        public event EventHandler DateClicked;

        public DateTime Value
        {
            get { return ViewState["Value"] == null ? DateTime.MinValue : (DateTime)ViewState["Value"]; }
            set
            {
                ViewState["Value"] = value;
            }
        }

        [DefaultValue(DayOfWeek.Monday)]
        public DayOfWeek StartsOn
        {
            get { return ViewState["StartsOn"] == null ? DayOfWeek.Monday : (DayOfWeek)ViewState["StartsOn"]; }
            set
            {
                ViewState["StartsOn"] = value;
            }
        }

        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        // Override this one to create specific HTML for your widgets
        public override string GetHTML()
        {
            return string.Format("<div id=\"{0}\"{2}{3}>{1}</div>",
                ClientID,
                GetChildControlsHTML(),
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute());
        }

        public override string GetClientSideScript()
        {
            string tmp = base.GetClientSideScript();
            tmp += GetChildrenClientSideScript(Controls);
            return tmp;
        }

        protected override string GetChildrenClientSideScript()
        {
            return GetChildrenClientSideScript(Controls);
        }

        private string GetChildrenClientSideScript(ASP.ControlCollection controls)
        {
            string retVal = "";
            foreach (ASP.Control idx in controls)
            {
                if (idx.Visible)
                {
                    if (idx is RaControl)
                    {
                        retVal += (idx as RaControl).GetClientSideScript();
                    }
                    retVal += GetChildrenClientSideScript(idx.Controls);
                }
            }
            return retVal;
        }

        private string GetChildControlsHTML()
        {
            // Must set all children to RenderHtml to get this to work...
            SetAllChildrenToRenderHtml(Controls);

            // Streaming Controls into Memory Stream and returning HTML as string...
            MemoryStream stream = new MemoryStream();
            TextWriter writer = new StreamWriter(stream);
            ASP.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(writer);

            // Render children
            RenderChildren(htmlWriter);
            htmlWriter.Flush();
            writer.Flush();

            // Return string representation of HTML
            stream.Position = 0;
            TextReader reader = new StreamReader(stream);
            string retVal = reader.ReadToEnd();
            return retVal;
        }

        private void SetAllChildrenToRenderHtml(ASP.ControlCollection controls)
        {
            foreach (ASP.Control idx in controls)
            {
                if (idx is RaControl)
                {
                    (idx as RaControl).Phase = RenderingPhase.RenderHtml;
                }
                SetAllChildrenToRenderHtml(idx.Controls);
            }
        }

        #endregion

        // IMPORTANT!!
        // When we have controls which contains "special" child controls we need
        // to make sure those controls are being RE-created in the OnLoad overridden method of
        // the Control.
        // This logic is being done in the next two methods...!
        protected override void OnLoad(EventArgs e)
        {
            EnsureChildControls();
            base.OnLoad(e);
        }

        protected override void CreateChildControls()
        {
            CreateCalendarControls();
        }

        private LinkButton SelectedValueBtn
        {
            get { return ViewState["SelectedValueBtn"] == null ? null : (LinkButton)FindControl((string)ViewState["SelectedValueBtn"]); }
            set
            {
                ViewState["SelectedValueBtn"] = value.ID;
            }
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

        private void CreateCalendarControls()
        {
            // Finding date to start on
            DateTime idxDate = new DateTime(Value.Year, Value.Month, 1);
            while (idxDate.DayOfWeek != StartsOn)
            {
                idxDate = idxDate.AddDays(-1);
            }

            // Creating table to wrap the whole thing inside of
            HTML.HtmlTable tbl = new HTML.HtmlTable();
            tbl.EnableViewState = false;
            tbl.ID = "tbl_" + Value.ToString("dd_MM_yyyy", System.Globalization.CultureInfo.InvariantCulture);

            // Creating the header row which contains the Year and Month DropDownLists
            CreateYearMonthPicker(tbl);

            // Creating Weekdays at top of calendar
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

            // Looping through creating childcontrols
            while (true)
            {
                HTML.HtmlTableRow row = new HTML.HtmlTableRow();
                row.EnableViewState = false;
                row.ID = idxDate.ToString("MM_dd", System.Globalization.CultureInfo.InvariantCulture) + Value.ToString("dd_MM_yyyy", System.Globalization.CultureInfo.InvariantCulture);
                
                // Creating week number cell
                // Calculates according to ISO-8601
                HTML.HtmlTableCell week = new HTML.HtmlTableCell();
                week.EnableViewState = false;
                week.Attributes.Add("class", "weekno");
                week.InnerHtml = GetWeekNumber(idxDate).ToString();
                row.Cells.Add(week);

                // Creating week cells for actual dates...
                for (int idx = 0; idx < 7; idx++)
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

                    row.Cells.Add(cell);

                    idxDate = idxDate.AddDays(1);
                }
                tbl.Rows.Add(row);
                if (idxDate.Month != Value.Month)
                    break;
            }

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

            // Rooting the Table as the LAST thing we do...!
            Controls.Add(tbl);
        }

        void today_Click(object sender, EventArgs e)
        {
            Value = DateTime.Now.Date;
            SignalizeReRender();
            Controls.Clear();
            CreateCalendarControls();
            if (SelectedValueChanged != null)
                SelectedValueChanged(this, new EventArgs());
            if (DateClicked != null)
                DateClicked(this, new EventArgs());
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

            // Year DropDownList
            DropDownList year = new DropDownList();
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

            // Month DropDownList
            DropDownList month = new DropDownList();
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

            headerRow.Cells.Add(headerCell);
            tbl.Rows.Add(headerRow);
        }

        void month_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drop = sender as DropDownList;
            int newMonth = Int32.Parse(drop.SelectedItem.Value);
            DateTime newValue = new DateTime(Value.Year, newMonth, Math.Min(28, Value.Day));
            if (Value.Date != newValue.Date)
            {
                Value = newValue;
                SignalizeReRender();
                Controls.Clear();
                CreateCalendarControls();
                if (SelectedValueChanged != null)
                    SelectedValueChanged(this, new EventArgs());
            }
        }

        void year_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drop = sender as DropDownList;
            int newYear = Int32.Parse(drop.SelectedItem.Value);
            DateTime newValue = new DateTime(newYear, Value.Month, Math.Min(28, Value.Day));
            if (Value.Date != newValue.Date)
            {
                Value = newValue;
                SignalizeReRender();
                Controls.Clear();
                CreateCalendarControls();
                if (SelectedValueChanged != null)
                    SelectedValueChanged(this, new EventArgs());
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            DateTime newValue = DateTime.ParseExact(button.ID, "yyyy_MM_dd", System.Globalization.CultureInfo.InvariantCulture);
            if (Value.Date != newValue.Date)
            {
                Value = newValue;
                SignalizeReRender();
                Controls.Clear();
                CreateCalendarControls();
                if (SelectedValueChanged != null)
                    SelectedValueChanged(this, new EventArgs());
            }
            if (DateClicked != null)
                DateClicked(this, new EventArgs());
        }
    }
}
