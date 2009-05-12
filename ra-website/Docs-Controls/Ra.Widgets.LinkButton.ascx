<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.LinkButton.ascx.cs" 
    Inherits="Docs_Controls_LinkButton" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ra:LinkButton 
    runat="server" 
    ID="lnk" 
    OnClick="lnk_Click"
    Text="Click me..." />

