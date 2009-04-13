<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Support.aspx.cs" 
    Inherits="RaWebsite.Support" 
    Title="Get support with Ra-Ajax" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Support</h1>
    <p>
        Our primary support mechanism for Ra-Ajax is <a href="http://stacked.ra-ajax.org">our forums</a>.
        Make sure you check to see if there already exists an answer for your question before posting it.
        Mostly you will get an answer to your question within 24 hours, but since we have no income
        from answering questions by non-paying users we obviously can't give you any guarantees.
    </p>
    <h2>Documentation</h2>
    <p>
        You can find the documentation for Ra-Ajax <a href="Documentation.aspx">here</a>.
    </p>
    <h2>Professional Support</h2>
    <p>
        Ra-Ajax is maintained and created by Ra-Software AS, a Norwegian privately held company which
        supplies consulting services for Ra-Ajax.
    </p>
    <p>
        If you have an application you want to create or you need help with Ra-Ajax problems or just 
        another senior developer to your existing development department you can contact us by sending
        <a href="mailto:thomas@ra-ajax.org">Thomas Hansen an email</a>. Ra-Software AS can offer Ra-Ajax 
        Training, custom development, coaching, code-reviews and mostly anything imaginable within 
        the context of Ra-Ajax.
    </p>
    <p>
        Read about <a href="Author.aspx">our qualifications here</a> or <a href="Testimonials.aspx">what our customers say about us</a>.
    </p>
    <h2>About Stacked</h2>
    <p>
        Our support mechanism is not a traditional <em>forum</em> but an inhouse developed question/answer
        application built after the ideas of StackOverflow.com. It is Open Source licensed and can
        be found <a href="http://code.google.com/p/stacked/">here</a> if you want it for your own
        project.
    </p>
</asp:Content>

