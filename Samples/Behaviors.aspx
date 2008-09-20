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
    	modify the <em>behavior</em> of that Ajax Control. Many different Ajax Behaviors exists in Ra-Ajax like for 
    	instance the <em>BehaviorDraggable</em>, which you can see below. This Behavior makes any widget Draggable. 
    	By attaching a Behavior to an existing widget you basically modify its capabilities. For instance when 
    	you attach the BehaviorDraggable to a Widget, that Widget becomes <em>Draggable</em> and you can drag and drop 
    	the widget around on the screen and even trap server-side events when the widget is dropped to its new position.
    	Where you of course have access to its new position in that Event Handler in your C# server-side code.
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

    <h2>Ajax Behaviors is a BIG gun!</h2>
    <p>
    	All though this gives you a lot of power it is very easy to overdo Ajax Behaviors by adding them in
    	places where they either make no sense or they break the normal expected logic of a webpage. Like for instance I would
    	be careful with adding up BehaviorDraggable to an Ajax Button since that would probably to such a large degree change
    	the "expected behavior" for that button that your users would be confused when trying to interact with that button etc...
    </p>
    <p>
    	Ajax Behaviors is a BIG weapon and like all big weapons it should be handled with care and not misused since then it
    	has the nasty habit of doing bad things towards your feet ;)
    </p>
    <h2>Create even richer Ajax Controls by combining Behaviors</h2>
    <p>
    	Below you can see an example of an Ajax Slider. This slider doesn't really exist in Ra-Ajax but has been created
    	by combining a couple of controls with some Ajax Behaviors.
    </p>
    <div class="slider">
	    <ra:Label 
	        runat="server" 
	        ID="slider"
	        CssClass="slidThumb"
	        Text="&nbsp;">

	    	<ra:BehaviorDraggable 
	    		runat="server"
	    		ID="sliderDragger"
	    		OnDropped="draggerDragger_Dropped" />

	    </ra:Label>
    </div>
    <div class="spacerSmall">&nbsp;</div>
    <p>
    	Note that the above Ajax Slider control is intentionally kept ugly to make sure the code is easy to understand and
    	may serve as a reference for others wanting to create their own Ajax Extension Controls using Ra-Ajax.
    </p>
    <h2>Ajax Window Sample</h2>
    <p>
    	Here we have created an Ajax Window using Ajax Behaviors for references. The Ajax Window is also a perfect example of how
    	to create your own Ajax Extension Controls. It is not meant as an exhaustive Ajax Window implementation but rather a "pointing finger"
    	towards how to create your own Ajax Window. Though of course feel free to use it as it is...
    </p>
    <ext:Window 
    	runat="server"
    	Caption="Ajax Window"
    	CssClass="window smallWnd"
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
    <p>
    	This Window is purely built on the server with 84 lines of code in total (as of the 18th of September 2008) and has
    	no Custom JavaScript in it at all. In fact the Window is created using these Ajax Controls. 
    </p>
    <ul class="bulList">
    	<li>ASP.NET Panel</li>
    	<li>Ra-Ajax LinkButton</li>
    	<li>Ra-Ajax Label</li>
    	<li>BehaviorDraggable</li>
    </ul>
    <p>
    	And by combining those "building block controls" we're able to create a relatively advanced Ajax Window
    	without having to resort to JavaScript at all. This is a general pattern in Ra-Ajax in fact - You
    	can always build fairly complex Ajax Controls yourself by combining the existing "less complex" Controls 
    	in Ra-Ajax together to form your complex Ajax Control.
    </p>
    <a href="Combining.aspx">On to Combining Controls</a>
</asp:Content>

