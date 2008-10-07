/*
 * Ra-Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;

namespace Samples
{
    public partial class Dynamic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ViewState["control"] = dropper.SelectedItem.Value;

            if (ViewState["control"].Equals("custom"))
            {
                LoadCustomControls();
            }
            else
            {
                System.Web.UI.Control ctrl = LoadControl(ViewState["control"].ToString() + ".ascx");
                pnlDynamicControls.Controls.Add(ctrl);
            }
        }

        private void LoadCustomControls()
        {
            Button tmp = new Button();
            tmp.Text = "Click me";
            tmp.Click += new EventHandler(tmp_Click);
            pnlDynamicControls.Controls.Add(tmp);
        }

        void tmp_Click(object sender, EventArgs e)
        {
            (sender as Button).Text = "Clicked...!!";
        }

        protected void dropper_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlDynamicControls.Controls.Clear();
            if (dropper.SelectedItem.Value == "custom")
            {
                LoadCustomControls();
            }
            else
            {
                System.Web.UI.Control ctrl = LoadControl(dropper.SelectedItem.Value + ".ascx");
                pnlDynamicControls.Controls.Add(ctrl);
            }
            pnlDynamicControls.ReRender();
            ViewState["control"] = dropper.SelectedItem.Value;
        }
    }
}
