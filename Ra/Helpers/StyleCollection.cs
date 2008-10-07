/*
 * Ra-Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Web.UI;

namespace Ra.Widgets
{
    /**
     * Style collection class
     */
    public class StyleCollection
    {
        private class StyleValue
        {
            // Since some Style values are only there to be serialized to the ViewState
            // and not sent as JSON back to the client or rendered into the HTML markup
            // we differentiate between these two value by having two different
            // values for those concepts.
            // Though most of the time these values are the same, sometimes especially when
            // effects are being rendered they might actually be different...
            private string _value;
            private string _viewStateValue;
            private bool _viewStateValueFromNonJSON;

            public StyleValue(string value, string viewStateValue)
            {
                this._value = value;
                this._viewStateValue = viewStateValue;
            }

            public string Value
            {
                get { return _value; }
                set { _value = value; }
            }

            public string ViewStateValue
            {
                get { return _viewStateValue; }
                set { _viewStateValue = value; }
            }

            public bool ViewStateValueLocked
            {
                get { return _viewStateValueFromNonJSON; }
                set { _viewStateValueFromNonJSON = value; }
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
                    if (_styleValues[idxKey].Value != null)
                    {
                        // Transforming from CSS file syntax to DOM syntax, e.g. background-color ==> backgroundColor
                        // and border-style ==> borderStyle
                        string idx = idxKey;
                        while (idx.IndexOf('-') != -1)
                        {
                            int where = idx.IndexOf('-');
                            idx = idx.Substring(0, where) + idx.Substring(where + 1, 1).ToUpper() + idx.Substring(where + 2);
                        }
                        styles[idx] = _styleValues[idxKey].Value;
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
            SetStyleValue(idx, value, false);
        }

        private void SetStyleValue(string idx, string value, bool sendChanges)
        {
            // Short cutting if value is not changed
            if (this[idx] == value)
                return;

            // Checking to see if we should send this value as JSON back to client
            // Note that even though our user-code has told us that he wants to
            // transmit changes back to client, this is still an equation of also whether or
            // not the control is tracking ViewState and the value is not the same as the previous value
            // etc...
            bool shouldJson =
                sendChanges &&
                _trackingViewState &&
                (!_styleValues.ContainsKey(idx) || _styleValues[idx].Value != value);

            // Adding to StyleCollection
            AddStyleToCollection(idx, value, shouldJson);
        }

        private void AddStyleToCollection(string idx, string value, bool shouldJson)
        {
            if (_styleValues.ContainsKey(idx))
            {
                // Key exists from before
                StyleValue oldValue = _styleValues[idx];
                if (shouldJson)
                {
                    // ONLY if shouldJson == true we set the "Value" property since that's the one
                    // being sent as JSON back to the client...
                    oldValue.Value = value;

                    // Note that if the ViewStateValue has been previously set by a method
                    // call which had shouldJson == false then we do NOT UPDATED the ViewStateValue
                    // since this might occur if you set Style values on the control AFTER you have 
                    // rendered effects and in such cases the Effect StyleValue change should have
                    // "ViewState priority" and override the Style value explicitly set in user-code...
                    if (!oldValue.ViewStateValueLocked)
                        oldValue.ViewStateValue = value;
                }
                else
                {
                    // If we should not JSON this value we ONLY set the ViewStateValue since this
                    // might be a "ViewState-only" value...
                    // Note that even though the ViewStateValueLocked is true we still
                    // overwrite the previous one since this might occur when rendering multiple effects
                    // which all modifies the ViewStateValue and then the LAST ViewStateValue is the one
                    // which should have precedence...
                    oldValue.ViewStateValue = value;
                    oldValue.ViewStateValueLocked = true;
                }
            }
            else
            {
                // Key doesn't exist from before
                StyleValue nValue = new StyleValue(null, value);

                // Note that we only set the "Value" property if this value is supposed to be
                // serialized back to the client through the JSON mechanism
                if (shouldJson)
                    nValue.Value = value;

                // Storing it in our dictionary...
                _styleValues[idx] = nValue;
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

                // Note that since it's most semantically correct to return the ViewStateValue
                // since that will be the Value of the style property on the next request we do NOT
                // return the Value but rather the ViewStateValue which might give "funny results" if you
                // retrieve the ViewStateValue after an Effect has rendered.
                // However since this will be the value on the next request, this is the semantically "best" 
                // solution when having to choose between two evils....
                if (_styleValues.ContainsKey(idx))
                    return _styleValues[idx].ViewStateValue;

                return null;
			}
			set { this.SetStyleValue(idx, value, true); }
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

        public override string ToString()
        {
            return GetStyles(false);
        }

        public string ToString(bool dropNonCSSValues)
        {
            return GetStyles(false, dropNonCSSValues);
        }

        private string GetStyles(bool returnOnlyViewStateValues)
        {
			return GetStyles(returnOnlyViewStateValues, false);
        }

		// This method is mostly used only for the ToString bugger, serialization to ViewState
		// and creation of style HTML attribute.
        private string GetStyles(bool returnOnlyViewStateValues, bool dropNonCSSValues)
        {
            string retVal = "";
            foreach (string idxKey in _styleValues.Keys)
            {
				// For cases where we're rendering style attribute (among other things)
				if (dropNonCSSValues)
				{
					// TODO: These values should have their own "hacks" to be able to render the values also
					// for "HTML rendering" cases...
					switch (idxKey)
					{
						case "opacity":
						    this._control.AddInitCall(string.Format("element.setOpacity({0})", _styleValues[idxKey].Value));
							continue;
					}
				}
				
				// If we're serializing to ViewState we don't want to have the "static" values
				// which are defined in .ASPX file or before OnInit...
                if (returnOnlyViewStateValues)
                {
                    // Here we are returning ONLY the ViewStateValue
                    if (_styleValues[idxKey].ViewStateValue != null)
                        retVal += idxKey + ":" + _styleValues[idxKey].ViewStateValue + ";";
                }
                else
                {
                    // TODO: Do we need to check to see if the Value is null here.....??
                    string value = _styleValues[idxKey].Value == null ? 
                        _styleValues[idxKey].ViewStateValue : 
                        _styleValues[idxKey].Value;
                    retVal += idxKey + ":" + value + ";";
                }
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
            
            string[] stylePairs = state.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string idx in stylePairs)
            {
                string[] raw = idx.Split(':');
                _styleValues[raw[0]] = new StyleValue(null, raw[1]);
            }
        }

        internal object SaveViewState()
        {
            return GetStyles(true);
        }

        internal void TrackViewState()
        {
            _trackingViewState = true;
        }
    }
}
