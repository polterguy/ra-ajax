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
            All content at my this blog is the Copyright of <a href="http://ra-ajax.org">Ra Software AS</a> 
            but can freely be distributed under the terms of 
            <a href="http://www.gnu.org/licenses/fdl.html">GNU Free Documentation License</a> which means you 
            are <em>encouraged to copy, modify and redistribute all the content at this website</em> as much as 
            you wish. I would particulary encourage people with their own blogs, webserver and so on to 
            redistribute any content they wish on their own server. Though redistribution must happen under the 
            terms of the GFDL version 1.2. Also occassionally I may publish content which is not the copyright
            of mine. Obviously that content must meet the requirements of the original copyright holder. All code
            in our blogs is published under the terms of the MIT license. I appreciate a link to this website or
            the specific blog you found the material on if you re-publish my content, but this is not a legal
            requirement...
        </p>
    </div>

</asp:Content>

