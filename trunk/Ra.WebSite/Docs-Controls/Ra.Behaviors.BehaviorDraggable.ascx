<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Behaviors.BehaviorDraggable.ascx.cs" 
    Inherits="Docs_Controls_BehaviorDraggable" %>

<div style="position:relative;width:550px;height:350px;">

    <ra:Button 
        runat="server" 
        ID="toggleBehavior" 
        Text="Disable Behavior" 
        style="margin:5px;"
        OnClick="toggleBehavior_Click" />
    <br />
    <ra:Panel 
        runat="server" 
        ID="pnl" 
        style="position:absolute;border:dashed 2px #999;cursor:move;">

        <p style="margin:5px;">Drag me around ... :)</p>
        <ra:BehaviorDraggable runat="server" ID="dragger" />
        
    </ra:Panel>

</div>
