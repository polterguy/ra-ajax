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
using Ra.Helpers;
using Ra.Widgets;
using System.IO;
using HTML = System.Web.UI.HtmlControls;

namespace Ra.Extensions
{
    [ASP.ToolboxData("<{0}:AccordionView runat=server></{0}:AccordionView>")]
    public class AccordionView : Panel
    {
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
			: base(null, 0.0M)
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
  sinoidal:true
}});", 
                    _idRemove, 
                    _parent.AnimationSpeed.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    RenderChainedOnStart(),
                    RenderChainedOnFinished(),
                    RenderChainedOnRender());
            }

            public override string RenderChainedOnStart()
            {
                return string.Format(@"
    this.other = Ra.$('{1}');
    this.otherToHeight = this.other.getDimensions().height;
    this.elementFromHeight = this.element.getDimensions().height;
    this.other.style.height = '0px';
    this.other.style.display = '';
",
                    _idRemove, _idShow);
            }

            public override string RenderChainedOnFinished()
            {
                return @"
    this.element.style.display = 'none';
    this.element.style.height = '';
    this.other.style.height = this.otherToHeight + 'px';
";
            }

            public override string RenderChainedOnRender()
            {
                return @"
    this.other.style.height = (this.otherToHeight * pos) + 'px';
    this.element.style.height = (this.elementFromHeight * (1.0 - pos)) + 'px';
";
            }
        }

        void btn_Click(object sender, EventArgs e)
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
