<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Behaviors.aspx.cs" 
    Inherits="Behaviors" 
    Title="Ra-Ajax Behaviors" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra Ajax Samples - Ajax Behaviors</h1>
    <p>
    	An <em>Ajax Behavior</em> is a type of object you can attach to an existing Widget or Ajax Control which will
    	modify the <em>"behavior"</em> of the Ajax Control. Many Ajax Behaviors exists in Ra-Ajax like for instance the 
    	<em>BehaviorDraggable</em> which you can see below which makes any widget Draggable. By attaching a Behavior to
    	an existing widget you basically modify its capabilities like for instance when you attach the BehaviorDraggable
    	to the Widget the Widget becomes <em>draggable</em> and you can drag and drop the widget around on the screen
    	and even trap server-side events when the widget is dropped in its new position.
    </p>
    <p>
    	Try to drag and drop the Label below to see the BehaviorDraggable in action.
    </p>

    <ra:Label 
        runat="server" 
        ID="lbl"
        style="position:absolute;top:500px;left:350px;font-style:italic;color:#999;border:solid 1px #ddd;padding:5px;width:100px;text-align:left;"
        Text="Try to drag and drop me around in the browser">

    	<ra:BehaviorDraggable 
    		runat="server"
    		ID="dragger"
    		OnDropped="dragger_Dropped" />

    </ra:Label>

    <div style="height:200px;">&nbsp;</div>
	<br />
    <a href="Combining.aspx">On to "Combining Controls"...</a>
</asp:Content>

