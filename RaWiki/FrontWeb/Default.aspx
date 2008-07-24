<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="_Default" 
    Title="Untitled Page" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Src="AdminMode.ascx" 
    TagName="AdminMode" 
    TagPrefix="ucAdmin" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Ra Wiki</h1>
    <p runat="server" id="content">
        Ra Wiki - A wiki system for those who expects more!
    </p>

    <ra:Panel 
        runat="server" 
        ID="adminWrapper" 
        Visible="false">
        <ucAdmin:AdminMode 
            ID="AdminMode1" 
            runat="server" />
    </ra:Panel>

    <div class="actionbar">
        <ra:LinkButton 
            runat="server" 
            ID="adminMode" 
            OnClick="adminMode_Click"
            Visible="false"
            Text="Admin" />
    </div>

</asp:Content>

