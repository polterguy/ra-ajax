<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.Panel.ascx.cs" 
    Inherits="Docs_Controls_Panel" %>

<ra:LinkButton 
    runat="server" 
    ID="lnk" 
    OnClick="lnk_Click"
    Text="Click me..." />

<ra:Panel 
    runat="server" 
    style="padding:15px;border:dashed 2px #999;background-color:#eee;"
    ID="pnl">
    This is a panel. A Panel is a "container 
    control" for other controls.
</ra:Panel>