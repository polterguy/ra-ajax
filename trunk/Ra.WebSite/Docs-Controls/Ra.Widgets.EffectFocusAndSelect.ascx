<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.EffectFocusAndSelect.ascx.cs" 
    Inherits="Docs_Controls_EffectFocusAndSelect" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ra:Button 
    runat="server" 
    ID="btn" 
    Text="Click me..." 
    OnClick="btn_Click" />

<ra:Panel 
    runat="server" 
    style="display:none;padding:15px;margin:15px;border:solid 1px Black;"
    ID="pnl">
    <ra:TextBox 
        runat="server" 
        ID="txt" 
        Text="To be selected..." />
</ra:Panel>

