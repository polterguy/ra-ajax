/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using Ra.Widgets;
using ASP = System.Web.UI;
using System.ComponentModel;
using Ra.Effects;

namespace Ra.Extensions.Widgets
{
    /**
     * An accordion is a list of panels where only one can be visible at any time. When header of any
     * one is clicked that panel will be visibe and the previously visible one will be in-visibe except
     * for its header. Kind of like the left navigational parts of Microsoft Outlook. Every panel within
     * the Accordion must be AccordionView instances.
     */
    [ASP.ToolboxData("<{0}:Accordion runat=server></{0}:Accordion>")]
    public class Accordion : Panel
    {
        /**
         * Raised when the active AccordionView is changed. When the user clicks an inactive 
         * AccordionView, then this event will be raised and the currently active AccordionView 
         * will be accessible through e.g. the ActiveAccordionViewIndex property of the Accordion.
         */
        public event EventHandler ActiveAccordionViewChanged;

        /**
         * Index of currently viewing panel. Notice that the first AccordionView has an index of 0 and the
         * second has an index of 1, etc.
         */
        [DefaultValue(0)]
        public int ActiveAccordionViewIndex
        {
            get { return ViewState["ActiveAccordionViewIndex"] == null ? 0 : (int)ViewState["ActiveAccordionViewIndex"]; }
            set
            {
                ViewState["ActiveAccordionViewIndex"] = value;
            }
        }

        /**
         * Overridden to provide a sane default value. The default value of this property is "accordion".
         */
        [DefaultValue("ra-accordion")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "ra-accordion";
                return retVal;
            } 
            set { base.CssClass = value; }
        }

        /**
         * Overridden to provide a sane default value. The default value of this property is "ul".
         */
        [DefaultValue("ul")]
        public override string Tag
        {
            get { return ViewState["Tag"] == null ? "ul" : (string)ViewState["Tag"]; }
            set { ViewState["Tag"] = value; }
        }

        internal int GetIndex(AccordionView accView)
        {
            int idxNo = 0;
            foreach(ASP.Control idx in Controls)
            {
                if(idx is AccordionView)
                {
                    if (idx.ID == accView.ID)
                        return idxNo;
                    idxNo += 1;
                }
            }
            throw new IndexOutOfRangeException("AccordionView with that index doesn't exists");
        }

        private AccordionView GetView(int accordionIndex)
        {
            int idxNo = 0;
            foreach(ASP.Control idx in Controls)
            {
                if(idx is AccordionView)
                {
                    if (idxNo == accordionIndex)
                        return idx as AccordionView;
                    idxNo += 1;
                }
            }
            throw new IndexOutOfRangeException("No AccordionView with that high index number exists in the collection");
        }

        internal void SetActiveView(AccordionView accordionView)
        {
            int newIndex = GetIndex(accordionView);
            if (newIndex == ActiveAccordionViewIndex)
                return;
            AccordionView previouslySelectedAccView = GetView(ActiveAccordionViewIndex);
            ActiveAccordionViewIndex = newIndex;
            if(ActiveAccordionViewChanged != null)
            {
                ActiveAccordionViewChanged(this, new EventArgs());
            }
            // Here we create animations to roll up the previously selected on
            // and roll down the currently selected one
            new EffectRollUp(previouslySelectedAccView.ContentControl, 500)
                .ChainThese(new EffectRollDown(accordionView.ContentControl, 500))
                .Render();
            accordionView.CssClass = "ra-acc-view";
            previouslySelectedAccView.CssClass = "ra-acc-view ra-acc-view-collapsed";
        }
    }
}
