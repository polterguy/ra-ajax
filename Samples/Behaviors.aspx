<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Behaviors.aspx.cs" 
    Inherits="Behaviors" 
    Title="Ra-Ajax Behaviors Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra Ajax Samples - Behaviors</h1>
    <p>
    	An <em>Ajax Behavior</em> is a type of object you can attach to an existing Widget or Ajax Control which will
    	modify the <em>behavior</em> of that Ajax Control. Many Ajax Behaviors exist in Ra-Ajax like for instance the 
    	<em>BehaviorDraggable</em>, which you can see below. This Behavior makes any widget Draggable. By attaching a 
    	Behavior to an existing widget you basically modify its capabilities, like for instance when you attach the 
    	BehaviorDraggable to a Widget, that Widget becomes <em>Draggable</em> and you can drag and drop the widget 
    	around on the screen and even trap server-side events when the widget is dropped to its new position.
    </p>
    <p>
    	Try to drag and drop the Label below to see the BehaviorDraggable in action.
    </p>

	<ra:Button 
		runat="server" 
		id="btnHide1" 
		style="position:absolute;top:500px;left:350px;"
		Text="Hide first label" 
		OnClick="btnHide1_Click" />

	<ra:Button 
		runat="server" 
		id="btnHide2" 
		style="position:absolute;top:500px;left:550px;"
		Text="Hide second label" 
		OnClick="btnHide2_Click" />

    <ra:Label 
        runat="server" 
        ID="lbl1"
        style="cursor:move;position:absolute;top:550px;left:350px;color:#33f;border:solid 1px #33f;padding:5px;width:100px;"
        Text="Try to drag and drop me around in the browser">

    	<ra:BehaviorDraggable 
    		runat="server"
    		ID="dragger1" 
    		OnDropped="dragger1_Dropped" />

    </ra:Label>

    <ra:Label 
        runat="server" 
        ID="lbl2"
        style="cursor:move;position:absolute;top:550px;left:550px;color:#33f;border:solid 1px #33f;padding:5px;width:100px;"
        Text="Try to drag and drop me too around in the browser">

    	<ra:BehaviorDraggable 
    		runat="server"
    		ID="dragger2"
    		OnDropped="dragger2_Dropped" />

    </ra:Label>
    <div style="height:200px;">&nbsp;</div>
    <h2>Ajax Behaviors is a BIG gun!</h2>
    <p>
    	Note though that all though this gives you a LOT of power it is very easy to overdo Ajax Behaviors by adding them in
    	places where they either make no sense or they break the normal expected logic of a webpage. Like for instance I would
    	be careful with adding up BehaviorDraggable to an Ajax Button since that would probably to such a large degree change
    	the "expected behavior" for that button that your users would be confused when trying to interact with that button etc...
    </p>
    <p>
    	Ajax Behaviors is a BIG weapon and like all big weapons it should be handled with care and not misused since then it
    	has the nasty habit of doing bad things towards your feet ;)
    </p>
    <br />
    <h2>Create even richer Ajax Controls by combining Behaviors</h2>
    <p>
    	Below you can see an example of an Ajax Slider. This slider doesn't really exist in Ra-Ajax but has been created
    	by combining a couple of controls with some Ajax Behaviors.
    </p>
    <br />
    <div style="width:200px;border:solid 1px Black;">
    	&nbsp;
	    <ra:Label 
	        runat="server" 
	        ID="slider"
	        style="font-size:10px;cursor:move;position:absolute;top:1031px;left:328px;color:#33f;border:solid 1px #33f;width:25px;height:15px;"
	        Text="&nbsp;">

	    	<ra:BehaviorDraggable 
	    		runat="server"
	    		ID="sliderDragger"
	    		OnDropped="draggerDragger_Dropped" />

	    </ra:Label>
    </div>
    <br />
    <a href="Combining.aspx">On to Combining Controls</a>
</asp:Content>

