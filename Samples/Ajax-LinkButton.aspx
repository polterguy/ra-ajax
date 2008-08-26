<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-LinkButton.aspx.cs" 
    Inherits="AjaxLinkButton" 
    Title="Ajax LinkButton Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ajax LinkButton Sample</h1>
    <p>
        An <em>Ajax LinkButton</em> is roughly speaking a hyperlink (<em>&lt;a href="...</em>) ajaxified.
        Though while a "normal" hyperlink makes the browser move to another page, the Ra-Ajax LinkButton creates
        an Ajax Request which calls back into the server and raises and event for you which you can handle.
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
        The Ajax LinkButton is useful whenever you need a "button" but you don't want to use a "normal" Button
        (<em>&lt;input type="button"...</em>) but rather would have something which renders like a hyperlink.
    </p>
    <br />
    <h2>The Ajax queue or the __VIEWSTATE problem</h2>
    <p>
        If you google for <em>__VIEWSTATE and Ajax</em> you will get a feeling for the <em>sequential Ajax queue
        problem</em>. All though most people would believe so, the <em>__VIEWSTATE and Ajax problem</em> is not the 
        real problem. The ViewState problem is just a symptom of something going wrong.
    </p>
    <p>
        Most Ajax Frameworks (including ASP.NET AJAX) do allow more than one Ajax Request to hit the server
        at the same time. This is the source of many problems, including the <em>__VIEWSTATE and Ajax problem</em>.
        The problem is due to the fact that when you create an Ajax Request which goes towards your server, 
        then you may actually do something which changes the DOM structure on the client in that Ajax Request. 
        Then if you allow *another* Ajax Request to start before the first one has returned then whatever FORM values
        the second Ajax Request fetches to send back to the server might be completely wrong and "undefined"
        since their values have been changed in the first Ajax Request which still haven't returned.
    </p>
    <p>
        The reason why so many sees this as a ViewState problem is that it most commonly shows up as a ViewState
        problem since no matter what you do on the server to manipulate your UI then the ViewState WILL be changed
        and then when you create your second Ajax Request (before the first one have returned) then whatever
        ViewState you submit on your SECOND Ajax Request will NOT be the correct one since this ViewState value
        was collected before the second Request was returned.
    </p>
    <p>
        This is in fact nothing else than variations on the problems we used to have in multi threading 
        applications when we were doing conventional desktop programming. You might think of it like a 
        variation on a <em>Race Condition</em>.
    </p>
    <br />
    <h2>Ra-Ajax and its Ajax queue</h2>
    <p>
        Ra-Ajax solves this problem by having an <em>Ajax Queue</em> which will start queuing up Ajax requests
        if there is another one currently going on which haven't returned yet. This means that all Ajax Requests
        which are initiated will go into an <em>Ajax Queue</em> and not be dispatched before there's a "free lane"
        in the "ajax highway" which it can pass on. In fact I have created an example for you to look at below.
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
        When you click the first Ajax LinkButton then there is an *INTENTIONAL* Sleep of 5 seconds to simulate
        a lengthy request or job. Then when you click the second LinkButton (hopefully you managed to do this 
        before the first ones returned) you will see that nothing happens and the request is not even dispatched
        *before* the first Ajax Request returns. In most Ajax Frameworks I am aware of (including ASP.NET AJAX) 
        the second Ajax Request would be allowed to start before the first one returned and change the Text of
        that LinkButton to <em>"Then QUICKLY click this LinkButton before the other one returnsI am CLICKED --"</em>.
        Then when the first Ajax Request returns the Text of the second LinkButton will be changed to; 
        <em>"-- watch me as, "</em>. This is semantically *WRONG*! If you want to test this with another Ajax 
        Framework to see if it handles your Ajax Queue correctly you can copy the logic code from the codebehind 
        of this page into a sample built around your favorite Ajax Framework and see how it handles the above 
        sample.
    </p>
    <p>
        In fact the Ajax Queue of Ra-Ajax can be seen as you click the LinkButtons above if you have 
        <a href="http://getfirebug.com">FireBug</a> installed by observing that as long as the first
        Ajax Request still is "hanging" towards the server no other Ajax Request will turn up in the 
        Console of FireBug.
    </p>
    <p>
        Let me sum up the "rights and wrongs" here. The second Ajax LinkButton should end up displaying something 
        like the below according to whether or not it implements an Ajax Queue.
    </p>
    <ul>
        <li>
            WRONG; <em>"-- watch me as, "</em> 
        </li>
        <li>
            RIGHT; <em>"-- watch me as, I am CLICKED --"</em>
        </li>
    </ul>
    <p>
        If your favorite Ajax Framework doesn't display the results according to the above logic
        then it does NOT implement an Ajax Queue which is crucial in order to avoid creating really obscure
        and impossible to track down bugs later down the road for you...
    </p>
</asp:Content>

