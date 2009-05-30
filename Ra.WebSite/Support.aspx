<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Support.aspx.cs" 
    Inherits="RaWebsite.Support" 
    Title="Get support with Ra-Ajax" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Support</h1>
    <ul class="list">
        <li><a href="http://stacked.ra-ajax.org">Forums</a></li>
        <li><a runat="server" href="~/Docs.aspx">Documentation</a></li>
    </ul>
    <p>
        Our primary support mechanism for Ra-Ajax is <a href="http://stacked.ra-ajax.org">our forums</a>.
        Make sure you check to see if there already exists an answer for your question before posting it.
        Mostly you will get an answer to your question within 24 hours, but since we have no income
        from answering questions by non-paying users we obviously can't give you any guarantees.
    </p>
    <h2>Documentation</h2>
    <p>
        You can find the documentation for Ra-Ajax <a href="Docs.aspx">here</a>.
    </p>
    <h2>Professional help or want to use Ra-Ajax in closed source projects?</h2>
    <p>
        Ra-Software does not publish Ra-Ajax under any other license then the GPL. However, 
        Ra-Software is a consulting company and when we do consulting we will use Ra-Ajax 
        ourselves and you will be granted a commercial/proprietary license of Ra-Ajax 
        which we and you will use in your project which does not restrict your project
        to the GPL license.
    </p>
    <p>
        This means that if you want to build proprietary software using Ra-Ajax you're gonna
        have to purchase consulting services from us. We do however provide consulting services
        all over the world (with the help of Skype, email and such) and we also have physical
        appearance in Northern California, Oslo Norway and Alexandria Egypt.
    </p>
    <p>
        Read about <a href="Author.aspx">our qualifications here</a> or <a href="Testimonials.aspx">what our customers say about us</a>.
    </p>
</asp:Content>

