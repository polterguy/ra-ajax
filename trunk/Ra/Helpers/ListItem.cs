/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ra.Widgets
{
    /**
     * Items for "listible controls" like for instance SelectList.
     */
    public class ListItem
    {
        private string _value;
        private string _text;
        private bool _enabled = true;
        private SelectList _selectList;

        /**
         * Creates a default item
         */
        public ListItem()
        { }

        /**
         * Creates a default item with the given text/value
         */
        public ListItem(string text, string value)
        {
            _text = text;
            _value = value;
        }

        private bool _hasSetSelectedTrue;
        /**
         * True if item is selected
         */
        public bool Selected
        {
            get
            {
                if (_selectList.SelectedItem == null)
                    return false;
                return _selectList.SelectedItem.Equals(this);
            }
            set
            {
                if (value)
                {
                    if (_selectList == null)
                        _hasSetSelectedTrue = true;
                    else
                        _selectList.SelectedItem = this;
                }
                else if (this.Selected)
                    _selectList.SelectedIndex = 0;
            }
        }

        internal SelectList SelectList
        {
            get { return _selectList; }
            set
            {
                _selectList = value;
                if (_hasSetSelectedTrue)
                    _selectList.SelectedItem = this;
            }
        }


        /**
         * If false item is disabled
         */
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        /**
         * Friendly text of item
         */
        public string Text
        {
            get { return string.IsNullOrEmpty(_text) ? "" : _text; }
            set { _text = value; }
        }

        /**
         * Value of item, should be unique within the control
         */
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
