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

    <h1>Ajax TextArea Sample</h1>
    <p>
        The <em>Ajax TextArea</em> is actually one of the places in Ra-Ajax where you will immediately feel 
        that there's a difference between Ra-Ajax, ASP.NET AJAX and the WebControls namespace in ASP.NET. In fact the 
        Ra-Ajax TextArea is the TextBox type with the TextMode property set to "MultiLine". The reason why
        we have chosen to build a special MultiLine TextArea control is because by using the same Control type
        for both rendering <em>&lt;textarea</em> and <em>&lt;input type="text"</em> ASP.NET is effectively 
        breaking <em>LSP or the Liskow Substitution Principle</em>.
    </p>
    <ra:TextArea 
        runat="server" 
        ID="txt" 
        Columns="20" 
        Rows="5" 
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
        Try to type into the Ajax TextArea above and hit Submit...
    </p>
    <p>
        Notice also how the above TextArea also have the OnFocused Event handler set which just make sure that
        all text in the TextArea is selected when Focus is given to it. Also notice how we set Focus to the TextArea
        and select all text within it when you click the Submit button. See this by clicking the "Show Code" in
        the top/right corner of this page.
    </p>
    <br />
    <h2>A thing about LSP</h2>
    <p>
        LSP or <em>Liskow Substitution Principle</em> which it is also known by means that in order for
        some class A to inherit from some class B then <em>A must be a perfect superset of B</em>. The 
        classic school example of such an <em>is a relationship</em> is best constructed utilizing the sample of
        a Rectangle and a Square. 
    </p>
    <p>
        For most people inheriting the <em>Square class</em> from the 
        <em>Rectangle type</em> would seem tempting. Though since the Rectangle have properties
        which the Square does not have (hint; width and height) then this is per definition according
        to the Liskow Substitution Principle wrong.
    </p>
    <p>
        Unfortunately ASP.NET and especially the WebControls namespace is full of these "breaking LSP" concepts.
        One example is that the <em>System.Web.UI.Page</em> class inherits indirectly from the
        <em>System.Web.UI.Control</em> class through the TemplateControl. Thoughs since Ra-Ajax tries
        to be as compatible as possible in regards to other ASP.NET Applications in general there is
        not really much Ra-Ajax can do in regards to the <em>Page LSP problem</em>.
    </p>
    <p>
        However what we can do is to not have our own inheritance hierarchy go and break LSP all over
        the place. And one example of such a place is the <em>Ajax TextArea class</em>. In fact you can yourself
        see this problem manifested in conventional ASP.NET by dragging a <em>System.Web.UI.WebControls.TextBox</em>
        onto your page, and without changing its TextMode setting its Rows and Columns property to some integer values.
    </p>
    <p>
        The thing with the above example of an <em>&lt;input type="text"</em> with the Rows and Columns properties
        set is that the ASP.NET runtime must either choose to ignore those properties (which is confusing for
        the developer since he doesn't understand why this is not working) or the ASP.NET runtime must choose
        to actually render them out to the browser (which again will break (X)HTML compliance and render
        undefined HTML markup to the browser)
    </p>
    <p>
        To avoid the above LSP problem we in Ra-Ajax have tried to do "the right thing" in regards to LSP
        at least in those places where we actually have control. One example of such is the 
        <em>Ra-Ajax TextArea class</em>.
    </p>
    <a href="Ajax-TextBox.aspx">On to Ajax TextBox</a>
</asp:Content>
