<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.BehaviorDroppable.ascx.cs" 
    Inherits="Docs_Controls_BehaviorDroppable" %>

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
        style="position:absolute;border:dashed 2px #999;cursor:move;padding:5px;">

        <ra:Label 
            runat="server" 
            ID="lbl" 
            Text="Drop me in basket" />

        <ra:BehaviorDraggable 
            runat="server" 
            ID="draggerShopping" />

    </ra:Panel>

    <ra:Panel 
        runat="server" 
        ID="pnlDropper" 
        style="position:absolute;border:dashed 2px #999;cursor:move;top:5px;right:5px;padding:5px;">

        <ra:Label 
            runat="server" 
            ID="lbl2" 
            Text="Basket" />

        <ra:BehaviorDroppable 
            runat="server" 
            TouchedCssClass="touched"
            OnDropped="Dropped"
            ID="dropperBasket" />

    </ra:Panel>

</div>
