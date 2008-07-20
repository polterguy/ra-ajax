<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="BlogItem.aspx.cs" 
    Inherits="BlogItem" 
    Title="Untitled Page" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">
    <div class="blog">
        <h1 runat="server" id="header"></h1>
        <i class="date" runat="server" id="date"></i>
        <br />
        <br />
        <div runat="server" id="body"></div>
    </div>

</asp:Content>

