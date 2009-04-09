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

namespace Samples
{
    public partial class CRMSample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBindRepeater();
        }

        private void DataBindRepeater()
        {
            rep.DataSource = Customer.Customers.FindAll(
                delegate(Customer idx)
                {
                    if (search.Text == "" || search.Text == "Search")
                        return true;
                    else
                        return 
                            idx.Name.ToLower().Contains(search.Text.ToLower()) || 
                            idx.Address.ToLower().Contains(search.Text.ToLower());
                });
            rep.DataBind();
        }

        protected void search_Focused(object sender, EventArgs e)
        {
            search.Text = "";
        }

        protected void search_Blur(object sender, EventArgs e)
        {
            if (search.Text == "")
            {
                search.Text = "Search";
            }
        }

        protected void search_KeyUp(object sender, EventArgs e)
        {
            DataBindRepeater();
            repWrp.ReRender();
        }

        protected void ViewContacts(object sender, EventArgs e)
        {
            ExtButton btn = sender as ExtButton;
            int contactID = int.Parse(btn.Xtra);
        }
    }
}
