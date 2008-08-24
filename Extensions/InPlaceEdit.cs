/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Helpers;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:InPlaceEdit runat=server />")]
    public class InPlaceEdit : Panel, ASP.INamingContainer
    {
        private LinkButton _link = new LinkButton();
        private TextBox _text = new TextBox();

        public event EventHandler TextChanged;

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

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            CreateEditControls();
        }

        private void CreateEditControls()
        {
            // Creating LinkButton
            _link.ID = "btn";
            _link.Click += new EventHandler(_link_Click);
            _link.Text = Text;
            Controls.Add(_link);

            // Creating TextBox
            _text = new TextBox();
            _text.ID = "txt";
            _text.Text = _link.Text;
            _text.Visible = false;
            _text.Blur += new EventHandler(_text_Blur);
            Controls.Add(_text);
        }

        void _text_Blur(object sender, EventArgs e)
        {
            _link.Text = _text.Text;
            _text.Visible = false;
            _link.Visible = true;
            Text = _link.Text;

            if (TextChanged != null)
                TextChanged(this, new EventArgs());
        }

        void _link_Click(object sender, EventArgs e)
        {
            if (_link.Text == "[null]")
                _text.Text = "";
            else
                _text.Text = _link.Text;
            _text.Visible = true;
            _link.Visible = false;
            _text.Focus();
            _text.Select();
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

        protected override void SetAllChildrenToRenderHtml(ASP.ControlCollection controls)
        {
            _link.Phase = RenderingPhase.RenderHtml;
            _link.Visible = true;
            _text.Visible = false;
        }

        public override string GetHTML()
        {
            return string.Format("<span id=\"{0}\"{2}{3}>{1}</span>",
                ClientID,
                GetChildControlsHTML(),
                GetCssClassHTMLFormatedAttribute(),
                GetStyleHTMLFormatedAttribute());
        }
    }
}
