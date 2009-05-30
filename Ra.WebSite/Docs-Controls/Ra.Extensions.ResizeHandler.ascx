<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.ResizeHandler.ascx.cs" 
    Inherits="Docs_Controls_ResizeHandler" %>

<ra:Label 
    runat="server" 
    Text="Changes..."
    style="font-size:24px;padding:15px;display:block;"
    ID="lbl" />

<p>
    Try to resize the Browser Window...
</p>

<ra:ResizeHandler 
    runat="server" 
    OnResized="re_Resized"
    ID="res" />