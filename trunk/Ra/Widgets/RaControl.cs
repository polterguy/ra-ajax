/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Ra Software AS thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Text;
using System.ComponentModel;

namespace Ra.Widgets
{
    public abstract class RaControl : Control, IRaControl
    {
        public enum RenderingPhase
        {
            Invisible,
            Visible,
            ReRender,
            RenderHtml
        };

        // Used to track if control has been rendered previously
        private RenderingPhase _controlRenderingState = RenderingPhase.Invisible;

        [Browsable(false)]
        public RenderingPhase Phase
        {
            get { return _controlRenderingState; }
            set { _controlRenderingState = value; }
        }

        private Dictionary<string, object> _JSONValues = new Dictionary<string, object>();

        public Dictionary<string, string> GetJSONValueDictionary(string key)
        {
            if (this.IsTrackingViewState)
            {
                if (!_JSONValues.ContainsKey(key))
                {
                    _JSONValues[key] = new Dictionary<string, string>();
                }
                return (Dictionary<string, string>)_JSONValues[key];
            }
            else
            {
                // If we're NOT tracking ViewState yet we're not bothering to store
                // changes in the _JSONValues at all since they might occur due
                // to declarative syntax in e.g. .ASPX file or OnInit or some other
                // places...
                // Though to avoid runtime failure and scattering if checks all over the place
                // we're returning a "dummy" object here...
                return new Dictionary<string, string>();
            }
        }

        internal bool HasJSONValueDictionary(string key)
        {
            return _JSONValues.ContainsKey(key);
        }

        public void SetJSONValueString(string key, string value)
        {
            if (this.IsTrackingViewState)
                _JSONValues[key] = value;
        }

        public void SetJSONValueBool(string key, bool value)
        {
            if (this.IsTrackingViewState)
                _JSONValues[key] = value;
        }

        public void SetJSONGenericValue(string key, string value)
        {
            if (this.IsTrackingViewState)
            {
                Dictionary<string, string> generic = this.GetJSONValueDictionary("Generic");
                generic[key] = value;
            }
        }

        protected bool _hasSetFocus;
        public override void Focus()
        {
            _hasSetFocus = true;
            if (AjaxManager.Instance.IsCallback)
            {
                SetJSONValueString("Focus", "");
            }
        }

        public virtual string SerializeJSON()
        {
            // Short circuting
            if (_JSONValues.Count == 0)
                return null;

            StringBuilder builder = new StringBuilder();
            builder.Append("{");

            bool first = true;
            foreach (string idxKey in _JSONValues.Keys)
            {
                if (first)
                    first = false;
                else
                    builder.Append(",");
                object idxValue = _JSONValues[idxKey];
                SerializeJSONValue(idxKey, idxValue, builder);
            }
            builder.Append("}");
            return builder.ToString();
        }

        protected virtual void SerializeJSONValue(string key, object value, StringBuilder builder)
        {
            // TODO: Create more general approach, this one only handles TWO level deep JSON objects...
            if (value.GetType() == typeof(string))
            {
                builder.AppendFormat("\"{0}\":\"{1}\"",
                    key,
                    value.ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r"));
                return;
            }
            if (value.GetType() == typeof(bool))
            {
                builder.AppendFormat("\"{0}\":{1}",
                    key,
                    value);
                return;
            }
            if (value.GetType() == typeof(Dictionary<string, string>))
            {
                Dictionary<string, string> values = value as Dictionary<string, string>;
                builder.AppendFormat("\"{0}\":[", key);
                bool first = true;
                foreach (string idxKey in values.Keys)
                {
                    if (first)
                        first = false;
                    else
                        builder.Append(",");
                    builder.AppendFormat("[\"{0}\",\"{1}\"]", 
                        idxKey,
                        values[idxKey].Replace("\\", "\\\\").Replace("\"", "\\\""));
                }
                builder.Append("]");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            if (DesignMode)
                throw new ApplicationException("Ra Ajax doesn't support Design time");

            // To initialize control
            AjaxManager.Instance.InitializeControl(this);

            // Including JavaScript
            AjaxManager.Instance.IncludeMainRaScript();
            AjaxManager.Instance.IncludeMainControlScripts();

            // Registering control with the AjaxManager
            AjaxManager.Instance.CurrentPage.RegisterRequiresControlState(this);

            base.OnInit(e);
        }

        protected override void LoadControlState(object savedState)
        {
            if (!AjaxManager.Instance.IsCallback)
                Phase = RenderingPhase.Invisible;
            if (savedState != null)
                Phase = (RenderingPhase)savedState;
        }

        protected override object SaveControlState()
        {
            if (Phase == RenderingPhase.ReRender || Phase == RenderingPhase.RenderHtml)
                return RenderingPhase.Visible;
            if (Visible)
                return RenderingPhase.Visible;
            else
                return RenderingPhase.Invisible;
        }

        private bool IsChildControl(RaControl idx)
        {
            return idx.ClientID != this.ClientID && idx.ClientID.IndexOf(this.ClientID) == 0;
        }

        protected virtual string GetChildrenClientSideScript()
        {
            return "";
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            if (DesignMode)
                throw new ApplicationException("Ra Ajax doesn't support Design time");

            if (Visible)
            {
                if (AjaxManager.Instance.IsCallback)
                {
                    if (Phase == RenderingPhase.Invisible)
                    {
                        // Control was made Visible THIS request...!
                        AjaxManager.Instance.Writer.WriteLine("Ra.$('{0}').replace('{1}');",
                            ClientID,
                            GetHTML().Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n"));
                        AjaxManager.Instance.Writer.WriteLine(GetClientSideScript());
                        RenderChildren(writer);
                    }
                    else if (Phase == RenderingPhase.Visible)
                    {
                        // JSON changes, control was visible also previous request...
                        string JSON = SerializeJSON();
                        if (JSON != null)
                        {
                            AjaxManager.Instance.Writer.WriteLine("Ra.Control.$('{0}').handleJSON({1});",
                                ClientID,
                                JSON);
                        }
                        RenderChildren(writer);
                    }
                    else if (Phase == RenderingPhase.ReRender)
                    {
                        AjaxManager.Instance.Writer.WriteLine("Ra.Control.$('{0}').reRender('{1}');",
                            ClientID,
                            GetHTML().Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n"));
                        RenderChildren(writer);
                        AjaxManager.Instance.Writer.WriteLine(GetChildrenClientSideScript());
                    }
                    else if (Phase == RenderingPhase.RenderHtml)
                    {
                        writer.Write(GetHTML());
                    }
                }
                else
                {
                    if (Phase == RenderingPhase.RenderHtml)
                    {
                        writer.Write(GetHTML());
                    }
                    else
                    {
                        writer.Write(GetHTML());
                        AjaxManager.Instance.Writer.WriteLine(GetClientSideScript());
                    }
                }
            }
            else // if (!Visible)
            {
                if (AjaxManager.Instance.IsCallback)
                {
                    if (Phase == RenderingPhase.ReRender || Phase == RenderingPhase.Visible)
                    {
                        if (AjaxManager.Instance.IsCallback)
                        {
                            AjaxManager.Instance.Writer.WriteLine("Ra.Control.$('{0}').destroy();", ClientID);
                        }
                    }
                    else if (Phase == RenderingPhase.RenderHtml)
                    {
                        writer.Write(GetInvisibleHTML());
                    }
                }
                else
                {
                    writer.Write(GetInvisibleHTML());
                }
            }
        }

        // Used for dispatching events for the Control
        public virtual void DispatchEvent(string name)
        { }

        // Used to retrieve the client-side initialization script
        private bool _scriptRetrieved;
        public virtual string GetClientSideScript()
        {
            if (_scriptRetrieved)
                return "";
            _scriptRetrieved = true;
            if (_hasSetFocus)
                return string.Format("\r\nRa.C('{0}', {{focus:true}});", ClientID);
            else
                return string.Format("\r\nRa.C('{0}');", ClientID);
        }

        // The HTML for the control
        public abstract string GetHTML();

        // The INVISIBLE HTML for the control
        public virtual string GetInvisibleHTML()
        {
            return string.Format("<span id=\"{0}\" style=\"display:none;\">&nbsp;</span>", ClientID);
        }

        public void SignalizeReRender()
        {
            if (!AjaxManager.Instance.IsCallback || Phase == RenderingPhase.Invisible)
                return;
            if (this.IsTrackingViewState)
            {
                Phase = RenderingPhase.ReRender;
            }
        }
    }
}
