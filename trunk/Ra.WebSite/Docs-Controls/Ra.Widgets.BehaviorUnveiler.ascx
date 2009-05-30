<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.BehaviorUnveiler.ascx.cs" 
    Inherits="Docs_Controls_BehaviorUnveiler" %>

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
    ID="Label1" 
    style="opacity:0.3;font-size:18px;margin:15px;"
    Tag="div"
    Text="Watch me as you hover over me">
    <ra:BehaviorUnveiler 
        runat="server" 
        ID="BehaviorUnveiler1" />
</ra:Label>

<ra:Label 
    runat="server" 
    ID="Label2" 
    style="opacity:0.3;font-size:18px;margin:15px;"
    Tag="div"
    Text="Watch me as you hover over me">
    <ra:BehaviorUnveiler 
        runat="server" 
        ID="BehaviorUnveiler2" />
</ra:Label>

<ra:Label 
    runat="server" 
    ID="Label3" 
    style="opacity:0.3;font-size:18px;margin:15px;"
    Tag="div"
    Text="Watch me as you hover over me">
    <ra:BehaviorUnveiler 
        runat="server" 
        ID="BehaviorUnveiler3" />
</ra:Label>

<ra:Label 
    runat="server" 
    ID="Label4" 
    style="opacity:0.3;font-size:18px;margin:15px;"
    Tag="div"
    Text="Watch me as you hover over me">
    <ra:BehaviorUnveiler 
        runat="server" 
        ID="BehaviorUnveiler4" />
</ra:Label>

