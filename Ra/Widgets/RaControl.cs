/*
* Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
* Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
* This code is licensed under the LGPL version 3 which 
* can be found in the license.txt file on disc.
* 
*/

using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Collections.Generic;

/**
 * Namespace where all the Widgets from the Ra core can be found together with some of their
 * helper classes.
 */
namespace Ra.Widgets
{
    /**
     * Base control for all other Ajax Controls in Ra-Ajax. Inherit from this if you want a 
     * minimalistic extension Ajax control without the overhead from RaWebControl.
     */
	public abstract class RaControl : Control
	{
        private bool _hasRendered;
        private bool _reRender;
        private Dictionary<string, object> _JSONValues = new Dictionary<string, object>();
        protected bool _hasSetFocus;
        private List<string> _extraInitFunctions = new List<string>();

        /**
         * Extra information you can "tag" control with. BE CAREFUL with this one though since it adds
         * to the ViewState. Don't add up LARGE things into it...
         */
        [DefaultValue("")]
        public string Xtra
        {
            get { return ViewState["Xtra"] == null ? "" : (string)ViewState["Xtra"]; }
            set { ViewState["Xtra"] = value; }
        }

        protected override void LoadControlState(object savedState)
		{
			if (!AjaxManager.Instance.IsCallback)
			{
				// For postbacks and initial hits we ALWAYS re-render all controls (obviously)
				_hasRendered = false;
			}
            // Mono sometimes messes up the types here for what reasons I don't know... :(
            // It appears that it tries to load control state for objects added dynamically this round
            // which obviously is WRONG...!
            else if (savedState != null && savedState.GetType() == typeof(bool))
			{
				_hasRendered = (bool)savedState;
			}
		}

		protected override object SaveControlState()
		{
			return Visible;
		}

        // TODO: Try to make entire thing private....
		[Browsable(false)]
		internal bool HasRendered
		{
			get { return _hasRendered; }
			private set { _hasRendered = value; }
		}

        /**
         * Call this method to re-render the control. A call to this method will ensure that all
         * child controls in the Controls collection will be re-rendered. Meaning the innerHTML of
         * the control will be sent in its entirety back to the client.
         */
		public void ReRender()
		{
			if (!AjaxManager.Instance.IsCallback || !HasRendered)
				return;
			_reRender = true;
		}

		private bool IsAnyAncestorRenderingHtml
		{
			get
			{
				Control idx = Parent;
                while (idx != null)
                {
	                RaControl temp = idx as RaControl;
	                if (temp != null)
	                {
                        if (temp._reRender || !temp.HasRendered)
                            return true;
                    }
                    idx = idx.Parent;
                }
				return false;
			}
		}

        // TODO: Refactor, change Behavior to make it possible to make this private, or investigate options...
		internal void RenderOnlyJSON(HtmlTextWriter writer)
		{
			string JSON = SerializeJSON();
			if (!string.IsNullOrEmpty(JSON))
			{
				AjaxManager.Instance.Writer.WriteLine("Ra.Control.$('{0}').handleJSON({1});", ClientID, JSON);
			}
			RenderChildren(writer);
		}
		
		private void RenderAllChildrenToAjaxManager()
		{
			// Note that we're NOT rendering the Children directly into the AjaxManager stream
			// but rather we're creating a wrapper stream which we're pushing everything into
			// The IsParentReRenderingLogic of the child controls will then make sure that we're
			// rendering into the "given stream" and then afterwards
            // we "extract" the contents and render it inside of the AjaxManager Stream
			MemoryStream memStream = new MemoryStream();
			TextWriter txtWriter = new StreamWriter(memStream);
			HtmlTextWriter htmlWriter = new HtmlTextWriter(txtWriter);
			
            // Notic the stream we're using here...
			RenderChildren(htmlWriter);

            // Flushing and sending to "real" stream
			htmlWriter.Flush();
			txtWriter.Flush();
			memStream.Flush();
			memStream.Position = 0;
			TextReader reader = new StreamReader(memStream);
			AjaxManager.Instance.Writer.Write(reader.ReadToEnd().Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n"));
		}		

		private void ReRenderControlAndAllChildren(HtmlTextWriter writer)
		{
			// Here we must wrap all child widgets into a MemoryStream and then
			// render the contents of that into the AjaxManager.Stream wrapped inside of the "this element" HTML...
			
            // Rendering the "this widget" Opening HTML
			AjaxManager.Instance.Writer.Write("Ra.Control.$('{0}').reRender('{1}",
				ClientID,
				GetOpeningHTML());
			
			RenderAllChildrenToAjaxManager();

            // "Ending" process...
			AjaxManager.Instance.Writer.Write(GetClosingHTML());
			AjaxManager.Instance.Writer.WriteLine("');");
			
			// THEN we get the scripts. It is VERY important that the scripts are rendered after the HTML for the widgets
            // ALL of the widgets...!!
			AjaxManager.Instance.Writer.WriteLine(GetChildrenClientSideScript());
		}

		private void ReRenderDueToParentReRendering(HtmlTextWriter writer)
		{
			writer.Write(GetOpeningHTML());
			RenderChildren(writer);
			writer.Write(GetClosingHTML());
		}

		private void RenderControlVisibleForFirstTime(HtmlTextWriter writer)
		{
			AjaxManager.Instance.Writer.Write("Ra.$('{0}').replace('{1}",
				ClientID,
				GetOpeningHTML().Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n"));

			RenderAllChildrenToAjaxManager();

			AjaxManager.Instance.Writer.Write("{0}');\r\n",
				GetClosingHTML().Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n"));

			AjaxManager.Instance.Writer.WriteLine(GetClientSideScript());
			AjaxManager.Instance.Writer.WriteLine(GetChildrenClientSideScript());
		}

		public override void RenderControl(HtmlTextWriter writer)
		{
			if (DesignMode)
				throw new ApplicationException("Ra-Ajax doesn't support Design time");

			if (Visible)
			{
				if (AjaxManager.Instance.IsCallback)
				{
					// PRIORITY COUNTS...!!
					if (IsAnyAncestorRenderingHtml)
					{
						// Control is visible, this is a callback and some parent widget has signalized
						// that it wants the entire thing re-rendered
						ReRenderDueToParentReRendering(writer);
					}
					else if (_reRender)
					{
						// Control is Visible, it is a callback and user wants to re-render the control
						// Therefor we destroy the control, re-render it and re-render all its children
						ReRenderControlAndAllChildren(writer);
					}
					else if (HasRendered)
					{
						// Control is Visible, it is a Callback and control has been rendered before and we don't do re-render
						// Therefor we send JSON back only and render its children
						RenderOnlyJSON(writer);
					}
					else
					{
						// Control is Visible, it has never been rendered before and no Parent control is re-rendering
						// Control was being made visible this request
						RenderControlVisibleForFirstTime(writer);
					}
				}
				else
				{
					writer.Write(GetOpeningHTML());
					RenderChildren(writer);
					writer.Write(GetClosingHTML());
					AjaxManager.Instance.Writer.WriteLine(GetClientSideScript());
				}
			}
			else // if (!Visible)
			{
				if (AjaxManager.Instance.IsCallback)
				{
                    if (HasRendered)
                    {
                        // Control is NOT visible, this is a callback and Control has been rendered before
                        // We Need to DESTROY Control here. If Control has not been rendered before we do NOTHING!
                        AjaxManager.Instance.Writer.WriteLine("Ra.Control.$('{0}').destroy('{1}');", ClientID, GetInvisibleHTML());
                    }
                    else
                    {
                        // Callback, in-visible
                        string invisibleHtml = GetInvisibleHTML();
                        if (string.IsNullOrEmpty(invisibleHtml))
                            invisibleHtml = GetDefaultInvisibleHTML();
                        writer.Write(invisibleHtml);
                    }
				}
				else
				{
					// Not callback and NOT visible
                    string invisibleHtml = GetInvisibleHTML();
                    if (string.IsNullOrEmpty(invisibleHtml))
                        invisibleHtml = GetDefaultInvisibleHTML();
                    writer.Write(invisibleHtml);
				}
			}
		}

        /**
         * Collection of behaviors for the control
         */
		[Browsable(false)]
		public virtual IEnumerable<Behavior> Behaviors
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

        /**
         * Helper to get the first Behavior of type T. Will query the Behaviors collection and return
         * the first instance which is of type T in the collection.
         */
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

        // TODO: Refactor all the SetJSONValue methods...!!
        // Make them into ONE which takes an object and then do GetType or something within that
        // method...
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

        /**
         * Gives the control focus
         */
		public override void Focus()
		{
			_hasSetFocus = true;
			if (AjaxManager.Instance.IsCallback)
			{
				SetJSONValueString("Focus", "");
			}
		}

		private string SerializeJSON()
		{
			// Short circuting
			if (_JSONValues.Count == 0)
				return null;

			StringBuilder builder = new StringBuilder();

			foreach (string idxKey in _JSONValues.Keys)
			{
				object idxValue = _JSONValues[idxKey];
				SerializeJSONValue(idxKey, idxValue, builder);
			}
			if (builder.Length > 0)
				return "{" + builder.ToString() + "}";
			return string.Empty;
		}

        /**
         * If you have an Extension Ajax Control which has a property of a type which the default
         * method is not able to serialize then override this method in your Extension control
         * and check for your custom types and if not custom type then return base implementation.
         */
		protected virtual void SerializeJSONValue(string key, object value, StringBuilder builder)
		{
			// TODO: Create more general approach, this one only handles TWO level deep JSON objects...
			if (value.GetType() == typeof(string))
			{
                if (builder.Length > 0 )
                    builder.Append(",");					
				builder.AppendFormat("\"{0}\":\"{1}\"",
					key,
					value.ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r"));
			}
			else if (value.GetType() == typeof(System.Drawing.Color))
			{
                if (builder.Length > 0 )
                    builder.Append(",");					
				System.Drawing.Color color = (System.Drawing.Color)value;
					string tmp = System.Drawing.ColorTranslator.ToHtml(color);
					builder.AppendFormat("\"{0}\":\"{1}\"",
					key,
					tmp);
			}
			else if (value.GetType() == typeof(bool))
			{
                if (builder.Length > 0 )
                    builder.Append(",");					
				builder.AppendFormat("\"{0}\":{1}",
					key,
					value.ToString().ToLower());
			}
            else if (value.GetType() == typeof(int))
            {
                if (builder.Length > 0)
                    builder.Append(",");
                builder.AppendFormat("\"{0}\":{1}",
                    key,
                    value);
            }
            else if (value.GetType() == typeof(decimal))
            {
                if (builder.Length > 0)
                    builder.Append(",");
                builder.AppendFormat("\"{0}\":{1}",
                    key,
                    value);
            }
            else if (value.GetType() == typeof(System.Drawing.Rectangle))
			{
                if (builder.Length > 0 )
                    builder.Append(",");					
				System.Drawing.Rectangle rect = (System.Drawing.Rectangle)value;
					builder.AppendFormat("{0}:{{left:{1},top:{2},width:{3},height:{4}}}",
					key,
					rect.Left, rect.Top, rect.Width, rect.Height);
			}
			else if (value.GetType() == typeof(System.Drawing.Point))
			{
                if (builder.Length > 0 )
                    builder.Append(",");					
				System.Drawing.Point pt = (System.Drawing.Point)value;
					builder.AppendFormat("{0}:{{x:{1},y:{2}}}",
					key,
					pt.X, pt.Y);
			}
			else if (value.GetType() == typeof(Dictionary<string, string>))
			{
				Dictionary<string, string> values = value as Dictionary<string, string>;
				if (values.Count > 0)
				{
	                if (builder.Length > 0 )
	                    builder.Append(",");					
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
            else			
			    throw new ApplicationException("Type not found in SerializeJSONValue - you should override this method in your own controls if you're serializing custom types. Types was; " + value.GetType().Name);
		}

		protected override void OnInit(EventArgs e)
		{
			if (DesignMode)
				throw new ApplicationException("Ra-Ajax doesn't support Design time");

			// To initialize control
			AjaxManager.Instance.InitializeControl(this);

			// Including JavaScript
			AjaxManager.Instance.IncludeMainRaScript();
			AjaxManager.Instance.IncludeMainControlScripts();

			// Registering control with the AjaxManager
			AjaxManager.Instance.CurrentPage.RegisterRequiresControlState(this);

			base.OnInit(e);
		}

		protected virtual string GetChildrenClientSideScript()
		{
            // Recursively iterating the child controls collection and returning the registration
            // scripts for all controls inside of the "this" control
            return GetChildrenClientSideScript(Controls);
		}

		private string GetChildrenClientSideScript(ControlCollection controls)
		{
			string retVal = "";
			foreach (Control idx in controls)
			{
				if (idx.Visible)
				{
					if (idx is RaControl)
					{
						retVal += (idx as RaControl).GetClientSideScript();
					}
					retVal += GetChildrenClientSideScript(idx.Controls);
				}
			}
			return retVal;
		}

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

		internal void AddInitCall(string functionReference)
		{
			_extraInitFunctions.Add(functionReference);
		}

        /**
         * Override this one in your own Ajax Extension controls to get the registration script
         * for your widgets unless you are good with Ra.C as the registration script.
         * Normally you'd do this if you have your own JavaScript file for your widget.
         * Note though that often it is better to override on of the;
         * * GetClientSideScriptType
         * * GetEventsRegisterScript
         * * GetClientSideScriptOptions
         * methods...
         */
        protected virtual string GetClientSideScript()
		{
			// Retrieving all the different option types
			string options = GetScriptOptions(false);
			string behaviors = GetScriptBehaviors(!string.IsNullOrEmpty(options));
			string evts = GetScriptEvents(!string.IsNullOrEmpty(options) || !string.IsNullOrEmpty(behaviors));

			string optionsString = options + behaviors + evts;
			
			string extraFunctionCalls = "";
			foreach (string idx in this._extraInitFunctions)
			{
				extraFunctionCalls += "." + idx;
			}

			// Appending the closing brace
			if (!string.IsNullOrEmpty(optionsString))
				optionsString = ",{" + optionsString + "}";
			return string.Format("\r\n{2}('{0}'{1}){3};", 
				ClientID, 
				optionsString,
				GetClientSideScriptType(),
		        extraFunctionCalls);
		}

        /**
         * Override to return another type only for the client-side registration script, default
         * implementation returns; "Ra.C" which is shorthand for "new Ra.Control"
         */
		protected virtual string GetClientSideScriptType()
		{
			return "Ra.C";
		}

        /**
         * Override to return the explicitly handled events for your widgets. Default 
         * implementation is string.Empty
         */
		protected virtual string GetEventsRegisterScript()
		{
			return string.Empty;
		}

        /**
         * Override to return the options for your client-side registration script. Default
         * will return only the "focus" option.
         */
		protected virtual string GetClientSideScriptOptions()
		{
			if (_hasSetFocus)
				return "focus:true";
			return "";
		}

        /**
         * Override to return the Behavior registration scripts for your widgets. Default will just loop through
         * the behaviors in your Behaviors list. Very seldom you would want to override this one btw...
         */
		protected string GetBehaviorRegisterScript()
		{
			string retVal = "";
			bool isFirst = true;
			foreach (Behavior idx in Behaviors)
			{
                if (idx.Visible)
                {
                    if (isFirst)
                        isFirst = false;
                    else
                        retVal += ",";
                    retVal += idx.GetRegistrationScript();
                }
			}
			if (retVal != string.Empty)
				retVal = "[" + retVal + "]";
			return retVal;
		}

        /**
         * Opening HTML for your control. If you control can have child controls in the Controls collection
         * you would probably want to override also the GetClosingHTML method if you override this one.
         * If your widget cannot have child controls then only override this one and drop overriding 
         * the GetClosingHTML method.
         */
		protected abstract string GetOpeningHTML();

        /**
         * If your widget can have child controls in the Controls collection then override this one to
         * return the closing HTML element tag of your widget.
         */
		protected virtual string GetClosingHTML()
		{
			return string.Empty;
		}

		// The INVISIBLE HTML for the control
        /**
         * Unless your widget can get away with &lt;span style="display:none;"... as the in-visible
         * HTML of the control, then override this one to return the in-visible HTML of your control.
         * Scenarios where this might be reasonable is where your control is e.g. a &lt;li... type
         * of control and rendering an invisible span will break XHTML compliance.
         */
		public virtual string GetInvisibleHTML()
		{
			return string.Empty;
		}

        private string GetDefaultInvisibleHTML()
        {
            return string.Format("<span id=\"{0}\" style=\"display:none;\"></span>", ClientID);
        }
	}
}
