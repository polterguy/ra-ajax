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

namespace Ra.Widgets
{
    public abstract class RaWebControl : RaControl
    {
        StyleCollection _styles;

        public RaWebControl()
        {
            _styles = new StyleCollection(this);
        }

        #region [ -- Properties -- ]

        [DefaultValue("")]
        public string CssClass
        {
            get { return ViewState["CssClass"] == null ? "" : (string)ViewState["CssClass"]; }
            set { ViewState["CssClass"] = value; }
        }

        public StyleCollection Style
        {
            get { return _styles; }
        }

        #endregion
    }
}
