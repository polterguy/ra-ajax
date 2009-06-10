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
    <div class="button">
        <a href="http://code.google.com/p/ra-ajax/">
            <img class="buttonImage" src="media/box_download_48.png" alt="Download" />
            <span class="buttonText">Download Ra-Ajax</span>
            <span class="buttonText" style="margin-top:0;font-size:10px;">Latest version 2.0.1</span>
        </a>
    </div>
    <div class="button">
        <a href="Testimonials.aspx">
            <img class="buttonImage" src="media/accepted_48.png" alt="Docs" />
            <span class="buttonText">Testimonials</span>
            <span class="buttonText" style="margin-top:0;font-size:10px;">Cutomers' Opinions</span>
        </a>
    </div>
    <div class="button">
        <a href="Support.aspx">
            <img class="buttonImage" src="media/questionmark_48.png" alt="Help" />
            <span class="buttonText">Help &amp; Support</span>
        </a>
    </div>
    <div class="button">
        <a href="SoftwareFactory.aspx">
            <img class="buttonImage" src="media/blockdevice.png" alt="Software" />
            <span class="buttonText">Need Software?</span>
            <span class="buttonText" style="margin-top:0;font-size:10px;">More about Ra-Software</span>
        </a>
    </div>
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

