<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-TextArea.aspx.cs" 
    Inherits="AjaxTextArea" 
    Title="Ra-Ajax TextArea Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra Ajax Samples - TextArea</h1>
    <p>
        The <em>Ajax TextArea</em> is actually one of the places in Ra-Ajax where you will immediately feel 
        the difference between Ra-Ajax, ASP.NET AJAX and the ASP.NET WebControls. In fact, the 
        Ra-Ajax TextArea is a TextBox with the TextMode property set to MultiLine. The reason why
        we have chosen to build a special MultiLine TextArea control is because by using the same Control type
        for both rendering <em>&lt;textarea</em> and <em>&lt;input type="text"</em> ASP.NET is effectively 
        breaking <em>LSP or the Liskow Substitution Principle</em>.
    </p>
    <p>
        <ra:TextArea 
            runat="server" 
            ID="txt" 
            Columns="20" 
            Rows="5" 
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
            CssClass="updateLbl"
            style="color:#33f;" />
    </p>
    <p>
        Try to type into the Ajax TextArea above and hit Submit.
    </p>
    <p>
        Notice also how the above TextArea has the OnFocused Event handler set, which just makes sure that
        the Text in the TextArea is selected when it gets Focus. Also notice how we set Focus to the TextArea
        and select its Text when you click the Submit button. Check out this code by clicking
        "Show Code" at the top right corner of this page.
    </p>
    <h2>About LSP</h2>
    <p>
        LSP or <em>Liskow Substitution Principle</em> means that in order for
        some class A to inherit from some class B then <em>A must be a perfect superset of B</em>. The 
        classic school example of such thing is an <em>is-a relationship</em>. Like for example
        a Rectangle and a Square. 
    </p>
    <p>
        For most people, inheriting the <em>Square class</em> from the 
        <em>Rectangle type</em> would seem tempting. Though since the Rectangle has properties
        which the Square does not have (width and height) then this is, according
        to the Liskow Substitution Principle, wrong.
    </p>
    <p>
        Unfortunately ASP.NET, specially the WebControls namespace, is full of these violations of LSP.
        One example is that the <em>System.Web.UI.Page</em> class inherits indirectly from the
        <em>System.Web.UI.Control</em> class through the TemplateControl. But since Ra-Ajax tries
        to be as compatible as possible with other ASP.NET Applications, there is
        not really much that Ra-Ajax can do in regards to the <em>Page LSP problem</em>.
    </p>
    <p>
        However what we can do, is to not violate LSP in our own inheritance hierarchy. And one example of 
        that is the <em>Ajax TextArea class</em>. In fact, you can yourself
        see this problem manifested in conventional ASP.NET by dragging a <em>System.Web.UI.WebControls.TextBox</em>
        on your page, and without changing its TextMode property, set its Rows and Columns property to some integer values.
    </p>
    <p>
        The thing with the above example of an <em>&lt;input type="text"</em> with the Rows and Columns properties
        set, is that the ASP.NET runtime must either choose to ignore those properties, which is confusing for
        the developer since he doesn't understand why this is not working, or the ASP.NET runtime must choose
        to actually render them out to the browser, which will break (X)HTML compliance and render
        undefined HTML markup to the browser.
    </p>
    <p>
        To avoid the above LSP problem, we in Ra-Ajax have tried to do "the right thing", at least in those 
        places where we actually have control. And that's why you will in Ra-Ajax find a special Ajax TextArea
        Control. It might be a little bit unintuitive for those who have spent a lot of time in the WebControls
        namespace of ASP.NET, but after a while I think you will come to appreciate this decision.
    </p>
    <a href="Ajax-TextBox.aspx">On to Ajax TextBox</a>
</asp:Content>
