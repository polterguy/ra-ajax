<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="_Default" 
    Title="Ra Ajax Samples" %>

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
        Welcome to the main Ajax Samples of Ra-Ajax. These samples are written in such a way that it makes
        a lot sense to read them <em>sequentially</em> which means that you should start on the top at the 
        <em>Start Here</em> sample and work your way down as you read on. And then later for reference 
        purposes you can go directly to whichever topic you are stuck with. Here we start with the 
        <em>Hello World</em> application. Type something into the textbox and click the Submit button.
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
        style="font-style:italic;color:#999;"
        ID="lblResults" />
    <br />
    <br />
    <h2>What happened?</h2>
    <p>
        If you click the the Show Code button at the top/right corner you will see that there is no 
        JavaScript at all created to run the Ajax you just observed. Ra Ajax is heavily inspired by 
        <a href="http://anthemdotnet.com/">Anthem.Net</a> in that you can run pretty advanced Ajax 
        functionality without being forced to write JavaScript at all. Though with one crucial 
        difference. Ra-Ajax uses 
        <a href="http://weblogs.asp.net/despos/archive/2007/09/19/partial-rendering-misses-ajax-architectural-points.aspx">Partial Rendering</a>
        as seldom as possible but relies instead on sending changes back to the client from the server
        as <a href="http://www.json.org/">JSON</a> which then again is mapped towards functions on the 
        Client Side JavaScript. Only when *absolutely* neccessary Ra-Ajax will actually resort to Partial 
        Rendering and render HTML back from the Server.
    </p>
    <p>
        If you use <a href="http://getfirebug.com">FireBug</a> and check out the response from the server you
        will in fact observe something like this;
        <br />
        <em>Ra.Control.$('ctl00_cnt1_lblResults').handleJSON({"Text":"Hello xxxx and welcome to the world :)"});</em>
        <br />
        The above is the JSON sendt from the server to the label on the client side for updating the text value
        of the label.
    </p>
    <br />
    <h2>Why not just use the far easier Partial Rendering method?</h2>
    <p>
        Because by keeping the "state" on the client we can do far more advanced stuff than if we were using
        Partial Rendering which will eliminate the state on the client. Imagine the above Label had 
        <em>OnKeyUp Event Handlers</em>, then those event handlers would be deleted when we re-rendered
        the Label if we were using Partial Rendering. Or if we were to keep those Event Handlers we would
        at least have to re-render them after the HTML was updated, in addition to that ALL the properties
        of the Label would have to be sent back from the server instead of the ones just changed which would
        increase the bandwidth usage a LOT.
    </p>
    <p>
        None of this really matters though before you start creating really "advanced" stuff, imagine the below.
    </p>
    <br />
    <ra:Panel runat="server" ID="pnl" style="background-color:Yellow;border:solid 1px Black;padding:15px;height:100px;">
        <ra:Button 
            runat="server" 
            ID="submit2" 
            Text="Changed color" 
            OnClick="submit2_Click" />
        <br />
        Notice how the background-color of this panel changes as you click the above button...
    </ra:Panel>
    <br />
    <h2>Partial Rendering</h2>
    <p>
        The above would be very easy to do also in a "Partial Rendering Framework" like ASP.NET AJAX (UpdatePanel) or 
        Anthem.NET but it would either force a re-rendering of the entire panel, or it would force you into using 
        JavaScript. Partial Rendering of the above panel would eliminate the state on the client meaning you would have 
        to re-add Event Handlers and so on for EVERY control inside of the panel. Not to mention it would be orders of 
        magnitudes larger in regards to bandwidth usage. Especially for a complex panel. Or imagine the "worst case 
        scenario" where you are changing some of the Panel's properties as the user is writing inside of a TextBox 
        inside the panel. That would destroy focus from the TextBox as you are writing in addition to that if you 
        wrote something while the Ajax Request was going towards the server you would also loose those changes in 
        the TextBox. And in fact that one simple example above is *impossible* in a Partial Rendering Ajax Framework
        since when rerendered the Button would loose the focus while in a framework similar to Ra-Ajax that's not 
        a problem. In Ra-Ajax the above Ajax Response for the Panel looks like this;
    </p>
    <p>
        <em>Ra.Control.$('ctl00_cnt1_pnl').handleJSON({"AddStyle":[["backgroundColor","Yellow"]]});</em>
    </p>
    <br />
    <h2>JavaScript - The best Assembly Language of the century!</h2>
    <p>
        JavaScript is a great language, I personally LOVE it but most application developers have enough worries with
        trying to figure out the new stuff in C# or VB.NET like anonymous delegates, LINQ and so on. Our bet is that
        JavaScript is <em>the Assembly language of the century</em> and that JavaScript should as much as possible
        be completely abstracted away. So just like modern C#, C++ and VB.NET compilers try to abstract away the
        assembly code, Ra-Ajax tries to abstract away the JavaScript.
    </p>
    <p>
        By abstracting JavaScript completely away your apps will be;
    </p>
    <ul>
        <li>More secure (no business logic code on the client side)</li>
        <li>Easier to maintain (easier to use one programming language than two)</li>
        <li>Easier to optimize (it's easier to optimize a library than "User Code")</li>
        <li>More portable (it's easier to ensure a library runs on "all browsers" than an application)</li>
        <li>Speedier and more responsive (Ra-Ajax contains less than 20KB of JavaScript)</li>
        <li>etc, etc, etc...</li>
    </ul>
    <p>
        So by using Ra-Ajax you don't any longer have to worry about JavaScript and you can just develop
        in C#, VB.NET and ASP.NET on the server as you're used to. So from being an "application language"
        JavaScript has effectively been reduced to the <em>View</em> in a <em>Model View Control (MVC)</em>
        application.
    </p>
    <p>
        Note that you CAN still combine JavaScript if you wish with Ra-Ajax. First of all you might come
        to a place where you would either like to combine JavaScript with Ra-Ajax or create your own Ajax
        Extension Controls etc. Also Ra-Ajax could also always need another pair of hands on development
        if you're a JavaScript guru ;)
    </p>
    <p>
        It took us 30 years before we could completely eliminate the need to do Assembly Programming, I have
        no believe in that Ra-Ajax will overnight completely eliminate the need to do JavaScript...
    </p>
    <p>
        But if Ra-Ajax succeeds maybe you would far more seldom have to resort to JavaScript and browser 
        compatibility etc and instead at least focus 90% of your energy on your <em>domain problems</em>
        instead of having to fiddle with <em>Assembly Programming</em>... :)
    </p>
    <a href="Flexible.aspx">On to "Flexible Ajax Event System"...</a>
</asp:Content>

