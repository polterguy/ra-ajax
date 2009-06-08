<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Widgets.ResizeHandler.ascx.cs" 
    Inherits="Docs_Controls_ResizeHandler" %>

<ra:Label 
    runat="server" 
    Text="Changes..."
    style="font-size:24px;padding:15px;display:block;"
    ID="lbl" />

<p>
    Try to resize the Browser Window...
</p>

<ra:GlobalUpdater 
    runat="server" 
    MaxOpacity="0.7" 
    Delay="300"
    style="z-index:1500;position:absolute;top:0;left:0;width:100%;height:100%;background-color:Red;color:White;font-size:48px;"
    ID="updater">
    <div style="padding-top:45%;">GlobalUpdater kicking in</div>
</ra:GlobalUpdater>

<ra:ResizeHandler 
    runat="server" 
    OnResized="re_Resized"
    ID="res" />