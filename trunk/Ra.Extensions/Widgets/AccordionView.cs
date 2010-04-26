/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using System.ComponentModel;
using System.Web.UI;
using Ra.Builder;
using Ra.Widgets;
using ASP = System.Web.UI;

namespace Ra.Extensions.Widgets
{
    /**
     * Panels for the Accordion control. An Accordion is basically just a collection of 
     * AccordionView controls.
     */

    [ToolboxData("<{0}:AccordionView runat=server></{0}:AccordionView>")]
    [PersistChildren(true)]
    [ParseChildren(true, "Content")]
    public class AccordionView : Panel, INamingContainer
    {
        private readonly Panel _content = new Panel();
        private readonly LinkButton _caption = new LinkButton();

        /**
         * Header string for AccordionView. This is the text that will be displayed at the top "select area"
         * of the AccordionView.
         */
        [DefaultValue("")]
        public string Caption
        {
            get { return _caption.Text; }
            set { _caption.Text = value; }
        }

        /**
         * Overridden to provide a sane default value. The default value of this property is "ra-acc-view".
         */
        [DefaultValue("ra-acc-view")]
        public override string CssClass
        {
            get
            {
                string retVal = base.CssClass;
                if (retVal == string.Empty)
                    retVal = "ra-acc-view";
                return retVal;
            }
            set { base.CssClass = value; }
        }

        /**
         * Overridden to provide a sane default value. The default value of this property is "li".
         */
        [DefaultValue("li")]
        public override string Tag
        {
            get { return ViewState["Tag"] == null ? "li" : (string)ViewState["Tag"]; }
            set { ViewState["Tag"] = value; }
        }


        /**
         * The actual "content part" of the Window. This is where your child controls from the markup
         * will be added. If you add up items dynamically to the Window then this is where you should
         * add them up. So instead of using "myWindow.Controls.Add" you should normally use 
         * "myWindow.Content.Add" when adding Controls dynamically to the Window.
         */
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public ControlCollection Content
        {
            get { return _content.Controls; }
        }

        /**
         * The Control that contains the Child Controls of the AccordionView
         */
        public ASP.Control ContentControl
        {
            get { return _content; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            CreateContentControls();
        }

        private void CreateContentControls()
        {
            _content.ID = "cont";
            _content.CssClass = "ra-acc-view-content";
            SetVisibility();
            Controls.Add(_content);

            _caption.ID = "capt";
            _caption.CssClass = "ra-acc-view-caption";
            _caption.Click +=
                delegate
                    {
                        (Parent as Accordion).SetActiveView(this);
                    };
            Controls.Add(_caption);
        }

        private void SetVisibility()
        {
            int idxCurrentVisibleView = (Parent as Accordion).ActiveAccordionViewIndex;
            int idxOfThisView = (Parent as Accordion).GetIndex(this);
            if(idxCurrentVisibleView == idxOfThisView)
            {
                _content.Style[Styles.display] = "";
            }
            else
            {
                _content.Style[Styles.display] = "none";
                if (!CssClass.Contains("ra-acc-view-collapsed"))
                    CssClass += " ra-acc-view-collapsed";
            }
        }

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element el = builder.CreateElement(Tag))
            {
                AddAttributes(el);
                using (Element nw = builder.CreateElement("div"))
                {
                    nw.AddAttribute("class", "ra-acc-view-nw");
                    using (Element ne = builder.CreateElement("div"))
                    {
                        ne.AddAttribute("class", "ra-acc-view-ne");
                        using (Element n = builder.CreateElement("div"))
                        {
                            n.AddAttribute("class", "ra-acc-view-n");
                            using (Element icon = builder.CreateElement("span"))
                            {
                                icon.AddAttribute("class", "ra-acc-view-icon");
                            }
                            _caption.RenderControl(builder.Writer);
                            using (Element icon = builder.CreateElement("span"))
                            {
                                icon.AddAttribute("class", "ra-acc-view-collapse-icon");
                            }
                        }
                    }
                }
                using (Element w = builder.CreateElement("div"))
                {
                    w.AddAttribute("class", "ra-acc-view-w");
                    using (Element e = builder.CreateElement("div"))
                    {
                        e.AddAttribute("class", "ra-acc-view-e");
                        _content.RenderControl(builder.Writer);
                    }
                }
                using (Element nw = builder.CreateElement("div"))
                {
                    nw.AddAttribute("class", "ra-acc-view-sw");
                    using (Element ne = builder.CreateElement("div"))
                    {
                        ne.AddAttribute("class", "ra-acc-view-se");
                        using (Element n = builder.CreateElement("div"))
                        {
                            n.AddAttribute("class", "ra-acc-view-s");
                        }
                    }
                }
            }
        }
    }
}
