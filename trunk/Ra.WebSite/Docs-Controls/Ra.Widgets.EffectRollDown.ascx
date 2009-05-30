<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.EffectRollDown.ascx.cs" 
    Inherits="Docs_Controls_EffectRollDown" %>

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
    Text="Watch me as you click button..."
    style="display:none;background-color:#eee;"
    ID="lbl" />
