<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Extensions.aspx.cs" 
    Inherits="Extensions" 
    Title="Extending Ra Ajax" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Extending Ra Ajax</h1>
    <p>
        To extend Ra Ajax with your own custom <em>Ajax Extension Controls</em> is really easy due
        to the code model in Ra Ajax. Here you can see an Ajax Calendar control I have created
        without <em>one single line of custom JavaScript</em>.
    </p>
    <ra:Label 
        runat="server" 
        ID="selectedDate" 
        Style="font-style:italic;color:Gray;"
        Text="Select a date" />
    <br />
    <br />
    <ext:Calendar 
        runat="server" 
        ID="calendar" 
        OnSelectedValueChanged="calendar_SelectedValueChanged"
        CssClass="calendar"
        Value="2008.07.20 23:54" />

</asp:Content>

