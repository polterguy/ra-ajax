/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
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
		
		[Browsable(false)]
		public IEnumerable<Behavior> Behaviors
		{
			get
			{
				foreach (Control idx in Controls)
				{
					if (idx is Behavior)
						yield return idx as Behavior;
				}
			}
		}
		
		public T FirstBehavior<T>() where T : Behavior
		{
			foreach (Behavior idx in Behaviors)
			{
				if (idx is T)
					return idx as T;
			}
			return null;
		}
		
		protected string GetBehaviorRegisterScript()
		{
			string retVal = "";
			bool isFirst = true;
			foreach (Behavior idx in Behaviors)
			{
				if (isFirst)
					isFirst = false;
				else
					retVal += ",";
				retVal += idx.GetRegistrationScript(); 
			}
			if (retVal != string.Empty)
				retVal = "beha:[" + retVal + "]";
			return retVal;
		}

        private Dictionary<string, object> _JSONValues = new Dictionary<string, object>();

        internal Dictionary<string, string> GetJSONValueDictionary(string key)
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

        protected void SetJSONValueString(string key, string value)
        {
            if (this.IsTrackingViewState)
                _JSONValues[key] = value;
        }

        protected void SetJSONValueObject(string key, object value)
        {
            if (this.IsTrackingViewState)
                _JSONValues[key] = value;
        }

        protected void SetJSONValueBool(string key, bool value)
        {
            if (this.IsTrackingViewState)
                _JSONValues[key] = value;
        }

        protected void SetJSONGenericValue(string key, string value)
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

        protected string SerializeJSON()
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

        protected void SerializeJSONValue(string key, object value, StringBuilder builder)
        {
            // TODO: Create more general approach, this one only handles TWO level deep JSON objects...
            if (value.GetType() == typeof(string))
            {
                builder.AppendFormat("\"{0}\":\"{1}\"",
                    key,
                    value.ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r"));
                return;
            }
            if (value.GetType() == typeof(System.Drawing.Color))
            {
				System.Drawing.Color color = (System.Drawing.Color)value;
				string tmp = System.Drawing.ColorTranslator.ToHtml(color);
                builder.AppendFormat("\"{0}\":\"{1}\"",
                    key,
                    tmp);
                return;
            }
            if (value.GetType() == typeof(bool))
            {
                builder.AppendFormat("\"{0}\":{1}",
                    key,
                    value);
                return;
            }
            if (value.GetType() == typeof(int))
            {
                builder.AppendFormat("\"{0}\":{1}",
                    key,
                    value);
                return;
            }
            if (value.GetType() == typeof(System.Drawing.Rectangle))
            {
				System.Drawing.Rectangle rect = (System.Drawing.Rectangle)value;
                builder.AppendFormat("{0}:{{left:{1},top:{2},width:{3},height:{4}}}",
                    key,
                    rect.Left, rect.Top, rect.Width, rect.Height);
                return;
            }
            if (value.GetType() == typeof(System.Drawing.Point))
            {
				System.Drawing.Point pt = (System.Drawing.Point)value;
                builder.AppendFormat("{0}:{{x:{1},y:{2}}}",
                    key,
                    pt.X, pt.Y);
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
			if (Phase != RenderingPhase.Visible)
			{
				SetAllChildrenToRenderHtml(this.Controls);
			}
        }

		protected virtual void SetAllChildrenToRenderHtml(ControlCollection controls)
        {
            foreach (Control idx in controls)
            {
                if (idx is RaControl)
                {
                    (idx as RaControl).Phase = RenderingPhase.RenderHtml;
                }
                SetAllChildrenToRenderHtml(idx.Controls);
            }
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
		// This could have been an abstract method though
		// some widgets don't really need to dispatch events
		// so therefor we've made an implementation for it.
		// Reconsider later...?
        public virtual void DispatchEvent(string name)
        { }

        // Used to retrieve the client-side initialization script
        private bool _scriptRetrieved;
        public virtual string GetClientSideScript()
        {
			string behaviors = GetBehaviorRegisterScript();
			if (_scriptRetrieved)
				return "";
			_scriptRetrieved = true;
			if (_hasSetFocus)
				return string.Format("\r\nRa.C('{0}', {{focus:true{1}}});", ClientID, (behaviors == string.Empty ? "" : "," + behaviors));
			else
				return string.Format("\r\nRa.C('{0}'{1});", ClientID, (behaviors == string.Empty ? "" : ",{" + behaviors + "}"));
        }

        // The HTML for the control
        public abstract string GetHTML();

        // The INVISIBLE HTML for the control
        public virtual string GetInvisibleHTML()
        {
            return string.Format("<span id=\"{0}\" style=\"display:none;\">&nbsp;</span>", ClientID);
        }

        public void ReRender()
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
