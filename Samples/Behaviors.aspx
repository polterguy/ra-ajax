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

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

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
		Text="Hide first label" 
		OnClick="btnHide1_Click" />

	<ra:Button 
		runat="server" 
		id="btnHide2" 
		style="margin-left:100px;"
		Text="Hide second label" 
		OnClick="btnHide2_Click" />
	<div class="dragContainer">
	    <ra:Label 
	        runat="server" 
	        ID="lbl1" 
	        CssClass="dragger1"
	        Text="See me snap...">

	    	<ra:BehaviorDraggable 
	    		runat="server"
	    		ID="dragger1"
	    		OnDropped="dragger1_Dropped" />

	    </ra:Label>

	    <ra:Label 
	        runat="server" 
	        ID="lbl2"
	        CssClass="dragger2"
	        Text="Try to drag and drop me too around in the browser">

	    	<ra:BehaviorDraggable 
	    		runat="server"
	    		ID="dragger2"
	    		OnDropped="dragger2_Dropped" />

	    </ra:Label>
	    <ra:Label 
	        runat="server" 
	        ID="dropperLbl"
	        CssClass="dropper"
	        Text="Drop draggers onto me...">

	    	<ra:BehaviorDroppable 
	    		runat="server"
	    		TouchedCssClass="droppablesTouched"
	    		OnDropped="dropper_Dropped"
	    		ID="dropper" />

	    </ra:Label>
    </div>
    <div class="spacer">&nbsp;</div>

    <h2>Ra-Ajax have 100% IE support, REALLY!</h2>
    <p>
    	Though <em>our samples does not have 100% IE support</em>. This means that some of the samples will not work 100% correct
    	in neither IE6 nor IE7. This is a conscious choice we have done since history have shown us that to try to support all
    	the different quirks in IE for a waste amount of samples like we have here will first of all consume a lot of our energy
    	but also more importantly degrade the quality of our HTML and CSS. We have done most of the "easy hacks" but as you can 
    	see above the Draggable Label above does not work in IE at all. This is easy to fix by making sure that label is absolute
    	positioned, but this would degrade the quality of the HTML for all other browsers. And therefor we have choosen NOT to
    	support IE for some of our samples.
    </p>
    <p>
    	Note that all JavaScript and the core functionality of Ra-Ajax works 100% correct in all the big browsers, including 
    	IE6 and onwards. But as explained above, not all of our samples does. 
    </p>
    <br />
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
    <div style="width:200px;border:solid 1px Black;padding-top:2px;position:absolute;">
    	&nbsp;
	    <ra:Label 
	        runat="server" 
	        ID="slider"
	        style="font-size:10px;cursor:move;position:absolute;color:#33f;border:solid 1px #33f;width:25px;height:15px;-moz-user-select:none;"
	        Text="&nbsp;">

	    	<ra:BehaviorDraggable 
	    		runat="server"
	    		ID="sliderDragger"
	    		OnDropped="draggerDragger_Dropped" />

	    </ra:Label>
    </div>
    <p style="margin-top:55px;">
    	Note that the above Ajax Slider control is intentionally kept ugly to make sure the code is easy to understand and
    	may serve as a reference for others wanting to create their own Ajax Extension Controls using Ra-Ajax.
    </p>
    <br />
    <h2>Ajax Window Sample</h2>
    <p>
    	Here we have created an Ajax Window using Ajax Behaviors for references. The Ajax Window is also a perfect example of how
    	to create your own Ajax Extension Controls. It is not meant as an exhaustive Ajax Window implementation but rather a "pointing finger"
    	towards how to create your own Ajax Window. Though of course feel free to use it as it is ;)
    </p>
    <br />
    <ext:Window 
    	runat="server"
    	Caption="Ajax Window"
    	CssClass="window"
    	style="width:250px;height:250px;z-index:500;position:absolute;"
    	id="window">
    	<div style="padding:5px;">
			Here you can see an example of an Ajax Window created as a composition control utilizing Ajax Behaviors.
			<br />
			<ra:Button 
				runat="server" 
				id="btn" 
				Text="Click me" 
				OnClick="btn_Click" />
		</div>
    </ext:Window>
    <a href="Combining.aspx">On to Combining Controls</a>
</asp:Content>

