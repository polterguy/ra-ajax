<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Behaviors.aspx.cs" 
    Inherits="Samples.Behaviors" 
    Title="Ajax Behaviors" %>

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

    <h1>Ajax Behaviors</h1>
    <p>
    	An <em>Ajax Behavior</em> is a type you can attach to existing Widgets which will
    	modify the <em>behavior</em> of that widget. The two blue rectangles below are actually just normal
    	Panels which are modified with the <em>BehaviorDraggable</em> so that you can drag them around
    	on screen.
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
    <p>
        The gray rectangle to the right is modified with the <em>BehaviorDroppable</em> so that it will
        be a "drop point" for your draggable panels.
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
    <h2>Ra-Ajax respects HTML</h2>
    <p>
        Many Ajax Libraries are creating their widgets through JavaScript. When you look at the HTML source
        of Ajax libraries like that you will see one or two &lt;span&gt; elements. Little or none of the text
        you are physically seeing in your page will be found in the HTML of your page. The problem with such an 
        approach is that Google and other Search Engines will also see nothing but these empty spans.
    </p>
    <p>
        This makes such Ajax libraries unsuitable for Search Engine Friendly Applications. With 
        Ra-Ajax you can easily create really Rich Internet Applications and still have everything be 100% 
        visible for Search Engines.
    </p>
    <p>
        If you click <em>"View Source"</em> for this page you will see all the HTML for the Accordion to the right,
        the Window at the top and the draggable labels. Very few Ajax Libraries does this in fact! Most popular 
        Ajax Libraries will create widgets through sending in HTML text as JavaScript - which makes all Search Engines
        choke and not understand what your page is about.
    </p>
    <p>
        This makes you loose a lot of potential customers since Google will send you fewer visitors, in addition to 
        making it much harder for screen-readers and accessibility.
    </p>
    <p>
        When you combine that with the fact that Ra-Ajax has about 10 KB of JavaScript in total, you 
        realize that with Ra-Ajax you can actually create front websites which will completely outperform
        your competitors in both visibility and performance!
    </p>
    <a href="Combining.aspx">On to Combining Controls</a>
</asp:Content>

