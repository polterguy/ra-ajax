<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Window.ascx.cs" 
    Inherits="Docs_Controls_Window" %>

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
    Text="Show Window" 
    OnClick="btn_Click" />

<ext:Window 
    runat="server" 
    ID="wnd" 
    Visible="false"
    Caption="Window caption"
    style="position:absolute;top:250px;left:250px;z-index:500;width:250px;">

    <div style="height:150px;padding:50px;">Here's an example of a Window.</div>
    <ra:BehaviorObscurable runat="server" ID="obscurr" />

</ext:Window>