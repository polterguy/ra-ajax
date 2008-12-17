/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
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

namespace Ra.Extensions
{
    /**
     * Panels for the Accordion control
     */
    [ASP.ToolboxData("<{0}:AccordionView runat=server></{0}:AccordionView>")]
    public class AccordionView : Panel
    {
        /**
         * header string for accordionview
         */
        [DefaultValue("")]
        public string Caption
        {
            get { return ViewState["Caption"] == null ? "" : (string)ViewState["Caption"]; }
            set
            {
                ViewState["Caption"] = value;
            }
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            // Since we're dependant upon that the ViewState has finished loading before
            // we initialize the ChildControls since how the child controls (and which)
            // child controls are being re-created is dependant upon a ViewState saved value
            // this is the earliest possible time we can reload the ChildControls for the
            // Control
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            CreateCompositionControls();
        }

        private void CreateCompositionControls()
        {
            // Creating top wrapper
            Panel topWrapper = new Panel();
            topWrapper.ID = "top";
            topWrapper.CssClass = "accordion-header";

            // Creating button inside of top wrapper
            LinkButton btn = new LinkButton();
            btn.ID = "change_active";
            btn.Text = Caption;
            btn.Click += new EventHandler(btn_Click);
            topWrapper.Controls.Add(btn);

            Controls.AddAt(0, topWrapper);
        }

        private class EffectChange : Effect
        {
            private string _idRemove;
            private string _idShow;
            private Accordion _parent;

            public EffectChange(string idRemove, string idShow, Accordion parent)
			: base(null, 0)
            {
                _idRemove = idRemove;
                _idShow = idShow;
                _parent = parent;
            }

            public override void Render()
            {
                AjaxManager.Instance.WriterAtBack.WriteLine(@"
Ra.E('{0}', {{
  onStart: function() {{{2}}},
  onFinished: function() {{{3}}},
  onRender: function(pos) {{{4}}},
  duration:{1},
  transition:'{5}'
}});", 
                    _idRemove, 
                    _parent.AnimationDuration.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    RenderParalledOnStart(),
                    RenderParalledOnFinished(),
                    RenderParalledOnRender(),
                    Effect.Transition.Explosive);
            }

            public override string RenderParalledOnStart()
            {
                return string.Format(@"
    this.other = Ra.$('{1}');
    this.otherToHeight = this.other.getDimensions().height;
    this.elementFromHeight = this.element.getDimensions().height;
    this.other.setStyle('height','0px');
    this.other.setStyle('display','');
",
                    _idRemove, _idShow);
            }

            public override string RenderParalledOnFinished()
            {
                return @"
    this.element.setStyle('display','none');
    this.element.setStyle('height','');
    this.other.setStyle('height',this.otherToHeight + 'px');
";
            }

            public override string RenderParalledOnRender()
            {
                return @"
    this.other.setStyle('height',(this.otherToHeight * pos) + 'px');
    this.element.setStyle('height',(this.elementFromHeight * (1.0 - pos)) + 'px');
";
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            SetActive();
        }

        public void SetActive()
        {
            if (IsActive())
                return;
            string oldActiveClientId = "";
            int idxNoThis = 0;
            bool foundThis = false;
            foreach (AccordionView idx in (Parent as Accordion).Views)
            {
                if (idx.IsActive())
                {
                    oldActiveClientId = idx.ClientID + "_CHILDREN";
                }
                if (!foundThis)
                {
                    if (this == idx)
                        foundThis = true;
                    else
                        idxNoThis += 1;
                }
            }
            Effect effect = new EffectChange(oldActiveClientId, ClientID + "_CHILDREN", Parent as Accordion);
            effect.Render();
            (Parent as Accordion).ActiveAccordionViewIndex = idxNoThis;
            (Parent as Accordion).RaiseActiveChanged();
        }

        protected override void RenderChildren(System.Web.UI.HtmlTextWriter writer)
        {
            Controls[0].RenderControl(writer);
            writer.Write("<div class=\"body\" id=\"{0}_CHILDREN\"{1}>", ClientID, IsActive() ? "" : " style=\"display:none;\"");
            writer.Write("<div class=\"body-content\">");
            for (int idx = 1; idx < Controls.Count; idx++)
            {
                Controls[idx].RenderControl(writer);
            }
            writer.Write("</div>");
            writer.Write("</div>");
        }

        /**
         * Returns true if this is the activce accordionview in the parent Accordion
         */
        public bool IsActive()
        {
            bool isActive = false;
            Accordion acc = Parent as Accordion;
            int idxNo = 0;
            foreach (AccordionView idx in acc.Views)
            {
                if (idx == this)
                    break;
                idxNo += 1;
            }
            if (idxNo == acc.ActiveAccordionViewIndex)
                isActive = true;
            return isActive;
        }
    }
}
