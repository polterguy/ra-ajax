/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI;
using RaSelector;

namespace Samples
{
    public partial class Dynamic_Sample : Dynamic
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dynamic.LoadControls("Chart.ascx");
            }
        }

        protected void dynamic_Reload(object sender, Ra.Widgets.Dynamic.ReloadEventArgs e)
        {
            Control ctrl = Page.LoadControl(e.Key);
            if (e.Key == "Chart.ascx")
            {
                Series series = new Series("Column");
                series.ChartType = SeriesChartType.Column;
                series.BorderWidth = 2;
                series.ShadowOffset = 3;
                series.Points.AddY(First);
                series.Points.AddY(Second);
                series.Points.AddY(Third);
                Selector.SelectFirst<System.Web.UI.DataVisualization.Charting.Chart>(ctrl).Series.Add(series);
                series = new Series("Spline");
                series.ChartType = SeriesChartType.Spline;
                series.BorderWidth = 5;
                series.ShadowOffset = 4;
                series.Points.AddY(First);
                series.Points.AddY(Second);
                series.Points.AddY(Third);
                Selector.SelectFirst<System.Web.UI.DataVisualization.Charting.Chart>(ctrl).Series.Add(series);
                reset.Visible = true;
            }
            if (IsPostBack)
                new EffectFadeIn(dynamic, 300)
                    .Render();
            dynamic.Controls.Add(ctrl);
        }

        protected void reset_Click(object sender, EventArgs e)
        {
            dynamic.LoadControls("ChartDataCollector.ascx");
            reset.Visible = false;
        }

        public override void Change(string from)
        {
            switch (from)
            {
                case "ChartDataCollector.ascx":
                    dynamic.LoadControls("Chart.ascx");
                    break;
            }
        }
    }
}
