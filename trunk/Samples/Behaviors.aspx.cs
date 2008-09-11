/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Drawing;
using Ra.Widgets;

public partial class Behaviors : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			dragger1.Bounds = new Rectangle(350, 550, 500, 300);
			dragger1.Snap = new Point(10, 10);
			dragger2.Bounds = new Rectangle(350, 550, 500, 300);
			sliderDragger.Bounds = new Rectangle(328, 1031, 170, 0);
		}
	}
	
	protected void dropper_Dropped(object sender, BehaviorDroppable.DroppedEventArgs e)
	{
		dropperLbl.Text = e.Dragger.Parent.ID + " was dropped";
	}
	
    protected void dragger1_Dropped(object sender, EventArgs e)
    {
		lbl1.Text = string.Format("Label dragged and dropped to {0}, {1}", lbl1.Style["left"], lbl1.Style["top"]);
		Effect effect = new EffectFadeIn(lbl1, 0.4M);
		effect.Render();
    }

    protected void dragger2_Dropped(object sender, EventArgs e)
    {
		lbl2.Text = string.Format("Label dragged and dropped to {0}, {1}", lbl2.Style["left"], lbl2.Style["top"]);
		Effect effect = new EffectFadeIn(lbl2, 0.4M);
		effect.Render();
    }
	
    protected void draggerDragger_Dropped(object sender, EventArgs e)
    {
		slider.Text = (Int32.Parse(slider.Style["left"].Replace("px", "")) - 328).ToString();
		Effect effect = new EffectFadeIn(slider, 0.4M);
		effect.Render();
    }
	
	protected void btnHide1_Click(object sender, EventArgs e)
	{
		lbl1.Visible = !lbl1.Visible;
		btnHide1.Text = lbl1.Visible ? "Hide first label" : "Show first label"; 
	}
	
	protected void btnHide2_Click(object sender, EventArgs e)
	{
		lbl2.Visible = !lbl2.Visible;
		btnHide2.Text = lbl2.Visible ? "Hide second label" : "Show second label"; 
	}
}
