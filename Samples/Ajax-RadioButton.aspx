<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-RadioButton.aspx.cs" 
    Inherits="Samples.AjaxRadioButton" 
    Title="Ra-Ajax RadioButton Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Src="Combining.ascx" 
    TagName="Combining" 
    TagPrefix="uc1" %>

<%@ Register 
    Src="HelloWorld.ascx" 
    TagName="HelloWorld" 
    TagPrefix="uc2" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra Ajax Samples - RadioButton</h1>
    <p>
        An <em>Ajax RadioButton</em> is a wrapper around the <em>&lt;input type="radio"</em> HTML
        form element. Imagine having Ajax Controls which instead of generating JavaScript events when you interact
        with their DOM events, would create Ajax Requests to your server and raise server-side events that you can 
        handle using your favorite .NET language. The Ajax RadioButton (and all other Ra-Ajax Controls) are created 
        like that in fact.
    </p>
    <ul>
        <li>
            <ra:RadioButton 
                runat="server" 
                GroupName="controls"
                ID="rdo2" 
                Text="Hello World" 
                OnCheckedChanged="rdo1_CheckedChanged" />
        </li>
        <li>
            <ra:RadioButton 
                runat="server" 
                GroupName="controls"
                ID="rdo1" 
                Text="Combining Ajax Controls" 
                OnCheckedChanged="rdo1_CheckedChanged" />
        </li>
    </ul>
    <ra:Panel 
        runat="server" 
        ID="pnl"
        CssClass="panel">

        <uc2:HelloWorld 
            ID="HelloWorld1" 
            runat="server" 
            Visible="false" />
        <uc1:Combining 
            ID="Combining1" 
            runat="server" 
            Visible="false" />

    </ra:Panel>
    <h2>Run Your Ajax Apps Everywhere</h2>
    <p>
        Ra-Ajax is still quite immature, but its goal is to be able to run Ajax on every single platform
        in the world which has a browser that implements the <em>XMLHTTPRequest</em> and also has the
        capacity to do some DOM manipulation. This is very easy in Ra-Ajax due to the fact that Ra-Ajax almost 
        doesn't have any JavaScript at all. In fact the entire JavaScript for Ra-Ajax is 11.6KB at the time
        of this writing.
    </p>
    <p>
        Now to port JavaScript to all the different browsers, is the most difficult and time consuming job any 
        Ajax framework vendor can face. By reducing the amount of JavaScript significantly until there is almost no 
        JavaScript left to port, that job becomes easier compared to if we would have to port something which 
        was way larger.
    </p>
    <p>
        At the same time by following Open Web Standards everywhere possible and not being tempted to
        have the <em>core Ajax Logic</em> depending upon having <em>"special attributes"</em> on DOM elements
        or have <em>"block-level DOM elements inside of in-line DOM elements"</em>, and so on, this job becomes
        even far easier.
    </p>
    <p>
        In fact if your toaster has a browser, then I am almost willing to bet body parts on that you will
        be able to have Ra-Ajax running within it before the v1.0 release.
    </p>
    <a href="Ajax-TextArea.aspx">On to Ajax TextArea</a>
</asp:Content>
