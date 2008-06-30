/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc in addition to that 
 * the code also is licensed under a pure GPL license for those that
 * cannot for some reasons obey by rules in the MIT(ish) kind of license.
 * 
 */

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Text;

namespace Ra.Widgets
{
    public abstract class RaControl : Control, IRaControl
    {
        public enum RenderingPhase
        {
            Invisible,
            MadeVisibleThisRequest,
            RenderHtml,
            Destroy,
            PropertyChanges
        };

        // Used to track if control has been rendered previously
        // Ra has five rendering states;
        // * Invisible controls (not rendered)
        // * Controls being made visible this request (render HTML and run replace on wrapper span)
        // * Controls visible initially or parent container control set to visible this rendering (pure HTML rendering)
        // * Controls being set to invisible (render destroy)
        // * Controls already visible but have property changes (JSON serialization of properties and method results)
        private RenderingPhase _controlRenderingState = RenderingPhase.Invisible;

        internal RenderingPhase Phase
        {
            get { return _controlRenderingState; }
            set { _controlRenderingState = value; }
        }

        private Dictionary<string, object> _JSONValues = new Dictionary<string, object>();

        public Dictionary<string, string> GetJSONValueDictionary(string key)
        {
            if (!_JSONValues.ContainsKey(key))
            {
                _JSONValues[key] = new Dictionary<string, string>();
            }
            return (Dictionary<string, string>)_JSONValues[key];
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
                    value.ToString().Replace("\\", "\\\\").Replace("\"", "\\\""));
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
            if (savedState != null)
                Phase = (RenderingPhase)savedState;

            // If the ControlState was stored previously with a Destroy value
            // then the control is logically now Invisible
            if (Phase == RenderingPhase.Destroy)
                Phase = RenderingPhase.Invisible;

            // If the Phase was previously saved with any other value than Invisible/Destroy
            // then the control must logically be in PropertyChanges mode...
            else if (Phase != RenderingPhase.Invisible)
                Phase = RenderingPhase.PropertyChanges;
        }

        // SaveControlState will be called BEFORE the RenderControl is being called by the ASP.NET
        // runtime. This means that in the SaveControlState we can set up the "stat machine" which
        // determines which state the control is in in regards to rendering.

        protected override object SaveControlState()
        {
            // If control is Invisible acording to RenderingPhase and still control is
            // logicall Visible then this is the first rendering of the control
            if (Phase == RenderingPhase.Invisible && Visible)
            {
                // If it's an Ajax Callback e need to run the "replace logic"
                // If it's NOT an Ajax Callback then we just render the HTML plain...
                if (AjaxManager.Instance.IsCallback)
                    Phase = RenderingPhase.MadeVisibleThisRequest;
                else
                    Phase = RenderingPhase.RenderHtml;
            }

            // If control state is not Invisible but still Control is logically not Visible
            // we need to run the "destroy logic" by setting its rendering phase to Destroy
            else if (Phase != RenderingPhase.Invisible && !Visible)
            {
                // Setting rendering phase to destroy
                Phase = RenderingPhase.Destroy;

                // We ALSO must loop through all CHILD controls and set their state to Invisible...!
                // We don't HAVE to explicitly destroy the child controls from the server since
                // our JavaScript API will make sure of that for us...
                foreach (RaControl idx in AjaxManager.Instance.RaControls)
                {
                    if (IsChildControl(idx))
                        idx.Phase = RenderingPhase.Invisible;
                }
            }

            // Returning Phase to the ControlState serialization logic of the runtime
            return Phase;
        }

        private bool IsChildControl(RaControl idx)
        {
            return idx.ClientID != this.ClientID && idx.ClientID.IndexOf(this.ClientID) == 0;
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            switch (Phase)
            {
                case RenderingPhase.Destroy:
                    // TODO: Destroy control
                    // Handled in AjaxManager.RenderCallback
                    break;
                case RenderingPhase.Invisible:
                    // We must render the Wrapper Span, but ONLY if this is NOT an Ajax Callback...
                    if (!AjaxManager.Instance.IsCallback)
                        writer.Write(GetInvisibleHTML());
                    break;
                case RenderingPhase.MadeVisibleThisRequest:
                    // Replace wrapper span for control
                    // Handled in AjaxManager.RenderCallback
                    break;
                case RenderingPhase.PropertyChanges:
                    // Serialize JSON changes to control
                    // Handled in AjaxManager.RenderCallback
                    break;
                case RenderingPhase.RenderHtml:
                    // We must render the HTML for the Control, but ONLY if this is NOT an Ajax Callback
                    if (!AjaxManager.Instance.IsCallback)
                        writer.Write(GetHTML());
                    break;
            }
        }

        // Used for dispatching events for the Control
        public abstract void DispatchEvent(string name);

        // Used to retrieve the client-side initialization script
        public abstract string GetClientSideScript();

        // The HTML for the control
        public abstract string GetHTML();

        // The INVISIBLE HTML for the control
        public virtual string GetInvisibleHTML()
        {
            return string.Format("<span id=\"{0}\" style=\"display:none;\">&nbsp;</span>", ClientID);
        }
    }
}
