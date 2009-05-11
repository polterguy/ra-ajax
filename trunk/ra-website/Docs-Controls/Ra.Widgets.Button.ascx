<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.Button.ascx.cs" 
    Inherits="Docs_Controls_BehaviorUpdater" %>

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
