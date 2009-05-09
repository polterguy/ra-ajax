<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.BehaviorDraggable.ascx.cs" 
    Inherits="Docs_Controls_BehaviorDraggable" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<div style="position:relative;width:550px;height:350px;">

    <ra:Panel 
        runat="server" 
        ID="pnl" 
        style="position:absolute;border:dashed 2px #999;cursor:move;">

        <p style="margin:5px;">Drag me around ... :)</p>
        <ra:BehaviorDraggable runat="server" ID="dragger" />

    </ra:Panel>

</div>
