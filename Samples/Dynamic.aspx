<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Dynamic.aspx.cs" 
    Inherits="Dynamic" 
    Title="Dynamic Ajax Controls" %>

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
    runat="server">

    <h1>Ra Ajax Samples - Dynamic Ajax Controls</h1>
    <p>
        Often you will want to build up the Ajax Controls on your page <em>dynamically</em>. For 
        such circumstances you would want to use the <em>ReRender</em> method. For such circumstances
        you would normally wrap an Ajax Panel around the surface you wish to dynamically update the 
        contents of, and then when you have exchanged the controls of that surface call the <em>"ReRender"</em>
        method.
    </p>
    <p>
        The DropDownList below decides which content the Ajax Panel underneath should have, and then
        therefor the "type of control". When you change the value of the Ajax DropDownList you will 
        see that also the content of the panel is changed to a different set of controls.
    </p>
    <ra:DropDownList 
        runat="server" 
        ID="dropper" 
        OnSelectedIndexChanged="dropper_SelectedIndexChanged">
        <ra:ListItem Text="Name and Age retriever" Value="Combining" />
        <ra:ListItem Text="Hello World" Value="HelloWorld" />
        <ra:ListItem Text="Custom" Value="custom" />
    </ra:DropDownList>

    <ra:Panel 
        runat="server" 
        style="background-color:Yellow;border:solid 1px Black;padding:15px;height:100px;margin:15px;"
        ID="pnlDynamicControls" />
    <br />
    <h2>Dynamic Ajax Controls in use</h2>
    <p>
        Of course, the above example is very trivial. It is intentionally made that way for simplicity. But
        I hope you can understand the power of being able to dynamically load Ajax Controls in Ajax 
        Callbacks and what that feature may do in regards to assisting you in building up your content.
        You could instead of loading UserControls, load any controls you wish and 
        have any event handlers you wish for them and in fact the third selection of the DropDownList 
        does just that.
    </p>
    <p>
        Also if you look at the above three different "control collections" you will notice that they 
        all carry different logic in addition to difference in rendering. Also everything is, as usual,
        done purely on the server with no need to resort to JavaScript.
    </p>
    <br />
    <h2>Different scenarios for usage</h2>
    <p>
        Here is a couple of examples of where this feature might come in handy.
    </p>
    <ul>
        <li>Wizard Controls</li>
        <li>Settings Page(s)</li>
        <li>Different UI for different User Roles</li>
        <li>Different Content for Different Agents</li>
        <li>Different "modes" of your application (edit and preview mode)</li>
        <li>Generic content built from e.g. a database settings collection, XML file etc</li>
        <li>etc...</li>
    </ul>
    <p>
        The whole secret is just understanding when to call the <em>ReRender method</em>. Use the ReRender
        only when you are deleting, changing or adding to the Control Collection of your Ajax Controls.
    </p>
    <br />
    <h2>When to use the ReRender Method</h2>
    <p>
        If you look at the source code for this page you will see that we are not using ReRender
        <em>"everytime we reload the controls"</em> but in fact only when we change the controls inside 
        of the panel. This is very crucial, since if you don't follow this rule you will end up wasting a 
        lot of bandwidth and might get into obscure bugs and so on.
    </p>
    <br />
    <h2>Important to remember about Dynamic Controls</h2>
    <p>
        First of all always make sure you re-create the <em>exact same set of controls</em> on every single 
        request after you have initially created them. In this page we're doing this by storing a value
        in the ViewState which says "which specific control" we have created and then in the Page_Load
        we check to see which "type of control" to re-create.
    </p>
    <p> 
        Intuitively for a sample which tries to
        do something similar as this webpage, most people would think that they can just use the 
        value of the DropDownList. This will not work correctly since when the user changes
        the control by selecting a new item in the DropDownList the <em>new control</em> will be loaded in
        the Page_Load though it shouldn't be created before the SelectedIndexChanged Event Handler
        of the DropDownList. Therefor we use the ViewState.
    </p>
    <p>
        Never call the <em>ReRender</em> method unless you are changing the controls in your Panel
        or whatever control you're using as the Parent Control. If you do, you will use way more
        bandwidth than you should. Always call <em>ReRender</em> when you have changed the 
        <em>Controls Collection</em> of your Parent Control. Unless you do this, none of your new 
        controls will get rendered back to the client and you will get "funny" bugs.
    </p>
    <p>
        Below is the important code to notice in this example, stripped-down as "boilerplate code" so
        that you can see the dampened solution alone.
    </p>
    <pre>
// To re-create already created controls
protected void Page_Load(object sender, EventArgs e)
{
    if (ViewState["control"].Equals("custom"))
    {
        LoadCustomControls();
    }
    else
    {
        System.Web.UI.Control ctrl = 
            LoadControl(ViewState["control"].ToString() + ".ascx");
        pnlDynamicControls.Controls.Add(ctrl);
    }
}
    </pre>
    <pre>
// To load up a NEW control collection into the Parent Control
protected void dropper_SelectedIndexChanged(object sender, EventArgs e)
{
    // Must remove the "old" collection of controls from the Control
    pnlDynamicControls.Controls.Clear();
    if (dropper.SelectedItem.Value == "custom")
    {
        LoadCustomControls();
    }
    else
    {
        System.Web.UI.Control ctrl = 
            LoadControl(dropper.SelectedItem.Value + ".ascx");
        pnlDynamicControls.Controls.Add(ctrl);
    }

    // Making sure the Panel is re-rendered since otherwise the controls
    // will not be rendered to the client or obscure bugs will show up...
    pnlDynamicControls.ReRender();

    // Making sure we store "which type of control(s)" we have loaded 
    // into our "Parent Control"
    ViewState["control"] = dropper.SelectedItem.Value;
}
    </pre>
    <br />
    <h2>HTTP is stateless</h2>
    <p>
        To Windows Forms, GTK and Qt developers beginning ASP.NET, the fact that we need to "re-create" 
        the controls on every single postback/callback might seem unintuitive
        but remember that HTTP is a stateless protocol. This means that when the server
        is finished serving your request it is gone forever. Even the fact that you have
        ever been to the server at all is "gone". This means that the page itself must
        remember "which controls" to load. Normally for static controls the .aspx file
        will handle this for you through its "magic". But for Dynamically Created Controls
        it has no mechanism to know "which controls to reload". This means you must take
        care of which controls to reload.
    </p>
    <p>
        This is not unique for Ra-Ajax but rather a general problem for all applications
        built on top of ASP.NET and in fact the HTTP protocol too. But for some obscure 
        reason it seems to show up a lot more often in Ajax Frameworks than in "normal
        conventional" ASP.NET applications. I guess with power, comes the lust to do more ;)
    </p>
    <p>
        If you are stuck with problems regarding Dynamically Created Ra-Ajax Controls then
        please make sure to read and understand what's said here, since chances are that
        you are doing something wrong when creating them, re-creating them, deleting the
        "old ones" or calling ReRender. Then if you are still stuck, feel free to post
        a question in our <a href="http://ra-ajax.org/Forums/Forums.aspx">forums</a> asking 
        for help :)
    </p>
    <a href="Ajax-Comet.aspx">On to Ajax Comet Sample</a>
</asp:Content>

