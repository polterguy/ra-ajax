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
        TextBox _txt;
        Label _items;

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

        /**
         * EventHandler fired when AutoCompleter needs new items
         */
        public event EventHandler<RetrieveAutoCompleterItemsEventArgs> RetrieveAutoCompleterItems;

        protected override void OnInit(EventArgs e)
        {
            EnsureChildControls();
            base.OnInit(e);
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

            // Retrieving DynamicItems (for cases where we have controls with attached events inside of it)
            RetrieveDynamicallyCreatedItems();
        }

        private void _txt_KeyUp(object sender, EventArgs e)
        {
            _items.Controls.Clear();
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
