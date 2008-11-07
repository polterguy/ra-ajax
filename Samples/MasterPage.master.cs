/*
 * Ra-Ajax, Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * Ra is licensed under an MITish license. The licenses should 
 * exist on disc where you extracted Ra and will be named license.txt
 * 
 */

using System;
using Ra.Widgets;
using Ra.Extensions;
using System.IO;
using System.Web;
using System.Drawing;

namespace Samples
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Defaulting "alphacube" skin to be visible...
            includeWhite.Visible = false;
            includeMac.Visible = false;
            includeSpread.Visible = false;
            if (!IsPostBack)
            {
                if (Session["wndWowPosition"] != null)
                {
                    Point pt = (Point)Session["wndWowPosition"];
                    wowWnd.Style["left"] = pt.X.ToString() + "px";
                    wowWnd.Style["top"] = pt.Y.ToString() + "px";
                    wowWnd.Style["position"] = "absolute";
                }
                string url = this.Request.Url.ToString();
                url = url.Substring(url.LastIndexOf("/") + 1);
                int idxNo = 0;
                foreach (AccordionView idx in accordion.Views)
                {
                    foreach (System.Web.UI.Control idxCtrl in idx.Controls)
                    {
                        if (idxCtrl is System.Web.UI.LiteralControl)
                        {
                            System.Web.UI.LiteralControl lit = idxCtrl as System.Web.UI.LiteralControl;
                            if (lit.Text.IndexOf(url) != -1)
                            {
                                accordion.ActiveAccordionViewIndex = idxNo;
                                break;
                            }
                        }
                    }
                    idxNo += 1;
                }
            }
        }

        protected void chooseSkin_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (chooseSkin.SelectedItem.Text)
            {
                case "Alphacube":
                    includeWhite.Visible = false;
                    includeAlphacube.Visible = true;
                    includeMac.Visible = false;
                    includeSpread.Visible = false;
                    break;
                case "White":
                    includeWhite.Visible = true;
                    includeAlphacube.Visible = false;
                    includeMac.Visible = false;
                    includeSpread.Visible = false;
                    break;
                case "Mac OS X":
                    includeWhite.Visible = false;
                    includeAlphacube.Visible = false;
                    includeMac.Visible = true;
                    includeSpread.Visible = false;
                    break;
                case "Spread":
                    includeWhite.Visible = false;
                    includeAlphacube.Visible = false;
                    includeMac.Visible = false;
                    includeSpread.Visible = true;
                    break;
            }
        }

        protected void wowWnd_Moved(object sender, EventArgs e)
        {
            Session["wndWowPosition"] = new Point(Int32.Parse(wowWnd.Style["left"].Replace("px", "")), Int32.Parse(wowWnd.Style["top"].Replace("px", "")));
        }

        protected void btnShowCode_Click(object sender, EventArgs e)
        {
            if (!tabShowCode.Visible)
            {
                // We only fetch code ONCE since second time and so on is just DHTML "candy"
                GetCSharpCode();
                GetASPXCode();
            }
            if (!tabShowCode.Visible || tabShowCode.Style["display"] == "none")
            {
                tabShowCode.Visible = true;
                Effect effect = new EffectFadeIn(tabShowCode, 400);
                effect.Joined.Add(new EffectRollDown());
                effect.Render();
            }
            else
            {
                Effect effect = new EffectFadeOut(tabShowCode, 400);
                effect.Joined.Add(new EffectRollUp());
                effect.Render();
            }
        }

        protected void closeIE_Click(object sender, EventArgs e)
        {
            Effect effect = new EffectFadeOut(pnlCrappyBrowser, 400);
            effect.Render();
        }

        private void GetASPXCode()
        {
            string path = this.Request.PhysicalPath;
            if (path.IndexOf(".forum") != -1)
            {
                path = this.Request.PhysicalApplicationPath + "Forums\\Post.aspx";
            }
            else if (path.IndexOf(".blogger") != -1)
            {
                path = this.Request.PhysicalApplicationPath + "Blog.aspx";
            }
            else if (path.IndexOf(".blog") != -1)
            {
                path = this.Request.PhysicalApplicationPath + "BlogItem.aspx";
            }
            using (TextReader reader = new StreamReader(File.OpenRead(path)))
            {
                string allCode = "\r\n" + reader.ReadToEnd();
                allCode = allCode.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\t", "    ");
                allCode = ReplaceCodeEntities(allCode, "keyword", new string[] { 
                "Page", 
                "Register", 
                "asp:Content"});
                lblCodeASPX.Text = allCode;
            }
        }

        private void GetCSharpCode()
        {
            string path = this.Request.PhysicalPath + ".cs";
            if (path.IndexOf(".forum") != -1)
            {
                path = this.Request.PhysicalApplicationPath + "Forums\\Post.aspx.cs";
            }
            else if (path.IndexOf(".blogger") != -1)
            {
                path = this.Request.PhysicalApplicationPath + "Blog.aspx.cs";
            }
            else if (path.IndexOf(".blog") != -1)
            {
                path = this.Request.PhysicalApplicationPath + "BlogItem.aspx.cs";
            }
            using (TextReader reader = new StreamReader(File.OpenRead(path)))
            {
                string allCode = "\r\n" + reader.ReadToEnd();
                allCode = allCode.Replace("<", "&lt;").Replace(">", "&gt;");
                allCode = ReplaceCodeEntities(allCode, "keyword", new string[] { 
                "class", 
                "using", 
                "string", 
                "foreach", 
                "if", 
                "else", 
                "int", 
                "public", 
                "partial", 
                "protected", 
                "private", 
                "false",
                "true",
                "null",
                "void", 
                "object"});
                allCode = allCode.Replace("/*", "<span class=\"comment\">/*");
                allCode = allCode.Replace("*/", "*/</span>");
                lblCodeCS.Text = allCode.Replace("\t", "    ");
            }
        }

        private string ReplaceCodeEntities(string allCode, string cssClass, string[] words)
        {
            string retVal = allCode;
            foreach (string idx in words)
            {
                retVal = retVal.Replace(idx, string.Format("<span class=\"{1}\">{0}</span>", idx, cssClass));
            }
            return retVal;
        }
    }
}
