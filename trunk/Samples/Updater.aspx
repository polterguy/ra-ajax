<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Updater.aspx.cs" 
    Inherits="AjaxUpdater" 
    Title="Ra-Ajax Updater Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra Ajax Samples - Updater</h1>
    <p>
    	An <em>Ajax Updater</em> is a special kind of <a href="Behaviors.aspx">Ajax Behavior</a> which when the Control
    	is creating an Ajax Request for any reasons the Updater will be called and do its logic. Then when the Request returns
    	from the Server the Updater will also kick in and do its logic. Now the Ajax Sample below has an Ajax Updater which
    	while there is a request going on will obscur the main area of the WebPage so that the user cannot interact with
    	the UI while the Ajax Control below is going towards the server.
    </p>
    <ra:Button 
    	runat="server" 
    	id="btn" 
    	Text="Click me" 
    	OnClick="btn_Click">
    	<ra:BehaviorUpdater 
    		runat="server"
    		Color="#aaaaaa"
    		Delay="500" />
    </ra:Button>
    <br />
    <ra:Label 
    	runat="server" 
    	id="lbl"
    	style="font-style:italic;color:#999"
    	Text="Updates..." />
    <br />
    <p>
    	Notice also that there's a delay set for the above Updater of 500 milliseconds which will pass before
    	the updater logic kicks in.
    </p>
    <br/>
    <h2>Ajax Updater usage</h2>
    <p>
    	The Ajax Updaters are use like any other <a href="Behaviors.aspx">Ajax Behavior</a> in that it is attached to a
    	specific control by adding it inside of the Control declarative markup or by adding it in your code-behind file
    	as a Child Control in the Controls Collection. The above Ajax Updater have properties for the Color and the
    	Delay which is the number of milliseconds before it "kicks in". But the Ajax Updaters are easily extendible and
    	you can easily create your own Updaters by looking at the existing ones.
    </p>
    <p>
    	Their main usage is for lengthy jobs which you know will last for long on the server-side. Mostly these 
    	"lengthy jobs" are for specific Ajax Controls on your page like for instance a "create report" button
    	or something similar like that and therefor we have choosen to make the Ajax Updaters attached to specific
    	control instead of "global events". If they were "global events" for your entire page, then you would also
    	have them kick in for you non-lengthy operations which also can occur on the same page. Also by having them
    	attached to specific controls you can have different Ajax Updaters for different Ajax Controls on your page
    	which adds to the flexibility of your solution.
    </p>
    <br />
    <h2>ALL the problems, not just an Ajax Control Library</h2>
    <p>
    	When you develop applications for the web then the Ajax Problems is not your only problems, though they are probably
    	your largest problems you still have other problems too. Ra-Ajax tries to be a complete solution which means that
    	it also contains solutions for other problems like for instance recursively searching for controls with specific IDs etc.
    	Our goal is to <em>fix the issues that Redmond doesn't fix</em>. This means that not only is Ra-Ajax an Ajax Library
    	but you should also expect to see your other GUI problems solved in it. If you find a problem which is of general nature
    	that you wish to solve then please let us know and we'll see if it fits with the Ra-Ajax model and is easy to 
    	incorporate into Ra-Ajax.
    </p>
    <p>
    	A good example is our <em>AjaxManager.Instance.FindControl<T>(string id)</em> method which will recursively search
    	through your page and your master page to look for a control with the given ID and return it as a typeof(T) object.
    </p>
    <br />
    <a href="Ajax-Comet.aspx">On to Ajax Comet Sample</a>
</asp:Content>

