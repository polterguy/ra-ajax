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
            AddDynamicButton();
        }

        protected void dynamicControl1_DynamicLoad(object sender, DynamicControl.DynamicControlEventArgs e)
        {
            switch (e.DynamicControlTypeName)
            {
                case "dynamicButton":
                    AddDynamicButton();
                    break;
            }
        }

        private void AddDynamicButton()
        {
            Button dynamicButton = new Button();
            dynamicButton.ID = "dynamicButton";
            dynamicButton.Text = "Change Label Text";
            dynamicButton.Click += delegate { label.Text = string.Format("From Dynamic Button Click: {0}", DateTime.Now); };

            dynamicControl1.SetControl(dynamicButton, "dynamicButton");
        }
    }
}
