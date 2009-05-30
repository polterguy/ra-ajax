<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.InPlaceEdit.ascx.cs" 
    Inherits="Docs_Controls_InPlaceEdit" %>

<ra:InPlaceEdit 
    runat="server" 
    ID="edit" 
    OnTextChanged="edit_TextChanged"
    Text="Click this area and edit text" />

<ra:Label 
    runat="server" 
    ID="lbl" 
    Text="&nbsp;" />