<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.BehaviorObscurable.ascx.cs" 
    Inherits="Docs_Controls_BehaviorObscurable" %>

<ra:ExtButton 
    runat="server" 
    ID="btn" 
    OnClick="btn_Click"
    Text="Show an obscured control" />

<ra:ExtButton 
    runat="server" 
    ID="btn2" 
    OnClick="btn2_Click"
    Visible="false"
    style="position:absolute;z-index:1000;"
    Text="I am obscured - click to close">

    <ra:BehaviorObscurable 
        runat="server" 
        ID="obscurer" />

</ra:ExtButton>

<ra:Label 
    runat="server" 
    ID="lblInfo" 
    Visible="false" />