/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using System.Threading;

namespace Samples
{
    public partial class DynamicControlSample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void staticButton_Click(object sender, EventArgs e)
        {
            label.Text = "Hi From Static Button";
        }

        protected void dynamicControl1_DynamicLoad(object sender, DynamicControl.DynamicControlEventArgs e)
        {
            switch (e.DynamicControlID)
            {
                case "dynamicButton":
                    AddDynamicButton();
                    break;
                default:
                    // First Time
                    AddDynamicButton();
                    break;
            }
        }

        private void AddDynamicButton()
        {
            Button dynamicButton = new Button();
            dynamicButton.ID = "dynamicButton";
            dynamicButton.Text = "Change Label Text (Dynamic Button)";
            dynamicButton.Click += delegate { label.Text = "Hi from Dynamic Button\n"; };

            dynamicControl1.AddControl(dynamicButton);
        }
    }
}
