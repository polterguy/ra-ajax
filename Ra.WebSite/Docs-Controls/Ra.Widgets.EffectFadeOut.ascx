<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.EffectFadeOut.ascx.cs" 
    Inherits="Docs_Controls_EffectFadeOut" %>

<ra:Button 
    runat="server" 
    ID="btn" 
    Text="Click me..." 
    OnClick="btn_Click" />

<ra:Label 
    runat="server" 
    ID="lbl" 
    Text="Watch me as you click button" />

