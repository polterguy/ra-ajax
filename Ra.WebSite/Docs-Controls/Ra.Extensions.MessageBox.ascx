<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.MessageBox.ascx.cs" 
    Inherits="Docs_Controls_MessageBox" %>

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
    Text="Changes..."
    ID="lbl" />

<ext:ExtButton 
    runat="server" 
    ID="btn" 
    OnCLick="btn_Click"
    Text="Show message box" />

<ext:MessageBox 
    runat="server" 
    ID="msg" 
    MessageBoxType="Get_Text" 
    OnClosed="msg_Closed" 
    style="position:absolute;top:250px;left:50px;z-index:500;"
    Body="What's your name cowboy?" />