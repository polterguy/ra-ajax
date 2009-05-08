using System;
using Ra.Extensions;
using Ra.Widgets;

namespace RefSamples
{
    public partial class SliderMenuSample : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            slider.BreadCrumbControl = customBreadParent;

            if (!IsPostBack)
            {
                btn.Text = slider.ActiveLevel;
            }
        }

        protected void slider_Navigate(object sender, EventArgs e)
        {
            btn.Text = slider.ActiveLevel;
        }

        protected void foo(object sender, EventArgs e)
        {
            btn.Text = btn.Text + "howdy";
            System.Threading.Thread.Sleep(2000);
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
            SlidingMenuLevel level = sender as SlidingMenuLevel;
            for (int idx = 0; idx < 5; idx++)
            {
                SlidingMenuItem i = new SlidingMenuItem();
                i.ID = level.ID + idx;
                i.Text = "Window " + idx;
                level.Controls.Add(i);

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