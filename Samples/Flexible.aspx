<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Flexible.aspx.cs" 
    Inherits="Samples.Flexible" 
    Title="Ra-Ajax Flexibility" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra Ajax Samples - Event System</h1>
    <p>
        Normally in ASP.NET WebControls you would expect a Button to have OnClick and mostly no
        other Event Handlers. In Ra-Ajax all the Ajax Controls have tons of additional event handlers
        in addition to the "native ones". Try to hover over the Button below for instance.
    </p>
    <ra:Button 
        runat="server" 
        id="btn"
        OnMouseOver="btn_MouseOver"
        OnMouseOut="btn_MouseOut"
        Text="Hover and watch text" />
    <p>
        In addition you would find events like;
    </p>
    <ul class="bulList">
        <li>Focused</li>
        <li>Blur</li>
        <li>MouseOut</li>
        <li>MouseOver</li>
        <li>KeyUp (TextBox and TextArea)</li>
        <li>etc...</li>
    </ul>
    <p>
        And all those events will automagically map to server side events and you wouldn't have to
        write any JavaScript at all to handle them!
    </p>
    <h2>Do anything from anywhere!</h2>
    <p>
        In Ra-Ajax you can do <em>anything from anywhere</em>. This means that for instance in the 
        OnMouseOver Event Handler for a TextBox you can show a "tooltip Panel" or anything you wish.
        You can change any property of any Ra-Ajax control from any Ra-Ajax Event Handler in your
        server-side code. And everything will just "automagically" map back to the client and make
        the requested changes. Try to hover over the TextBox below.
    </p>
    <ra:TextBox 
        runat="server" 
        ID="txt" 
        OnMouseOut="txt_MouseOut"
        OnMouseOver="txt_MouseOver" />
    <ra:Panel 
        runat="server" 
        ID="pnl" 
        Visible="false"
        style="position:absolute;"
        CssClass="panel">
        Only visible when hovering over the TextBox :)
    </ra:Panel>
    <div class="spacerSmall">&nbsp;</div>
    <p>
        This makes it very easy to create "advanced functionality". Now by adding a little bit of <em>Ajax Effects</em>
        in addition, this becomes even more interesting...
    </p>
    <p>
    	Just to be clear; <em>the Ajax Button and the Ajax TextBox also have the OnClick Event Handlers and
    	the OnTextChanged Event Handlers. (of course)</em>
    </p>
    <a href="Effects.aspx">On to "Ajax Effects"...</a>
</asp:Content>

