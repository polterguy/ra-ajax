<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="Samples._Default" 
    Title="Ra-Ajax Samples" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra-Ajax Samples</h1>
    <p>
        Ra-Ajax is an <em>Open Source and Free of Charge Ajax Library for ASP.NET</em> and Mono. This is our samples
        which you will get when you <a href="http://code.google.com/p/ra-ajax/">download</a> Ra-Ajax. All code
        demonstrated in these samples are also in our <a href="http://code.google.com/p/ra-ajax/">download</a>.
    </p>
    <div class="thumbs">
        <a href="Viewport-Calendar-Starter-Kit.aspx" class="links">
            <span class="image1">&nbsp;</span>
            <span class="header">Ajax Calendar Starter-Kit</span>
            <span class="text">Demonstrates how to create an Ajax Calendar Application in addition to being an "Ajax Starter-Kit" for your own projects. <br />Written in C#...</span>
        </a>
    </div>
    <div class="thumbs">
        <a href="Viewport-Sample.aspx" class="links">
            <span class="image2">&nbsp;</span>
            <span class="header">Ajax Viewport Starter-Kit</span>
            <span class="text">Demonstrates Ajax Login, TabControl and TreeView. Is also an "Ajax Starter-Kit" for your own projects to kickstart development of your own projects. <br />Written in C#...</span>
        </a>
    </div>
    <div class="thumbs">
        <a href="Viewport-GridView-Sample.aspx" class="links">
            <span class="image3">&nbsp;</span>
            <span class="header">Ajax GridView/DataGrid Starter-Kit</span>
            <span class="text">This is a VB.NET Ajax Sample in addition to serving as an "Ajax Starter-Kit". This Starter-Kit shows how to build an Ajax DataGrid or GridView application.<br />Written in VB.NET...</span>
        </a>
    </div>
    <p>
        All of the above samples are "wow samples" ment to be somewhat close to real world samples of what
        you might expect to end up by utilizing the building blocks in Ra-Ajax. Most other samples here are less "rich"
        and more for reference purposes then to showcase Ra-Ajax.
    </p>
    <h2 style="clear:both;">Hello World Ajax Application</h2>
    <p>
        This is our <em>"Hello World"</em> Ajax application. Type something into the TextBox below 
        and click Submit...
    </p>
    <p>
        <ra:TextBox 
            runat="server" 
            ID="name" />
    </p>
    <p>
        <ra:Button 
            runat="server" 
            ID="submit" 
            Text="Submit" 
            OnClick="submit_Click" />
    </p>
    <p>
        <ra:Label 
            runat="server" 
            CssClass="updateLbl"
            ID="lblResults" />
    </p>
    <h2>What happened?</h2>
    <p>
        If you click the Show Code button you will see that there is no 
        JavaScript at all written to run the Ajax functionality you just observed. Ra-Ajax is heavily inspired by 
        <a rel="nofollow" href="http://anthemdotnet.com/">Anthem.Net</a> in that you can run pretty advanced Ajax 
        functionality without being forced into writing JavaScript at all. Though there is one crucial 
        difference between Ra-Ajax and most other Ajax Frameworks - Ra-Ajax uses 
        <a href="http://weblogs.asp.net/despos/archive/2007/09/19/partial-rendering-misses-ajax-architectural-points.aspx">Partial Rendering</a>
        as seldom as possible but relies instead on sending changes back to the client from the server
        as <a rel="nofollow" href="http://www.json.org/">JSON</a> which then again is mapped towards functions on the 
        Client-Side through a small JavaScript API. Only when absolutely neccessary Ra-Ajax will actually resort to Partial 
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
    <p>
        <em>Partial Rendering</em> which is the way Anthem.NET and ASP.NET AJAX would do this is by its 
        inventor - Jason Diamond referred to as a "hack". And it has lots of limitations.
    </p>
    <p>
        This is of course completely abstracted away from you as an end-developer and nothing you need to
        think of. But at least now you know why you can build much richer applications in way less time
        then you can with most Ajax Frameworks.
    </p>
    <h2>Why doesn't Ra-Ajax use Partial Rendering?</h2>
    <p>
        Because by keeping the "state" on the client we can do far more advanced and rich UI interaction then 
        if we were using Partial Rendering. Partial Rendering will eliminate the state on the client which in 
        the case of Ajax is a bad thing. In addition every property (attribute) of the Label would have to be 
        sent back from the server while Ra-Ajax only sends back the properties and attributes that actually 
        changed. This increases the quality of the user experience and creates a far more responsive application 
        for your end users. It also consumes way less bandwidth. The end result is that if you use Ra-Ajax 
        instead of any Partial Rendering based Ajax Framework you get to deliver better looking and better 
        performing applications.
    </p>
    <p>
        None of this really matters though before you start creating what Partial Rendering Frameworks
        probably would describe as "advanced functionality", like the sample below;
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
            In a Partial Rendering based Ajax Framework you would have to either re-render
            the entire contents of the panel, or resort to JavaScript.
        </p>
        <p>
            If you resort to JavaScript this would make your code more difficult to create and 
            maintain. And if you choose Partial Rendering you would consume far more bandwidth.
        </p>
        <p>
            This is one of the main reasons why you can create much more rich functionality 
            in Ra-Ajax then you can in ASP.NET AJAX or Anthem.NET without making the code
            more complex or hard to maintain.
        </p>
        <p>
            This is a very naive and simple example, and with purpose! Imagine how fast into your
            development you will start "hitting walls" with something that relies on Partial 
            Rendering - like for instance ASP.NET AJAX...
        </p>
        <p>
            Then try to imagine how much work and pain it would be to implement something like the 
            <a href="Viewport-Calendar-Starter-Kit.aspx">Ajax Calendar Starter-Kit sample</a> in e.g. 
            ASP.NET AJAX...
        </p>
    </ra:Panel>
    <h2>Why does Ra-Ajax abstract away JavaScript?</h2>
    <p>
        JavaScript is a great language, but most application developers have enough worries 
        trying to figure out the new stuff in C# or VB.NET like anonymous delegates, LINQ and so on. 
        Our bet is that JavaScript is <em>the Assembly language of the century</em> and that 
        JavaScript should be as much as possible abstracted away from application developers. 
        So just like modern C#, C++ and VB.NET compilers try to abstract away the assembly code, 
        Ra-Ajax tries to abstract away the JavaScript.
    </p>
    <p>
        This is why we refer to Ra-Ajax as a <em>"Managed Ajax Framework"</em> - since you don't have 
        to think about the DOM, JavaScript and different browsers...
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
    	Ra-Ajax is a <a href="http://ra-ajax.org/managed-ajax-a-new-approach-to-ajax.blog" title="Managed Ajax">Managed Ajax library</a>. 
    	We have abstracted away JavaScript and DOM for you completely within Ra-Ajax so that there is no
    	need for you to think of the differences between FireFox and IE etc. We have already
    	done that job for you!
    </p>
    <p>
        By completely abstracting JavaScript away, your apps will be:
    </p>
    <ul class="bulList">
        <li><strong>Faster to create</strong>, far less code.</li>
        <li><strong>Easier to maintain</strong>, no need to call "JavaScript Guru" to fix bugs.</li>
        <li><strong>More responsive</strong>, ~8KB of JavaScript in typical Ra-Ajax application.</li>
    </ul>
    <p>
        JavaScript is a great platform for building portable applications, and many JavaScript libraries
        like MooTools, jQuery and Prototype does a great job in abstracting away the DOM and browser differences.
        Though, Ra-Ajax doesn't stop there. The purpose of Ra-Ajax is to make application development easier.
        And just because it's <em>easier to develop in JavaScript</em>, doesn't necessarily mean that it will be
        <em>easier to develop applications</em>. Use the right tools for the job! If your job is to develop
        web applications, then Ra-Ajax is the right tool! If your job is to develop Super Mario bros in JavaScript
        or integrate with Digg.com, etc - then probably jQuery, MooTools or Prototype would be the right tool.
    </p>
    <p>
        Note that you can still combine Ra-Ajax with JavaScript if you wish! You might come
        to a place where you would like to combine JavaScript with Ra-Ajax or create your own Ajax
        Extension Controls. Or maybe you're just a MooTools or jQuery supporter who cannot
        possibly even consider completely getting rid of those toolboxes. But this is your choice and
        JavaScript is an optional language when using Ra-Ajax.
    </p>
    <p>
        Ra-Ajax is compatible with all larger JavaScript libraries, so if you want to combine Ra-Ajax with
        Prototype.js, MooTools or jQuery then this is very easy.
    </p>
    <p>
        PS!<br />
        These samples are written so that they make a lot of sense to read in a sequential 
        manner if you want to "dive deep" into the Ra-Ajax programming model. Meaning you can just read every piece 
        of text on all our samples and then click the link at the bottom everytime you finish a page. If you're 
        ready to do this then click the link below to go to our next Ajax Sample.
    </p>
    <a href="Flexible.aspx">On to "Flexible Ajax Event System"...</a>
</asp:Content>

