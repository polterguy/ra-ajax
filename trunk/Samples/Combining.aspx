<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Combining.aspx.cs" 
    Inherits="Samples.Combining" 
    Title="Combining Ra Ajax Controls" %>

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

    <h1>Combining Ra Ajax Controls</h1>
    <p>
        Much of the point in Ra-Ajax isn't really apparent before you understand how easy it is to
        <em>combine the Ra-Ajax Controls</em> within. By combining Ra-Ajax controls you can create 
        far more complex controls. Either as standalone server-side controls and thereby re-using 
        them in all your projects, or by combining them inline on your .ASPX page or maybe in a 
        UserControl to get some mini re-use within your projects.
    </p>
    <br />
    <h2>Ra-Ajax and UserControls</h2>
    <p>
        The below is an example of a UserControl which is created for reuse within your project.
        Normally this is very useful if you have some Constructs which you need to reuse inside 
        of your project only.
    </p>
    <dataRetriever:Combining 
        ID="uc" 
        runat="server" 
        OnSaved="uc_Saved" />
    <ra:Label 
        runat="server" 
        style="font-style:italic;color:#999;"
        ID="lblResults" />
    <br />
    <br />
    <h2>Ra-Ajax Extension Controls (WebControls)</h2>
    <p>
        The example below is a <em>Ra-Ajax Extension Controls</em> built as a server-side control
        which means you can easily reuse it for all your projecs.
    </p>
    <ext:Calendar 
        runat="server" 
        ID="calendar" 
        OnSelectedValueChanged="calendar_SelectedValueChanged"
        CssClass="calendar" />
    <br />
    <ra:Label 
        runat="server" 
        style="font-style:italic;color:#999;"
        ID="selectedDate" />
    <p>
        In fact the above <em>Ajax Calendar Control</em> is entirely built purely as a server-side 
        Ajax Control, reusing only existing Ra-Ajax Controls like <em>LinkButton, Panel, Label, DropDownList</em>
        and so on. So you don't even have to know JavaScript for creating your own Ajax Extension controls :)
        <br />
        <em>(though sometimes it helps to know JavaScript and sometimes you will need it ;)</em>
    </p>
    <p>
        The above <em>Ajax Calendar</em> is also included as part of the Ra-Ajax Extension project.
    </p>
    <br />
    <h2>Let us know...</h2>
    <p>
        ...by sending us and <a href="mailto:thomas@ra-ajax.org">email</a> if you create really cool extension 
        widgets and want to license them as Open Source so that we can link to your website or maybe even add 
        it into the main Ra-Ajax project :)
    </p>
    <a href="Dynamic.aspx">On to "Dynamic Ajax Controls"...</a>
</asp:Content>

