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
        to the code model in Ra Ajax. Here you can see an <em>Ajax Calendar control</em> I have 
        created without <em>one single line of custom JavaScript</em>.
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
    <p>
        This calendar control is only a few hundred lines of C#, no Custom JavaScript used at all to create it. In fact the way
        it is created is by combining existing Ajax Controls in Ra Ajax together like for instance the LinkButton and the 
        DropDownList controls. Then by simply overriding the <em>OnLoad</em> and the <em>CreateChildControls</em> we are
        able to create a completely ajaxified Calendar Control which we can consume in other projects.
    </p>
    <p>
        If you take a look at the code with the <em>Show Code</em> button you will see that this is a 100% declarative enabled
        Ajax Control with no Custom JavaScript needed to neither consume it nor to create it. And it is highly customizable 
        through use of CSS. And it all works 100% flawlessly together with the entire Ra Ajax runtime which means we can
        use it transparently as if it was just another normal Ra Ajax control. Below is an example where we are consuming
        our Calendar Control to create an <em>Ajax DateTimePicker</em>.
    </p>
    <ra:Label 
        runat="server" 
        Style="font-style:italic;color:Gray;"
        ID="lblDate" 
        Text="Pick a date" />
    <br />
    <ra:Button 
        runat="server" 
        ID="btnPickDate" 
        Text="..." 
        OnClick="btnPickDate_Click" />
    <br />
    <ext:Calendar 
        runat="server" 
        ID="calendarDTP" 
        Visible="false"
        OnSelectedValueChanged="calendarDTP_SelectedValueChanged"
        CssClass="calendar"
        Value="2008.07.20 23:54" />
    <p>
        And the Calendar Control is 100% localizable which is done in C# on the server so you can localize it to any 
        language you wish. By default it uses the System.Threading.Thread.CurrentThread.CurrentUICulture CultureInfo.
        And the whole thing will probably work like all other Ra Ajax Controls on any browser including browsers for
        iPhone, WindowsMobile, Linux, Mac OS X, etc...
    </p>

</asp:Content>

