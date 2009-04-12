<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="BlogItem.aspx.cs" 
    Inherits="RaWebsite.BlogItem" 
    Title="Untitled Page" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">
    <div class="blog">
        <h1 runat="server" id="header"></h1>
        <i class="date" runat="server" id="date"></i>
        <br />
        <div runat="server" id="body"></div>
        <a 
            style="font-size:10px;" 
            runat="server" 
            id="previous" 
            title="Previous blog">&lt;&lt; Previous</a>
        <br />
        <a 
            style="font-size:10px;" 
            runat="server" 
            id="next" 
            title="Next blog">Next &gt;&gt;</a>
    </div>

</asp:Content>

