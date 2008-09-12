/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Collections.Generic;
using Ra.Widgets;

public partial class AjaxDataGrid : System.Web.UI.Page
{
	public class DataGridPeople
	{
		public string _name;
		public bool _isAdmin;
		
		public DataGridPeople(string name, bool isAdmin)
		{
			Name = name;
			IsAdmin = isAdmin;
		}
		
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}
		
		public bool IsAdmin
		{
			get { return _isAdmin; }
			set { _isAdmin = value; }
		}
	}
	
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			datagrid.DataSource = People;
			datagrid.DataBind();
		}
    }
	
	protected void NameChanged(object sender, EventArgs e)
	{
		Ra.Extensions.InPlaceEdit edit = sender as Ra.Extensions.InPlaceEdit;
		lbl.Text = string.Format("Name changed to {0}", edit.Text);
		Effect effect = new EffectFadeIn(lbl, 0.4M);
		effect.Render();
	}
	
	protected void AdminChanged(object sender, EventArgs e)
	{
		Ra.Widgets.CheckBox edit = sender as Ra.Widgets.CheckBox;
		lbl.Text = string.Format("IsAdmin changed to {0}", edit.Checked);
		Effect effect = new EffectFadeIn(lbl, 0.4M);
		effect.Render();
	}
	
	private List<DataGridPeople> People
	{
		get
		{
			if (Session["People"] == null)
			{
				List<DataGridPeople> tmp = new List<DataGridPeople>();
				tmp.Add(new DataGridPeople("Thomas", true));
				tmp.Add(new DataGridPeople("Kariem", true));
				tmp.Add(new DataGridPeople("John Doe", false));
				Session["People"] = tmp;
			}
			return Session["People"] as List<DataGridPeople>;
		}
	}
}
