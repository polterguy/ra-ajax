<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.ExtButton.ascx.cs" 
    Inherits="Docs_Controls_ExtButton" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ext:ExtButton 
    runat="server" 
    ID="btn" 
    Text="Click me" 
    OnClick="btn_Click" />