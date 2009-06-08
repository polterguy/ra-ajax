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
    <h2>Daily Builds of Ra-Ajax</h2>
    <p>
        We now have <a href="/daily">daily builds of Ra-Ajax</a>. At around 1 am 
        (Norway Local Time) each day, a new build will be available for download. This
        only takes place if there are any changes in the code repository since the previous
        daily build. So some days might be skipped.
    </p>
    <h2>Need Software?</h2>
    <p>
        Ra-Software AS is a consultancy company and we are what we refer to ourselves as
        a <em>Software Factory</em>. Check out our <a href="SoftwareFactory.aspx">consulting services</a> 
        for details about what we can help you out with.
    </p>
</asp:Content>

