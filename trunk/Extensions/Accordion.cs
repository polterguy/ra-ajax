/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Helpers;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:Accordion runat=server></{0}:Accordion>")]
    public class Accordion : Panel, ASP.INamingContainer
    {
        public event EventHandler ActiveAccordionViewChanged;

        [DefaultValue(0)]
        public int ActiveAccordionViewIndex
        {
            get { return ViewState["SelectedViewIndex"] == null ? 0 : (int)ViewState["SelectedViewIndex"]; }
            set
            {
                ViewState["SelectedViewIndex"] = value;
            }
        }

        [DefaultValue(0.4)]
        public decimal AnimationSpeed
        {
            get
            {
                if (ViewState["AnimationSpeed"] == null)
                    return 0.4M;
                return (decimal)ViewState["AnimationSpeed"];
            }
            set
            {
                ViewState["AnimationSpeed"] = value;
            }
        }

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
