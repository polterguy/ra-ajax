using System;
using Ra.Extensions;

namespace RefSamples
{
    public partial class SliderMenuSample : System.Web.UI.Page
    {
        protected void slider_ItemClicked(object sender, EventArgs e)
        {
            SliderMenuItem item = sender as SliderMenuItem;
            //item.Text = "Clicked";
        }
    }
}