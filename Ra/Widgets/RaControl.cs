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
        private Dictionary<string, object> _JSONValues = new Dictionary<string, object>();
        protected bool _hasSetFocus;

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

			bool addComma = false;
            foreach (string idxKey in _JSONValues.Keys)
            {
                if (addComma)
                    builder.Append(",");
                object idxValue = _JSONValues[idxKey];
                addComma = SerializeJSONValue(idxKey, idxValue, builder);
            }
			if (builder.Length > 0)
				return "{" + builder.ToString() + "}";
            return string.Empty;
        }

		// This one returns true ONLY if there was something actually ADDED to the builder...
		// Note if you have "custom objects" you want to serialize in your own extension widgets
		// you should OVERRIDE this method and return base for everything except your "custom types"...
        protected virtual bool SerializeJSONValue(string key, object value, StringBuilder builder)
        {
            // TODO: Create more general approach, this one only handles TWO level deep JSON objects...
            if (value.GetType() == typeof(string))
            {
                builder.AppendFormat("\"{0}\":\"{1}\"",
                    key,
                    value.ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r"));
                return true;
            }
            if (value.GetType() == typeof(System.Drawing.Color))
            {
				System.Drawing.Color color = (System.Drawing.Color)value;
				string tmp = System.Drawing.ColorTranslator.ToHtml(color);
                builder.AppendFormat("\"{0}\":\"{1}\"",
                    key,
                    tmp);
                return true;
            }
            if (value.GetType() == typeof(bool))
            {
                builder.AppendFormat("\"{0}\":{1}",
                    key,
                    value);
                return true;
            }
            if (value.GetType() == typeof(int))
            {
                builder.AppendFormat("\"{0}\":{1}",
                    key,
                    value);
                return true;
            }
            if (value.GetType() == typeof(System.Drawing.Rectangle))
            {
				System.Drawing.Rectangle rect = (System.Drawing.Rectangle)value;
                builder.AppendFormat("{0}:{{left:{1},top:{2},width:{3},height:{4}}}",
                    key,
                    rect.Left, rect.Top, rect.Width, rect.Height);
                return true;
            }
            if (value.GetType() == typeof(System.Drawing.Point))
            {
				System.Drawing.Point pt = (System.Drawing.Point)value;
                builder.AppendFormat("{0}:{{x:{1},y:{2}}}",
                    key,
                    pt.X, pt.Y);
                return true;
            }
            if (value.GetType() == typeof(Dictionary<string, string>))
            {
                Dictionary<string, string> values = value as Dictionary<string, string>;
				if (values.Count > 0)
				{
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
					return true;
				}
				return false;
            }
			throw new ApplicationException("Type not found in SerializeJSONValue - you should override this method in your own controls if you're serializing custom types. Types was; " + value.GetType().Name);
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
                        if (!string.IsNullOrEmpty(JSON))
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
		
		private string GetScriptOptions(bool addComma)
		{
			string options = GetClientSideScriptOptions();
			if (!string.IsNullOrEmpty(options))
			{
				if (addComma)
					options = "," + options;
			}
			return options;
		}
		
		private string GetScriptBehaviors(bool addComma)
		{
			string behaviors = GetBehaviorRegisterScript();
			if (!string.IsNullOrEmpty(behaviors))
			{
				if (addComma)
					behaviors = ",beha:" + behaviors;
				else
					behaviors = "beha:" + behaviors;
			}
			return behaviors;
		}
		
		private string GetScriptEvents(bool addComma)
		{
			string evts = GetEventsRegisterScript();
			if (!string.IsNullOrEmpty(evts))
			{
				if (addComma)
					evts = ",evts:[" + evts + "]";
				else
					evts = "evts:[" + evts + "]";
			}
			return evts;
		}

        // Used to retrieve the client-side initialization script
        private bool _scriptRetrieved;
        public virtual string GetClientSideScript()
        {
			if (_scriptRetrieved)
				return "";
			_scriptRetrieved = true;

			// Retrieving all the different option types
			string options = GetScriptOptions(false);
			string behaviors = GetScriptBehaviors(!string.IsNullOrEmpty(options));
			string evts = GetScriptEvents(!string.IsNullOrEmpty(options) || !string.IsNullOrEmpty(behaviors));
			
			string optionsString = options + behaviors + evts;
			
			// Appending the closing brace
			if (!string.IsNullOrEmpty(optionsString))
				optionsString = ",{" + optionsString + "}";
			return string.Format("\r\n{2}('{0}'{1});", 
				ClientID, 
				optionsString,
				GetClientSideScriptType());
        }
		
		protected virtual string GetClientSideScriptType()
		{
			return "Ra.C";
		}
		
		protected virtual string GetEventsRegisterScript()
		{
			return string.Empty;
		}
		
		protected virtual string GetClientSideScriptOptions()
		{
			if (_hasSetFocus)
				return "focus:true";
			return "";
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
				retVal = "[" + retVal + "]";
			return retVal;
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
