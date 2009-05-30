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
using System.Collections.Generic;
using Ra;
using Ra.Extensions.Widgets;
using Ra.Effects;

namespace Samples
{
    public partial class CalendarStarter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calendarStart.Value = DateTime.Now.Date;
                calendarEnd.Value = DateTime.Now.AddDays(14).Date;
                UpdateActivitiesGrid();

                // Calendar rendering logic throws if you have StartsOn defined and
                // no date...
                activityWhen.Value = DateTime.Now.Date;
            }
        }

        protected void resizer_Resized(object sender, ResizeHandler.ResizedEventArgs e)
        {
            int width = Math.Max(e.Width - 359, 400);
            int height = Math.Max(e.Height - 101, 200);
            int heightLeft = Math.Max(e.Height - 320, 50);
            pnlBottomLeft.Style["height"] = heightLeft.ToString() + "px";
            wndRight.Style["width"] = width.ToString() + "px";
            pnlRight.Style["height"] = height.ToString() + "px";
        }

        protected void createWindow_EscPressed(object sender, EventArgs e)
        {
            createWindow.Visible = false;
        }

        protected void calendarStart_SelectedValueChanged(object sender, EventArgs e)
        {
            if (calendarStart.Value > calendarEnd.Value)
                calendarEnd.Value = calendarStart.Value.AddDays(1);
            else
            {
                // To force a re-rendering of all our calendars
                calendarEnd.Value = calendarEnd.Value;
            }

            // To force a re-rendering of all our calendars
            calendarStart.Value = calendarStart.Value;
            UpdateActivitiesGrid();
        }

        protected void calendarEnd_SelectedValueChanged(object sender, EventArgs e)
        {
            if (calendarEnd.Value <= calendarStart.Value)
                calendarStart.Value = calendarEnd.Value.AddDays(-1);
            else
            {
                // To force a re-rendering of all our calendars
                calendarStart.Value = calendarStart.Value;
            }
            calendarEnd.Value = calendarEnd.Value;
            UpdateActivitiesGrid();
        }

        private void UpdateActivitiesGrid()
        {
            // Resetting the "current editing item"
            grid.SelectedIndex = -1;

            if (editPnl.Style["display"] != "none")
            {
                new EffectFadeOut(editPnl, 500)
                    .ChainThese(new EffectFadeIn(intro, 500))
                    .Render();
            }

            string str = "";
            str += calendarStart.Value.ToString("dddd dd. MMM yy");
            str += " - ";
            str += calendarEnd.Value.ToString("dddd dd. MMM yy");
            str += " - ";
            str += (calendarEnd.Value - calendarStart.Value).TotalDays + " days";
            wndBottomLeft.Caption = str;
            List<Activity> activities = new List<Activity>(ActivitiesDatabase.Database);
            activities.RemoveAll(
                delegate(Activity idx)
                {
                    return !(idx.When >= calendarStart.Value && idx.When < calendarEnd.Value);
                });
            activities.Sort(
                delegate(Activity left, Activity right)
                {
                    return left.When.CompareTo(right.When);
                });
            grid.DataSource = activities;
            grid.DataBind();
            pnlBottomLeft.ReRender();
        }

        protected void calendarStart_RenderDay(object sender, Calendar.RenderDayEventArgs e)
        {
            if (e.Date >= calendarStart.Value && e.Date < calendarEnd.Value)
            {
                e.Cell.Attributes.Add("class", "dateSelected");
            }
            CheckToSeeIfActivity(e);
        }

        protected void calendarEnd_RenderDay(object sender, Calendar.RenderDayEventArgs e)
        {
            if (e.Date >= calendarStart.Value && e.Date < calendarEnd.Value)
            {
                e.Cell.Attributes.Add("class", "dateSelected");
            }
            CheckToSeeIfActivity(e);
        }

        private static void CheckToSeeIfActivity(Calendar.RenderDayEventArgs e)
        {
            foreach (Activity idx in ActivitiesDatabase.Database)
            {
                if (idx.When.Date == e.Date)
                {
                    string cls = e.Cell.Attributes["class"];
                    if (cls == null)
                        cls = "";
                    cls += " activity";
                    e.Cell.Attributes["class"] = cls;
                    break;
                }
            }
        }

        protected void create_Click(object sender, EventArgs e)
        {
            createWindow.Visible = true;
            createHeader.Text = "Header of activity";
            createHeader.Select();
            createHeader.Focus();
        }

        protected void createBtn_Click(object sender, EventArgs e)
        {
            createWindow.Visible = false;
            Activity a = new Activity(createHeader.Text, createBody.Text, createDate.Value);
            ActivitiesDatabase.Database.Add(a);

            // Making sure our Calendar controls are being re-rendered...
            calendarEnd.Value = calendarEnd.Value;
            calendarStart.Value = calendarStart.Value;
            UpdateActivitiesGrid();
        }

        protected void EditItem(object sender, EventArgs e)
        {
            // Running effects...
            if (intro.Style["display"] != "none")
            {
                new EffectFadeOut(intro, 500)
                    .ChainThese(new EffectFadeIn(editPnl, 500))
                    .Render();
            }
            else
            {
                new EffectHighlight(pnlRight, 500).Render();
            }

            // Finding the Activity which was clicked
            Guid id = new Guid(((sender as LinkButton).Parent.Controls[1] as HiddenField).Value);

            // Setting the BG color of the current editing item in our GridView...
            int index = ((sender as LinkButton).Parent.Parent as System.Web.UI.WebControls.GridViewRow).RowIndex;
            grid.SelectedIndex = index;
            pnlBottomLeft.ReRender();

            // Finding our activity
            Activity a = ActivitiesDatabase.Database.Find(
                delegate(Activity idx)
                {
                    return idx.ID == id;
                });

            // "Binding" the Editing parts...
            activityHeader.Text = a.Header;
            activityBody.Text = a.Body;
            activityBody.Select();
            activityBody.Focus();
            activityWhen.Value = a.When;
            activityId.Value = a.ID.ToString();
        }

        protected void DeleteItem(object sender, EventArgs e)
        {
            // Running effects...
            if (editPnl.Style["display"] != "none")
            {
                new EffectFadeOut(editPnl, 500)
                    .ChainThese(new EffectFadeIn(intro, 500))
                    .Render();
            }
            new EffectHighlight(pnlBottomLeft, 500).Render();

            // Finding the Activity which was clicked
            Guid id = new Guid(((sender as LinkButton).Parent.Controls[1] as HiddenField).Value);

            // Finding our activity
            ActivitiesDatabase.Database.RemoveAll(
                delegate(Activity idx)
                {
                    return idx.ID == id;
                });
            grid.SelectedIndex = -1;
            calendarEnd.Value = calendarEnd.Value;
            calendarStart.Value = calendarStart.Value;
            UpdateActivitiesGrid();
        }

        protected void save_Click(object sender, EventArgs e)
        {
            Guid id = new Guid(activityId.Value);

            Activity a = ActivitiesDatabase.Database.Find(
                delegate(Activity idx)
                {
                    return idx.ID == id;
                });
            a.Body = activityBody.Text;
            a.Header = activityHeader.Text;
            a.When = activityWhen.Value;
            UpdateActivitiesGrid();

            // Some nice effects...
            new EffectFadeOut(editPnl, 500)
                .ChainThese(new EffectFadeIn(intro, 500))
                .Render();

            // Making sure we're re-rendering our Calendars
            calendarEnd.Value = calendarEnd.Value;
            calendarStart.Value = calendarStart.Value;

            status.Text = "Updating item";
            new EffectHighlightText(status, 500).Render();
        }

        protected void close_Click(object sender, EventArgs e)
        {
            // Updating to remove the EditIndex bugger...
            UpdateActivitiesGrid();

            // Some nice effects...
            new EffectFadeOut(editPnl, 500)
                .ChainThese(new EffectFadeIn(intro, 500))
                .Render();
        }
    }
}