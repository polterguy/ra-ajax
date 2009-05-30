<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.SelectList.ascx.cs" 
    Inherits="Docs_Controls_SelectList" %>

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
    ID="lbl" 
    Text="Watch me as you select radio buttons" />

<br />

<ra:SelectList 
    runat="server" 
    ID="sel" 
    OnSelectedIndexChanged="SelectChanged">
    <ra:ListItem Text="Highlight" Value="Highlight" />
    <ra:ListItem Text="Fade In" Value="FadeIn" />
</ra:SelectList>