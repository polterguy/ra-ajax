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
    <h3>Ajax Calendar</h3>
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
        OnDateClicked="calendarDTP_DateClicked"
        CssClass="calendar"
        style="position:absolute;"
        Value="2008.07.20 23:54" />
    <p>
        And the Calendar Control is 100% localizable which is done in C# on the server so you can localize it to any 
        language you wish. By default it uses the System.Threading.Thread.CurrentThread.CurrentUICulture CultureInfo.
        The whole thing will probably work like a charm like all other Ra Ajax Controls on any relatively standard 
        compliant browser including browsers for iPhone, WindowsMobile, Linux, Mac OS X, etc. 
    </p>
    <h3>Ajax TabControl</h3>
    <p>
        Here's another example of how to create an Ajax Extension control yourself. This time we have created an 
        <em>Ajax TabControl</em> for you. Also this Ajax Control is completely written without one line of
        Custom JavaScript and completely created in C#. The TabControl works by being a wrapper around a collection
        of Ra Panels and then it will only show ONE of those panels at the same time. Then the user can switch
        active panel by a set of buttons at the top of it.
    </p>
    <ext:TabControl runat="server" ID="tab" CssClass="tab-control">
        <ext:TabView Caption="Tab view 1" runat="server" ID="tab1" CssClass="content">
            Here you can see an example of an <em>Ajax TabControl</em> and as you can see
            it is also possible to add up other Ajax Controls into the TabControl just
            as if it was a normal Panel. Try clicking the LinkButton below...
            <br />
            <ra:LinkButton 
                runat="server" 
                ID="lnkTest" 
                Text="Click me..."
                OnClick="lnkTest_Click" />
        </ext:TabView>
        <ext:TabView Caption="Tab view 2" runat="server" ID="tab2" CssClass="content">
            Here we even threw in an Ajax Calendar more just for the fun of it :)
            <br />
            <ra:Label 
                runat="server" 
                ID="lblCalTab" 
                Style="font-style:italic;color:Gray;"
                Text="Select a date" />
            <br />
            <br />
            <ext:Calendar 
                runat="server" 
                ID="calTab" 
                OnSelectedValueChanged="calTab_SelectedValueChanged" 
                OnRenderDay="calTab_RenderDay"
                CssClass="calendar"
                Value="2008.07.20 23:54" />
            <br />
            In the above calendar we have added up an Event Handler for the RenderDay Event and
            added up the "holidays for planet Venus in green" as you can see. We could have
            taken this WAY further by even add up our own CONTROLS for specific dates and so
            on to create a "complete calendar" for whatever we wish. But we've kept the sample
            small, light and consice to make it easy to understand.
        </ext:TabView>
        <ext:TabView Caption="Third" runat="server" ID="tab3" CssClass="content">
            While here we just have some very stupid text to show that <em>this is also</em> pure
            <span style="color:Yellow;">HTML</span> which can be manipulated <strong>just</strong>
            like any other HTML in your forms :)
            <h3>HOWDY :)</h3>
            <img src="media/ajax.png" alt="Ajax logo" />
        </ext:TabView>
    </ext:TabControl>
    <p>
        If you want to see an easier way of creating complex Ajax functionality without even having to create your 
        own Ajax Extension Control then you can have a look at the <a href="Flexible.aspx">Ajax AutoCompleter Example</a>.
    </p>

</asp:Content>

