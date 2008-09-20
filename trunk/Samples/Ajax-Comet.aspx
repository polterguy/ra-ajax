<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Comet.aspx.cs" 
    Async="true" 
    Inherits="AjaxComet" 
    Title="Ra-Ajax Comet Sample" %>

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

    <h1>Ra Ajax Samples - Ajax Comet</h1>
    <p>
        Ra-Ajax has an <em>Ajax Comet</em> component which you can use if you need realtime updates to your webpage or
        you don't want to have the overhead of constantly polling the server by using the 
        <a href="Ajax-Timer.aspx">Ajax Timer</a>.
    </p>
    <p>
        Be careful with Comet though. It has a nasty habit of seriously stressing the resources on both the
        client-side and the server side. Comet is a <em>last resort solution</em> which you only should use
        when <em>all other options are inadequate</em>. Comet is only to be used if you are 
        100% sure about that you need it!
    </p>
    <p>
    	Since the Comet is currently in pre-release preview mode we have <strong>disabled the comet in this example</strong>
    	but you can test it locally by setting its Enabled property to true when you download Ra-Ajax.
    </p>
    <ra:Panel 
        runat="server" 
        ID="chat" 
        style="border:solid 1px Black; background-color:Yellow; width:100%; height:220px;padding:5px;">
    </ra:Panel>
    <ra:TextBox 
        runat="server" 
        ID="newChat" 
        Text="INTENTIONALLY DISABLED" 
        style="width:75%;margin-top:5px;"
        OnFocused="newChat_Focused" />
    <ra:Button 
        runat="server" 
        ID="submit" 
        Enabled="false"
        Text="Submit" 
        OnClick="submit_Click" />
    <ext:Comet 
        runat="server" 
        ID="comet"
        Enabled="false"
        OnTick="comet_Tick" />
    <br />
    <br />
    <h2>Comet concerns</h2>
    <p>
        Mostly IE (except for some rumours about IE8) doesn't support Comet very well due to the 
        "2 HTTP connection per IP problem". This is impossible to fix without forcing users
        into using another browser. Or creating the Comet solution so that it uses multiple servers,
        one for GUI retrieval and a dedicated for only doing Comet HTTP Requests.
    </p>
    <p>
        When you use Comet you should set the page into <em>asynchronous mode</em>. This can be done
        easily in ASP.NET 2.0 by setting the <em>Async</em> page directive to <em>true</em>.
        Unless you do this you will notice that your entire WebServer freezes when you have many 
        simultanous users at your comet pages in total.
    </p>
    <p>
        <strong>Comet does NOT scale</strong>! Or to be accurate it doesn't scale as good as conventional Ajax,
        which means that you probably should use it as little as possible and only when strictly necessary.
        If you go berserk with Comet or use it in places where you really don't need it you will experience
        very weird behavior on your webservers like freezes due to resource draining.
    </p>
    <p>
        We have tried to implement Comet in Ra-Ajax in such a way that it will have a minimum impact on
        your server and the browsers of your clients. But really, to completely abstract away all the
        problems of Comet is just not possible.
    </p>
    <p>
        We will take no responsibility what so ever due to problems you might experience due to using 
        the Ra-Ajax Comet Control. It has been written with the intent of removing as many problems
        as possible and be as stable as possible, but Comet is DANGEROUS and most often you can use an 
        <a href="Ajax-Timer.aspx">Ajax Timer</a> instead.
    </p>
    <p>
        I have yet to see an application which truly needed Comet! Except for Stock Ticker Applications and
        Chatting Applications there probably are few good reasons to use Comet in the first place.
        Even the above Chat Example could easily have been implemented with far less
        problems by using our <a href="Ajax-Timer.aspx">Ajax Timer Control</a>. By using an
        Ajax Timer instead of the Comet solution you would have used more bandwidth. Still
        the server-side would have scaled far better, probably by orders of magnitudes. And you would have
        had a far more portable solution in regards to supported browsers. In general you would
        have had *less* problems. But here's an Ajax Comet Control for those cases when you
        <em>absolutely need it</em>.
    </p>
    <p>
        <strong>BE CAREFUL WITH COMET!</strong>
    </p>
    <h2>Ra-Ajax Comet is ALPHA</h2>
    <p>
        The Ra-Ajax Comet component is currently in Alpha mode which means it should serve as no more than
        just a teaser of what's to come. We will release a more stable Comet component in the future, but for now
        the Comet component in Ra-Ajax is mostly meant to be a "teaser preview look" of what's to come. Or for
        those who are curious and wants to build their own Comet component.
    </p>
    <h2>How does this Comet thing work?</h2>
    <p>
    	You add up one Comet Control on your page and you supply an OnTick event handler for it
    	in your codebehind. Then whenever something happens which raises an Event, your OnTick Event Handler
    	will be called and from it you can manipulate any Widget Property you wish.
    </p>
    <p>
    	Please note that the Ra-Ajax Comet component is a <em>two phase implementation</em>. This means
    	that first it creates a very small Ajax request which will only return the Comet Event ID back to the
    	client when/if a Comet Event is raised. Then the client-side Ajax Engine will create a "normal" Ajax 
    	Request which contains the ViewState and all other Control Properties (form elements) serialized
    	which it transfers back to the server. This means that among other things the Comet component
    	will not interfere with the Ajax Queue in a harmful way since the Comet Ajax Request (first request)
    	will "bypass" the Ajax Request Queue while the second "driver Ajax Request" (which raises the Tick
    	Event) will go through the normal Ajax Queue.
    </p>
    <p>
    	Only by doing it this way we're able to keep the Ajax Queue logic which doesn't create race conditions
    	in your code.
    </p>
    <a href="Ajax-Button.aspx">On to Ajax Button</a>
</asp:Content>
