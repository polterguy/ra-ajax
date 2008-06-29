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

        // Override this one to handle events fired on the client-side
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

        // Override this one to create specific initialization script for your widgets
        public override string GetClientSideScript()
        {
            if( Clicked == null )
                return string.Format("new Ra.Control('{0}');", ClientID);
            else
                return string.Format("new Ra.Control('{0}', {{evts:['click']}});", ClientID);
        }

        // Override this one to create specific HTML for your widgets
        public override string GetHTML()
        {
            return string.Format("<input type=\"button\" value=\"Value\" id=\"{0}\" />", ClientID);
        }
    }
}
