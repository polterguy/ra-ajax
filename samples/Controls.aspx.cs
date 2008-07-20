using System;
using Ra.Widgets;

public partial class Controls : System.Web.UI.Page
{
    protected void btn_Click(object sender, EventArgs e)
    {
        btn.Text = "Clicked";
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        chk.Text = "Clicked";
    }

    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        Effect effect = new EffectFadeIn(drop, 1.0M);
        effect.Render();
        hid.Value = drop.SelectedItem.Value;
    }

    protected void btn2_Click(object sender, EventArgs e)
    {
        btn2.Text = hid.Value;
    }

    protected void imgBtn_Click(object sender, EventArgs e)
    {
        imgBtn.AlternateText = "New alternate text";
        lblIMGButton.Text = "ImageButton was clicked";
    }

    protected void btn3_Click(object sender, EventArgs e)
    {
        lbl.Text = "Changed value";
    }

    protected void btn4_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectFadeIn(pnl, 1.0M);
        effect.Render();
    }

    protected void rdo_CheckedChanged(object sender, EventArgs e)
    {
        rdo1.Text = rdo1.Checked.ToString();
        rdo2.Text = rdo2.Checked.ToString();
    }

    protected void txtArea_TextChanged(object sender, EventArgs e)
    {
        txtArea.Text = "Did you see the update?";
    }

    protected void txtBox_TextChanged(object sender, EventArgs e)
    {
        txtBox.Text = "Did you see the update?";
    }

    protected void lnkBtn_Click(object sender, EventArgs e)
    {
        lnkBtn.Text = "You clicked me :)";
    }

    protected void hover(object sender, EventArgs e)
    {
        hoverLnkBtn.Text = "HOVERED!!";
    }

    protected void hoverOut(object sender, EventArgs e)
    {
        hoverLnkBtn.Text = "No hover";
    }
}
