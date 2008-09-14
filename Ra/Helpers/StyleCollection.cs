/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Ra.Helpers;
using System.ComponentModel.Design.Serialization;
using System.Web.UI;

namespace Ra.Widgets
{
    public class StyleCollection
    {
        private class StyleValue
        {
            private string _value;
            private bool _shouldSerializeToViewState;
            private bool _shouldSerializeToJSON;

            public StyleValue(string value, bool shouldSerializeToViewState, bool shouldSerializeToJSON)
            {
                this._value = value;
                this._shouldSerializeToJSON = shouldSerializeToJSON;
                this._shouldSerializeToViewState = shouldSerializeToViewState;
            }

            public string Value
            {
                get { return _value; }
                set { _value = value; }
            }

            public bool ShouldSerializeToViewState
            {
                get { return _shouldSerializeToViewState; }
                set { _shouldSerializeToViewState = value; }
            }

            public bool ShouldSerializeToJSON
            {
                get { return _shouldSerializeToJSON; }
                set { _shouldSerializeToJSON = value; }
            }
        }

        private RaWebControl _control;
        private bool _trackingViewState;
        private Dictionary<string, StyleValue> _styleValues = new Dictionary<string, StyleValue>();

        public StyleCollection(RaWebControl control)
        {
            _control = control;
            _control.PreRender += new EventHandler(_control_PreRender);
        }

        void _control_PreRender(object sender, EventArgs e)
        {
            if (_control.HasRendered && _styleValues.Count > 0)
            {
                Dictionary<string, string> styles = _control.GetJSONValueDictionary("AddStyle");
                foreach (string idxKey in _styleValues.Keys)
                {
                    if (_styleValues[idxKey].ShouldSerializeToJSON)
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
		// This is useful for scenarios where you have e.g. an Effect which "effectively" do change
		// the Style collection but does it indirectly so that you don't really want
		// the changes to be sent over the JSON mechanism.
		// This is used in among other places at the different effects in Ra-Ajax
        public string this[string idx, bool sendChanges]
        {
            set
            {
				if (this[idx] == value)
					return;
                bool shouldJson = sendChanges && _trackingViewState && (!_styleValues.ContainsKey(idx) || _styleValues[idx].Value != value);
                _styleValues[idx] = new StyleValue(value, _trackingViewState, shouldJson);
            }
        }
		
		public string this[string idx]
		{
			get
			{
                // Easy validation
                if (idx.ToLower() != idx)
                    throw new ApplicationException("Cannot have a style property which contains uppercase letters");

                if (_styleValues.ContainsKey(idx))
                    return _styleValues[idx].Value;

                return null;
			}
			set { this[idx, true] = value; }
		}

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

        private string GetStyles(bool returnOnlyViewStateValues)
        {
            string retVal = "";
            foreach (string idxKey in _styleValues.Keys)
            {
                if (returnOnlyViewStateValues)
				{
					if (_styleValues[idxKey].ShouldSerializeToViewState)
						retVal += idxKey + ":" + _styleValues[idxKey].Value + ";";
				}
                else
                    retVal += idxKey + ":" + _styleValues[idxKey].Value + ";";
            }
            return retVal;
        }

        public bool IsTrackingViewState
        {
            get { return _trackingViewState; }
        }

        public void LoadViewState(string state)
        {
            if (string.IsNullOrEmpty(state))
                return;
            
            string[] stylePairs = state.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string idx in stylePairs)
            {
                string[] raw = idx.Split(':');
                _styleValues[raw[0]] = new StyleValue(raw[1], true, false);
            }
        }

        public object SaveViewState()
        {
            return GetStyles(true);
        }

        public void TrackViewState()
        {
            _trackingViewState = true;
        }
    }
}
