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

<ra:Label 
    runat="server" 
    Text="Changes..."
    style="display:block;padding:15px;font-size:24px;"
    ID="lbl" />

<ext:Timer 
    runat="server" 
    ID="timer" 
    Duration="1000"
    OnTick="timer_Tick" />