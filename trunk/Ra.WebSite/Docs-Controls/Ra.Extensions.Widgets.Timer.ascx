<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Widgets.Timer.ascx.cs" 
    Inherits="Docs_Controls_Timer" %>

<div style="position:relative;height:270px;border:dotted 1px #999;">
<ra:Label 
    runat="server" 
    Text="Changes..."
    style="display:block;padding:5px;font-size:14px;position:absolute;"
    ID="lbl" />
</div>

<ra:Timer 
    runat="server" 
    ID="timer" 
    Duration="1000"
    OnTick="timer_Tick" />