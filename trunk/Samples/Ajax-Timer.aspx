<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Timer.aspx.cs" 
    Inherits="AjaxTimer" 
    Title="Ajax Timer Sample" %>

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

    <h1>Ajax Timer Sample</h1>
    <p>
        The <em>Ajax Timer</em> in Ra-Ajax is mostly a wrapper around the <em>setTimeout</em> function in JavaScript.
        Though instead of getting a Client-Side event handler it creates an Ajax Request and raises the 
        <em>Tick Event</em> on the server for you meaning you can completely abstract away the notion of JavaScript
        and setTimeout and all that.
    </p>
    <ra:Label 
        runat="server" 
        ID="lbl" 
        Text="Watch me change" 
        style="font-style:italic;color:#999;" />
    <ext:Timer 
        runat="server" 
        ID="timer" 
        OnTick="timer_Tick" />
    <br />
    <p>
        Watch closely the Label above as it changes every second. The code for changing the Label Text value
        is created 100% on the server and runs in the protected (blue pill) environment of server-side code :)
    </p>
    <p>
        Obviously the above sample is a very useless example of an Ajax Timer, a more appropriate example
        of using the Ajax Timer class would maybe be a Chat Client. Or maybe an email system which polls the
        server every n'th minute to check for new emails and so on. We are using the Ajax Timer in our
        own <a href="http://ra-wiki.org">Wiki System</a> to make sure the user does not get a Session
        timeout when he is editing a wiki entry. This works since every time you call into the server
        in ASP.NET it will reset the "minutes to session timeout" countdown...
    </p>
    <br />
    <h2>Features of the Ra-Ajax Timer</h2>
    <p>
        The Ra-Ajax Timer have a property called <em>Milliseconds</em> which is the number of Milliseconds
        between every time it will raise the <em>OnTick event</em>. It has (obviously) an event called
        <em>Tick</em> which is raised every n'th Millisecond. And the third important property it has is
        <em>Enabled</em> which if false will make the Ajax Timer not raise the Tick event and actually stop
        polling the server raising the Tick Event.
    </p>
    <p>
        The Timer does repeat every n'th Millisecond which means that this is a <em>repeating timer</em>
        Ajax Control. If you need a Timer which does not repeat and only raises the Tick event once then
        just set the <em>Enabled property</em> to <em>false</em> in the first Tick event and it will stop
        polling the server.
    </p>
    <p>
        One interesting trait of the Ra-Ajax Timer is that it doesn't start the Timer again before the
        server has returned from the previous Tick event. This means that you can actually (however this
        is obviously extremely unwise) set the Ajax Timer Millisecond property to 1 and it will not
        "go berserk" and do a "client-side stack overflow" for you since the setTimeout JavaScript
        function will not be called before the previous Tick Event is returned from the server. This
        is a nifty feature which will ensure that it also works mostly predictable in also clients
        with extremely small amounts of bandwidth even though the amount of data transfered between
        Ticks is very large.
    </p>
    <p>
        Though be careful with it. Used unwisely the Ajax Timer may very well "slashdot" your servers 
        even though that was not your intention...
    </p>
    <p>
        You will also experience better and more responsive solutions if you try to reduce the number 
        of timers per page. Normally I would encourage most solutions to not use more than one Ajax 
        Timer per page.
    </p>
    <a href="Ajax-TabControl.aspx">On to Ajax TabControl</a>
</asp:Content>
