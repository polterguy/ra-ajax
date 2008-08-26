<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-RadioButton.aspx.cs" 
    Inherits="AjaxRadioButton" 
    Title="Ajax RadioButton Sample" %>

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

    <h1>Ajax RadioButton Sample</h1>
    <p>
        And <em>Ajax RadioButton</em> is no more than an Ajax Wrapper around the <em>&lt;input type="radio"</em> HTML
        FORM element. Imagine having Ajax Controls which instead of generating JavaScript events when you interact
        with their DOM events it would create Ajax Requests to your server from which you have full access to the
        entire DOM structure within and can do almost everything you can do within their JavaScript DOM events
        counterparts. The Ajax RadioButton (and all other Ra-Ajax Controls) are those Ajax Controls in fact.
    </p>
    <ra:RadioButton 
        runat="server" 
        GroupName="controls"
        ID="rdo1" 
        Text="Combining Ajax Controls RadioButton" 
        OnCheckedChanged="rdo1_CheckedChanged" />
    <br />
    <ra:RadioButton 
        runat="server" 
        GroupName="controls"
        ID="rdo2" 
        Text="Hello World RadioButton" 
        OnCheckedChanged="rdo1_CheckedChanged" />
    <ra:Panel 
        runat="server" 
        ID="pnl"
        style="padding:15px;background-color:Yellow;border:solid 1px #999;width:50%;">

        <uc2:HelloWorld 
            ID="HelloWorld1" 
            runat="server" 
            Visible="false" />
        <uc1:Combining 
            ID="Combining1" 
            runat="server" 
            Visible="false" />

    </ra:Panel>
    <br />
    <h2>Run your Ajax Apps *everywhere*</h2>
    <p>
        Ra-Ajax is still quite immature, but its goal is to be able to run Ajax on every single platform
        in the world which have a browser which implements the <em>XMLHTTPRequest</em> and also have the
        capacity to do some DOM manipulation. This is VERY easy in Ra-Ajax due to that Ra-Ajax almost 
        doesn't have any JavaScript at all. In fact the entire JavaScript for Ra-Ajax is 11.6KB of JavaScript as
        of this writing (26th of August 2008). Now to port JavaScript to all the different browsers is *the* 
        most difficult and time consuming job any Ajax Framework developer does. And by reducing that job by 
        orders of magnitudes until there is almost no JavaScript left to port that job becomes significantly 
        easier compared to if we would have to port something which was way larger.
    </p>
    <p>
        At the same time by following Open Web Standards *everywhere possible* and not being tempted to
        have the <em>core Ajax Logic</em> depend upon having <em>"special attributes"</em> on DOM elements
        or have <em>"block-level DOM elements inside of inline DOM elements"</em> and so on this job becomes
        far easier.
    </p>
    <p>
        In fact if your toaster have a browser then I am almost willing to bet body parts on that you will
        be able to have Ra-Ajax run within it before the 1.0 version of Ra-Ajax. Including the above
        Ajax RadioButtons ;)
    </p>
    <a href="Ajax-TextArea.aspx">On to Ajax TextArea</a>
</asp:Content>
