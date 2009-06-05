<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Documentation-SiteMap.aspx.cs" 
    Inherits="RaWebsite.DocsSitemap" 
    Title="Ra-Ajax Documentation SiteMap" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Documentation SiteMap</h1>
    <p>
        This page is mostly created so that Google and other search engines will find 
        all our classes and tutorials. But it can also be a nice page to start out 
        surfing our documentation if you don't like the Ajax interface of the 
        <a href="Docs.aspx">main Ra-Ajax documentation page</a>.
    </p>
    <h2>Ra-Ajax Tutorials documentation</h2>
    <asp:Repeater runat="server" ID="repTutorials">
        <HeaderTemplate><ul class="list"></HeaderTemplate>
        <FooterTemplate></ul></FooterTemplate>
        <ItemTemplate>
            <li>
                <a href='<%#Eval("URL") %>'><%#Eval("Text") %></a>
            </li>
        </ItemTemplate>
    </asp:Repeater>
    <h2>Ra-Ajax Classes documentation</h2>
    <asp:Repeater runat="server" ID="rep">
        <HeaderTemplate><ul class="list"></HeaderTemplate>
        <FooterTemplate></ul></FooterTemplate>
        <ItemTemplate>
            <li>
                <a href='<%#Eval("URL") %>'><%#Eval("Text") %></a>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

