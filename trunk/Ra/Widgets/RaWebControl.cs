/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Web.UI;
using System.ComponentModel;
using System.Collections.Generic;

namespace Ra.Widgets
{
    /**
     * Class for "visual" Ajax Controls. Mostly all Ajax Controls in Ra-Ajax inherits from this class
     * instead of the RaControl class since this class implements logic for the Style property collection
     * and also for the CssClass property. In addition this control have events for virtually every event
     * you can dream up, including MouseOver, MouseOut, Click, DblClick, MouseDown, KeyPress and many more.
     */
    public abstract class RaWebControl : RaControl, IAttributeAccessor, IRaControl
    {
        private StyleCollection _styles;

        /**
         * Raised when control is clicked
         */
        public event EventHandler Click;

        /**
         * Raised when control is double clicked
         */
        public event EventHandler DblClick;

        /**
         * Raised when mouse is pressed down on top of control
         */
        public event EventHandler MouseDown;

        /**
         * Raised when mouse is pressed down and released on top of control
         */
        public event EventHandler MouseUp;

        /**
         * Raised when mouse is over the control, opposite of MouseOut
         */
        public event EventHandler MouseOver;

        /**
         * Raised when mouse is moved over the control
         */
        public event EventHandler MouseMove;

        /**
         * Raised when mouse is leaving the control, opposite of MouseOver
         */
        public event EventHandler MouseOut;

        /**
         * Raised when key is pressed down over an element and then released
         */
        public event EventHandler KeyPress;

        /**
         * Raised when key is pressed down over an element
         */
        public event EventHandler KeyDown;

        /**
         * Raised when key is released over an element
         */
        public event EventHandler KeyUp;

        // Only purpose is to instantiate the _styles field with the this as the parameter
        public RaWebControl()
        {
            _styles = new StyleCollection(this);
        }

        /**
         * CSS class name for the root HTML element. Default value is "" and will not render the class 
         * attribute on the HTML element.
         */
        [DefaultValue("")]
        public virtual string CssClass
        {
            get { return ViewState["CssClass"] == null ? "" : (string)ViewState["CssClass"]; }
            set
            {
                if (value != CssClass)
                    SetJSONValueString("CssClass", value);
                ViewState["CssClass"] = value;
            }
        }

        /**
         * If true then the wiget can raise multiple events (Ajax Requests) even though the first 
         * Ajax Request have still not returned. The default value is true.
         * If this value is false then the widget will NOT queue up consecutive Ajax Requests
         * even though the user interacts with the widget in such a way that it is supposed to
         * before the previous Ajax Request have returned. This is very useful for scenarios
         * where you have e.g. a Button which spends a lot of time when clicked on the server
         * due to server execution overhead and you don't want the user to queue up new Ajax
         * Requests if he clicks the Button multiple times due to that the user feels that 
         * "nothing is happening" when clicked the first time. Kind of like an alternative to
         * a BehaviorUpdater, but locally for a specific widget.
         */
        [DefaultValue(true)]
        public bool QueueMultipleRequests
        {
            get { return ViewState["QueueMultipleRequests"] == null ? true : (bool)ViewState["QueueMultipleRequests"]; }
            set { ViewState["QueueMultipleRequests"] = value; }
        }

        /**
         * Tooltip text of control, will show when mouse is hovered over control...
         */
        [DefaultValue("")]
        public virtual string Tooltip
        {
            get { return ViewState["Tooltip"] == null ? "" : (string)ViewState["Tooltip"]; }
            set
            {
                if (value != Tooltip)
                    SetJSONGenericValue("title", value);
                ViewState["Tooltip"] = value;
            }
        }

        /**
         * TabIndex value of control
         */
        [DefaultValue(0)]
        public int TabIndex
        {
            get { return ViewState["TabIndex"] == null ? 0 : (int)ViewState["TabIndex"]; }
            set
            {
                if (value != TabIndex)
                    SetJSONGenericValue("tabindex", value.ToString());
                ViewState["TabIndex"] = value;
            }
        }

        /**
         * Client-side event (JavaScript function) which will trigger when button is clicked.
         * Normally you wouldn't use this except when developing controls yourself. Be very careful
         * with this method since it "moves you into JS land" which often is a dangerous place to be.
         * The reason why we chose to have it in the first place is because some controls needs to trap
         * events on the client-side. Like for instance the Accordion.
         */
        [DefaultValue("")]
        public string OnClickClientSide
        {
            get { return ViewState["OnClickClientSide"] == null ? "" : (string)ViewState["OnClickClientSide"]; }
            set
            {
                if (value != OnClickClientSide)
                    SetJSONValueString("OnClickClientSide", value);
                ViewState["OnClickClientSide"] = value;
            }
        }

        /**
         * Collection of style-values, maps to the style attribute on the root HTML element
         */
        public virtual StyleCollection Style
        {
            get { return _styles; }
        }

        protected string GetWebControlAttributes()
        {
            string retVal = GetCssClassHTMLFormatedAttribute() + GetStyleHTMLFormatedAttribute() + GetTooltipAttribute();
            if (TabIndex != 0)
            {
                if (!string.IsNullOrEmpty(retVal))
                    retVal += " ";
                retVal += "tabindex=\"" + TabIndex + "\"";
            }
            return retVal;
        }

        private string GetTooltipAttribute()
        {
            return Tooltip == "" ? "" : (" title=\"" + Tooltip + "\"");
        }

        public virtual void DispatchEvent(string name)
        {
            switch (name)
            {
                case "click":
                    if (Click != null)
                        Click(this, new EventArgs());
                    break;
                case "dblclick":
                    if (DblClick != null)
                        DblClick(this, new EventArgs());
                    break;
                case "mousedown":
                    if (MouseDown != null)
                        MouseDown(this, new EventArgs());
                    break;
                case "mouseup":
                    if (MouseUp != null)
                        MouseUp(this, new EventArgs());
                    break;
                case "mouseover":
                    if (MouseOver != null)
                        MouseOver(this, new EventArgs());
                    break;
                case "mousemove":
                    if (MouseMove != null)
                        MouseMove(this, new EventArgs());
                    break;
                case "mouseout":
                    if (MouseOut != null)
                        MouseOut(this, new EventArgs());
                    break;
                case "keypress":
                    if (KeyPress != null)
                        KeyPress(this, new EventArgs());
                    break;
                case "keydown":
                    if (KeyDown != null)
                        KeyDown(this, new EventArgs());
                    break;
                case "keyup":
                    if (KeyUp!= null)
                        KeyUp(this, new EventArgs());
                    break;
                default:
                    throw new ApplicationException("Unknown event fired for control");
            }
        }

        protected override string GetEventsRegisterScript()
        {
            string evts = string.Empty;
            if (Click != null)
                evts += "['click', true]";
            if (DblClick != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['dblclick']";
            }
            if (MouseDown != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['mousedown']";
            }
            if (MouseUp != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['mouseup']";
            }
            if (MouseMove != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['mousemove']";
            }
            if (MouseOver != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += GetMouseOverEventScript();
            }
            if (MouseOut != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += GetMouseOutEventScript();
            }
            if (KeyPress != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['keypress']";
            }
            if (KeyDown != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['keydown']";
            }
            if (KeyUp != null)
            {
                if (evts.Length != 0)
                    evts += ",";
                evts += "['keyup']";
            }
            return evts;
        }

        protected virtual string GetMouseOutEventScript()
        {
            return "['mouseout']";
        }

        protected virtual string GetMouseOverEventScript()
        {
            return "['mouseover']";
        }

        #region [ -- Overridden Base Class methods -- ]

        // Overridden to start tracking on the Style collection
        protected override void TrackViewState()
        {
            base.TrackViewState();
            Style.TrackViewState();
        }

        // Overridden to save the ViewState for the Style collection
        protected override object SaveViewState()
        {
            object[] content = new object[2];

            // This order we must remember for the LoadViewState logic ;)
            content[0] = Style.SaveViewState();
            content[1] = base.SaveViewState();
            return content;
        }

        // Overridden to reload the ViewState for the Style collection
        protected override void LoadViewState(object savedState)
        {
            object[] content = savedState as object[];

            Style.LoadViewState(content[0] as string);

            // When we save the ViewState we save the base class object as the second instance in the array...
            base.LoadViewState(content[1]);
        }

        protected override string GetClientSideScriptOptions()
        {
            string retVal = base.GetClientSideScriptOptions();
            if (!string.IsNullOrEmpty(OnClickClientSide))
            {
                if (!string.IsNullOrEmpty(retVal))
                    retVal += ",";
                retVal += "clientClick:" + OnClickClientSide;
            }
            if (!QueueMultipleRequests)
            {
                if (!string.IsNullOrEmpty(retVal))
                    retVal += ",";
                retVal += "noQueue:true";
            }
            return retVal;
        }

        #endregion

        #region [ -- Properties -- ]

        #endregion

        #region IAttributeAccessor Members

        public string GetAttribute(string key)
        {
            // TODO: Implement...
            return null;
        }

        public void SetAttribute(string key, string value)
        {
            if (key.ToLower() == "style")
            {
                string[] styles = value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string idx in styles)
                {
                    string[] keyValue = idx.Split(':');
                    Style[keyValue[0]] = keyValue[1];
                }
            }
        }

        private string GetStyleHTMLFormatedAttribute()
        {
            string style = Style.GetStylesForResponse();
            if (!string.IsNullOrEmpty(style))
                style = string.Format(" style=\"{0}\"", style);
            return style;
        }

        private string GetCssClassHTMLFormatedAttribute()
        {
            string cssClass = string.IsNullOrEmpty(CssClass) ? "" : " class=\"" + CssClass + "\"";
            return cssClass;
        }

        #endregion
    }
}
