<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Sitemap.aspx.cs" 
    Inherits="Sitemap" 
    Title="Ra Ajax - Sitemap" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Ra Ajax Sitemap</h1>
    <p>
        These are the most important pages in Ra-Ajax.org
    </p>
    <ul class="sitemap">
        <li>
            <a id="A1" runat="server" href="~/" title="Ra Ajax Home">Ra-Ajax Home</a>
        </li>
        <li>
            <a id="A2" runat="server" href="~/Advantages.aspx" title="Ra Ajax Advantages">Ra-Ajax Advantages</a>
        </li>
        <li>
            <a id="A4" runat="server" href="~/Controls.aspx" title="Ra Ajax Controls">Ra-Ajax Controls</a>
        </li>
        <li>
            <a id="A9" runat="server" href="~/samples/" title="Ra Ajax Samples">Ra-Ajax Samples</a>
        </li>
        <li>
            <a id="A5" runat="server" href="~/Facts.aspx" title="Ra Ajax Facts">Ra-Ajax Facts</a>
        </li>
        <li>
            <a id="A6" runat="server" href="http://code.google.com/p/ra-ajax/" title="Download Ra Ajax">Download Ra-Ajax</a>
        </li>
        <li>
            Blogs:
            <ul class="sitemap">
                <li>
                    <a id="A3" runat="server" href="~/thomas.blogger" title="Thomas Hansen's Blog">Thomas Hansen's Blog</a>
                </li>
            </ul>
        </li>
        <li>
            <a id="A7" runat="server" href="~/Forums/Forums.aspx" title="Ra Ajax Forums">Ra-Ajax Forums</a>
        </li>
        <li>
            <a id="A8" runat="server" href="~/Author.aspx" title="About Us">About Us</a>
        </li>
    </ul>
    
</asp:Content>
