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
    <ra:Panel 
        runat="server" 
        style="width:100%;background-color:Yellow;border:solid 1px #999;padding:5px;position:relative;margin-top:25px;"
        Visible="false"
        ID="createArticlePnl">
        Article name 
        <ra:TextBox 
            runat="server" 
            style="width:200px;"
            ID="nArticleName" />
        <ra:Button 
            runat="server" 
            ID="createArticleBtn" 
            OnClick="createArticleBtn_Click"
            Text="Create" />
    </ra:Panel>

    <div class="actionbar">
        <ra:LinkButton 
            runat="server" 
            ID="adminMode" 
            OnClick="adminMode_Click"
            Visible="false"
            Text="Admin" />
        <ra:LinkButton 
            runat="server" 
            ID="createArticle" 
            OnClick="createArticle_Click"
            Visible="false"
            Text="Create article" />
    </div>

</asp:Content>

