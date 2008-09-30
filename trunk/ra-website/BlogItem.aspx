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
        <h3>Copyright</h3>
        <p>
            All content at this blog is the Copyright of <a href="http://ra-ajax.org">Ra Software AS</a> 
            but can freely be distributed under the terms of 
            <a href="http://www.gnu.org/licenses/fdl.html">GNU Free Documentation License</a>.
        </p>
    </div>

</asp:Content>

