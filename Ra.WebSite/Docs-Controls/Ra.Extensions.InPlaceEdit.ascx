<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.InPlaceEdit.ascx.cs" 
    Inherits="Docs_Controls_InPlaceEdit" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ext:InPlaceEdit 
    runat="server" 
    ID="edit" 
    OnTextChanged="edit_TextChanged"
    Text="Click this area and edit text" />

<ra:Label 
    runat="server" 
    ID="lbl" 
    Text="&nbsp;" />