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
using System.Web.UI.DataVisualization.Charting;
using Ra.Effects;

namespace Samples
{
    public partial class Chart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindChart();
                lastVal.Select();
                lastVal.Focus();
            }
        }

        private void BindChart()
        {
            Series series = new Series("Column");
            series.ChartType = SeriesChartType.Column;
            series.BorderWidth = 2;
            series.ShadowOffset = 3;
            series.Points.AddY(13);
            series.Points.AddY(56);
            series.Points.AddY(11);
            Chart1.Series.Add(series);
            series = new Series("Spline");
            series.ChartType = SeriesChartType.Spline;
            series.BorderWidth = 5;
            series.ShadowOffset = 4;
            series.Points.AddY(22);
            series.Points.AddY(27);
            series.Points.AddY(int.Parse(lastVal.Text));
            Chart1.Series.Add(series);
        }

        protected void ChangeChart(object sender, EventArgs e)
        {
            BindChart();
            chartWrp.ReRender();
            lastVal.Select();
            lastVal.Focus();
            chartWrp.Style["display"] = "none";
            new EffectFadeIn(chartWrp, 400)
                .Render();
        }
    }
}
