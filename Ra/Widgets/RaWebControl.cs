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
using System.Web.UI;
using System.ComponentModel;
using System.Collections.Generic;

namespace Ra.Widgets
{
    public abstract class RaWebControl : RaControl
    {
        StyleCollection _styles;

        // Need default CTOR impl. to make sure we get to create the style collection with 
        // the pointer to the this widget...
        public RaWebControl()
        {
            _styles = new StyleCollection(this);
        }

        #region [ -- Overridden Base Class methods -- ]

        protected override void TrackViewState()
        {
            base.TrackViewState();
            Style.TrackViewState();
        }

        // Overridden to save the ViewState for the Style collection
        protected override object SaveViewState()
        {
            object[] content = new object[2];

            Dictionary<string, string> styles = Style.GetStylesForViewState();

            // For easiness we ALWAYS save the Style collection, even though there's nothing to save
            // This will take up _VERY_ few bytes of extra data on the ViewState
            if (styles == null)
                styles = new Dictionary<string, string>();

            // For optimalization issues we're "flattening" the Dictionary since
            // if we serialize the dictionary "raw" it'll become HUGE...!!
            string[] styleStrings = new string[styles.Count];
            int idxNo = 0;
            foreach (string idxKey in styles.Keys)
            {
                styleStrings[idxNo++] = idxKey + ":" + styles[idxKey];
            }

            // This order we must remember for the LoadViewState logic ;)
            content[0] = styleStrings;
            content[1] = base.SaveViewState();
            return content;
        }

        // Overridden to reload the ViewState for the Style collection
        protected override void LoadViewState(object savedState)
        {
            object[] content = savedState as object[];
            string[] styles = content[0] as string[];
            Dictionary<string, string> styleDictionary = new Dictionary<string, string>();

            // Looping through the "flattened" Dictionary to reload into real Dictionary...
            foreach (string idx in styles)
            {
                string[] raw = idx.Split(':');
                styleDictionary[raw[0]] = raw[1];
            }
            Style.SetStylesFromViewState(styleDictionary);

            // When we save the ViewState we save the base class object as the second instance in the array...
            base.LoadViewState(content[1]);
        }

        #endregion

        #region [ -- Properties -- ]

        [DefaultValue("")]
        public string CssClass
        {
            get { return ViewState["CssClass"] == null ? "" : (string)ViewState["CssClass"]; }
            set { ViewState["CssClass"] = value; }
        }

        // TODO: Serialize Style to ViewState
        public StyleCollection Style
        {
            get { return _styles; }
        }

        #endregion
    }
}
