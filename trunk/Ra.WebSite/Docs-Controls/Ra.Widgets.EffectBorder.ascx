<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.EffectBorder.ascx.cs" 
    Inherits="Docs_Controls_EffectBorder" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ra:Label 
    runat="server" 
    ID="lbl" 
    style="padding:15px;margin:15px;"
    Tag="div"
    Text="Watch me as you click button" />

<ra:Button 
    runat="server" 
    ID="btn" 
    Text="Click me..." 
    OnClick="btn_Click" />
