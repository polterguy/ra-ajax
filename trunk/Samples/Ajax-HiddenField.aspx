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

    <h1>Ra Ajax Samples - HiddenField</h1>
    <p>
        This is our reference sample for our <em>Ajax HiddenField Control</em>. The way you are used
        to creating Ajax applications in <a href="http://www.asp.net/ajax/">ASP.NET AJAX</a> is probably
        by having normal WebControls like Button, CheckBox and HiddenField on your page and then
        wrapping those controls inside an UpdatePanel. Ra-Ajax works in a completely different way, which 
        this <em>HiddenField sample</em> is a testimonial to.
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
    <h2>So how does it work then?</h2>
    <p>
        If you look at the source code for this page by clicking the "Show Code" button at the top
        of the page, you will see that there are no <em>UpdatePanels</em> here. Instead you will see
        that we're using another Control set. Instead of writing <em>&lt;asp:HiddenField</em> we're 
        writing <em>&lt;<strong>ra</strong>:HiddenField</em> and so on. Then when any Ra-Ajax Control 
        triggers an Ajax Request, we're serializing the entire form and from the server we have a
        <em>Response Filter</em> which again returns values from Ra-Ajax Controls in a special way
        by using JSON.
    </p>
    <br />
    <h2>Why not use UpdatePanels?</h2>
    <p>
        UpdatePanels have their advantages, among other things they are easier to implement, in 
        addition to that, they will mostly enable existing normal postback controls to create Ajax 
        Requests and to some extent, maybe even re-render themselves on the server.
        Though as we discussed in our <a href="Ajax-DropDownList.aspx">Ajax DropDownList</a> sample 
        (note: you should read these samples sequentially the first time) Partial Rendering, which is 
        effectively what UpdatePanels do, has several disadvantages.
    </p>
    <p>
        In addition, the complexity of UpdatePanels will often become very large. Imagine that you have
        a rather big and complex webpage where you have 15 different sections that need to be updated
        based on a very specific set of conditions. Then you would have to add up <em>15 different update 
        panels</em> which probably have at least a handful of <em>trigger collections</em> each.
        Needless to say, this becomes very tedious to maintain after you leave it and come back,
        say 6 months afterwards, to create "version 2" of your application.
    </p>
    <p>
        <em>"...let me see, which of my 15 update panels, which have 5 triggers on average, which becomes
        in total 75 different triggers, was this bug within now..."</em>
    </p>
    <p>
        Ra-Ajax will instead completely mimic the way you're used to in creating conventional postback ASP.NET applications
        and make it possible for you, in most cases, to just replace the ASP: tag prefix with RA:
        and you're ajaxified. So although UpdatePanels can look tempting from a distance, mostly in
        complex applications you will notice that using them will make your application more difficult to maintain, more 
        difficult to debug and more difficult to understand for outsiders.
    </p>
    <br />
    <h2>Proof</h2>
    <p>
        Imagine the above <em>HiddenField Ajax sample</em>. Now if we were to create this logic with 
        UpdatePanels we would have to have one UpdatePanel wrapped around the TextBox. Another 
        UpdatePanel wrapped around our last Button. Then when the first button is clicked
        we would have to have a trigger for that event on the first UpdatePanel. Then
        when the second Button is clicked we would have to have a trigger for the second UpdatePanel
        which re-renders our second Button. And in fact already, we've done something which is 
        <em>impossible</em> to do in ASP.NET AJAX without resorting to custom JavaScript, since in the 
        solution above when you click the second Button that button actually keeps the focus.
        In ASP.NET AJAX that last UpdatePanel would be re-rendered and the second Button would
        lose focus.
    </p>
    <br />
    <h2>And this was a no-brainer!</h2>
    <p>
        Imagine how complex it can become when you create something complex which is of *actual use*...!
        <br />
        All we did here was to have a HiddenField which at the click of a Button got a new Value
        from a TextBox before that TextBox Text property was set to "". Then at the click of another
        Button we retrieve that HiddenField Value and put it as the Text property of the clicked button.
        If you don't count the lines of code for the method signature but only the "functional parts"
        of that code, they are <em>3 lines of code</em>. Still in ASP.NET AJAX this is impossible
        without resorting to writing JavaScript yourself.
    </p>
    <p>
        With Ra-Ajax you don't need to use UpdatePanels and Trigger Collections. In fact, the code you write with Ra-Ajax 
        controls would be exactly the same code you would write in a conventional ASP.NET application. And the Ajax magic 
        will just happen without you needing to worry about it.
    </p>
    <p>
        UpdatePanels might be easy to implement and look cool on stage when demoing. But they surely aren't 
        easy to use when you dive into something of higher complexity than Hello World. And they sure don't 
        look that nice when you have fifty of them, each having a trigger collection of 15 items.
    </p>
    <p>
        If you are still using ASP.NET Ajax and UpdatePanels, 
        <a href="http://code.google.com/p/ra-ajax">Download Ra-Ajax</a> and give it a quick try. We think you will like it
        instantly.
    </p>
    <a href="Ajax-Image.aspx">On to Ajax Image</a>
</asp:Content>

