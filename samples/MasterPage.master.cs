using System;
using Ra.Widgets;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Effect effect = new EffectFadeIn(btnShowCode, 2.0M);
            effect.Render();
        }
    }

    protected void btnShowCode_Click(object sender, EventArgs e)
    {
        pnlShowCode.Visible = true;
        Effect effect = new EffectRollDown(pnlShowCode, 1.0M, 600);
        effect.Render();
    }

    protected void closeShowCode_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectRollUp(pnlShowCode, 1.0M, 600);
        effect.Render();
    }
}
