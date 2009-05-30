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
using Ra.Extensions.Widgets;

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
                    if (search.Text == "" || search.Text == "Filter")
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
            if (search.Text == "Filter")
                search.Text = "";
        }

        protected void search_Blur(object sender, EventArgs e)
        {
            if (search.Text == "")
            {
                search.Text = "Filter";
            }
        }

        protected void search_KeyUp(object sender, EventArgs e)
        {
            DataBindRepeater();
            repWrp.ReRender();
        }

        protected void ViewCustomer(object sender, EventArgs e)
        {
            ExtButton btn = sender as ExtButton;
            Guid customerID = new Guid(btn.Xtra);
            createNew.Visible = true;
            createNew.Caption = "Edit existing customer";
            Customer cust = Customer.Customers.Find(
                delegate(Customer idx)
                {
                    return idx.ID == customerID;
                });
            custID.Value = cust.ID.ToString();
            name.Text = cust.Name;
            adr.Text = cust.Address;
            name.Focus();
            name.Select();
        }

        protected void DeleteCustomer(object sender, EventArgs e)
        {
            ExtButton btn = sender as ExtButton;
            Guid customerID = new Guid(btn.Xtra);
            Customer.Customers.RemoveAll(
                delegate(Customer idx)
                {
                    return idx.ID == customerID;
                });
            DataBindRepeater();
            repWrp.ReRender();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            createNew.Visible = true;
            createNew.Caption = "Create new customer";
            custID.Value = "";
            name.Text = "Name of customer";
            adr.Text = "Adr of customer";
            name.Select();
            name.Focus();
        }

        protected void ok_Click(object sender, EventArgs e)
        {
            createNew.Visible = false;
            Customer cust = null;
            if (custID.Value != "")
            {
                Guid id = new Guid(custID.Value);
                cust = Customer.Customers.Find(
                    delegate(Customer idx)
                    {
                        return idx.ID == id;
                    });
                cust.Name = name.Text;
                cust.Address = adr.Text;
            }
            else
            {
                cust = new Customer(name.Text, adr.Text);
                Customer.Customers.Add(cust);
            }
            DataBindRepeater();
            repWrp.ReRender();
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            createNew.Visible = false;
        }
    }
}
