using System;
using Ra.Widgets;

public partial class SandBox : System.Web.UI.Page
{
    protected void btnContinue1_Click(object sender, EventArgs e)
    {
        Effect eff = new EffectRollDown(panelResults, 1.0M, 500);
        eff.Render();

        Effect eff2 = new EffectFadeIn(panelResults, 1.0M);
        eff2.Render();
    }
}
