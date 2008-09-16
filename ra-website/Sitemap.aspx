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
    <ul>
        <li>
            <a runat="server" href="~/" title="Ra Ajax Home">Ra-Ajax Home</a>
        </li>
    </ul>
    
</asp:Content>

