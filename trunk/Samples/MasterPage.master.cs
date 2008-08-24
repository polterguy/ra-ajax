/*
 * Ra Ajax, Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * Ra is licensed under an MITish license. The licenses should 
 * exist on disc where you extracted Ra and will be named license.txt
 * 
 */

using System;
using Ra.Widgets;
using System.IO;
using System.Web;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void btnShowCode_Click(object sender, EventArgs e)
    {
        pnlShowCode.Visible = true;
        Effect effect = new EffectRollDown(pnlShowCode, 1.0M, 600);
        effect.Render();
        GetCSharpCode();
        ViewState["code"] = "C#";
    }

    protected void closeIE_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectFadeOut(pnlCrappyBrowser, 0.4M);
        effect.Render();
    }

    protected void switchCodeType_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectFadeIn(lblCode, 0.5M);
        effect.Render();
        if (ViewState["code"].ToString() == "C#")
        {
            ViewState["code"] = ".ASPX";
            GetASPXCode();
            switchCodeType.Text = "Switch to C# code";
        }
        else
        {
            ViewState["code"] = "C#";
            GetCSharpCode();
            switchCodeType.Text = "Switch to .ASPX";
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
            string allCode = reader.ReadToEnd();
            allCode = allCode.Replace("<", "&lt;").Replace(">", "&gt;");
            lblCode.Text = allCode;
        }
    }

    private void GetCSharpCode()
    {
        string path = this.Request.PhysicalPath + ".cs";
        if( path.IndexOf(".forum") != -1 )
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
            string allCode = reader.ReadToEnd();
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
            lblCode.Text = allCode;
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

    protected void closeShowCode_Click(object sender, EventArgs e)
    {
        Effect effect = new EffectRollUp(pnlShowCode, 1.0M, 600);
        effect.Render();
    }
}
