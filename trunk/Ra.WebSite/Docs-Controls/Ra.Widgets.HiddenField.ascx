<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.HiddenField.ascx.cs" 
    Inherits="Docs_Controls_HiddenField" %>

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
    Text="Set HiddenField to DateTime.Now" 
    OnClick="btn_Click" />

<ra:HiddenField 
    runat="server" 
    Value="Not set yet...."
    ID="hid" />

<ra:Button 
    runat="server" 
    ID="btn2" 
    Text="Retrieve value of HiddenField" 
    OnClick="btn2_Click" />

