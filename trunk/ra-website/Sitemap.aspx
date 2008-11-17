<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Sitemap.aspx.cs" 
    Inherits="RaWebsite.Sitemap" 
    Title="Ra-Ajax - Sitemap" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Ra-Ajax Sitemap</h1>
    <p>
        These are the most important pages in Ra-Ajax.org
    </p>
    <ul class="blog">
        <li>
            <a id="A1" runat="server" href="~/" title="Ra-Ajax Home">Ra-Ajax Home</a>
        </li>
        <li>
            <a id="A9" runat="server" href="~/samples/" title="Ra-Ajax Samples">Ra-Ajax Samples</a>
        </li>
        <li>
            <a id="A5" runat="server" href="~/Facts.aspx" title="Ra-Ajax Facts">Ra-Ajax FAQ</a>
        </li>
        <li>
            <a id="A6" runat="server" href="http://code.google.com/p/ra-ajax/" title="Download Ra-Ajax">Download Ra-Ajax</a>
        </li>
        <li>
            Blogs:
            <ul class="blog">
                <li>
                    <a id="A3" runat="server" href="~/thomas.blogger" title="Thomas Hansen's Blog">Thomas Hansen's Blog</a>
                </li>
                <li>
                    <a id="A10" runat="server" href="~/Kariem.blogger" title="Kariem Ali's Blog">Kariem Ali's Blog</a>
                </li>
                <li>
                    <a id="A2" runat="server" href="~/Rick.blogger" title="Rick Gibson's Blog">Rick Gibson's Blog</a>
                </li>
            </ul>
        </li>
        <li>
            <a id="A7" runat="server" href="~/Forums/Forums.aspx" title="Ra-Ajax Forums">Ra-Ajax Forums</a>
        </li>
        <li>
            <a id="A8" runat="server" href="~/Author.aspx" title="Hire Ajax Experts">Hire Ajax Experts</a>
        </li>
        <li>
            <a id="A4" runat="server" href="~/Testimonials.aspx" title="Testimonials">Testimonials</a>
        </li>
        <li>
            <a id="A11" runat="server" href="Starter-Kits.aspx" title="Ajax Starter-Kits">Ajax Starter-Kits - Purchase</a>
        </li>
        <li>
            <a id="A12" runat="server" href="~/Donate.aspx" title="Donate to Ra-Ajax">Donate to Ra-Ajax</a>
        </li>
    </ul>
    
</asp:Content>

