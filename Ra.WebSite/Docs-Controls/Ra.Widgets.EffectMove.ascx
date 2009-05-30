<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.EffectMove.ascx.cs" 
    Inherits="Docs_Controls_EffectMove" %>

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

<div style="position:relative;width:300px;height:50px;">
    <ra:Label 
        runat="server" 
        Text="Watch me as you click button..."
        style="position:absolute;top:35px;left:5px;"
        ID="lbl" />
</div>
