<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Timer.ascx.cs" 
    Inherits="Docs_Controls_Timer" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<div style="position:relative;height:270px;border:dotted 1px #999;">
<ra:Label 
    runat="server" 
    Text="Changes..."
    style="display:block;padding:5px;font-size:14px;position:absolute;"
    ID="lbl" />
</div>

<ext:Timer 
    runat="server" 
    ID="timer" 
    Duration="1000"
    OnTick="timer_Tick" />