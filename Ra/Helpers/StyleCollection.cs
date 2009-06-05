/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Web.UI;
using System.Web;
using System.Globalization;

namespace Ra.Widgets
{
    /**
     * Style collection class. Used in RaWebControl to track style properties
     */
    public class StyleCollection
    {
        private class StyleValue
        {
            private string _beforeViewStateTrackingValue;
            private string _viewStateValue;
            private string _afterViewStateTrackingValue;
            private string _onlyViewStateValue;

            public StyleValue()
            { }

            // This is the one found in e.g. .ASPX file or in OnInit or which in 
            // any other ways are set BEFORE TrackingViewState has been called
            public string BeforeViewStateTrackingValue
            {
                get { return _beforeViewStateTrackingValue; }
                set { _beforeViewStateTrackingValue = value; }
            }

            // This is the value which is actually in the ViewState - e.g. from previous call etc...
            public string ViewStateValue
            {
                get { return _viewStateValue; }
                set { _viewStateValue = value; }
            }

            // This one is set AFTER the ViewState has been loaded and TrackViewState has been called
            // This is the one being rendered into the Response either through JSON or through the HTML
            public string AfterViewStateTrackingValue
            {
                get { return _afterViewStateTrackingValue; }
                set { _afterViewStateTrackingValue = value; }
            }

            // This one is used only for effects which needs to manipulate the style collection
            // on the client but does NOT want it to be written into the response (HTML/JSON) in any ways...
            public string OnlyViewStateValue
            {
                get { return _onlyViewStateValue; }
                set { _onlyViewStateValue = value; }
            }
        }

        // Parent control
        private RaWebControl _control;

        // True if LoadViewState has been called
        private bool _trackingViewState;

        // Collection of actual Style Values....
        private Dictionary<string, StyleValue> _styleValues = new Dictionary<string, StyleValue>();

        internal StyleCollection(RaWebControl control)
        {
            _control = control;

            // Handling the PreRender event to serialize style values back to the client
            // Only used if IsCallBack == true
            // TODO: REFACTOR this logic and put it into the RaWebControl instead....!!!
            // TOO tight coupling...
            _control.PreRender += new EventHandler(_control_PreRender);
        }

        private void _control_PreRender(object sender, EventArgs e)
        {
            if (_control.HasRendered && _styleValues.Count > 0)
            {
                Dictionary<string, string> styles = _control.GetJSONValueDictionary("AddStyle");
                foreach (string idxKey in _styleValues.Keys)
                {
                    if (_styleValues[idxKey].AfterViewStateTrackingValue != null)
                    {
                        // Transforming from CSS file syntax to DOM syntax, e.g. background-color ==> backgroundColor
                        // and border-style ==> borderStyle
                        string idx = idxKey;
                        while (idx.IndexOf('-') != -1)
                        {
                            int where = idx.IndexOf('-');
                            idx = idx.Substring(0, where) + idx.Substring(where + 1, 1).ToUpper() + idx.Substring(where + 2);
                        }
                        styles[idx] = _styleValues[idxKey].AfterViewStateTrackingValue;
                    }
                }
            }
        }

		// This one is mostly ment for Control Developers or Effect Developers since
		// if the last parameter is false it will not render the changes through the JSON
		// logic but it WILL render the changes into the ViewState.
        // Normally you would NEVER uswe this one directly unless you're an extension 
        // control/behavior/effect developer...
		// This is useful for scenarios where you have e.g. an Effect which "effectively" do change
		// the Style collection but does it indirectly so that you don't really want
		// the changes to be sent over the JSON mechanism.
		// This is used in the Effect-logic in Ra-Ajax since the effects are actually modifying
        // the style values, but on the CLIENT which means we need to update the ViewState since otherwise
        // the client-side rendering of the style properties will make the server and the client come out
        // of "sync"...
        // If you create an effect which actually updates the styles of the control, but on the client as
        // a part of the rendering of the effect then you should use this method to update the ViewState
        // value of that style property but you should NOT set the Style property directly in your own code...
        /**
         * Use this if you create an extension effect or something else which actually manipulates
         * the style attribute on the element on the client but you don't want to send the
         * style value back as JSON. Advanced, mostly for extension control developers.
         */
        public void SetStyleValueViewStateOnly(string idx, string value)
        {
            SetStyleValue(idx, value, true);
        }

        private void SetStyleValue(string idx, string value, bool viewStateOnly)
        {
            // Short cutting if value is not changed
            if (this[idx] == value)
                return;

            // Adding to StyleCollection
            AddStyleToCollection(idx, value, viewStateOnly);
        }

        private void AddStyleToCollection(string idx, string value, bool viewStateOnly)
        {
            string styleName = idx.Trim().ToLowerInvariant();
            string styleValue = value.Trim();
            
            if (styleName == "opacity")
            {
                decimal opacity;
                
                // use full opacity if the user didn't specify a correct value
                if (!decimal.TryParse(styleValue, NumberStyles.Float, CultureInfo.InvariantCulture, out opacity))
                    opacity = 1.0M;

                if (HttpContext.Current.Request.Browser.Browser == "IE")
                {
                    styleName = "filter";
                    if (opacity == 1.0M)
                        styleValue = "alpha(enabled=false)";
                    else
                        styleValue = string.Format("alpha(opacity={0:0})", Math.Round(opacity * 100));
                }
                else
                {
                    styleValue = opacity.ToString(CultureInfo.InvariantCulture);
                }
            }

            if (_styleValues.ContainsKey(styleName))
            {
                // Key exists from before
                StyleValue oldValue = _styleValues[styleName];
                if (viewStateOnly)
                {
                    oldValue.OnlyViewStateValue = styleValue;
                }
                else
                {
                    if (_trackingViewState)
                        oldValue.AfterViewStateTrackingValue = styleValue;
                    else
                        oldValue.BeforeViewStateTrackingValue = styleValue;
                }
            }
            else
            {
                // Key doesn't exist from before
                StyleValue nValue = new StyleValue();

                if (viewStateOnly)
                {
                    nValue.OnlyViewStateValue = styleValue;
                }
                else
                {
                    if (_trackingViewState)
                        nValue.AfterViewStateTrackingValue = styleValue;
                    else
                        nValue.BeforeViewStateTrackingValue = styleValue;
                }

                // Storing it in our dictionary...
                _styleValues[styleName] = nValue;
            }
        }

        /**
         * Returns the value of the key, might return null
         */
		public string this[string idx]
		{
			get
			{
                // Easy validation
                if (idx.ToLower() != idx)
                    throw new ApplicationException("Cannot have a style property which contains uppercase letters");

                if (_styleValues.ContainsKey(idx))
                {
                    if (_styleValues[idx].OnlyViewStateValue != null)
                        return _styleValues[idx].OnlyViewStateValue;
                    else if (_styleValues[idx].AfterViewStateTrackingValue != null)
                        return _styleValues[idx].AfterViewStateTrackingValue;
                    else if (_styleValues[idx].ViewStateValue != null)
                        return _styleValues[idx].ViewStateValue;
                    else
                        return _styleValues[idx].BeforeViewStateTrackingValue;
                }

                return null;
			}
			set { this.SetStyleValue(idx, value, false); }
		}

        /**
         * Strongly typed version, prefer this one if you want to get/set style values
         */
        public string this[Styles idx]
        {
            get
            {
                string styleString = TransformStyleToString(idx);
                return this[styleString];
            }
            set
            {
                string styleString = TransformStyleToString(idx);
                this[styleString] = value;
            }
        }

        private static string TransformStyleToString(Styles idx)
        {
            string tmp = "";
            switch (idx)
            {
                case Styles.floating:
                    tmp = "clear";
                    break;
                default:
                    tmp = idx.ToString();
                    break;
            }
            string styleString = "";
            foreach (char idxChar in tmp)
            {
                if (char.IsUpper(idxChar))
                {
                    styleString += "-";
                    styleString += char.ToLower(idxChar);
                }
                else
                    styleString += idxChar;
            }
            return styleString;
        }

        public string GetViewStateOnlyStyles()
        {
            string retVal = "";
            foreach (string idxKey in _styleValues.Keys)
            {
                string value = "";
                // Here we are returning ONLY the ViewStateValue
                if (_styleValues[idxKey].OnlyViewStateValue != null)
                {
                    value = _styleValues[idxKey].OnlyViewStateValue;
                }
                else if (_styleValues[idxKey].AfterViewStateTrackingValue != null)
                {
                    value = _styleValues[idxKey].AfterViewStateTrackingValue;
                }
                else if (_styleValues[idxKey].ViewStateValue != null)
                {
                    value = _styleValues[idxKey].ViewStateValue;
                }
                if (!string.IsNullOrEmpty(value))
                {
                    retVal +=
                        TransformToViewStateShorthand(idxKey) +
                        ":" +
                        value +
                        ";";
                }
            }
            return retVal;
        }

        public string GetStylesForResponse()
        {
            string retVal = "";
            foreach (string idxKey in _styleValues.Keys)
            {
                string value = "";
                if (_styleValues[idxKey].AfterViewStateTrackingValue != null)
                {
                    value = _styleValues[idxKey].AfterViewStateTrackingValue;
                }
                else if (_styleValues[idxKey].ViewStateValue != null)
                {
                    value = _styleValues[idxKey].ViewStateValue;
                }
                else if (_styleValues[idxKey].BeforeViewStateTrackingValue != null)
                {
                    value = _styleValues[idxKey].BeforeViewStateTrackingValue;
                }

                // NOT rendering "empty" styles...
                if (value != "")
                    retVal += idxKey + ":" + value + ";";
            }
            return retVal;
        }

        internal bool IsTrackingViewState
        {
            get { return _trackingViewState; }
        }

        internal void LoadViewState(string state)
        {
            if (string.IsNullOrEmpty(state))
                return;

            /// Removing whitespaces...
            state = state.Trim();

            string[] stylePairs = state.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string idx in stylePairs)
            {
                string[] raw = idx.Split(':');
                StyleValue v = new StyleValue();
                v.ViewStateValue = raw[1];
                _styleValues[TransformFromViewStateShorthand(raw[0])] = v;
            }
        }

        private string TransformFromViewStateShorthand(string key)
        {
            switch (key)
            {
                case "a":
                    return "background";
                case "b":
                    return "background-color";
                case "c":
                    return "border";
                case "d":
                    return "cursor";
                case "e":
                    return "display";
                case "f":
                    return "position";
                case "g":
                    return "height";
                case "h":
                    return "width";
                case "i":
                    return "font";
                case "j":
                    return "margin";
                case "k":
                    return "padding";
                case "l":
                    return "left";
                case "m":
                    return "overflow";
                case "n":
                    return "right";
                case "o":
                    return "top";
                case "p":
                    return "z-index";
                case "q":
                    return "color";
                case "r":
                    return "text-align";
                case "s":
                    return "opacity";
                case "t":
                    return "bottom";
                case "u":
                    return "line-height";
                case "v":
                    return "background-image";
                case "w":
                    return "background-position";
                case "x":
                    return "border-color";
                case "y":
                    return "border-width";
                case "z":
                    return "font-family";
                case "1":
                    return "font-size";
                default:
                    return key;
            }
        }

        private string TransformToViewStateShorthand(string key)
        {
            switch (key)
            {
                case "background":
                    return "a";
                case "background-color":
                    return "b";
                case "border":
                    return "c";
                case "cursor":
                    return "d";
                case "display":
                    return "e";
                case "position":
                    return "f";
                case "height":
                    return "g";
                case "width":
                    return "h";
                case "font":
                    return "i";
                case "margin":
                    return "j";
                case "padding":
                    return "k";
                case "left":
                    return "l";
                case "overflow":
                    return "m";
                case "right":
                    return "n";
                case "top":
                    return "o";
                case "z-index":
                    return "p";
                case "color":
                    return "q";
                case "text-align":
                    return "r";
                case "opacity":
                    return "s";
                case "bottom":
                    return "t";
                case "line-height":
                    return "u";
                case "background-image":
                    return "v";
                case "background-position":
                    return "w";
                case "border-color":
                    return "x";
                case "border-width":
                    return "y";
                case "font-family":
                    return "z";
                case "font-size":
                    return "1";
                default:
                    return key;
            }
        }

        internal object SaveViewState()
        {
            return GetViewStateOnlyStyles();
        }

        internal void TrackViewState()
        {
            _trackingViewState = true;
        }
    }
}
