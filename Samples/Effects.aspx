<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Effects.aspx.cs" 
    Inherits="Effects" 
    Title="Ra Ajax Effects" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra Ajax Samples - Ajax Effects</h1>
    <p>
        There's no fun in Ajax if you don't have a powerful <em>Ajax Effect Collection</em> coupled with
        your Ajax Library. Ra-Ajax contains several different predefined Ajax Effects. In addition it is
        very easy to create your own Ajax Effects by looking at some of the existing ones.
    </p>
    <ra:Button 
        runat="server" 
        ID="btn" 
        Text="Click me" 
        OnClick="btn_Click" />
    <br />
    <ra:Panel 
        runat="server" 
        ID="pnl" style="position:absolute;background-color:Yellow;border:solid 1px Black;padding:15px;height:100px;width:100px;">
        Watch the Ajax Effect as you click the above button...
    </ra:Panel>
    <div style="height:300px;">&nbsp;</div>
    <p>
        Now try to click the button below here to see another Ajax Effect.
    </p>
    <ra:Button 
        runat="server" 
        ID="btn2" 
        OnClick="btn2_Click" 
        Enabled="false"
        Text="Click me too" />
    <p>
        And finally the button below here
    </p>
    <ra:Button 
        runat="server" 
        ID="btn3" 
        OnClick="btn3_Click"
        Enabled="false"
        Text="Click me too" />
    <p>
        And as normal no JavaScript knowledge is required to use these constructs, take a look at 
        the source code for this page by clicking the "Show code" button at the top/right corner.
        Also this is just a small subset of the Ajax Effects that exists in Ra-Ajax.
    </p>
    <p>
        And you can see here that we've created our Effects in the Click event handler of a button.
        You can of course create Ajax Effects on the server side from any Ra-Ajax Event Handler if 
        you wish.
    </p>
</asp:Content>

