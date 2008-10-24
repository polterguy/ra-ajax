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
    <ra:Label runat="server" ID="lbl" Text="Watch me change..." />
    <ext:AutoCompleter 
        runat="server" 
        ID="auto" 
        CssClass="auto"
        OnAutoCompleterItemSelected="auto_AutoCompleterItemSelected"
        OnRetrieveAutoCompleterItems="auto_RetrieveAutoCompleterItems" />
</asp:Content>

