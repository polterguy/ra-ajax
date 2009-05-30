<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Calendar.ascx.cs" 
    Inherits="Docs_Controls_Calendar" %>

<ra:Label 
    runat="server" 
    Text="Changes..."
    ID="lbl" />

<ra:Calendar 
    runat="server" 
    style="width:200px;" 
    OnSelectedValueChanged="cal_SelectedValueChanged"
    ID="cal" />