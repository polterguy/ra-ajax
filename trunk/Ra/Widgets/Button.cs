using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;

namespace Ra.Widgets
{
    [DefaultProperty("Text")]
    [ASP.ToolboxData("<{0}:Button runat=server />")]
    public class Button : ASP.Control
    {
        protected override void OnInit(EventArgs e)
        {
            AjaxManager.Instance.IncludeMainRaScript();
            base.OnInit(e);
        }
    }
}
