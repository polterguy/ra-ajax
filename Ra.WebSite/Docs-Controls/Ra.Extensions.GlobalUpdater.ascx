<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.GlobalUpdater.ascx.cs" 
    Inherits="Docs_Controls_Accordion" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ext:GlobalUpdater 
    runat="server" 
    MaxOpacity="0.7" 
    Delay="500"
    style="z-index:1500;position:absolute;top:0;left:0;width:100%;height:100%;background-color:Red;color:White;font-size:48px;"
    ID="updater">
    <div style="padding-top:45%;">GlobalUpdater kicking in</div>
</ext:GlobalUpdater>

<ra:Button 
    runat="server" 
    ID="btn" 
    Text="Click me to show updater" 
    OnClick="btn_Click" />

<ra:Label 
    runat="server" 
    ID="lbl" 
    Text="SLOW click..." />