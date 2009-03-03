<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Label.aspx.cs" 
    Inherits="Samples.AjaxLabel" 
    Title="Ra-Ajax Label Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra-Ajax Samples - Label</h1>
    <p>
        An <em>Ajax Label</em> is basically a wrapper around a <em>span</em> element. And although
        you have seen Ajax Labels in many of the samples so far, I still want to give you a 
        <em>reference example of an Ajax Label</em> so that you can have one place to go to for certainty
        when wondering about how the Ajax Label works.
    </p>
    <p>
        <ra:Label 
            runat="server" 
            ID="lbl" 
            Text="This is an Ajax Label" 
            CssClass="updateLbl" />
    </p>
    <p>
        <ra:Button 
            runat="server" 
            ID="btn" 
            Text="Click me" 
            OnClick="btn_Click" />
    </p>
    <p>
        Try to click the above Button and see the Label update.
    </p>
    <p>
        The Ajax Label is a "passive" control which means it doesn't have the possibility of initiating
        Ajax Requests itself. Though from any other Ajax Request, like the one initiated by clicking the Button above,
        you can change all the properties of the Label as you wish. In the above example however, we're
        just changing the Text property of the Label and then toggling between bold and normal font-weight.
    </p>
    <p>
        The Ajax Label is convenient if you have some static text on your page that you want to change later.
    </p>
    <h2>Ra-Ajax Renders Invisible Controls as Invisible</h2>
    <p>
        Ra-Ajax has a very nice feature that is borrowed from <a href="http://anthem-dot-net.sourceforge.net/">Anthem.NET</a>.
        If an Ajax Control, like the Ajax Label below, is rendered invisible, then that label
        will not occupy any space at all in the DOM, except for a "generic" <em>span element</em>, which is used as 
        a placeholder. This span serves the purpose of knowing exactly where in the DOM we should render the Control 
        in case it becomes Visible. The same technique is used for all Ajax Controls, including the 
        <a href="Ajax-Image.aspx">Ajax Image</a>, the <a href="Ajax-CheckBox.aspx">Ajax CheckBox</a> 
        and so on.
    </p>
    <p>
        <ra:Label 
            runat="server" 
            ID="lbl2" 
            Visible="false" 
            Text="This text is initially invisible" />
    </p>
    <p>
        <ra:Button 
            runat="server" 
            ID="btn2" 
            Text="Click me" 
            OnClick="btn2_Click" />
    </p>
    <p>
        If you try to click the button above, you will see that the Label becomes Visible and its content
        is shown in the browser. However, if you try to view the source of the HTML page you will notice
        that the HTML content of that label is not there!
    </p>
    <p>
        This is a very clever optimization technique in Ra-Ajax in regards to bandwidth and resource usage.
        First of all, imagine if the above Ajax Label contained the complete works of William Shakespeare.
        If this was written using another Ajax framework that does not use this optimization technique, even though 
        the Label might be initially invisible, it would still make the webpage very huge in size. This would mean that we 
        would be wasting a lot of bandwidth for something the users may never be interested in seeing!
    </p>
    <p>
        Secondly, as we discussed in our <a href="Ajax-Image.aspx">Ajax Image sample</a>, this is a security
        feature. Imagine if that Label's content was the patient record for some patient at the local hospital.
    </p>
    <p>
        This goes for all controls in Ra-Ajax. So even if you have a Panel containing 50 GridViews and Repeaters
        with 500 records each, then as long as the Panel itself was invisible, you would have nothing more 
        rendered on that page except for an invisible span which serves as a placeholder for that entire thing.
    </p>
    <a href="Ajax-LinkButton.aspx">On to Ajax LinkButton</a>
</asp:Content>

