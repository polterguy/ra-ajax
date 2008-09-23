<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Window.aspx.cs" 
    Inherits="AjaxWindow" 
    Title="Ra-Ajax Window Sample" %>

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

    <h1>Ajax Window Sample</h1>
    <p>
    	Ra-Ajax have two different Window Controls. One which is called <em>WindowFull</em> and another one which is
    	called <em>WindowLight</em>. The first one is very rich in its UI and probably due to the amount of "bling" the
    	one most would choose to use. The latter one is far easier on the DOM, CSS and browsers and might therefor
    	be more suitable when you need something ultra-fast with no images and a small amount of DOM nodes.
    </p>
    <p>
        <ra:Button 
    	    runat="server" 
    	    id="showWindow" 
    	    Text="Show Window" 
    	    OnClick="showWindow_Click" />
    </p>

    <ext:WindowFull 
	    runat="server"
	    Caption="Ajax Window - drag me"
	    CssClass="alphacube"
	    OnClosed="window_Closed"
	    style="position:absolute;"
	    id="window">

	    <div style="padding:0 15px 5px 15px;">
	        <h3>Window "Full"</h3>
	        <p>
	            This is our "full" window. It can basically be thought of as an advanced panel. 
	            Or a "richer panel".
	        </p>
	        <p>
	            <ra:Button 
	                runat="server" 
	                ID="animate" 
	                OnClick="animate_Click"
	                Text="Animate window" />
	        </p>
	        <p>
	            Note however that this is a "CSS &amp; DOM intensive Window" and for a lighter window
	            you might want to consider the WindowLight which is showcased below and also
	            quite more "easy" on crappy browsers. (say no names ;)
	        </p>
	        <p>
	            <ra:Button 
	                runat="server" 
	                ID="btnOpen" 
	                OnClick="btnOpen_Click"
	                Text="Open another Window" />
	        </p>
	    </div>

	</ext:WindowFull>

    <div style="position:absolute;">
        <ext:WindowFull 
	        runat="server"
	        Caption="Ajax Window - drag me"
	        CssClass="alphacube"
	        Visible="false"
	        style="position:absolute;top:-50px;left:100px;width:250px;height:200px;"
	        id="window2">

	        <div style="padding:0 15px 5px 15px;">
	            <h3>Another Window</h3>
	            <p>
	                This window is initially in-visible. Notice that as all in-visible controls almost no markup, and
	                no content is being added to the DOM or HTML before the Window is being made Visible.
	            </p>
	        </div>

	    </ext:WindowFull>
	</div>

    <div class="spacerLarge">&nbsp;</div>
    <p>
    	Try to move the Ajax Window by dragging and dropping its header.
    </p>
    <h2>Differences between WindowFull and WindowLight</h2>
    <p>
        Both of our Window controls are highly skinnable and flexible, and you can do mostly anything you can think of out
        of both. Though our WindowFull is a "fully fledged Ajax Window" and can utilize far more advanced skins. One example
        is that while the WindowFull can easily have advanced borders with images and such. The WindowLight is mostly restricted
        into using CSS borders if you use it.
    </p>
    <p>
        Below is an example of our <em>WindowLight</em>.
    </p>
    <div style="position:relative;">
        <ext:WindowLight 
            runat="server"
            Caption="Ajax Window"
            CssClass="window smallWnd"
            id="windowLight">
            <div style="padding:5px;">
                <p>
                    Try to move me around by dragging my header field.
                </p>
                <p>
                    <img 
                        alt="Flower" 
                        src="media/flower1.jpg" />
                </p>
            </div>
        </ext:WindowLight>
    </div>
    <div class="spacerLarge">&nbsp;</div>
    <p>
        As you can see above both of our Window Controls are draggable, closable and such. But while the DOM for our
        WindowLight is *4* HTML elements. The WindowFull has a lot more of DOM nodes and thereby is far more flexible,
        skinnable and flexible. Though at the cost of also being more intensive in use for older browsers/computers and/or
        less capable clients like Mobile Browsers and such.
    </p>
    <p>
        If you are building websites mostly for PCs/Macs where you expect most of your users to have modern browsers then
        you would probably want to use WindowFull to get more flexibility and beautiful design.
        But if you are building websites for mobile browsers or where you expect a lot of your users to use old browsers
        then you should probably use WindowLight.
    </p>
    <a href="Ajax-DataGrid.aspx">On to Ajax DataGrid</a>
</asp:Content>




