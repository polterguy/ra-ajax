<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="_Default" 
    Title="Untitled Page" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1 runat="server" id="header">Ra Wiki</h1>
    <p runat="server" id="content">
        An Open Source wiki system.
    </p>

</asp:Content>

