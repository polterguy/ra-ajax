<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.EffectHighlight.ascx.cs" 
    Inherits="Docs_Controls_EffectHighlight" %>

<ra:Button 
    runat="server" 
    ID="btn" 
    Text="Click me..." 
    OnClick="btn_Click" />

<ra:Label 
    runat="server" 
    Text="Watch me as you click button..."
    ID="lbl" />

