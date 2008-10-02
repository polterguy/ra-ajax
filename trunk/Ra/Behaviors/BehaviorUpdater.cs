/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using System.Drawing;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;

namespace Ra.Widgets
{
    /**
     * Will render an "obscure entire webpage" DIV with a very high z-index when control attached
     * to raises an Ajax Request
     */
    [ASP.ToolboxData("<{0}:BehaviorUpdater runat=\"server\" />")]
	public class BehaviorUpdater : Behavior
	{
        /**
         * Number of milliseconds which will pass before the obscurer will animate into view
         * and show up. If the request is going fast then by having a sane value for this property
         * you can make sure that only the "long" requests are actually running the updater logic
         * and obscuring the page.
         */
        public int Delay
        {
            get { return ViewState["Delay"] == null ? 0 : (int)ViewState["Delay"]; }
            set
            {
                if (value != Delay)
                    SetJSONValueObject("Delay", value);
                ViewState["Delay"] = value;
            }
        }

        /**
         * The color of the obscurer DOM element
         */
        public Color Color
        {
            get { return ViewState["Color"] == null ? System.Drawing.Color.Black : (Color)ViewState["Color"]; }
            set
            {
                if (value != Color)
                    SetJSONValueObject("Color", value);
                ViewState["Color"] = value;
            }
        }

		public override string GetRegistrationScript ()
		{
			string options = string.Empty;
			if( Delay != 0 )
			{
				options += string.Format("delay:{0}", Delay);
			}
			if( Color != System.Drawing.Color.Black )
			{
				if (options != string.Empty)
					options += ",";
				options += string.Format("color:'{0}'", System.Drawing.ColorTranslator.ToHtml(Color));
			}
			if( options != string.Empty)
				options = ",{" + options + "}";
			return string.Format("new Ra.BUpDel('{0}'{1})", this.ClientID, options);
		}

        protected override string GetClientSideScript()
		{
			return "";
		}
	}
}
