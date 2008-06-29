/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc in addition to that 
 * the code also is licensed under a pure GPL license for those that
 * cannot for some reasons obey by rules in the MIT(ish) kind of license.
 * 
 */

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

        // The next three methods are in order in regarding to the sequence
        // they are being dispatched in the Page life cycle
        // Since OnPreRender occurs before the Control State is being saved
        // We can in OnPreRender create the "state machine" which determines how
        // the control should be rendered...

        protected override void LoadControlState(object savedState)
        {
            if (savedState != null)
                Phase = (RenderingPhase)savedState;

            // If the ControlState was stored previously with a Destroy value
            // then the control is logically now Invisible
            if (Phase == RenderingPhase.Destroy)
                Phase = RenderingPhase.Invisible;

            // If this method is being called and we don't have the Invisible property 
            // we have previously saved the RenderingPhase meaning the control must 
            // logically be set to PropertyChanges...
            else if (Phase != RenderingPhase.Invisible)
                Phase = RenderingPhase.PropertyChanges;
        }

        protected override object SaveControlState()
        {
            // If control is Invisible acording to RenderingPhase and still this method is 
            // called and control within this method is Visible then this is the 
            // first rendering of the control and we need to render the "replace of wrapper span" logic
            if (Phase == RenderingPhase.Invisible && Visible)
            {
                if (AjaxManager.Instance.IsCallback)
                    Phase = RenderingPhase.MadeVisibleThisRequest;
                else
                    Phase = RenderingPhase.RenderHtml;
            }

            else if (Phase == RenderingPhase.PropertyChanges && !Visible)
            {
                Phase = RenderingPhase.Destroy;

                // We ALSO must loop through all CHILD controls and set their state to Invisible...!
                foreach (RaControl idx in AjaxManager.Instance.RaControls)
                {
                    // Checking to see if this is a CHILD control of the this widget...
                    if (idx.ClientID != this.ClientID && idx.ClientID.IndexOf(this.ClientID) == 0)
                        idx.Phase = RenderingPhase.Invisible;
                }
            }

            return Phase;
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            switch (Phase)
            {
                case RenderingPhase.Destroy:
                    // TODO: Destroy control
                    break;
                case RenderingPhase.Invisible:
                    // Do NOTHING
                    break;
                case RenderingPhase.MadeVisibleThisRequest:
                    // Replace wrapper span for control
                    break;
                case RenderingPhase.PropertyChanges:
                    // Serialize JSON changes to control
                    break;
                case RenderingPhase.RenderHtml:
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
    }
}
