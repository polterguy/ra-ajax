<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Effects.EffectMove.ascx.cs" 
    Inherits="Docs_Controls_EffectMove" %>

<ra:Button 
    runat="server" 
    ID="btn" 
    Text="Click me..." 
    OnClick="btn_Click" />

<div style="position:relative;width:300px;height:50px;">
    <ra:Label 
        runat="server" 
        Text="Watch me as you click button..."
        style="position:absolute;top:35px;left:5px;"
        ID="lbl" />
</div>
