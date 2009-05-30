<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.DateTimePicker.ascx.cs" 
    Inherits="Docs_Controls_DateTimePicker" %>

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

<ext:DateTimePicker 
    runat="server" 
    style="width:200px;" 
    OnSelectedValueChanged="cal_SelectedValueChanged"
    ID="cal" />