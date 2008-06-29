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

        public override string GetClientSideScript()
        {
            if( Clicked == null )
                return string.Format("new Ra.Control('{0}');", ClientID);
            else
                return string.Format("new Ra.Control('{0}', {{evts:['click']}});", ClientID);
        }

        public override string GetHTML()
        {
            return string.Format("<input type=\"button\" value=\"Value\" id=\"{0}\" />", ClientID);
        }
    }
}
