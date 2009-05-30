<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.EffectFadeOut.ascx.cs" 
    Inherits="Docs_Controls_EffectFadeOut" %>

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

<ra:Label 
    runat="server" 
    ID="lbl" 
    Text="Watch me as you click button" />

