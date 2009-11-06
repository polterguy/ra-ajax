/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using ASP = System.Web.UI;
using Ra.Widgets;
using HTML = System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web;
using Ra.Builder;
using Ra;

[assembly: ASP.WebResource("Ra.Extensions.Js.RichEdit.js", "text/javascript")]

namespace Ra.Extensions.Widgets
{
    /**
     * RichEdit control with HTML formatting capabilities
     */
    [ASP.ToolboxData("<{0}:RichEdit runat=server></{0}:RichEdit>")]
    public class RichEdit : RaWebControl, IRaControl
    {
        public class ExtraToolbarControlsEventArgs : EventArgs
        {
            internal ExtraToolbarControlsEventArgs(ControlCollection col)
            {
                Controls = col;
            }

            public ControlCollection Controls { get; internal set; }
        }

        private Panel _extraToolbarControls = new Panel();

        public event EventHandler GetImageDialog;
        public event EventHandler GetHyperLinkDialog;
        public event EventHandler<ExtraToolbarControlsEventArgs> GetExtraToolbarControls;

        /**
         * Text (HTML) of control
         */
        [DefaultValue("")]
        public string Text
        {
            get { return ViewState["Text"] == null ? "" : HttpUtility.UrlDecode((string)ViewState["Text"]); }
            set
            {
                if (value != Text)
                    SetJSONValueString("Text", value);
                ViewState["Text"] = value;
            }
        }

        /**
         * Selected text/HTML of control. Contains formatting
         */
        [Browsable(false)]
        public string Selection
        {
            get { return Page.Request.Params[ClientID + "__SELTEXT"]; }
        }

        public void PasteHTML(string html)
        {
            SetJSONValueString("Paste", html);
        }

        protected override void OnInit(EventArgs e)
        {
            if (!IsViewStateEnabled && ((Page)HttpContext.Current.CurrentHandler).IsPostBack && Visible)
            {
                // Making sure we get our NEW value loaded...
                string value = Page.Request.Params[ClientID + "__VALUE"];
                ViewState["Text"] = value;
            }
            base.OnInit(e);
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            _extraToolbarControls.ID = "plugins";
            if (GetExtraToolbarControls != null)
            {
                ExtraToolbarControlsEventArgs e = new ExtraToolbarControlsEventArgs(_extraToolbarControls.Controls);
                GetExtraToolbarControls(this, e);
            }
            Controls.Add(_extraToolbarControls);
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            if (((Page)HttpContext.Current.CurrentHandler).IsPostBack && Visible)
            {
                // Making sure we get our NEW value loaded...
                string value = Page.Request.Params[ClientID + "__VALUE"];
                ViewState["Text"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            AjaxManager.Instance.IncludeScriptFromResource(typeof(RichEdit), "Ra.Extensions.Js.RichEdit.js");
            base.OnPreRender(e);
        }

        void IRaControl.DispatchEvent(string name)
        {
            switch (name)
            {
                case "getImage":
                    {
                        if (GetImageDialog != null)
                        {
                            GetImageDialog(this, new EventArgs());
                        }
                    } break;
                case "getHyperLink":
                    {
                        if (GetHyperLinkDialog != null)
                        {
                            GetHyperLinkDialog(this, new EventArgs());
                        }
                    } break;
                default:
                    throw new ArgumentException("Unknown event sent to RichEdit");
            }
        }

        protected override string GetClientSideScriptType()
        {
            return "new Ra.RichEdit";
        }

        protected override void RenderRaControl(HtmlBuilder builder)
        {
            using (Element el = builder.CreateElement("div"))
            {
                AddAttributes(el);
                CreateToolbar(builder);

                // Creating editor area...
                using (Element txt = builder.CreateElement("div"))
                {
                    txt.AddAttribute("id", ClientID + "_LBL");
                    txt.AddAttribute("class", "editorMain");
                    txt.Write(Text);
                }

                // Creating the invisible TextArea - which holds the data for posting back
                using (Element value = builder.CreateElement("textarea"))
                {
                    value.AddAttribute("id", ClientID + "__VALUE");
                    value.AddAttribute("name", ClientID + "__VALUE");
                    value.AddAttribute("class", "editorMain");
                    value.AddAttribute("style", "display:none;");
                    value.Write(Text);
                }

                using (Element value = builder.CreateElement("input"))
                {
                    value.AddAttribute("id", ClientID + "__SELTEXT");
                    value.AddAttribute("name", ClientID + "__SELTEXT");
                    value.AddAttribute("type", "hidden");
                }

                // Creating the bottom bar - which holds the switch between HTML/DESIGN buttons
                using (Element bottomBar = builder.CreateElement("div"))
                {
                    bottomBar.AddAttribute("class", "bottomBar");
                    using (Element design = builder.CreateElement("button"))
                    {
                        design.AddAttribute("class", "designButton activeTypeButton");
                        design.AddAttribute("id", ClientID + "design");
                        design.AddAttribute("onclick", "return false;");
                        design.Write("Design");
                    }
                    using (Element html = builder.CreateElement("button"))
                    {
                        html.AddAttribute("class", "htmlButton");
                        html.AddAttribute("id", ClientID + "html");
                        html.AddAttribute("onclick", "return false;");
                        html.Write("HTML");
                    }
                }
            }
        }

        private void CreateToolbar(HtmlBuilder builder)
        {
            using (Element toolbar = builder.CreateElement("div"))
            {
                toolbar.AddAttribute("class", "toolbar");
                toolbar.AddAttribute("id", ClientID + "toolbar");

                // Text style strip...
                CreateStrip(builder,
                    delegate
                    {
                        using (Element textType = builder.CreateElement("select"))
                        {
                            textType.AddAttribute("class", "editorSelect");
                            textType.AddAttribute("id", ClientID + "selectTextType");
                            CreateOption(builder, "Normal", "");
                            CreateOption(builder, "Paragraph", "p");
                            CreateOption(builder, "Heading 1", "h1");
                            CreateOption(builder, "Heading 2", "h2");
                            CreateOption(builder, "Heading 3", "h3");
                            CreateOption(builder, "Address", "address");
                            CreateOption(builder, "Formatted", "pre");
                        }
                        using (Element textType = builder.CreateElement("select"))
                        {
                            textType.AddAttribute("class", "editorSelect");
                            textType.AddAttribute("id", ClientID + "selectFontName");
                            CreateOption(builder, "Font", "");
                            CreateOption(builder, "Arial", "Arial");
                            CreateOption(builder, "Courier New", "Courier New");
                            CreateOption(builder, "Garamond", "Garamond");
                            CreateOption(builder, "Sans Serif", "Sans Serif");
                            CreateOption(builder, "Tahoma", "Tahoma");
                            CreateOption(builder, "Times New Roman", "Times New Roman");
                            CreateOption(builder, "Verdana", "Verdana");
                        }
                        using (Element textType = builder.CreateElement("select"))
                        {
                            textType.AddAttribute("class", "editorSelect");
                            textType.AddAttribute("id", ClientID + "selectFontSize");
                            CreateOption(builder, "Size", "");
                            CreateOption(builder, "Very Small", "1");
                            CreateOption(builder, "Smaller", "2");
                            CreateOption(builder, "Small", "3");
                            CreateOption(builder, "Medium", "4");
                            CreateOption(builder, "Large", "5");
                            CreateOption(builder, "Larger", "6");
                            CreateOption(builder, "Very Large", "7");
                        }
                    });

                if (GetExtraToolbarControls != null)
                {
                    CreateStrip(builder,
                        delegate
                        {
                            _extraToolbarControls.RenderControl(builder.Writer);
                        });
                }

                // Making sure we've got a carriage return
                using (builder.CreateElement("br"))
                {
                }

                // Font-Style strip
                CreateStrip(builder,
                    delegate
                    {
                        CreateButton(builder, "bold");
                        CreateButton(builder, "italic");
                        CreateButton(builder, "underline");
                    });

                // Justify strip
                CreateStrip(builder,
                    delegate
                    {
                        CreateButton(builder, "justifyleft");
                        CreateButton(builder, "justifycenter");
                        CreateButton(builder, "justifyright");
                        CreateButton(builder, "justifyfull");
                    });

                // Insert strip
                CreateStrip(builder,
                    delegate
                    {
                        CreateButton(builder, "insertunorderedlist");
                        CreateButton(builder, "insertorderedlist");
                    });

                // Indent strip
                CreateStrip(builder,
                    delegate
                    {
                        CreateButton(builder, "indent");
                        CreateButton(builder, "outdent");
                    });

                if (GetImageDialog != null || GetHyperLinkDialog != null)
                {
                    // Image strip
                    CreateStrip(builder,
                        delegate
                        {
                            if (GetImageDialog != null)
                            {
                                CreateButton(builder, "image");
                            }
                            if (GetHyperLinkDialog != null)
                            {
                                CreateButton(builder, "hyperlink");
                            }
                        });
                }
            }
        }

        private static void CreateOption(HtmlBuilder builder, string name, string element)
        {
            using (Element el = builder.CreateElement("option"))
            {
                el.AddAttribute("value", element);
                el.Write(name);
            }
        }

        private void CreateButton(HtmlBuilder builder, string className)
        {
            using (Element bold = builder.CreateElement("button"))
            {
                bold.AddAttribute("class", "editorBtn " + className);
                bold.AddAttribute("id", ClientID + className);
                bold.AddAttribute("onclick", "return false;");
            }
        }

        private delegate void FunctorMethod(Element element);

        private static void CreateStrip(HtmlBuilder builder, FunctorMethod functor)
        {
            using (Element fontStyle = builder.CreateElement("span"))
            {
                fontStyle.AddAttribute("class", "strip");
                using (Element right = builder.CreateElement("span"))
                {
                    right.AddAttribute("class", "right");
                    using (Element left = builder.CreateElement("span"))
                    {
                        left.AddAttribute("class", "left");
                        using (Element center = builder.CreateElement("span"))
                        {
                            center.AddAttribute("class", "center");
                            using (Element content = builder.CreateElement("span"))
                            {
                                content.AddAttribute("class", "stripContent");
                                functor(content);
                            }
                        }
                    }
                }
            }
        }
    }
}
