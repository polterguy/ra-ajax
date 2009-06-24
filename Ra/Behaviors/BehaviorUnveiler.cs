/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using System.ComponentModel;
using System.Drawing;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;

namespace Ra.Behaviors
{
    /**
     * Will make the Control veiled by default and will be unveiled only upon mouse hover. Useful for
     * moving "focus" around on the screen according to where the user have his mouse.
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
         * The initial opacity of the Control when it is veiled. The default value is 0.3.
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
         * The maximum opacity of the Control when the mouse hovers over it. The default 
         * value is 1.0 which is the maximum possible value.
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

        /**
         * The duration of time in milliseconds that the unveiling effect will take to execute. The
         * default value is 500 milliseconds.
         */
        [DefaultValue(500)]
        public int Duration
        {
            get { return ViewState["Duration"] == null ? 500 : (int)ViewState["Duration"]; }
            set
            {
                if (value != Duration)
                    SetJSONValueObject("Duration", value);
                ViewState["Duration"] = value;
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
            if (Duration != 500)
            {
                if (options != string.Empty)
                    options += ",";
                options += string.Format("duration:{0}", Duration.ToString(System.Globalization.CultureInfo.InvariantCulture));
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
