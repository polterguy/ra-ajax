/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */

using System;
using Ra.Widgets;
using System.Xml;
using System.Collections.Generic;
using System.IO;

namespace RaWebsite
{
    public class DocItem
    {
        private string _URL;
        private string _Text;

        public DocItem(string url, string text)
        {
            URL = url;
            Text = text;
        }

        public string URL { get { return _URL; } set { _URL = value; } }
        public string Text { get { return _Text; } set { _Text = value; } }
    }

    public partial class DocsSitemap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBindClasses();
            DataBindTutorials();
        }

        private void DataBindTutorials()
        {
            List<DocItem> l = new List<DocItem>();
            foreach (string idx in Directory.GetFiles(Server.MapPath("~/tutorials/"), "*.txt"))
            {
                string text = idx.Replace(".txt", "").Replace(Server.MapPath("~/tutorials/"), "");
                l.Add(new DocItem("Docs.aspx?tutorial=" + text.Replace("(", "").Replace(") - ", "-").Replace(" ", "_"), text));
            }
            repTutorials.DataSource = l;
            repTutorials.DataBind();
        }

        private void DataBindClasses()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/docs-xml/index.xml"));
            List<DocItem> l = new List<DocItem>();
            foreach (XmlNode idx in doc.SelectNodes("/doxygenindex/compound[@kind=\"class\"]"))
            {
                switch (idx.ChildNodes[0].InnerText.Replace("::", "."))
                {
                    // removing the classes we don't really NEED documentation for...
                    case "Ra.Extensions.Helpers.CometQueue":
                    case "Ra.Core.CallbackFilter":
                    case "Ra.Extensions.Widgets.AccordionView.EffectChange":
                    case "Ra.Extensions.Widgets.AutoCompleter.RetrieveAutoCompleterItemsEventArgs":
                    case "Ra.Core.PostbackFilter":
                    case "Ra.Widgets.ListItemCollection":
                    case "Ra.Widgets.StyleCollection":
                    case "Ra.Widgets.StyleCollection.StyleValue":
                    case "Ra.Widgets.ListItem":
                        break;
                    default:
                        {
                            string value = idx.ChildNodes[0].InnerText.Replace("::", ".");
                            l.Add(new DocItem("Docs.aspx?class=" + value, value));
                        } break;
                }
            }
            l.Sort(
                delegate(DocItem left, DocItem right)
                {
                    return left.Text.CompareTo(right.Text);
                });
            rep.DataSource = l;
            rep.DataBind();
        }
    }
}