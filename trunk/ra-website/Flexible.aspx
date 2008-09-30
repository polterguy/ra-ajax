<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Flexible.aspx.cs" 
    Inherits="RaWebsite.Flexible" 
    Title="Flexible Ajax" %>

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

    <h1>Flexible Ajax with Ra Ajax</h1>
    <p>
        Ra Ajax is also far more flexible than the builtin ASP.NET Controls. While the ASP.NET controls only
        have events for things like TextChanged for TextBox and so on, Ra Ajax have MouseOver, MouseOut and
        even KeyUp events for its Ajax TextBox control. Here we have an example of an <em>Ajax TextBox</em>
        which have support for KeyUp events.
    </p>
    <ra:TextBox 
        runat="server" 
        ID="txt" 
        style="width:250px;"
        OnKeyUp="txt_KeyUp" />
    <br />
    <ra:Label 
        runat="server" 
        Text="Watch me change"
        style="font-style:italic;"
        ID="lbl" />
    <p>
        Now by combining a TextBox with the KeyUp event together with a Panel we can do really nice things. 
        Below is an Ajax AutoCompleter created by combining an Ajax Panel together with a TextBox with the
        KeyUp event handled. By typing into the TextBox below you will actually query through my blogs
        and return the blogs which contains whatever phrase you're typing in. Try for instance to type in;
        <em>how to create an ajax library</em> to search through my articles about "How to create an Ajax 
        Library".
    </p>
    <ra:TextBox 
        runat="server" 
        ID="autoTxt" 
        style="width:250px;"
        OnKeyUp="autoTxt_KeyUp" />
    <ra:Panel 
        runat="server" 
        Visible="false"
        ID="autoPnl" 
        CssClass="auto" />
    <p>
        This is just a simple example created to keep the code clean and understandable, but by combining
        constructs like this you can yourself create very powerful constructs without even touching JavaScript
        yourself. And if you wanted you could easily wrap the whole thing into a server control written in C#
        or VB.NET yourself to make it more reusable like we've done with the <a runat="server" href="~/Extensions.aspx" 
        title="Ra Ajax Extension Controls">Ajax Calendar Example</a>.
    </p>
    <p>
        If you create a nice Ajax Extension Control by combining other controls together then please let me know
        by <a href="mailto:polterguy@gmail.com">sending me an email</a> or <a href="Forums/Forums.aspx">posting in our forums</a> 
        since we are very interested in getting to know the different ways our library is being used :)
    </p>

</asp:Content>

