<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.MessageBox.ascx.cs" 
    Inherits="Docs_Controls_MessageBox" %>

<ra:Label 
    runat="server" 
    Text="Changes..."
    ID="lbl" />

<ra:ExtButton 
    runat="server" 
    ID="btn" 
    OnCLick="btn_Click"
    Text="Show message box" />

<ra:MessageBox 
    runat="server" 
    ID="msg" 
    MessageBoxType="Get_Text" 
    OnClosed="msg_Closed" 
    style="position:absolute;top:250px;left:50px;z-index:500;"
    Body="What's your name cowboy?" />