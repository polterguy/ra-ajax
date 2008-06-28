using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;

namespace Ra.Widgets
{
    [DefaultProperty("Text")]
    [ASP.ToolboxData("<{0}:Button runat=server />")]
    public class Button : RaControl, IRaControl
    {
        public event EventHandler Clicked;

        protected override void OnInit(EventArgs e)
        {
            // To initialize control
            AjaxManager.Instance.InitializeControl(this);

            // Including JavaScript
            AjaxManager.Instance.IncludeMainRaScript();
            AjaxManager.Instance.IncludeMainControlScripts();

            // Registering control with the AjaxManager
            AjaxManager.Instance.CurrentPage.RegisterRequiresControlState(this);

            base.OnInit(e);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            switch (Phase)
            {
                case RenderingPhase.Destroy:
                    // TODO: Destroy control
                    break;
                case RenderingPhase.Invisible:
                    // Do NOTHING
                    break;
                case RenderingPhase.MadeVisibleThisRequest:
                    // Replace wrapper span for control
                    break;
                case RenderingPhase.PropertyChanges:
                    // Serialize JSON changes to control
                    break;
                case RenderingPhase.RenderHtml:
                    writer.Write("<input type=\"button\" value=\"Value\" id=\"{0}\" />", ClientID);
                    break;
            }
        }

        public override void DispatchEvent(string name)
        {
            switch (name)
            {
                case "click":
                    if (Clicked != null)
                        Clicked(this, new EventArgs());
                    break;
                default:
                    throw new ApplicationException("Unknown event fired for control");
            }
        }
    }
}
