<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Comet.aspx.cs" 
    Async="true" 
    AsyncTimeout="20"
    Inherits="Samples.AjaxComet" 
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

    <h1>Ra-Ajax Comet</h1>
    <p>
        Ra-Ajax has a  Comet component which you can use if you need realtime updates to your 
        webpage or you don't want to have the overhead of constantly polling the server by using the 
        <a href="Ajax-Timer.aspx">Ajax Timer</a>.
    </p>
    <p>
        Here we have implemented a chat-client using the Ra-Ajax Comet component. To see the benefits of using
        it try to open multiple browsers towards this same webpage and try to submit a new chat item from
        one of the browsers and see how the other browser will actually update the chat items in "real time" 
        instantly.
    </p>
    <ra:Panel 
        runat="server" 
        ID="chat" 
        style="background-color:#eee;"
        CssClass="panel chat">
    </ra:Panel>
    <div>
        <ra:TextBox 
            runat="server" 
            ID="newChat" 
            Text="Type text here" 
            style="width:75%;" />
        <ra:Button 
            runat="server" 
            ID="submit" 
            Enabled="true"
            Text="Submit" 
            OnClick="submit_Click" />
        <ext:Comet 
            runat="server" 
            ID="comet" 
            MaxClients="200"
            Enabled="true"
            OnTick="comet_Tick" />
    </div>
    <p>
        <ra:Label 
            runat="server" 
            ID="lbl" 
            CssClass="updateLbl"
            Text="Number of connections" />
    </p>
    <h2>Comet concerns</h2>
    <p>
        There are a lot of concerns when working with Comet, mostly due to the fact that HTTP and the web
        wasn't really created for doing Comet. Some older browsers (IE) will have problems with multiple 
        connections towards the same server, in addition the server-side will also use significantly more
        resources in a Comet solution than in a conventional web application. Though the Comet component
        in Ra-Ajax is built to try to minimize the impact of all these issues by using tricks like
        two phase comet implementations, Asynchronous pages in ASP.NET and several other techniques. Though
        you should still be careful with Comet and only use it when you are absolutely sure of that you need
        it. Comet is a "really big gun"!
    </p>
    <p>
        When you use Comet you should set the page into <em>asynchronous mode</em>. This can be done
        easily in ASP.NET 2.0 by setting the <em>Async</em> page directive to <em>true</em>.
        Unless you do this you will notice that your entire WebServer freezes when you have many 
        simultanous users at your comet pages in total.
    </p>
    <p>
        <strong>Comet does NOT scale</strong> very well! Or to be accurate it doesn't scale as good as 
        conventional Ajax, which means that you probably should use it as little as possible and only 
        when strictly necessary. If you go berserk with Comet or use it in places where you really don't 
        need it you will experience weird behavior on your webservers like freezes due to resource 
        draining and bad scaling and server-errors. This is at the heart of Comet since it freezes up a
        thread for every request on the server, even though you're using the Asynchronous features
        of ASP.NET 2.0.
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
        <strong>BE CAREFUL WITH COMET!</strong>
    </p>
    <p>
    	Note that the Ra-Ajax Comet component is a <em>two phase implementation</em>. This means
    	that first it creates a very small Ajax request which will only return the Comet Event ID back to the
    	client when/if a Comet Event is raised. Then the client-side Ajax Engine will create a "normal" Ajax 
    	Request which contains all the other Control Properties (form elements) serialized
    	which it transfers back to the server. This means that among other things the Comet component
    	will not interfere with the Ajax Queue in a harmful way since the Comet Ajax Request (first request)
    	will "bypass" the Ajax Request Queue while the second "driver Ajax Request" (which raises the Tick
    	Event) will go through the normal Ajax Queue and not mess with form elements on your page.
    </p>
    <p>
    	Only by doing it this way we're able to keep the Ajax Queue logic which doesn't create race conditions
    	in your code.
    </p>
    <a href="Ajax-Button.aspx">On to Ajax Button</a>
</asp:Content>
