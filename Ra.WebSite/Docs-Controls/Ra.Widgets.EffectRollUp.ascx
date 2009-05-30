<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.EffectRollUp.ascx.cs" 
    Inherits="Docs_Controls_EffectRollUp" %>

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
