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
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;
using Ra.Behaviors;

namespace Ra.Extensions.Widgets
{
    /**
     * MessageBox control for easily getting confirmations, displaying modal data etc. Contains shorthands
     * for most "MessageBox scenarios". Mostly just a shorthand wrapper around the Window with some contained
     * controls like labels for body and TextBox for taking input, if the control is set into "input mode".
     */
    [ASP.ToolboxData("<{0}:MessageBox runat=\"server\" />")]
    public class MessageBox : Window
    {
        /**
         * Which type of MessageBox you wish to create
         */
        public enum TypeOfMessageBox
        {
            /**
             * Ok button only, mostly for just displaying data back to the user
             */
            OK,

            /**
             * Only an OK and a Cancel button
             */
            OK_Cancel,

            /**
             * Yes and No buttons only
             */
            Yes_No,

            /**
             * Yes, No and Cancel buttons
             */
            Yes_No_Cancel,

            /**
             * Single text input given together with an OK button
             */
            Get_Text,

            /**
             * Single text input given together with an OK button
             */
            Get_Text_Multiple_Lines
        }

        /**
         * How the MessageBox was closed
         */
        public enum ClosedHow
        {
            /**
             * Closed with OK button
             */
            OK,

            /**
             * Closed with Cancel button
             */
            Cancel,

            /**
             * Closed with Yes button
             */
            Yes,

            /**
             * Closed with No button
             */
            No
        }

        /**
         * EventArgs object passed into the Closed event of the MessageBox. Contains information about
         * how the MessageBox was closed and also (if in "input mode") the text the user typed into the 
         * MessageBox.
         */
        public class ClosedEventArgs : EventArgs
        {
            private ClosedHow _closedHow;
            private string _userInput;

            internal ClosedEventArgs(ClosedHow how, string userInput)
            {
                _closedHow = how;
                _userInput = userInput;
            }

            public ClosedHow HowClosed
            {
                get { return _closedHow; }
            }

            public string UserInput
            {
                get { return _userInput; }
            }
        }

        /**
         * This event is raised when the MessageBox is closed. The EventArgs will contain
         * information about how the MessageBox was closed and (if applicable) what input
         * the user submitted.
         */
        public new event EventHandler<ClosedEventArgs> Closed;

        private Label _body;
        private Button _ok;
        private Button _cancel;
        private Button _yes;
        private Button _no;
        private TextBox _userInput;
        private TextArea _userInputMultipleLines;
        private BehaviorObscurable _obscurer;
        private Panel tmp;

        /**
         * Overridden constructor tomake sure properties from the base class (Window) 
         * is being correctly initialized.
         */
        public MessageBox()
        {
            // Defaulting some of the properties from Window
            Closable = false;
            Movable = false;
            Visible = false;
        }

        /**
         * Body of MessageBox, this is the main text displayed in the center of the
         * MessageBox.
         */
        [DefaultValue("")]
        public string Body
        {
            get
            {
                if (ViewState["Body"] == null)
                    return "";
                return (string)ViewState["Body"];
            }
            set
            {
                ViewState["Body"] = value;
            }
        }

        /**
         * This decides which type of MessageBox you want to display. Available options
         * are e.g. to only show an information MessageBox (OK button only), get
         * data from user (Get_Text) and many more.
         */
        [DefaultValue(MessageBox.TypeOfMessageBox.OK)]
        public TypeOfMessageBox MessageBoxType
        {
            get
            {
                if (ViewState["MessageBoxType"] == null)
                    return TypeOfMessageBox.OK;
                return (TypeOfMessageBox)ViewState["MessageBoxType"];
            }
            set
            {
                ViewState["MessageBoxType"] = value;
            }
        }

        /**
         * Overridden to make sure control are initialized upon setting the 
         * MessageBox to visible.
         */
        public override bool Visible
        {
            get { return base.Visible; }
            set
            {
                base.Visible = value;
                if (value)
                {
                    if (_userInput != null)
                    {
                        _userInput.Text = "Give me some input...";
                        _userInput.Select();
                        _userInput.Focus();
                    }
                    else if (_userInputMultipleLines != null)
                    {
                        _userInputMultipleLines.Text = "Give me some input...";
                        _userInputMultipleLines.Select();
                        _userInputMultipleLines.Focus();
                    }
                    else if (_ok != null)
                        _ok.Focus();
                    else if (_yes != null)
                        _yes.Focus();
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            CreateMsgBoxControls();
        }

        private void CreateMsgBoxControls()
        {
            // Creating body text...
            _body = new Label();
            _body.Text = Body;
            _body.Tag = "div";
            _body.Style["text-align"] = "left";
            _body.Style["padding"] = "15px";
            _body.Style["position"] = "relative";
            _body.Style["padding-bottom"] = "35px";
            _body.ID = "body";
            Content.Add(_body);

            // Making sure it's MODAL
            _obscurer = new BehaviorObscurable();
            _obscurer.ID = "obsc";
            this.Controls.Add(_obscurer);
            
            switch( MessageBoxType)
            {
                case TypeOfMessageBox.OK:
                    CreateOKButton();
                    break;
                case TypeOfMessageBox.OK_Cancel:
                    CreateOKButton();
                    CreateCancelButton();
                    break;
                case TypeOfMessageBox.Yes_No:
                    CreateNoButton();
                    CreateYesButton();
                    break;
                case TypeOfMessageBox.Yes_No_Cancel:
                    CreateNoButton();
                    CreateYesButton();
                    CreateCancelButton();
                    break;
                case TypeOfMessageBox.Get_Text:
                    CreateOKButton();
                    CreateCancelButton();
                    CreateUserInput();
                    break;
                case TypeOfMessageBox.Get_Text_Multiple_Lines:
                    CreateOKButton();
                    CreateCancelButton();
                    CreateUserInputMultipleLines();
                    break;
                default:
                    throw new ApplicationException("TypeOfMessageBox type not implemented...");
            }
        }

        private void CreateUserInputMultipleLines()
        {
            _userInputMultipleLines = new TextArea();
            _userInputMultipleLines.ID = "txtML";
            _userInputMultipleLines.Style["display"] = "block";
            _userInputMultipleLines.Style["width"] = "80%";
            _userInputMultipleLines.Style["margin-left"] = "auto";
            _userInputMultipleLines.Style["margin-right"] = "auto";
            _userInputMultipleLines.EscPressed += delegate
            {
                this.Visible = false;
                string input = "";
                if (this.MessageBoxType == TypeOfMessageBox.Get_Text)
                    input = _userInput.Text;
                else if (this.MessageBoxType == TypeOfMessageBox.Get_Text_Multiple_Lines)
                    input = _userInputMultipleLines.Text;
                if (Closed != null)
                    Closed(this, new ClosedEventArgs(ClosedHow.Cancel, input));
            };
            _body.Controls.Add(_userInputMultipleLines);
        }

        private void CreateUserInput()
        {
            _userInput = new TextBox();
            _userInput.ID = "txt";
            _userInput.Style["display"] = "block";
            _userInput.Style["width"] = "80%";
            _userInput.Style["margin-left"] = "auto";
            _userInput.Style["margin-right"] = "auto";
            _userInput.EscPressed += delegate
            {
                this.Visible = false;
                string input = "";
                if (this.MessageBoxType == TypeOfMessageBox.Get_Text)
                    input = _userInput.Text;
                else if (this.MessageBoxType == TypeOfMessageBox.Get_Text_Multiple_Lines)
                    input = _userInputMultipleLines.Text;
                if (Closed != null)
                    Closed(this, new ClosedEventArgs(ClosedHow.Cancel, input));
            };
            this.DefaultWidget = "OK";
            _body.Controls.Add(_userInput);
        }

        private void CreateNoButton()
        {
            tmp = new Panel();
            tmp.ID = "tmpPnl";
            tmp.Style["position"] = "absolute";
            tmp.Style["bottom"] = "2px";
            tmp.Style["right"] = "2px";
            tmp.Style["text-align"] = "right";

            _no = new Button();
            _no.Text = "No";
            _no.ID = "no";
            _no.Click += delegate
            {
                this.Visible = false;
                string input = "";
                if (this.MessageBoxType == TypeOfMessageBox.Get_Text)
                    input = _userInput.Text;
                else if (this.MessageBoxType == TypeOfMessageBox.Get_Text_Multiple_Lines)
                    input = _userInputMultipleLines.Text;
                if (Closed != null)
                    Closed(this, new ClosedEventArgs(ClosedHow.No, input));
            };
            tmp.Controls.Add(_no);
        }

        private void CreateYesButton()
        {
            _yes = new Button();
            _yes.Text = "Yes";
            _yes.ID = "yes";
            _yes.Click += delegate
            {
                this.Visible = false;
                string input = "";
                if (this.MessageBoxType == TypeOfMessageBox.Get_Text)
                    input = _userInput.Text;
                else if (this.MessageBoxType == TypeOfMessageBox.Get_Text_Multiple_Lines)
                    input = _userInputMultipleLines.Text;
                if (Closed != null)
                    Closed(this, new ClosedEventArgs(ClosedHow.Yes, input));
            };
            tmp.Controls.Add(_yes);
            _body.Controls.Add(tmp);
        }

        private void CreateCancelButton()
        {
            _cancel = new Button();
            _cancel.ID = "cancel";
            _cancel.Text = "Cancel";
            _cancel.Style["position"] = "absolute";
            _cancel.Style["bottom"] = "2px";
            _cancel.Style["left"] = "2px";
            _cancel.Click += delegate
            {
                this.Visible = false;
                string input = "";
                if (this.MessageBoxType == TypeOfMessageBox.Get_Text)
                    input = _userInput.Text;
                else if (this.MessageBoxType == TypeOfMessageBox.Get_Text_Multiple_Lines)
                    input = _userInputMultipleLines.Text;
                if (Closed != null)
                    Closed(this, new ClosedEventArgs(ClosedHow.Cancel, input));
            };
            _body.Controls.Add(_cancel);
        }

        private void CreateOKButton()
        {
            _ok = new Button();
            _ok.Text = "OK";
            _ok.ID = "OK";
            _ok.Style["position"] = "absolute";
            _ok.Style["bottom"] = "2px";
            _ok.Style["right"] = "2px";
            _ok.Click += delegate
            {
                this.Visible = false;
                string input = "";
                if (this.MessageBoxType == TypeOfMessageBox.Get_Text)
                    input = _userInput.Text;
                else if (this.MessageBoxType == TypeOfMessageBox.Get_Text_Multiple_Lines)
                    input = _userInputMultipleLines.Text;
                if (Closed != null)
                    Closed(this, new ClosedEventArgs(ClosedHow.OK, input));
            };
            _body.Controls.Add(_ok);
        }
    }
}
