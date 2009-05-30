<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.BehaviorObscurable.ascx.cs" 
    Inherits="Docs_Controls_BehaviorObscurable" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ext:ExtButton 
    runat="server" 
    ID="btn" 
    OnClick="btn_Click"
    Text="Show an obscured control" />

<ext:ExtButton 
    runat="server" 
    ID="btn2" 
    OnClick="btn2_Click"
    Visible="false"
    style="position:absolute;z-index:1000;"
    Text="I am obscured - click to close">

    <ra:BehaviorObscurable 
        runat="server" 
        ID="obscurer" />

</ext:ExtButton>

<ra:Label 
    runat="server" 
    ID="lblInfo" 
    Visible="false" />