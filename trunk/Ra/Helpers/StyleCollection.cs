/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
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
            if (_styleValues.ContainsKey(idx))
            {
                // Key exists from before
                StyleValue oldValue = _styleValues[idx];
                if (viewStateOnly)
                {
                    oldValue.OnlyViewStateValue = value;
                }
                else
                {
                    if (_trackingViewState)
                        oldValue.AfterViewStateTrackingValue = value;
                    else
                        oldValue.BeforeViewStateTrackingValue = value;
                }
            }
            else
            {
                // Key doesn't exist from before
                StyleValue nValue = new StyleValue();

                if (viewStateOnly)
                {
                    nValue.OnlyViewStateValue = value;
                }
                else
                {
                    if (_trackingViewState)
                        nValue.AfterViewStateTrackingValue = value;
                    else
                        nValue.BeforeViewStateTrackingValue = value;
                }

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
                // Here we are returning ONLY the ViewStateValue
                if (_styleValues[idxKey].OnlyViewStateValue != null)
                    retVal += idxKey + ":" + _styleValues[idxKey].OnlyViewStateValue + ";";
                else if (_styleValues[idxKey].AfterViewStateTrackingValue != null)
                    retVal += idxKey + ":" + _styleValues[idxKey].AfterViewStateTrackingValue + ";";
                else if (_styleValues[idxKey].ViewStateValue != null)
                    retVal += idxKey + ":" + _styleValues[idxKey].ViewStateValue + ";";
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
                    value = _styleValues[idxKey].AfterViewStateTrackingValue;
                else if (_styleValues[idxKey].ViewStateValue != null)
                    value = _styleValues[idxKey].ViewStateValue;
                else if (_styleValues[idxKey].BeforeViewStateTrackingValue != null)
                    value = _styleValues[idxKey].BeforeViewStateTrackingValue;

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
            
            string[] stylePairs = state.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string idx in stylePairs)
            {
                string[] raw = idx.Split(':');
                StyleValue v = new StyleValue();
                v.ViewStateValue = raw[1];
                _styleValues[raw[0]] = v;
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
