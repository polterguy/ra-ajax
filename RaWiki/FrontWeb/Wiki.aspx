<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Wiki.aspx.cs" 
    Inherits="Wiki" 
    Title="Untitled Page" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <ext:TabControl runat="server" ID="tab" CssClass="tab-control">
        <ext:TabView runat="server" ID="tab1" Caption="Read" CssClass="content">
            <h1 runat="server" id="header_read">Wiki Header</h1>
        </ext:TabView>
        <ext:TabView runat="server" ID="tab2" Caption="Edit" CssClass="content">
            <h1 runat="server" id="header_write">Wiki Header</h1>
        </ext:TabView>
    </ext:TabControl>

</asp:Content>

