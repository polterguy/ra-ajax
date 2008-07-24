/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ra.Widgets
{
    public class ListItem
    {
        private string _value;
        private string _text;
        private bool _enabled = true;
        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public string Text
        {
            get { return string.IsNullOrEmpty(_text) ? "" : _text; }
            set { _text = value; }
        }

        public string Value
        {
            get { return string.IsNullOrEmpty(_value) ? "" : _value; }
            set { _value = value; }
        }

        public override bool Equals(object obj)
        {
            ListItem rhs = obj as ListItem;
            if (rhs == null)
                return false;
            return rhs.Value == Value && rhs.Text == Text;
        }

        public override int GetHashCode()
        {
            return (Value + Text).GetHashCode();
        }
    }
}
