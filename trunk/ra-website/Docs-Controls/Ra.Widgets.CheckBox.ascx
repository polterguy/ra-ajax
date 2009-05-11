<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.CheckBox.ascx.cs" 
    Inherits="Docs_Controls_BehaviorUpdater" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ra:CheckBox 
    runat="server" 
    ID="chk" 
    Text="Check me up..." 
    OnCheckedChanged="chk_CheckedChanged" />

