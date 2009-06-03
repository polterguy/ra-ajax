<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Effects.EffectFadeIn.ascx.cs" 
    Inherits="Docs_Controls_EffectFadeIn" %>

<ra:Button 
    runat="server" 
    ID="btn" 
    Text="Click me..." 
    OnClick="btn_Click" />

<ra:Label 
    runat="server" 
    ID="lbl" 
    style="display:none;"
    Text="Watch me as you click button" />

