/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
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
     * BehaviorObscurable, obscures all elements with lower z-index than the Parent Control.
     * Useful for e.g. creating modal windows and similar constructs.
     */
    [ASP.ToolboxData("<{0}:BehaviorObscurable runat=\"server\" />")]
    public class BehaviorObscurable : Behavior
	{
        /**
         * Amount of transparency for obscurer. 0.0 == 100% transparent (which is pointless), 
         * 1.0 = 100% non-transparent. Value must be in range [0.0, 1.0].
         */
        [DefaultValue(0.5)]
        public decimal Opacity
        {
            get { return ViewState["Opacity"] == null ? 0.5M : (decimal)ViewState["Opacity"]; }
            set
            {
                if (value < 0.0M || value > 1.0M)
                    throw new ArgumentException("Opacity value must be between 0.0 and 1.0");
                if (value != Opacity)
                    SetJSONValueObject("Opacity", value);
                ViewState["Opacity"] = value;
            }
        }

        /**
         * Explicitly overridden z-index style value of obscurer, otherwise Parent Control z-index
         * -1 will be used
         */
        [DefaultValue(-1)]
        public int ZIndex
        {
            get { return ViewState["ZIndex"] == null ? -1 : (int)ViewState["ZIndex"]; }
            set
            {
                if (value != ZIndex)
                    SetJSONValueObject("ZIndex", value);
                ViewState["ZIndex"] = value;
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

        public override string GetRegistrationScript()
		{
			string options = string.Empty;
            if (Color != System.Drawing.Color.Black)
            {
                if (options != string.Empty)
                    options += ",";
                options += string.Format("color:'{0}'", System.Drawing.ColorTranslator.ToHtml(Color));
            }
            if (ZIndex != -1)
            {
                if (options != string.Empty)
                    options += ",";
                options += string.Format("zIndex:'{0}'", ZIndex);
            }
            if (Opacity != 0.5M)
            {
                if (options != string.Empty)
                    options += ",";
                options += string.Format("opacity:'{0}'", Opacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            if (options != string.Empty)
				options = ",{" + options + "}";
            return string.Format("new Ra.BObscur('{0}'{1})", this.ClientID, options);
		}

        protected override string GetClientSideScript()
		{
			return "";
		}
	}
}
