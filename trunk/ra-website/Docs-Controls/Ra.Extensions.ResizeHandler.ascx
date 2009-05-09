<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.ResizeHandler.ascx.cs" 
    Inherits="Docs_Controls_ResizeHandler" %>

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
    style="font-size:24px;padding:15px;display:block;"
    ID="lbl" />

<p>
    Try to resize the Browser Window...
</p>

<ext:ResizeHandler 
    runat="server" 
    OnResized="re_Resized"
    ID="res" />