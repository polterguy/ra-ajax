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
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;
using System.Collections.Generic;

/**
 * Extension widgets for Ra-Ajax
 */
namespace Ra.Extensions
{
    /**
     * An accordion is a list of panels where only one can be visible at any time. When header of any
     * one is clicked that panel will be visibe and the previously visible one will be in-vsibe except
     * for its header
     */
    [ASP.ToolboxData("<{0}:Accordion runat=server></{0}:Accordion>")]
    public class Accordion : Panel, ASP.INamingContainer
    {
        /**
         * Raised when active accordion is changed
         */
        public event EventHandler ActiveAccordionViewChanged;

        /**
         * Index of currently viewing panel
         */
        [DefaultValue(0)]
        public int ActiveAccordionViewIndex
        {
            get { return ViewState["SelectedViewIndex"] == null ? 0 : (int)ViewState["SelectedViewIndex"]; }
            set
            {
                ViewState["SelectedViewIndex"] = value;
            }
        }

        /**
         * When changing active panel there will be a drop-down/up effect. This property defines the
         * number of milliseconds the animation will spend.
         */
        [DefaultValue(400)]
        public int AnimationDuration
        {
            get
            {
                if (ViewState["AnimationDuration"] == null)
                    return 400;
                return (int)ViewState["AnimationDuration"];
            }
            set
            {
                ViewState["AnimationDuration"] = value;
            }
        }

        /**
         * All AccordionViews within the accordion
         */
        [Browsable(false)]
        public IEnumerable<AccordionView> Views
        {
            get
            {
                foreach (ASP.Control idx in Controls)
                {
                    if (idx is AccordionView)
                        yield return idx as AccordionView;
                }
            }
        }

        internal void RaiseActiveChanged()
        {
            if (ActiveAccordionViewChanged != null)
                ActiveAccordionViewChanged(this, new EventArgs());
        }
    }
}