using System;
using System.ComponentModel;
using ASP = System.Web.UI.WebControls;
using System.Web.UI;

namespace Ra.Widgets
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Button runat=server />")]
    public class Button : ASP.Button
    {
        protected override void OnPreRender(EventArgs e)
        {
            AjaxManager.Instance.IncludeMainRaScript();
            base.OnPreRender(e);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Text);
        }
    }
}
