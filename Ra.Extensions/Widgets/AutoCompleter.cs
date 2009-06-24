/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;
using Ra.Effects;

namespace Ra.Extensions.Widgets
{
    /**
     * AutoCompleter for displaying dynamically items searched for through a TextBox. When something
     * is written into the AutoCompleter then the RetrieveAutoCompleterItems event will be raised
     * so that your code can populate the AutoCompleterItems collection. Then when a user selects
     * one of those items the AutoCompleterItemSelected event will be raised.
     */
    [ASP.ToolboxData("<{0}:AutoCompleter runat=server />")]
    public class AutoCompleter : Panel, ASP.INamingContainer
    {
        /**
         * AutoCompleter retrieve items EventArgs
         */
        public class RetrieveAutoCompleterItemsEventArgs : EventArgs
        {
            private ASP.ControlCollection _coll;
            private string _query;

            internal RetrieveAutoCompleterItemsEventArgs(ASP.ControlCollection coll, string query)
            {
                _coll = coll;
                _query = query;
            }

            /**
             * Control collection which your controls should be added to
             */
            public ASP.ControlCollection Controls
            {
                get { return _coll; }
            }

            /**
             * Search query the user is searching for
             */
            public string Query
            {
                get { return _query; }
            }
        }

        TextBox _txt = new TextBox();
        Label _items;

        /**
         * EventHandler fired when AutoCompleter needs new items. The EventArgs passed to this
         * event handler will be of type AutoCompleter.RetrieveAutoCompleterItemsEventArgs.
         * Use the Controls property of this EventArgs object to populate the AutoCompleter with
         * new AutoCompleter items. Note though that ONLY AutoCompleterItem widgets should be
         * added into this collection of controls.
         */
        public event EventHandler<RetrieveAutoCompleterItemsEventArgs> RetrieveAutoCompleterItems;

        /**
         * Fired when the user selects an AutoCompleterItem. Use SelectedItem property to figure
         * out which item was selected. SelectedItem will contain the ID of the Item which was selected.
         */
        public event EventHandler AutoCompleterItemSelected;

        /**
         * Overridden to provide a sane default value. The default value of this property is "auto".
         */
        [DefaultValue("auto")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "auto";
                return retVal;
            }
            set { base.CssClass = value; }
        }

        /**
         * This is the item which currently is selected (if any). Will return null if no
         * items are selected. Notice that this will return a STRING which is the ID of the
         * AutoCompleterItem which the user has selected. Use e.g. the RaSelector or something 
         * similar to retrieve the actual control (if needed)
         */
        [DefaultValue(null)]
        public string SelectedItem
        {
            get { return ViewState["SelectedItem"] == null ? null : (string)ViewState["SelectedItem"]; }
            set { ViewState["SelectedItem"] = value; }
        }

        /**
         * Keyboard shortcut to reach the TextBox of the autocompleter. Normally this will be differently
         * implemented with different browsers. For a default English FireFox installation for instance
         * you need to combine the key with pressing ALT and SHIFT. For other browsers this may vary
         * further.
         */
        [DefaultValue("")]
        public string AccessKey
        {
            get { return _txt.AccessKey; }
            set { _txt.AccessKey = value; }
        }

        /**
         * Text for control. You can modify through code the Text portions of the AutoCompleter TextBox.
         * Often this will be used to give "sane default" values. If used at all.
         */
        [DefaultValue("")]
        public string Text
        {
            get { return _txt.Text; }
            set { _txt.Text = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            EnsureChildControls();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Retrieving DynamicItems (for cases where we have controls with attached events inside of it)
            RetrieveDynamicallyCreatedItems();

            // Wireing up ItemSelected event handlers...
            WireItemSelectedEvents();
        }

        protected override void CreateChildControls()
        {
            CreateAutoCompleterControls();
            base.CreateChildControls();
        }

        private void CreateAutoCompleterControls()
        {
            // Figuring out CSS class to use
            string cssClass = this.CssClass;
            if (cssClass.IndexOf(' ') != -1)
                cssClass = cssClass.Split(' ')[0];

            // Creating TextBox
            _txt.ID = "txt";
            _txt.CssClass = cssClass + "-text";
            _txt.KeyUp += new EventHandler(_txt_KeyUp);
            Controls.Add(_txt);

            // Creating wrapper for dynamically retrieved items
            _items = new Label();
            _items.ID = "items";
            _items.CssClass = cssClass + "-items";
            _items.Tag = "ul";
            Controls.Add(_items);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (_items.Controls.Count == 0)
                _items.Visible = false;
            else if( string.IsNullOrEmpty(SelectedItem) )
            {
                if (!_items.Visible)
                    new EffectFadeIn(_items, 200).Render();
                else
                    new EffectHighlight(_items, 200).Render();
                _items.Visible = true;
                WireItemSelectedEvents();
            }
            base.OnPreRender(e);
        }

        private void WireItemSelectedEvents()
        {
            foreach (AutoCompleterItem idx in _items.Controls)
            {
                idx.Click += new EventHandler(idx_Click);
            }
        }

        private void idx_Click(object sender, EventArgs e)
        {
            SelectedItem = (sender as System.Web.UI.Control).ID;
            if (AutoCompleterItemSelected != null)
                AutoCompleterItemSelected(this, new EventArgs());
            _items.Controls.Clear();
        }

        private void _txt_KeyUp(object sender, EventArgs e)
        {
            _items.Controls.Clear();
            SelectedItem = null;
            RetrieveDynamicallyCreatedItems();
        }

        private void RetrieveDynamicallyCreatedItems()
        {
            if (RetrieveAutoCompleterItems != null)
            {
                RetrieveAutoCompleterItems(this, new RetrieveAutoCompleterItemsEventArgs(_items.Controls, _txt.Text));
                _items.ReRender();
            }
        }

        /**
         * Overridden to let the Focus method implementation from RaControl give focus to the TextBox
         * portions of this control.
         */
        public override void Focus()
        {
            _txt.Focus();
        }

        /**
         * Overridden to let the Select method implementation from RaControl select the TextBox
         * portions text property of this control.
         */
        public void Select()
        {
            _txt.Select();
        }
    }
}
