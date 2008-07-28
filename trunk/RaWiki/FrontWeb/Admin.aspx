<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Admin.aspx.cs" 
    Inherits="Admin" 
    Title="Ra Wiki - Admin interface" %>

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

    <h1>Ra Wiki - Admin interface</h1>
    <p runat="server" id="content">
        <em>Ra Wiki - A wiki system for those who expects more!</em>
    </p>
    <p>
        Ra Wiki is a fully ajaxified Wiki system for ASP.NET and Mono. Due to not forcing
        users to using Wiki syntax when editing but rather a full WYSIWYG editor it
        gives you far easier use. And due to that it is heavily built around the concept
        of <em>Ajax</em> it will be experienced as far more responsive in use than most 
        other wiki systems.
    </p>
    <p>
        In default mode you must be logged in with a confirmed email address to be able to 
        edit wikis, but browsing is completely open for all. This is by design to reduce spam
        in your wikis. 
    </p>
    <p>
        Notic that all though Ra Wiki uses WYSIWYG editors for editing content you can still 
        link to links inside the wiki itself by using the common "wiki linking" logic which 
        is constructed like this;
        <br />
        [some-url | anchor text of link]
        <br />
        Notice though that the URL is without the .wiki end parts.
    </p>
    <p>
        Also this default page is easy to edit to configure as you wish it to be.
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

