<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Widgets.DateTimePicker.ascx.cs" 
    Inherits="Docs_Controls_DateTimePicker" %>

<ra:Label 
    runat="server" 
    Text="Changes..."
    ID="lbl" />

<ra:DateTimePicker 
    runat="server" 
    style="width:200px;" 
    OnSelectedValueChanged="cal_SelectedValueChanged"
    ID="cal" />