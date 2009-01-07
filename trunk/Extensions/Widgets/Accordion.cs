/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
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
         * Overridden to provide a sane default value
         */
        [DefaultValue("accordion")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "accordion";
                return retVal;
            }
            set { base.CssClass = value; }
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
         * If true then the changing of ActiveTab will happen purely on the client and
         * not create an Ajax Request at all. Warning! If you chose to set this property
         * to true, then the accordion will not raise the ActiveAccordionViewChanged event
         * when active accordion is raised!
         * Also this value must be set when accordion is created or shown for the first time
         * and cannot be changed after accordion is created.
         */
        [DefaultValue(false)]
        public bool ClientSideChange
        {
            get
            {
                if (ViewState["ClientSideChange"] == null)
                    return false;
                return (bool)ViewState["ClientSideChange"];
            }
            set
            {
                ViewState["ClientSideChange"] = value;
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
