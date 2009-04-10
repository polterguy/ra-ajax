/*
 * Ra-Ajax, Copyright 2008 - 2009 - Thomas Hansen polterguy@gmail.com
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
using System.Web.UI;
using Ra;

namespace Samples
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
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
                new EffectRollUp(cntWrp, 500)
                    .JoinThese(new EffectFadeOut())
                    .ChainThese(
                        new EffectFadeIn(tabShowCode, 500)
                            .JoinThese(new EffectRollDown()))
                    .Render();
            }
            else
            {
                new EffectRollUp(tabShowCode, 500)
                    .JoinThese(new EffectFadeOut())
                    .ChainThese(
                        new EffectFadeIn(cntWrp, 500)
                            .JoinThese(new EffectRollDown()))
                    .Render();
            }
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
