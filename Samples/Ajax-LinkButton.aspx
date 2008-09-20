<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-LinkButton.aspx.cs" 
    Inherits="AjaxLinkButton" 
    Title="Ra-Ajax LinkButton Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra Ajax Samples - LinkButton</h1>
    <p>
        An <em>Ajax LinkButton</em> is, roughly speaking, an ajaxified hyperlink <em>(&lt;a href="...")</em>.
        While a normal hyperlink usually makes the browser go to another page, the Ra-Ajax LinkButton initiates
        an Ajax Request which calls back into the server and raises an event which you can handle.
    </p>
    <ra:LinkButton 
        runat="server" 
        ID="btn" 
        Text="Click this Ajax LinkButton" 
        OnClick="btn_Click" />
    <p>
        Try to click the above Ajax LinkButton and see the changes.
    </p>
    <p>
        The Ajax LinkButton is useful whenever you need a Button but you don't want to use a regular Button
        <em>(&lt;input type="button"...)</em>, and would rather like something which renders like a hyperlink.
    </p>
    <h2>The Ajax Queue or The ViewState Problem</h2>
    <p>
        If you google for <em>__VIEWSTATE and Ajax</em> you will get a feeling for the <em>sequential Ajax queue
        problem</em>. Although most people believe otherwise, the <em>__VIEWSTATE and Ajax problem</em> is not the 
        real problem. The ViewState problem is just a symptom of something going wrong.
    </p>
    <p>
        Most Ajax Frameworks, including ASP.NET AJAX, allow more than one Ajax Request to hit the server
        at the same time. This is the source of many problems, including the <em>__VIEWSTATE and Ajax problem</em>.
        The problem is due to the fact that when you create an Ajax Request, 
        then you may actually do something which changes the DOM structure on the client in that Ajax Request. 
        If you then allow another Ajax Request to start before the first one has returned, then whatever form values
        the second Ajax Request fetches to send back to the server might be completely wrong and "undefined"
        since their values were changed in the first Ajax Request which still hasn't returned.
    </p>
    <p>
        The reason why so many see this as a ViewState problem, is that it mostly manifests as a ViewState
        problem. Since whatever you do on the server to manipulate your UI, the ViewState will be changed
        and then when you create your second Ajax Request, before the first one has returned, whatever
        ViewState you submit on your second Ajax Request will not be the correct one, since its value
        was collected before the first Request returned.
    </p>
    <p>
        This is in fact nothing more than variations on the problems we used to have in multi-threaded 
        applications when we were doing conventional desktop programming. You might think of it like a 
        variation on a <em>Race Condition problem</em>.
    </p>
    <h2>Ra-Ajax and its Ajax Queue</h2>
    <p>
        Ra-Ajax solves this problem by having an <em>Ajax Queue</em>. It will start queuing up Ajax requests
        if there is one currently in progress. This means that all Ajax Requests
        that are initiated will go into an <em>Ajax Queue</em> and will not be dispatched before there's a "free lane"
        on the "Ajax highway" which they can use. In fact I have created an example for you to look at below.
    </p>
    <ra:LinkButton 
        runat="server" 
        ID="lnk1" 
        Text="Click this LinkButton" 
        OnClick="lnk1_Click" />
    <br />
    <ra:LinkButton 
        runat="server" 
        ID="lnk2" 
        Text="Then QUICKLY click this LinkButton before the other one returns" 
        OnClick="lnk2_Click" />
    <br />
    <p>
        When you click the first Ajax LinkButton, there is an intentional Thread.Sleep for 5 seconds to simulate
        a lengthy request taking place. Then when you click the second LinkButton (hopefully you manage to do this 
        before the first Request returns), you will see that nothing happens and the request is not even dispatched
        before the first Ajax Request returns.
    </p>
    <p>
        In most Ajax frameworks that I am aware of, including ASP.NET AJAX, the second Ajax Request would be allowed 
        to start before the first one returns and will change the Text property of the second LinkButton to 
        <em>"Then QUICKLY click this LinkButton before the other one returnsI am CLICKED --"</em>. If this is the 
        case, then when the first Ajax Request returns, the Text property of the second LinkButton will be changed 
        to <em>"-- watch me as, "</em>. This is semantically wrong!
    </p> 
    <p>
        If you want to test this with another Ajax framework to see if it handles your Ajax Requests correctly, 
        you can copy the logic code from the code-behind of this page to a sample built around your favorite 
        Ajax framework and see how it handles the above situation.
    </p>
    <p>
        In fact the Ajax Queue mechanism used in Ra-Ajax can be observed as you click the LinkButtons above. If 
        you have <a href="http://getfirebug.com">FireBug</a> installed, notice that as long as the first
        Ajax Request is still in progress, no other Ajax Request will show up in FireBug's Console.
    </p>
    <p>
        Let me sum up the rights and wrongs here. The second Ajax LinkButton should end up displaying something 
        like the below according to whether or not your Ajax framework implements an Ajax Request Queue.
    </p>
    <ul>
        <li>
            Wrong: <em>"-- watch me as, "</em> 
        </li>
        <li>
            Right: <em>"-- watch me as, I am CLICKED --"</em>
        </li>
    </ul>
    <p>
        If your favorite Ajax Framework doesn't display the results according to the above logic,
        then it does not implement an Ajax Queue which is crucial in order to avoid creating really obscure
        and impossible to track down bugs later down the road.
    </p>
    <a href="Ajax-Panel.aspx">On to Ajax Panel</a>
</asp:Content>

