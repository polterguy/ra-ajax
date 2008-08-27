<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-HiddenField.aspx.cs" 
    Inherits="AjaxHiddenField" 
    Title="Ajax HiddenField Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ajax HiddenField Sample</h1>
    <p>
        This is our reference sample for our <em>Ajax HiddenField Control</em>. The way you are used
        to creating Ajax Applications from <a href="http://www.asp.net/ajax/">ASP.NET AJAX</a> is probably
        by having "normal" WebControls like Button, CheckBox and HiddenFields on your page and then
        to "wrap" those controls inside of an UpdatePanel.
    </p>
    <p>
        Ra-Ajax works in a completely different way which this <em>HiddenField example</em> is a testimonial 
        in regards to.
    </p>
    <ra:HiddenField 
        runat="server" 
        ID="hid" />
    <ra:TextBox 
        runat="server" 
        ID="txt" />
    <ra:Button 
        runat="server" 
        ID="submit" 
        Text="Save value" 
        OnClick="submit_Click" />
    <ra:Button 
        runat="server" 
        ID="retrieveValue" 
        Text="Retrieve value" 
        OnClick="retrieveValue_Click" />
    <br />
    <br />
    <h2>So how does it work then...?</h2>
    <p>
        If you look at the source code for this page by clicking the "Show Code" button at the top
        of the page you will see that there are no <em>UpdatePanels</em> here. Instead you will see
        that we're using another Control set. Instead of writing <em>&lt;asp:HiddenField</em> we're 
        writing <em>&lt;<strong>ra</strong>:HiddenField</em> and so on. Then when any Ra-Ajax Control 
        triggers an Ajax Request we're serializing the entire form and from the server we have a
        <em>Response Filter</em> which again returns values from Ra-Ajax Controls in a special way
        by using JSON.
    </p>
    <br />
    <h2>Why not use UpdatePanels</h2>
    <p>
        UpdatePanels have their advantages, among other things they are easier to implement in 
        addition to that they will mostly get existing normal postback controls to also become able
        to create Ajax Requests and to some extend maybe also even re-render themselves on the server.
        Though as we discussed in our <a href="Ajax-DropDownList.aspx">Ajax DropDownList</a> sample 
        (you really should read these samples sequentially) Partial Rendering which effectively 
        UpdatePanels are have several disadvantages.
    </p>
    <p>
        In addition the complexity of UpdatePanels will very often become very large. Imagine you have
        a rather big and complex webpage where you have 15 different sections which needs to be updated
        in a very specific set of conditions. Then you would have to add up <em>15 different update 
        panels</em> which probably have at least a handful of <em>trigger collections</em> each.
        Needless to say this becomes very tedious to maintain after you leave this alone and come back
        say 6 months afterwards to create "version 2" of your application.
    </p>
    <p>
        <em>"...let me see, which of my 15 update panels which have 5 triggers on average which becomes
        in total 75 different triggers was this bug within now..."</em>
    </p>
    <p>
        Ra-Ajax will instead completely mimick the way you're used to creating ASP.NET applications from
        before ASP.NET AJAX came around and make it possible for you to just exchange the ASP: with RA:
        and you're ajaxified. So all though UpdatePanels can look tempting from a distance, mostly in
        complex applications you will notice that they tend to be more difficult to maintain, more 
        difficult to fix bugs in and more difficult to understand for outsiders...
    </p>
    <br />
    <h2>Proof;</h2>
    <p>
        Imagine the above <em>HiddenField Ajax sample</em>. Now if we were to create this logic with 
        UpdatePanels we would have to have one UpdatePanel wrapped around the TextBox. Another 
        UpdatePanel wrapped around our last Ajax Button. Then when the first button was clicked
        we would have to have a trigger for that event on the first UpdatePanel. Then
        when the second Button was clicked we would have to have a trigger for the second UpdatePanel
        which re-rendered our second TextBox. And in fact already we've done something which is 
        <em>impossible</em> to do in ASP.NET AJAX without resorting to "Custom JavaScript" since in the 
        solution above when you click the second Button that button actually keeps the focus.
        In ASP.NET AJAX that last UpdatePanel would be re-rendered and the second Button would
        loose the focus.
    </p>
    <br />
    <h2>And this was a no-brainer!</h2>
    <p>
        Imagine how complex it can become when you create something complex which is of *actual use*...!
        <br />
        All we did here was to have an HiddenField which at the click of a Button got a new Value
        from a TextBox before that TextBox Text property was set to "". Then at the click of another
        Button we retrieve that HiddenField Value and put it as the Text property of the clicked button.
        If you don't count the lines of code for the method signatures but only the "functional parts"
        of that code this is <em>3 lines of code</em>. Still in ASP.NET AJAX this is impossible
        without resorting to writing JavaScript yourself...
    </p>
    <p>
        Ohh yeah, and there were no UpdatePanels and no Trigger Collections. In fact the code is exactly
        the same code you would have written in a "conventional" ASP.NET WebControl solution... ;)
    </p>
    <p>
        UpdatePanels might be easy to implement and look cool on stage when demoing. But they surely aren't 
        easy to use when you dive into something of higher complexity than Hello World. And they sure don't 
        look that nice when you have fifty of them, each having a trigger collection of 15 items...
    </p>
    <p>
        Join the <em>"Say no to UpdatePanels movement"</em> and 
        <a href="http://code.google.com/p/ra-ajax">download Ra-Ajax TODAY</a>... ;)
    </p>
    <a href="Ajax-Image.aspx">On to Ajax Image</a>
</asp:Content>

