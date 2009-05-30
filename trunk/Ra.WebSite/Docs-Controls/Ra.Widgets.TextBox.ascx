<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.TextBox.ascx.cs" 
    Inherits="Docs_Controls_TextBox" %>

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

