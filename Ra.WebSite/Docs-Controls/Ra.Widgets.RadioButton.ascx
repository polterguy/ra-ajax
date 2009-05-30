<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.RadioButton.ascx.cs" 
    Inherits="Docs_Controls_RadioButton" %>

<ra:Label 
    runat="server" 
    ID="lbl" 
    Text="Watch me as you select radio buttons" />

<br />

<ra:RadioButton 
    runat="server" 
    ID="rdo1" 
    OnCheckedChanged="CheckedChanged" 
    GroupName="Effect"
    Text="Highlight" />

<ra:RadioButton 
    runat="server" 
    ID="rdo2" 
    OnCheckedChanged="CheckedChanged" 
    GroupName="Effect"
    Text="FadeIn" />


