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
     * Will make the Control veiled by default and will be unveiled only upon mouse hover.
     */
    [ASP.ToolboxData("<{0}:BehaviorUnveiler runat=\"server\" />")]
	public class BehaviorUnveiler : Behavior
	{
        public BehaviorUnveiler()
        { }

        public BehaviorUnveiler(decimal minOpacity, decimal maxOpacity)
        {
            MinOpacity = minOpacity;
            MaxOpacity = maxOpacity;
        }

        /**
         * The initial opacity of the Control when it is veiled.
         */
        [DefaultValue(0.3)]
        public decimal MinOpacity
        {
            get { return ViewState["MinOpacity"] == null ? 0.3M : (decimal)ViewState["MinOpacity"]; }
            set
            {
                if (value < 0.0M || value > 1.0M)
                    throw new ArgumentException("MinOpacity value must be between 0.0 and 1.0");
                if (value != MinOpacity)
                    SetJSONValueObject("MinOpacity", value);
                ViewState["MinOpacity"] = value;
            }
        }

        /**
         * The maximum opacity of the Control when the mouse hovers over it.
         */
        [DefaultValue(1.0)]
        public decimal MaxOpacity
        {
            get { return ViewState["MaxOpacity"] == null ? 1.0M : (decimal)ViewState["MaxOpacity"]; }
            set
            {
                if (value < 0.0M || value > 1.0M)
                    throw new ArgumentException("MaxOpacity value must be between 0.0 and 1.0");
                if (value != MaxOpacity)
                    SetJSONValueObject("MaxOpacity", value);
                ViewState["MaxOpacity"] = value;
            }
        }

		public override string GetRegistrationScript()
		{
            string options = string.Empty;
            if (MinOpacity != 0.3M)
            {
                options += string.Format("minOpacity:{0}", MinOpacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            if (MaxOpacity != 1.0M)
            {
                if (options != string.Empty)
                    options += ",";
                options += string.Format("maxOpacity:{0}", MaxOpacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            if (options != string.Empty)
                options = ",{" + options + "}";

            return string.Format("new Ra.BUnveil('{0}'{1})", this.ClientID, options);
		}

        protected override string GetClientSideScript()
		{
			return "";
		}
    }
}
