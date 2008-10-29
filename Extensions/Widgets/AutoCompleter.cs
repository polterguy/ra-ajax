/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
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

namespace Ra.Extensions
{
    /**
     * AutoCompleter for displaying dynamically items searched for through a TextBox
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

        TextBox _txt;
        Label _items;

        /**
         * EventHandler fired when AutoCompleter needs new items
         */
        public event EventHandler<RetrieveAutoCompleterItemsEventArgs> RetrieveAutoCompleterItems;

        /**
         * Fired when the user selects an AutoCompleterItem. Use SelectedItem property to figure
         * out which item was selected. SelectedItem will contain the ID of the Item which was selected
         */
        public event EventHandler AutoCompleterItemSelected;

        /**
         * This is the item which currently is selected (if any)
         */
        [DefaultValue(null)]
        public string SelectedItem
        {
            get { return ViewState["SelectedItem"] == null ? null : (string)ViewState["SelectedItem"]; }
            set { ViewState["SelectedItem"] = value; }
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
            // Creating TextBox
            _txt = new TextBox();
            _txt.ID = "txt";
            _txt.CssClass = this.CssClass + "-text";
            _txt.KeyUp += new EventHandler(_txt_KeyUp);
            Controls.Add(_txt);

            // Creating wrapper for dynamically retrieved items
            _items = new Label();
            _items.ID = "items";
            _items.CssClass = this.CssClass + "-items";
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

        public override void Focus()
        {
            _txt.Focus();
        }
    }
}
