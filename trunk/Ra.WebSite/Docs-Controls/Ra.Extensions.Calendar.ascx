<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Calendar.ascx.cs" 
    Inherits="Docs_Controls_Calendar" %>

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

<ext:Calendar 
    runat="server" 
    style="width:200px;" 
    OnSelectedValueChanged="cal_SelectedValueChanged"
    ID="cal" />