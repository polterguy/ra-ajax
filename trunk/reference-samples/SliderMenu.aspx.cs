﻿using System;
using Ra.Extensions;
using Ra.Widgets;

namespace RefSamples
{
    public partial class SliderMenuSample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            count.Text = "0";
        }

        protected void slider_ItemClicked(object sender, EventArgs e)
        {
            SlidingMenuItem item = sender as SlidingMenuItem;
            if (item.IsLeaf)
            {
                lbl.Text = item.ID;
            }
        }

        protected void window_GetChildNodes(object sender, EventArgs e)
        {
            int ct = int.Parse(count.Text);
            ct += 1;
            count.Text = ct.ToString();

            SlidingMenuLevel level = sender as SlidingMenuLevel;
            for (int idx = 0; idx < 5; idx++)
            {
                SlidingMenuItem i = new SlidingMenuItem();
                i.ID = level.ID + idx;
                //i.Text = "Window " + idx;
                level.Controls.Add(i);

                LinkButton b = new LinkButton();
                b.Text = "asdfoijh";
                i.Content.Controls.Add(b);

                SlidingMenuLevel l = new SlidingMenuLevel();
                l.Caption = "Window " + idx;
                l.ID = level.ID + "LL" + idx;
                l.GetChildNodes += window_GetChildNodes;
                i.Controls.Add(l);
            }
        }

        protected void views_GetChildNodes(object sender, EventArgs e)
        {
            SlidingMenuLevel level = sender as SlidingMenuLevel;
            for (int idx = 0; idx < 5; idx++)
            {
                SlidingMenuItem i = new SlidingMenuItem();
                i.ID = "dyn" + idx;
                i.Text = "View " + idx;
                level.Controls.Add(i);

                if (idx < 3)
                {
                    SlidingMenuLevel l = new SlidingMenuLevel();
                    l.ID = "dynChild" + idx;
                    l.GetChildNodes += l_GetChildNodes;
                    i.Controls.Add(l);
                }
            }
        }

        void l_GetChildNodes(object sender, EventArgs e)
        {
            SlidingMenuLevel level = sender as SlidingMenuLevel;

            SlidingMenuItem i = new SlidingMenuItem();
            i.ID = "dynOpen" + level.ID;
            i.Text = "Open View";
            level.Controls.Add(i);

            i = new SlidingMenuItem();
            i.ID = "dynClose" + level.ID;
            i.Text = "Close View";
            level.Controls.Add(i);
        }
    }
}