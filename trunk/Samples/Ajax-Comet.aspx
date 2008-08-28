<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Comet.aspx.cs" 
    Async="true" 
    AsyncTimeout="60000"
    Inherits="AjaxComet" 
    Title="Ajax Comet Sample" %>

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

    <h1>Ajax Comet Sample</h1>
    <p>
        Ra-Ajax has an <em>Ajax Comet component</em> which you can use if you need realtime updates or
        don't want to have the overhead of constantly polling by using the <a href="Ajax-Timer.aspx">Ajax Timer</a>.
    </p>
    <p>
        Be careful with Comet though. It has a nasty habit of seriously stressing the resources on both the
        client-side and the server side. Comet is a <em>last resort solution</em> which you only should use
        when <em>all other options are inadequate</em>. Comet is only to be used if you are 
        <em>100% sure about that you need it</em>!
    </p>
    <ra:Panel 
        runat="server" 
        ID="chat" 
        style="border:solid 1px Black; background-color:Yellow; width:100%; height:220px;padding:5px;">
    </ra:Panel>
    <ra:TextBox 
        runat="server" 
        ID="newChat" 
        Text="Write chat" 
        style="width:75%;margin-top:5px;"
        OnFocused="newChat_Focused" />
    <ra:Button 
        runat="server" 
        ID="submit" 
        Text="Submit" 
        OnClick="submit_Click" />
    <ext:Comet 
        runat="server" 
        ID="comet" 
        OnTick="comet_Tick" />
    <br />
    <br />
    <h2>Comet concerns</h2>
    <p>
        Mostly IE (except for some rumours about IE8) doesn't support Comet very well due to the 
        <em>2 HTTP connection per IP problem</em>. This is impossible to fix without forcing users
        to using another browser.
    </p>
    <p>
        When you use Comet you should set the page into <em>asynchronous mode</em>. This can be done
        easily in ASP.NET2.0 by setting the <em>Async</em> page directive to <em>true</em> and
        the <em>AsyncTimeout</em> to 60000 which is the number of Milliseconds before the page
        will timeout. Unless you do this you will notice that your entire WebServer freezes when you
        have more than 40 simultanous users at your comet pages in total.
    </p>
    <p>
        <em>Comet does NOT scale</em>. Or at least it doesn't scale as good as conventional Ajax
        which means that you probably should use it as little as possible and only when strictly necessary.
        If you go berserk with Comet or use it in places where you really don't need it you will experience
        very weird behavior on your webservers like freezes due to resource draining.
    </p>
    <p>
        We have tried to implement Comet in Ra-Ajax in such a way that it will have a minimum impact on
        your server and the browsers of your clients. But really, to completely abstract away all the
        problems of Comet is just not possible...
    </p>
    <p>
        I will take no responsibility what so ever due to problems you might experience due to using 
        the Ra-Ajax Comet Control. It have been written with the intend of removing as many problems
        as possible and be as stable as possible, but Comet is DANGEROUS and most often you can use an 
        <a href="Ajax-Timer.aspx">Ajax Timer</a> instead.
    </p>
    <p>
        I have yet to see an application which TRULY needed Comet except for Stock Ticker Applications.
        Even the above Chat Example could very easily have been implemented much easier and with far less
        problems by using our <a href="Ajax-Timer.aspx">Ajax Timer Control</a>. All though by using an
        Ajax Timer instead of the Comet solution you would have used more bandwidth. Though still
        the server-side would have scaled far better, probably orders of magnitudes. And you would have
        had a far more portable solution in regards to supported browsers, and in general you would
        have had far less problems. But here's an Ajax Comet Control for you for those cases when you
        <em>absolutely need it</em>...
    </p>
    <p>
        <strong>BE CAREFUL WITH COMET!</strong>
    </p>
    <a href="Ajax-Button.aspx">On to Ajax Button</a>
</asp:Content>
