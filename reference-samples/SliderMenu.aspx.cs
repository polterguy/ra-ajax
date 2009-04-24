using System;
using Ra.Extensions;

namespace RefSamples
{
    public partial class SliderMenuSample : System.Web.UI.Page
    {
        protected void slider_ItemClicked(object sender, EventArgs e)
        {
            SliderMenuItem item = sender as SliderMenuItem;
            if (item.IsLeaf)
            {
                lbl.Text = item.ID;
            }
        }

        protected void views_GetChildNodes(object sender, EventArgs e)
        {
            SliderMenuLevel level = sender as SliderMenuLevel;
            for (int idx = 0; idx < 5; idx++)
            {
                SliderMenuItem i = new SliderMenuItem();
                i.ID = "dyn" + idx;
                i.Text = "View " + idx;
                level.Controls.Add(i);

                if (idx < 3)
                {
                    SliderMenuLevel l = new SliderMenuLevel();
                    l.ID = "dynChild" + idx;
                    l.GetChildNodes += l_GetChildNodes;
                    i.Controls.Add(l);
                }
            }
        }

        void l_GetChildNodes(object sender, EventArgs e)
        {
            SliderMenuLevel level = sender as SliderMenuLevel;

            SliderMenuItem i = new SliderMenuItem();
            i.ID = "dynOpen" + level.ID;
            i.Text = "Open View";
            level.Controls.Add(i);

            i = new SliderMenuItem();
            i.ID = "dynClose" + level.ID;
            i.Text = "Close View";
            level.Controls.Add(i);
        }
    }
}