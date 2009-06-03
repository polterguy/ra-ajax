<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Effects.EffectTimeout.ascx.cs" 
    Inherits="Docs_Controls_EffectTimeout" %>

<ra:Button 
    runat="server" 
    ID="btn" 
    Text="Click me..." 
    OnClick="btn_Click" />

<ra:Label 
    runat="server" 
    Text="Watch me as you click button..."
    style="background-color:#eee;display:block;"
    ID="lbl" />
