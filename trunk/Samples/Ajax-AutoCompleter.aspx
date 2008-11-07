<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-AutoCompleter.aspx.cs" 
    Inherits="Samples.AjaxAutoCompleter" 
    Title="Ra-Ajax AutoCompleter Sample" %>

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

    <h1>Ra-Ajax Samples - AutoCompleter</h1>
    <p>
        Ajax AutoCompleters was probably the first "real" Ajax Controls created. The first implementation (I am aware of)
        was Thomas Aculous' AutoCompleter in ScriptAculous. This is our Ajax AutoCompleter sample.
    </p>
    <ra:Label 
        runat="server" 
        ID="lbl" 
        CssClass="infoLbl"
        Text="Watch me change..." />
    <ext:AutoCompleter 
        runat="server" 
        ID="auto" 
        CssClass="auto"
        OnAutoCompleterItemSelected="auto_AutoCompleterItemSelected"
        OnRetrieveAutoCompleterItems="auto_RetrieveAutoCompleterItems" />
    <p>
        Try to type something into the above TextBox and select an item with your mouse.
    </p>
    <p>
        All though we don't demonstrate this in the above AutoCompleter you can have any control you wish inside of
        it, you can even have controls that raises events and trap those events in your own user-code. You can
        easily add images for your AutoCompleterItems and do mostly whatever you wish. Though key navigation
        is currently a "future feature"...
    </p>
    <p>
        The items retrieved in the above AutoCompleter are also "dummy items". Normally you would use the Query
        parameter of the <em>OnRetrieveAutoCompleterItems</em> EventArgs and do something like this;
    </p>
    <p>
        <code>string.Format("select Name from Customers where Name like '%{0}'", e.Query.Replace("'", "''").Replace("\r", "").Replace("\n", ""));</code>
    </p>
    <p>
        ...to retrieve items from your database, but for the simplicity of the sample we've just created some "random items"
        as you type. Also if you query your database <strong>you should make sure you escape your SQL to
        avoid SQL Injection Attacks</strong>.
    </p>
    <p>
        You would be surprised to find out how many people actually have the 
        middle-name; <code>"'\r\n;drop database Customers;--"</code> ;)
    </p>
    <a href="Ajax-Wizard.aspx">On to AJax Wizard...</a>
</asp:Content>

