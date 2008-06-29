using System;
using System.Web.UI;

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

        // The next three methods are in order in regarding to the sequence
        // they are being dispatched in the Page life cycle
        // Since OnPreRender occurs before the Control State is being saved
        // We can in OnPreRender create the "state machine" which determines how
        // the control should be rendered...

        protected override void LoadControlState(object savedState)
        {
            if (savedState != null)
                _controlRenderingState = (RenderingPhase)savedState;

            // If this method is being called and we don't have the Invisible property 
            // we have previously saved the RenderingPhase meaning the control must 
            // logically be set to PropertyChanges...
            if (_controlRenderingState != RenderingPhase.Invisible)
                _controlRenderingState = RenderingPhase.PropertyChanges;

            // If the ControlState was stored previously with a Destroy value
            // then the control is logically now INvisible
            else if (_controlRenderingState == RenderingPhase.Destroy)
                _controlRenderingState = RenderingPhase.Invisible;
        }

        protected override void OnPreRender(EventArgs e)
        {
            // Since this method is called by the Page Life Cycle BEFORE SaveControlState
            // we can safely manipulate the RenderPhase property here.

            // If control is INvisible acording to RenderingPhase and still this method is 
            // called and control within this method is Visible then this is the 
            // first rendering of the control and we need to render the "replace of wrapper span" logic
            if (_controlRenderingState == RenderingPhase.Invisible && Visible)
            {
                if (AjaxManager.Instance.IsCallback)
                    _controlRenderingState = RenderingPhase.MadeVisibleThisRequest;
                else
                    _controlRenderingState = RenderingPhase.RenderHtml;
            }

            else if (_controlRenderingState == RenderingPhase.PropertyChanges && !Visible)
            {
                _controlRenderingState = RenderingPhase.Destroy;

                // We ALSO must loop through all CHILD controls and set their state to Invisible...!
                foreach (RaControl idx in AjaxManager.Instance.RaControls)
                {
                    // Checking to see if this is a CHILD control of the this widget...
                    if (idx.ClientID.IndexOf(this.ClientID) == 0)
                        idx.Phase = RenderingPhase.Invisible;
                }
            }

            base.OnPreRender(e);
        }

        protected override object SaveControlState()
        {
            if (_controlRenderingState != RenderingPhase.Invisible)
                return _controlRenderingState;
            return null;
        }

        public abstract void DispatchEvent(string name);

        public abstract string GetClientSideScript();
    }
}
