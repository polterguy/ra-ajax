<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-TextBox.aspx.cs" 
    Inherits="AjaxTextBox" 
    Title="Ajax TextBox Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ajax TextBox Sample</h1>
    <p>
        This is our <em>Ajax TextBox</em> sample and for those wondering where the "TextMode" property enum
        value of "MultiLine" has gone you should read our <a href="Ajax-TextArea.aspx">Ajax TextArea</a> sample.
    </p>
    <ra:TextBox 
        runat="server" 
        ID="txt" 
        OnFocused="txt_Focused"
        Text="Type something" />
    <br />
    <ra:Button 
        runat="server" 
        ID="btn" 
        Text="Submit" 
        OnClick="btn_Click" />
    <br />
    <ra:Label 
        runat="server" 
        ID="lbl" 
        style="color:#999;font-style:italic;" />
    <p>
        Try to type into the above Ajax TextBox and hit Submit...
    </p>
    <br />
    <h2>Try to turn off ViewState</h2>
    <p>
        In Ra-Ajax (including this Ajax TextBox example) you can actually turn off ViewState on the page level.
        Some Ajax Libraries are constructed so that it is impossible to turn off ViewState. By solving the
        <a href="Ajax-TextArea.aspx">Ajax TextArea LSP problems</a> in Ra-Ajax we have also at the same time
        also solved the <em>"ViewState cannot be turned off"</em> problem.
    </p>
    <p>
        Some of the reasons to that is given in our <a href="Ajax-CheckBox.aspx">Ajax CheckBox sample</a>.
        Note though that you can turn off the ViewState but you cannot physically remove the ViewState
        from the page. This is due to that Ra-Ajax is dependant upon <em>ControlState</em> which was a
        new concept in ASP.NET 2.0 which made it possible to force some values into getting written
        to the ViewState even though ViewState itself was turned off.
    </p>
    <br />
    <h2>How this works...</h2>
    <p>
        If you turn off the ViewState for this page on e.g. the Page Directive you will see that the above
        TextBox sample will still work. The reason is that we trap <em>changes when they happen</em> instead of
        <em>diffing the incoming page against the outgoing page</em> the way many other Ajax Libraries does. 
        While many other Ajax Frameworks will actually diff the content after ViewState is loaded against 
        the content sent back to the client when the Control(s) are about to be rendered and then send 
        that "diff". Ra-Ajax intercepts when you set a value of a property and then stores that value for 
        later sending it back to the client.
    </p>
    <p>
        This makes it possible for your Ra-Ajax applications to turn off ViewState and thereby saving
        a lot of bandwidth in your applications.
    </p>
    <p>
        Though I still must say that to turn off ViewState on the entire page is not something I would
        encourage most Ajax developers to do since the ViewState actually is a very beautiful concept
        and makes developing Ajax Applications (and "normal" web applications) in ASP.NET far easier.
    </p>
    <a href="Ajax-Accordion.aspx">On to Ajax Accordion</a>
</asp:Content>
