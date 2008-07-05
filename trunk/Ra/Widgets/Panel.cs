/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc in addition to that 
 * the code also is licensed under a pure GPL license for those that
 * cannot for some reasons obey by rules in the MIT(ish) kind of license.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Helpers;
using System.IO;

namespace Ra.Widgets
{
    [DefaultProperty("CssClass")]
    [ASP.ToolboxData("<{0}:Panel runat=server></{0}:Panel>")]
    public class Panel : RaWebControl, IRaControl, ASP.INamingContainer
    {
        #region [ -- Overridden (abstract/virtual) methods from RaControl -- ]

        // Override this one to create specific HTML for your widgets
        public override string GetHTML()
        {
            return string.Format("<div id=\"{0}\"{2}{3}>{1}</div>",
                ClientID,
                GetChildControlsHTML(),
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute());
        }

        public override string GetClientSideScript()
        {
            string tmp = base.GetClientSideScript();
            tmp += GetChildrenClientSideScript(Controls);
            return tmp;
        }

        private string GetChildrenClientSideScript(ASP.ControlCollection controls)
        {
            string retVal = "";
            foreach (ASP.Control idx in controls)
            {
                if (idx.Visible)
                {
                    if (idx is RaControl)
                    {
                        retVal += (idx as RaControl).GetClientSideScript();
                    }
                    retVal += GetChildrenClientSideScript(idx.Controls);
                }
            }
            return retVal;
        }

        private string GetChildControlsHTML()
        {
            // Must set all children to RenderHtml to get this to work...
            SetAllChildrenToRenderHtml(Controls);

            // Streaming Controls into Memory Stream and returning HTML as string...
            MemoryStream stream = new MemoryStream();
            TextWriter writer = new StreamWriter(stream);
            ASP.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(writer);

            // Render children
            RenderChildren(htmlWriter);
            htmlWriter.Flush();
            writer.Flush();

            // Return string representation of HTML
            stream.Position = 0;
            TextReader reader = new StreamReader(stream);
            string retVal = reader.ReadToEnd();
            return retVal;
        }

        private void SetAllChildrenToRenderHtml(ASP.ControlCollection controls)
        {
            foreach (ASP.Control idx in controls)
            {
                if (idx is RaControl)
                {
                    (idx as RaControl).Phase = RenderingPhase.RenderHtml;
                }
                SetAllChildrenToRenderHtml(idx.Controls);
            }
        }

        #endregion
    }
}
