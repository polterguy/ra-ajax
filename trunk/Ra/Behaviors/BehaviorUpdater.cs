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
     * Will render an "obscure entire webpage" div with a very high z-index when control attached
     * to raises an Ajax Request. This is kind of like the Ra-Ajax version of the (in)famous Ajax GIF
     * feature.
     */
    [ASP.ToolboxData("<{0}:BehaviorUpdater runat=\"server\" />")]
	public class BehaviorUpdater : Behavior
	{
        public BehaviorUpdater()
        { }

        public BehaviorUpdater(int delay)
        {
            Delay = delay;
        }

        public BehaviorUpdater(int delay, decimal opacity)
        {
            Delay = delay;
            Opacity = opacity;
        }

        public BehaviorUpdater(int delay, decimal opacity, Color color)
        {
            Delay = delay;
            Opacity = opacity;
            Color = color;
        }

        /**
         * Number of milliseconds which will pass before the obscurer will animate into view
         * and show up. If the request is going fast then by having a sane value for this property
         * you can make sure that only the "long" requests are actually running the updater logic
         * and obscuring the page.
         */
        [DefaultValue(0)]
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
         * Amount of transparency for updater. 0.0 == 100% transparent, 1.0 = 100% non-transparent.
         * Value must be in range [0.0, 1.0].
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
        
        /**
         * Explicitly overridden z-index style value of updater.
         */
        [DefaultValue(1000)]
        public int ZIndex
        {
            get { return ViewState["ZIndex"] == null ? 1000 : (int)ViewState["ZIndex"]; }
            set
            {
                if (value != ZIndex)
                    SetJSONValueObject("ZIndex", value);
                ViewState["ZIndex"] = value;
            }
        }

        /**
         * The url of an optional image to display with the updater.
         */
        public string ImageUrl
        {
            get { return ViewState["ImageUrl"] == null ? "" : ViewState["ImageUrl"].ToString(); }
            set
            {
                if (value != ImageUrl)
                    SetJSONValueString("ImageUrl", value);
                ViewState["ImageUrl"] = value;
            }
        }

		public override string GetRegistrationScript()
		{
			string options = string.Empty;
			if( Delay != 0 )
			{
				options += string.Format("delay:{0}", Delay);
			}
            if (Color != System.Drawing.Color.Black)
            {
                if (options != string.Empty)
                    options += ",";
                options += string.Format("color:'{0}'", System.Drawing.ColorTranslator.ToHtml(Color));
            }
            if (Opacity != 0.5M)
            {
                if (options != string.Empty)
                    options += ",";
                options += string.Format("opacity:'{0}'", Opacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            if (ImageUrl != string.Empty)
            {
                if (options != string.Empty)
                    options += ",";
                options += string.Format("image:'{0}'", ResolveUrl(ImageUrl));
            }
            
            if (options != string.Empty)
                options += ",";
            
            options += string.Format("zIndex:'{0}'", ZIndex);

            if (options != string.Empty)
				options = ",{" + options + "}";
			return string.Format("new Ra.BUpDel('{0}'{1})", this.ClientID, options);
		}

        protected override string GetClientSideScript()
		{
			return "";
		}
	}
}
