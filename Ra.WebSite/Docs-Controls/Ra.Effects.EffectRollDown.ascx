<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Effects.EffectRollDown.ascx.cs" 
    Inherits="Docs_Controls_EffectRollDown" %>

<ra:Button 
    runat="server" 
    ID="btn" 
    Text="Click me..." 
    OnClick="btn_Click" />

<ra:Label 
    runat="server" 
    Text="Watch me as you click button..."
    style="display:none;background-color:#eee;overflow-x:auto;overflow-y:visible;"
    ID="lbl" />
