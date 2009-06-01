<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="RaWebsite._Default" 
    Title="Ra-Ajax - Home" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Ra-Ajax</h1>
    <p>
        Ra-Ajax is a <em>no-JavaScript</em> Ajax library for ASP.NET. Ra-Ajax is Free Software and
        licensed under the GPL version 3.
    </p>
    <ul class="list">
        <li><a runat="server" href="~/samples/">View the samples</a></li>
        <li><a runat="server" href="~/Testimonials.aspx">Testimonials</a></li>
        <li><a href="http://code.google.com/p/ra-ajax/">Download Ra-Ajax</a></li>
        <li><a runat="server" href="~/Support.aspx">Get help</a></li>
        <li><a runat="server" href="~/SoftwareFactory.aspx">Need software?</a></li>
    </ul>
    <p>
        Ra-Ajax is extremely lightweight and consumes only tiny amounts of bandwidth. Since
        you don't need to know any JavaScript at all to start using it it is also very easy
        to start with. 
    </p>
    <p>
        Ra-Ajax also works with all major browsers, including most embedded 
        browsers which means you can develop Ajax Applications that will work with iPhone, 
        cellphones and WindowsMobile devices.
    </p>
    <p>
        Ra-Ajax also is 100% Mono compatible which means you can deploy your applications
        on Linux servers in addition to Windows Servers.
    </p>
    <h2>GPL problems...?</h2>
    <p>
        Do you need a non-GPL version of Ra-Ajax...?
    </p>
    <p>
        If you want to use Ra-Ajax without being restricted by the GPL all you need
        to do is to hire us as consultants and we will give you a proprietary license
        which you can use Ra-Ajax together with which will not restrict your product
        in any way.
    </p>
    <p>
        Send <a href="mailto:thomas@ra-ajax.org">Thomas an email</a> to get in contact
        with us regarding hiring us as consultants.
        <a href="SoftwareFactory.aspx">Read more about our consulting services here...</a>
    </p>
</asp:Content>

