<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="_Default" 
    Title="Ra-Ajax Samples" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra Ajax Samples</h1>
    <p>
        Welcome to the main samples page for Ra-Ajax. These samples are written in such a way that it makes
        a lot more sense to read them <em>sequentially</em>. This means that you should start at the top with the 
        <em>Samples Start Page</em> (the page you're reading) and work your way down as you read on. 
        Later on, you can go directly to a specific sample page for reference purposes. 
    </p>
    <p>
        Here we start with the
        <em>Hello World</em> application. Type your name into the textbox and click the Submit button.
    </p>
    <ra:TextBox 
        runat="server" 
        ID="name" />
    <br />
    <ra:Button 
        runat="server" 
        ID="submit" 
        Text="Submit" 
        OnClick="submit_Click" />
    <br />
    <ra:Label 
        runat="server" 
        CssClass="updateLbl"
        ID="lblResults" />
    <h2>What happened?</h2>
    <p>
        If you click the Show Code button at the top right corner you will see that there is no 
        JavaScript at all created to run the Ajax functionality you just observed. Ra-Ajax is heavily inspired by 
        <a rel="nofollow" href="http://anthemdotnet.com/">Anthem.Net</a> in that you can run pretty advanced Ajax 
        functionality without being forced to write JavaScript at all. Though with one crucial 
        difference. Ra-Ajax uses 
        <a rel="nofollow" href="http://weblogs.asp.net/despos/archive/2007/09/19/partial-rendering-misses-ajax-architectural-points.aspx">Partial Rendering</a>
        as seldom as possible but relies instead on sending changes back to the client from the server
        as <a rel="nofollow" href="http://www.json.org/">JSON</a> which then again is mapped towards functions on the 
        Client Side JavaScript. Only when absolutely neccessary Ra-Ajax will actually resort to Partial 
        Rendering and render HTML back from the Server.
        If you use <a href="http://getfirebug.com">FireBug</a> and check out the response from the server you
        will in fact observe something like this:
    </p>
    <p>
        <em>Ra.Control.$('ctl00_cnt1_lblResults').handleJSON({"Text":"Hello xxxx and welcome to the world :)"});</em>
    </p>
    <p>
        The above is the JSON sent from the server to the label on the client side to update the text value
        of the label.
    </p>
    <h2>Why not just use the far easier Partial Rendering method?</h2>
    <p>
        Because by keeping the "state" on the client we can do far more advanced stuff then if we are using
        Partial Rendering. Partial Rendering will eliminate the state on the client which in the case of Ajax
        is a bad thing. Imagine if the above Label had and <em>OnClick Event Handler</em>. Then that event handler
        would be deleted when we re-render the Label if we were using Partial Rendering. Or if we were to keep
        those Event Handlers we would at least have to re-create them after the HTML was updated. In addition 
        to that every property (attribute) of the Label would have to be sent back from the server while Ra-Ajax
        only sends back the properties and attributes that actually changed. This increases the quality of the
        user experience and creates a far more responsive application for your end users. It also stresses your
        server resources far less. The end result is that if you use Ra-Ajax instead of Partial Rendering you 
        get to deliver better applications and you also get to be <em>more happy and less frustrated</em>. ;)
    </p>
    <p>
        None of this really matters though before you start creating "really advanced" stuff, like the sample below;
    </p>
    <ra:Panel runat="server" ID="pnl" CssClass="panel">
        <ra:Button 
            runat="server" 
            ID="submit2" 
            Text="Click me..." 
            OnClick="submit2_Click" />
        <p>
	        Notice how the background-color of this panel changes as you click the above button...
        </p>
        <p>
	        If you look at the source for this page you will see that we're setting the style property
	        from an enum which maps towards the styles in the CSS standard. This ensures typesafety and less
	        typing errors.
        </p>
        <p>
	        Also Ra-Ajax has support for setting <em>opacity</em> through the opacity style value which
	        abstracts away the problems of setting the opacity for different browsers (which is a nightmare
	        to do by "hand")
        </p>
        <p>
        	Also notice the small amount of bandwidth which is sent from the server for this operation, even
        	though the HTML of this panel is actually pretty large. In a <em>Partial Rendering framework</em>
        	the entire HTML would be sent and the bandwidth usage would end up being orders of magnitudes larger.
        </p>
        <p>
        	And in fact the simple logic you're observing here is actually <strong>*impossible*</strong> in
        	a Partial Rendering framework! (since the button above would loose the focus)
        </p>
        <p>
        	Try to create something similar in e.g. ASP.NET AJAX and just keep on clicking space and
        	you will understand that the <em>partial rendering problem</em> goes pretty deep. Even for
        	a <em>really simple problem</em> like this one.
        </p>
    </ra:Panel>
    <h2>Why doesn't this work with Partial Rendering?</h2>
    <p>
        Because when the Panel is re-rendered using
        Partial Rendering the Button would loose focus since Partial Rendering would force a re-rendering of the 
        entire panel. Partial Rendering of the above panel would eliminate the state on the client meaning you would have 
        to re-add Event Handlers and so on for EVERY control inside of the panel. Not to mention it would be orders of 
        magnitude larger in regards to bandwidth usage. Especially for a complex panel. Or imagine the "worst case 
        scenario" where you are <a href="Ajax-Panel.aspx">changing some of the Panel's properties as the user is 
        writing inside an Ajax TextBox</a> within the panel. That would destroy focus from the TextBox as you are writing 
        in addition to that if you write something while the Ajax Request was fetching data from the server you would
        also loose those changes in the TextBox since the new "value" from the server would overwrite those changes
        done while the server was fetchind the "new panel" from the server.
    </p>
    <p>
    	But for most immediate problems the Bandwidth
        Usage is the most serious problem. In Ra-Ajax the above Ajax Response for the Panel is very small compared
        to a Partial Rendering Framework. Compare the Ra-Ajax response below against your favorite Partial Rendering
        Framework.
    </p>
    <p>
        <em>Ra.Control.$('ctl00_cnt1_pnl').handleJSON({"AddStyle":[["backgroundColor","Yellow"]]});</em>
    </p>
    <p>
        You will probably find it a <em>fraction</em> of the alternative.
    </p>
    <p>
    	To be 100% accurate with you though the above logic <em>is possible</em> in a Partial Rendering Framework though
    	it would either require some JavaScript hacking, some custom widget (or extender) for that specific purpose or
    	that you don't mind if the Button above looses focus between callbacks. Though it would be orders of magnitudes
    	larger in bandwidth usage if you used Partial Rendering.
    </p>
    <h2>JavaScript - The best Assembly Language of the century!</h2>
    <p>
        JavaScript is a great language, I personally LOVE it but most application developers have enough worries with
        trying to figure out the new stuff in C# or VB.NET like anonymous delegates, LINQ and so on. Our bet is that
        JavaScript is <em>the Assembly language of the century</em> and that JavaScript should be completely abstracted 
        away from <em>application developers</em>. So just like modern C#, C++ and VB.NET compilers try to abstract away the
        assembly code, Ra-Ajax tries to abstract away the JavaScript.
    </p>
    <p>
    	I am currently writing this on Linux/Ubuntu using Mono and MonoDevelop. This is possible for me because of
    	that C# is an abstraction which abstracts away the differences between the different Operational Systems,
    	CPUs, Graphic Cards and so on. I tend to think of JavaScript the same way as the Operational System.
    	Application Developers should not have to worry which browser-quirks they need to hack together and so
    	on just like I don't have to worry about the differences between Microsoft Windows and Linux/Ubuntu because
    	of that the Mono Team has already abstracted away that difference for me.
    </p>
    <p>
    	We believe that we have abstracted away JavaScript for you completely within Ra-Ajax and that there is no
    	need for you to think of the differences between FireFox 3.1.2.8 and Safari 3.0.5.7 and so on. We have already
    	done that job for you!
    </p>
    <p>
        By completely abstracting JavaScript away, your apps will be:
    </p>
    <ul class="bulList">
        <li><strong>More secure</strong> (no business logic code on the client side)</li>
        <li><strong>Easier to maintain</strong> (no more "browser quirks" for you)</li>
        <li><strong>Easier to optimize</strong> (it's easier to optimize a library than "User Code")</li>
        <li><strong>More browser compatible</strong> (*WE* get the browser problems)</li>
        <li><strong>Faster and more responsive</strong> (~20KB of JS in Ra-Ajax)</li>
        <li><strong>Maintained by happy developers ... ;)</strong> (*YOU!*)</li>
        <li>+++++++++++++</li>
    </ul>
    <p>
        By using Ra-Ajax you no longer have to worry about JavaScript and you can just develop
        in C#, VB.NET and ASP.NET on the server as you're used to. So from being an "application language"
        JavaScript has effectively been reduced to the <em>View</em> in a <em>Model View Control (MVC)</em>
        application.
    </p>
    <p>
        Note that you *can* still combine JavaScript <em>if you wish</em> with Ra-Ajax. You might come
        to a place where you would like to combine JavaScript with Ra-Ajax or create your own Ajax
        Extension Controls. Or maybe you're just one of those MooTools or jQuery lovers which cannot
        possibly even consider completely getting rid of those toolboxes. But this is YOUR CHOICE and
        JavaScript is an OPTION when using Ra-Ajax.
    </p>
    <p>
        BTW, Ra-Ajax could always use an extra pair of hands on development, if you're a JavaScript guru ;)
    </p>
    <p>
        It took us 30 years before we could completely eliminate the need to do Assembly Programming, I have
        no believe in that Ra-Ajax will overnight completely eliminate the need to do JavaScript. But my dream
        is to make the JS insertion point so completely abstracted away that 95% of all applications can be 
        developed completely blindfolded by people who doesn't even know what closures are ;)
    </p>
    <p>
        But if Ra-Ajax succeeds, maybe you would very rarely have to resort to JavaScript and deal with headaches
        like browser compatibility etc... and instead you would focus 90% of your energy on your <em>domain problems</em>
        instead of having to fiddle with <em>Assembly Programming</em>...
    </p>
    <a href="Flexible.aspx">On to "Flexible Ajax Event System"...</a>
</asp:Content>
