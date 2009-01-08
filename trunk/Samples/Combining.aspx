<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Combining.aspx.cs" 
    Inherits="Samples.Combining" 
    Title="Combining Ajax Widgets" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<%@ Register 
    Src="Combining.ascx" 
    TagName="Combining" 
    TagPrefix="dataRetriever" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Combining Ajax Controls</h1>
    <p>
        Much of the point in Ra-Ajax is how easy it is to
        <em>combine</em> Ajax Controls together. By combining Ajax controls, you can create 
        far more rich controls. Either as standalone server-side controls and thereby reusing 
        them in all your projects, or by combining them inline on your .ASPX page or maybe in a 
        UserControl to get some reusability within one project.
    </p>
    <h2>Ra-Ajax and UserControls</h2>
    <p>
        Below is an example of a UserControl which is created for reuse within your project.
        Normally this is very useful if you have some constructs which you need to reuse inside 
        of your project only.
    </p>
    <dataRetriever:Combining 
        ID="uc" 
        runat="server" 
        OnSaved="uc_Saved" />
    <ra:Label 
        runat="server" 
        CssClass="updateLbl"
        ID="lblResults" />
    <p>
        Note that all code for the UserControl is completely contained within the UserControl itself
        which makes it very easy to encapsulate all logic needed for specific parts of your applications
        in the place they should be. This creates far cleaner code for you!
    </p>
    <h2>Ra-Ajax Extension Controls (WebControls)</h2>
    <p>
        The example below is a Ra-Ajax Extension Control built as a server-side control
        which means you can easily reuse it across all of your projecs.
    </p>

    <ext:Calendar 
        runat="server" 
        ID="calendar" 
        style="width:190px;"
        OnSelectedValueChanged="calendar_SelectedValueChanged" />

    <p style="clear:both;">
        <ra:Label 
            runat="server" 
            CssClass="updateLbl"
            ID="selectedDate" />
    </p>
    <p>
        In fact, the above Ajax Calendar Control is built entirely as a server-side 
        Ajax Control, reusing only existing Ra-Ajax Controls like LinkButton, Panel, Label, SelectList
        and so on. So you don't even have to know JavaScript to create your own Ajax Extension controls.
    </p>
    <p>
        The above Ajax Calendar is also included as part of the Ra-Ajax Extensions project.
    </p>
    <h2>Let Us Know!...</h2>
    <p>
        ...if you create a really cool extension widget and want to license it as Open Source. Send 
        us an <a href="mailto:thomas@ra-ajax.org">email</a> so that we can link to your website or maybe even add 
        it to the main Ra-Ajax Extensions project. Or have it as a community extension.
    </p>
    <h2>Ra-Ajax contains less then 10KB of JavaScript</h2>
    <p>
        If you look at the average JavaScript amount in an average Ajax library you will often see that
        it is several hundreds of KB. Sometimes it will even be more then 1 MEGABYTE. Ra-Ajax have
        less then 10 KB of JavaScript for an average page. In fact even our 
        <a href="Viewport-Calendar-Starter-Kit.aspx">Calendar Starter-Kit</a> has less then 8 KB of JavaScript.
    </p>
    <p>
        This makes your applications far more responsive in the initial load. 
    </p>
    <p>
        In addition the page is carefully built with all best practices like adding JavaScript at the bottom
        of your page, etc. This makes an average Ra-Ajax webpage outperform an average Non-Ra-Ajax Webpage
        with orders of magnitudes.
    </p>
    <p>
        Most webpages here at our samples do score 90 or more in YSlow and loads in less then one second. Even
        with an empty browser cache!
    </p>
    <a href="Dynamic.aspx">On to "Dynamic Ajax Controls"...</a>
</asp:Content>

