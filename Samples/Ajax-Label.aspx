<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Label.aspx.cs" 
    Inherits="AjaxLabel" 
    Title="Ajax Label Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ajax Label Sample</h1>
    <p>
        An <em>Ajax Label</em> is basically a wrapper around a <em>&lt;span...</em> element. And all though
        you have seen Ajax Labels in many of the samples so far, I will still like to give you a 
        <em>reference example of an Ajax Label</em> so that you can have one place to go to for certainty
        when wondering about how the Ajax Label works.
    </p>
    <ra:Label 
        runat="server" 
        ID="lbl" 
        Text="This is an Ajax Label" 
        style="color:#999;font-style:italic;" />
    <br />
    <ra:Button 
        runat="server" 
        ID="btn" 
        Text="Click me" 
        OnClick="btn_Click" />
    <p>
        Try to click the above Button and see the Label update.
    </p>
    <p>
        The Ajax Label is a "passive" control which means it doesn't have the possibility of creating
        Ajax Requests itself. Though from any other Ajax Request like the one from the Button above
        you can change ALL the properties for the Label as you wish. In the above example however we're
        just changing the Text property of the Label and then toggling between bold and normal font-weight.
    </p>
    <p>
        The Ajax Label is convenient if you have some static text which you wish to change on your page.
    </p>
    <br />
    <h2>Ra-Ajax renders in-visible controls IN-VISIBLE</h2>
    <p>
        Ra-Ajax has a very nice feature which is borrowed from <a href="http://anthemdotnet.com/">Anthem.NET</a>.
        This is the fact that if an Ajax Control like the Ajax Label below is rendered IN-Visible then that label
        will not occupy any space at all except for a "generic" <em>&lt;span tag</em> which will be used to
        know which place to render the Control inside of the browser in case the Control is later being made 
        Visible. The same type of <em>span tag</em> will however be rendered for ALL Ajax Controls, including
        the <a href="Ajax-Image.aspx">Ajax Image</a>, the <a href="Ajax-CheckBox.aspx">Ajax CheckBox</a> and so
        on. This is just a "positioning element" which Ra-Ajax needs in order to know *where* on the page it 
        should render the Control IF it is being made visible.
    </p>
    <ra:Label 
        runat="server" 
        ID="lbl2" 
        Visible="false" 
        Text="This text is IN-Visible" />
    <br />
    <ra:Button 
        runat="server" 
        ID="btn2" 
        Text="Click me" 
        OnClick="btn2_Click" />
    <br />
    <p>
        If you try to click the button above you will see that the Label is being made Visible and the content
        of it shown in the browser. However if you try to view the source of the HTML page you will notice
        that the HTML content of that label is *NOT* there!
    </p>
    <p>
        This is first of all a very handsome optimalization in Ra-Ajax in regards to bandwidth and resource usage.
        First of all imagine if the above Ajax Label contained "the complete works of William Shakespear".
        Then in another solution which didn't do things the way Ra-Ajax does it even though the Label was 
        IN-Visible it would still make the webpage become several gigabytes in size. This would mean that we 
        would be wasting a LOT of bandwidth for something the user may not ever be interested in seeing!
    </p>
    <p>
        Second of all as we discussed in our <a href="Ajax-Image.aspx">Ajax Image sample</a> this is a *security*
        thing. Imagine if that was the complete patient record for some patient at the local hospital.
    </p>
    <p>
        This goes for ALL controls in Ra-Ajax. So even if you have a Panel containing 50 GridViews and Repeaters
        with 500 records each then as long as the Panel itself was in-visible then you would have nothing more 
        rendered on that page except for an in-visible span which served as a placeholder for that entire thing...
    </p>
    <a href="Ajax-LinkButton.aspx">On to Ajax LinkButton</a>
</asp:Content>

