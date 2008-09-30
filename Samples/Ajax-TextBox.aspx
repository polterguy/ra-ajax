<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-TextBox.aspx.cs" 
    Inherits="Samples.AjaxTextBox" 
    Title="Ra-Ajax TextBox Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra Ajax Samples - TextBox</h1>
    <p>
        This is our <em>Ajax TextBox</em> sample and for those wondering where the "TextMode" property enum
        value of "MultiLine" has gone, you should read our <a href="Ajax-TextArea.aspx">Ajax TextArea</a> sample page.
    </p>
    <p>
        <ra:TextBox 
            runat="server" 
            ID="txt" 
            OnFocused="txt_Focused"
            Text="Type something" />
    </p>
    <p>
        <ra:Button 
            runat="server" 
            ID="btn" 
            Text="Submit" 
            OnClick="btn_Click" />
    </p>
    <p>
    <ra:Label 
        runat="server" 
        ID="lbl" 
        CssClass="updateLbl" />
    </p>
    <p>
        Try to type into the above Ajax TextBox and hit Submit.
    </p>
    <h2>Try to turn off ViewState</h2>
    <p>
        In Ra-Ajax, including this Ajax TextBox sample, you can actually turn off ViewState at the page level.
        Some Ajax libraries are constructed so that it is impossible to turn off ViewState. By solving the
        <a href="Ajax-TextArea.aspx">Ajax TextArea LSP problems</a> in Ra-Ajax, we have at the same time
        solved the <em>"ViewState cannot be turned off"</em> problem.
    </p>
    <p>
        Some of the reasons for that is given in our <a href="Ajax-CheckBox.aspx">Ajax CheckBox sample</a>.
        Note though, that you can turn off the ViewState but you cannot physically remove the ViewState
        from the page. This is because Ra-Ajax is dependant upon <em>ControlState</em> which was a
        new concept in ASP.NET 2.0 that made it possible to force some values into getting written
        to the ViewState even though ViewState was turned off.
    </p>
    <h2>How does this work?</h2>
    <p>
        If you turn off the ViewState for this page, on e.g. the Page Directive, you will see that the above
        TextBox sample will still work. The reason is that we trap <em>changes when they happen</em> instead 
        of <em>diffing the incoming page against the outgoing page</em>, which is the method used by many other 
        Ajax libraries.  While many other Ajax frameworks will actually diff the content after ViewState is 
        loaded, against the content sent back to the client when the Control(s) are about to be rendered and 
        then send that "diff". Ra-Ajax intercepts when you set a value of a property, stores that 
        value and later sends it back to the client.
    </p>
    <p>
        This makes it possible in your Ra-Ajax applications to turn off ViewState and thereby save
        a lot of bandwidth.
    </p>
    <p>
        Though I must say that turning off ViewState on the entire page is not something I would
        encourage most Ajax developers to do, since the ViewState actually is a very beautiful concept
        and makes developing Ajax Applications (and "normal" web applications) in ASP.NET far easier.
    </p>
    <a href="Ajax-Accordion.aspx">On to Ajax Accordion</a>
</asp:Content>
