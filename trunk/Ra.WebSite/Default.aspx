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
    <a class="website-button" href="http://code.google.com/p/ra-ajax/">
        <img class="website-button-image" src="media/box_download_48.png" alt="Download" />
        <span class="website-button-text">Download Ra-Ajax</span>
        <span class="website-button-text" style="margin-top:0;font-size:10px;">Latest version 2.0.3</span>
    </a>
    <a class="website-button" href="Testimonials.aspx">
        <img class="website-button-image" src="media/accepted_48.png" alt="Docs" />
        <span class="website-button-text">Testimonials</span>
        <span class="website-button-text" style="margin-top:0;font-size:10px;">Cutomers' Opinions</span>
    </a>
    <a class="website-button" href="Support.aspx">
        <img class="website-button-image" src="media/questionmark_48.png" alt="Help" />
        <span class="website-button-text">Help &amp; Support</span>
    </a>
    <a class="website-button" href="SoftwareFactory.aspx">
        <img class="website-button-image" src="media/blockdevice.png" alt="Software" />
        <span class="website-button-text">Need Software?</span>
        <span class="website-button-text" style="margin-top:0;font-size:10px;">More about Ra-Software</span>
    </a>
    <br style="clear:left;" />
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

