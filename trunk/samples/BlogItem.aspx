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

    <h1 runat="server" id="header"></h1>
    <i runat="server" id="date"></i>
    <p runat="server" id="body"></p>
    <div style="height:500px;">&nbsp;</div>

</asp:Content>

