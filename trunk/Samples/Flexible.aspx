<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Flexible.aspx.cs" 
    Inherits="Samples.Flexible" 
    Title="Ajax Event System" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ajax Event System</h1>
    <p>
        Normally in ASP.NET WebControls you would expect a Button to have OnClick and mostly no
        other Event Handlers. In Ra-Ajax all the Ajax Controls have tons of additional event handlers
        in addition to the "native ones". Try to hover over the Panel/Button below for instance.
    </p>
    <ra:Panel 
        runat="server" 
        style="width:300px;border:dashed 2px Black; background-color:Yellow;padding:25px;margin:15px;" 
        OnMouseOver="pnlMouse_MouseOver"
        OnMouseOut="pnlMouse_MouseOut"
        ID="pnlMouse">
        <ra:Label 
            runat="server" 
            ID="lblResult" 
            style="text-align:center;"
            Text="Try to hover over panel" />
        <br />
        <ra:Panel 
            runat="server" 
            style="width:200px;border:dashed 2px Black; background-color:Orange;padding:25px;margin:15px;" 
            OnMouseOver="pnlMouse2_MouseOver"
            OnMouseOut="pnlMouse2_MouseOut"
            ID="pnlMouse2">
            <ra:Label 
                runat="server" 
                ID="lblResult2" 
                style="text-align:center;"
                Text="Try to hover over this panel too" />
        </ra:Panel>
        <br />
        <ra:Button 
            runat="server" 
            id="btn"
            OnMouseOver="btn_MouseOver"
            OnMouseOut="btn_MouseOut"
            Text="Hover and watch text" />
    </ra:Panel>
    <p>
        The above is actually a nightmare to do in JavaScript due to the event bubbling which
        forces you into tracking the to and from element since when the mouse hovers over the Button
        inside of the panel then the panel will first trigger a MouseOut event and afterwards trigger
        a MouseOver back again. We have *fixed* this in Ra-Ajax so that you don't have to think about 
        that. Meaning if it's over it's still over and it will NOT trigger a MouseOut event :)
    </p>
    <p>
        This means that no MouseOut events will be triggered when the mouse enters a 
        <strong>child element</strong> of the one handling the MouseOut event. And no MouseOver event
        will be triggered when the mouse leaves the child element and goes back to the main surface
        of the DOM element handling the MouseOut event.
    </p>
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
    <p>
        And if you must trap events on the Client for performance reasons, you can use the
        <em>OnClickClientSide</em> property which takes a reference to a client-side JavaScipt
        function to trap click events on the client.
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
        This is very similar to the way you're used to develop in JavaScript and conventional ASP.NET
        "postback" WebControls. All the Ra-Ajax controls have overridden the rendering logic so that
        any change you do to them while on the server will automagically return back to the client.
        This is almost unique to Ra-Ajax and gives you extreme flexibility in your solutions.
    </p>
    <p>
        For instance in the expand tree node event of our TreeView you could create a new TreeView,
        inject it into a Window, make that window Modal and show it as a child control of your TabControl
        on a totally different part of your page. Of course that specific sample seldom makes sense, 
        but the flexibility which you get with Ra-Ajax is almost unique to Ra-Ajax and makes your life
        as an Ajax Application developer a lot easier to live!
    </p>
    <p>
        This makes it very easy to create stunningly rich functionality! Unmatched by all other existing
        Ajax Framework out there...
    </p>
    <p>
        And out of the box you get it to work on all main browsers - no hassle! Even Opera for embedded
        devices and iPhone!
    </p>
    <a href="Effects.aspx">On to "Ajax Effects"...</a>
</asp:Content>

