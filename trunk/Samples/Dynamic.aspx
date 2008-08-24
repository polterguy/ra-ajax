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

    <h1>Dynamic Ajax Controls</h1>
    <p>
        Often you will want to build up the Ajax Controls on your page <em>dynamically</em>. For 
        such circumstances you would want to use the <em>ReRender</em> method. For such circumstances
        you would normally wrap an Ajax Panel around the surface you wish to dynamically update the 
        contents of and then when you have exchanged the controls of that surface call the <em>"ReRender"</em>
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
        Of course the above example is just pure rubbish intentionally made that way for simplicity. But
        I hope you can understand the power of being able to dynamically load Ajax Controls in Ajax 
        Callbacks and what that feature may do in regards to assisting you building up your content.
        You could instead of loading UserControls of course also load up any controls you wish and 
        have any event handlers you wish for them and in fact the third selection of the DropDownList 
        does just that.
    </p>
    <p>
        Also if you look at the above three different "control collections" you will notice that they 
        all carry different logic in addition to difference in rendering. Also everything is as normal
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
        <li>etc...</li>
    </ul>
    <p>
        The whole secret is just understanding *WHEN* to call the <em>ReRender method</em>. Use the ReRender
        ONLY when you are deleting, changing or adding to the Control Collection of your Ajax Controls.
    </p>
    <br />
    <h2>When to use the ReRender Method</h2>
    <p>
        If you look at the source code for this page you will for instance see that we are NOT using it 
        <em>"everytime we reload the controls"</em> but in fact ONLY when we CHANGE the controls inside 
        of the panel. This is *crucial* since if you don't follow this rule you will end up wasting a 
        lot of bandwidth and might get into obscure bugs and so on.
    </p>
</asp:Content>

