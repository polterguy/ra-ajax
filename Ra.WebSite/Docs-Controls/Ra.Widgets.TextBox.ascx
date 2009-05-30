<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.TextBox.ascx.cs" 
    Inherits="Docs_Controls_TextBox" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ra:TextBox 
    runat="server" 
    ID="txt" 
    OnKeyUp="txt_KeyPress"
    Text="Type into me" />

<br />

<ra:Label 
    runat="server" 
    ID="lbl" 
    Text="Watch me..." />

