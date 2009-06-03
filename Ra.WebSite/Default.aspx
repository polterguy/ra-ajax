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

    <a href="http://code.google.com/p/ra-ajax/">
        <ra:Image 
            runat="server" 
            ID="imgDownload" 
            ImageUrl="media/download.png" 
            AlternateText="Download Ra-Ajax" 
            style="float:right;opacity:0.3;">
            <ra:BehaviorUnveiler 
                runat="server" 
                id="behUnveiler" />
        </ra:Image>
    </a>
    <h1>Ra-Ajax</h1>
    <p>
        Ra-Ajax is a <em>no-JavaScript</em> Ajax library for ASP.NET. Ra-Ajax is Free Software and
        licensed under the GPL version 3.
    </p>
    <ul class="list">
        <li><a runat="server" href="~/samples/">Ra-Ajax Samples</a></li>
        <li><a runat="server" href="~/Testimonials.aspx">Testimonials</a></li>
        <li><a href="http://code.google.com/p/ra-ajax/">Download Ra-Ajax</a></li>
        <li><a runat="server" href="~/Support.aspx">Get Help</a></li>
        <li><a runat="server" href="~/SoftwareFactory.aspx">Need software?</a></li>
    </ul>
    <h2>Not comfortable with the GPL?</h2>
    <p>
        Do you need a non-GPL version of Ra-Ajax?
    </p>
    <p>
        If you want to use Ra-Ajax without being restricted by the GPL, you need
        to hire us as consultants and we will give you a proprietary license. This
        license will entitle you to use Ra-Ajax without the restrictions of the GPL.
    </p>
    <p>
        Send <a href="mailto:thomas@ra-ajax.org">Thomas an email</a> to get in contact
        in regards to hiring us as consultants. Read more about our 
        <a href="SoftwareFactory.aspx">consulting services</a>.
    </p>
</asp:Content>

