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
            AjaxManager.Instance.IncludeMainControlScripts();

            AjaxManager.Instance.CurrentPage.ClientScript.RegisterClientScriptBlock(typeof(Button), "register", 
                string.Format(@"
<script type=""text/javascript"">
function loaded() {{
  new Ra.Control('{0}');
}}

setTimeout(loaded, 10);

</script>
", ClientID));
            base.OnInit(e);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            writer.Write("<input type=\"button\" value=\"Value\" id=\"{0}\" />", ClientID);
            base.Render(writer);
        }
    }
}
