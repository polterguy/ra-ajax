/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
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
     * Label control which enables editing when clicked. This is kind of like a middle-man between a Label and 
     * a TextBox. It will display as a Label until clicked. Then it will enable the user to edit the text portions
     * of the Label through anormal TextBox.
     */
    [ASP.ToolboxData("<{0}:InPlaceEdit runat=server />")]
    public class InPlaceEdit : Panel, ASP.INamingContainer
    {
        private Label _link = new Label();
        private TextBox _text = new TextBox();

        /**
         * Raised when Text property is changed. The Text property of this control
         * will change when the user have clicked the control to make it editable
         * and then either moved focus away from the control or pressed the enter key.
         */
        public event EventHandler TextChanged;

        /**
         * Text of label. This is the Text property of the control and will be changed
         * when the user chooses to change it. It's kind of the equivalent to the Text
         * property of the TextBox.
         */
        [DefaultValue("")]
        public string Text
        {
            get
            {
                if (ViewState["Text"] == null)
                    return "";
                return (string)ViewState["Text"];
            }
            set
            {
                ViewState["Text"] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            EnsureChildControls();
            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            CreateEditControls();
        }

        private void CreateEditControls()
        {
            // Creating LinkButton
            _link.ID = "btn";
            _link.Click += _link_Click;
            _link.Text = Text;
            Controls.Add(_link);

            // Creating TextBox
            _text = new TextBox();
            _text.ID = "txt";
            _text.Text = _link.Text;
            _text.Visible = false;
			_text.Blur += _text_Updated;
            _text.EnterPressed += _text_Updated;
            _text.EscPressed += _text_EscPressed;
            Controls.Add(_text);
        }

        void _text_EscPressed(object sender, EventArgs e)
        {
            _text.Visible = false;
            _link.Visible = true;
        }

        private void _text_Updated(object sender, EventArgs e)
        {
            _link.Text = _text.Text;
            _text.Visible = false;
            _link.Visible = true;
            Text = _link.Text;

            if (TextChanged != null)
                TextChanged(this, new EventArgs());
        }

        private void _link_Click(object sender, EventArgs e)
        {
            if (_link.Text == "[null]")
                _text.Text = "";
            else
                _text.Text = _link.Text;
            _text.Visible = true;
            _link.Visible = false;
            new EffectFocusAndSelect(_text).Render();
        }

        protected override void OnPreRender(EventArgs e)
        {
            _link.CssClass = CssClass;
            _text.CssClass = CssClass;
            _link.Text = Text;
            if (string.IsNullOrEmpty(_link.Text))
                _link.Text = "[null]";
            base.OnPreRender(e);
        }
    }
}
