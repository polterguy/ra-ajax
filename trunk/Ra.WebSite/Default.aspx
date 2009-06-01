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
    <h2>GPL problems...?</h2>
    <p>
        Do you need a non-GPL version of Ra-Ajax...?
    </p>
    <p>
        If you want to use Ra-Ajax without being restricted by the GPL you need
        to hire us as consultants and we will give you a proprietary license
        which you can use Ra-Ajax by which will not restrict you to the GPL.
    </p>
    <p>
        Send <a href="mailto:thomas@ra-ajax.org">Thomas an email</a> to get in contact
        with us regarding hiring us as consultants.
        <a href="SoftwareFactory.aspx">Read more about our consulting services here...</a>
    </p>
</asp:Content>

