<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Account.aspx.cs" 
    Inherits="Account" 
    Title="Untitled Page" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1 runat="server" id="header">Account</h1>
    <ra:Button 
        runat="server" 
        ID="checkEmail" 
        OnClick="checkEmail_Click"
        Text="Check for emails" />

</asp:Content>

