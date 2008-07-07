using System;
using Ra.Widgets;

public partial class Author : System.Web.UI.Page
{
    protected void drop_click(object sender, EventArgs e)
    {
        pnlEmail.Visible = true;
        Effect effect = new EffectFadeIn(pnlEmail, 1.0M);
        effect.Render();
        effect = new EffectRollDown(pnlEmail, 1.0M, 60);
        effect.Render();
    }
}
