<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Effects.EffectFadeOut.ascx.cs" 
    Inherits="Docs_Controls_EffectFadeOut" %>

<ra:Button 
    runat="server" 
    ID="btn" 
    Text="Click me..." 
    OnClick="btn_Click" />

<ra:Panel 
    runat="server" 
    ID="lbl">
    Watch me as you click button
</ra:Panel>

