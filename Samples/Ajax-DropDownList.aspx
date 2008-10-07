<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-DropDownList.aspx.cs" 
    Inherits="Samples.AjaxDropDownList" 
    Title="Ra-Ajax DropDownList Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra-Ajax Samples - DropDownList</h1>
    <p>
        This is our <em>Ajax DropDownList</em> reference sample. Although we try to create most samples 
        like a "story" giving away clues to important things about Ra-Ajax as we progress, which means that
        you definitely should read them all sequentially and rather return to specific samples for reference 
        purposes later. The specific Control Samples obviously have to be more of a bread and butter construction. 
    </p>
    <p>
        Here you can see how you can use our <em>Ajax DropDownList</em> in your own projects. We're just 
        handling the <em>MouseOut, MouseOver and the SelectedIndexChanged events</em>, but there are also others for 
        Focus events and Blur events. In case you haven't noticed yet you should click the "Show code" button 
        in the top right corner, since a large reason for using Ra-Ajax is the fact that it enables 
        you to write complex Ajax Applications <em>without even knowing what JavaScript is</em> and keeping the
        same <em>Code Model</em> as you would do in conventional non-Ajax ASP.NET Applications.
    </p>
    <p>
        <ra:DropDownList 
            runat="server" 
            ID="list" 
            OnSelectedIndexChanged="selectedchanged" 
            OnMouseOut="mouseout" 
            OnMouseOver="mouseover">
            <ra:ListItem Text="Item 1" Value="Item1" />
            <ra:ListItem Text="Item 2" Value="Item2" />
        </ra:DropDownList>
    </p>
    <p>
        <ra:Label 
            runat="server" 
            ID="lblResults" 
            Text="Changes as you hover DropDownList"
            CssClass="updateLbl" />
    </p>
    <p>
        <ra:Label 
            runat="server" 
            ID="lblResults2" 
            Text="Changes as you SELECT items"
            CssClass="updateLbl" />
    </p>
    <p>
        Try to hover over the above DropDownList and then select one of its items...
    </p>
    <h2>Ra-Ajax and Partial Rendering</h2>
    <p>
        Ra-Ajax does not use <em>Partial Rendering</em> almost at all. Only when strictly necessary Ra-Ajax
        will turn to Partial Rendering. Instead, Ra-Ajax sends JSON from the server which defines which 
        properties on the controls that have changed. Then this JSON maps to methods in the <em>Control.js</em>
        JavaScript file. Which JSON is sent is determined according to which properties have been changed
        on the server. This means that only the things you actually change on your controls will be sent
        from the server and back to the client. Then the changed properties will be sent from the JavaScript
        JSON handler and to the specific method which handles just "that" JSON value. For instance if you
        check out the Ajax Request sent back from the server when you hover over the Ajax DropDownList above
        you will see that it send something like this;
    </p>
    <p>
        Ra.Control.$('ctl00_cnt1_lblResults').handleJSON({"Text":"mouse over"});
    </p>
    <p>
        The <em>Ra.Control.$</em> function is to retrieve a specific instance of a Ra-Ajax control on the client
        side. The client (browser) stores all the controls, including also the DropDownList above. The string
        inside of it <em>"ctl00_cnt1_lblResults"</em> is the ClientID of the first Label and also the HTML ID
        of the DOM Element for the Label on the client side.
    </p>
    <p>
        This will return an object of type Ra.Control which happens to have an "instance function" called 
        <em>handleJSON</em> which expects to be given a JSON object. The JSON handler will iterate through
        the list of (yes it is a list, although the above sample only shows ONE entry in it) mappings
        and use the first as the name of a function (<em>Text</em>) and send the last as the <em>value</em>
        to that function.
    </p>
    <p>
        Then the Control.js (or the Ra.Control object) has a function called <em>Text</em> which then again
        will set the innerHTML (through the <em>Ra.Element.setContent</em> function) of the DOM element.
    </p>
    <h2>Advantages of Not Relying on Partial Rendering</h2>
    <p>
        Partial Rendering is very easy to implement, however it has significant disadvantages compared to other
        alternatives which among other things include, <em>Custom JavaScript</em> and the method used in Ra-Ajax. 
        First of all, it is almost impossible to do complex things without resorting to Custom JavaScript.
    </p>
    <p> 
        Imagine if you had a Panel and inside that Panel you had a DropDownList or a TextBox. Then when the user 
        selects the "wrong value" you want the background-color of that Panel to become Red to signalize this to the user.
        Now if you had a DropDownList the DropDownList would lose focus, if you were to do this with Partial 
        Rendering. It is even worse if you had a TextBox, since then you could even run the risk of that the user
        had written something into the TextBox after the Ajax Request was initiated but before the Ajax 
        Request had returned. This would then, when the TextBox is re-rendered in the Partial Rendering phase, 
        remove characters that you actually had written and make writing text inside that TextBox almost 
        impossible.
    </p>
    <p>
        The other disadvantage it has is that Partial Rendering significantly increases the amount of bandwidth 
        usage! Let me repeat that again; <em>Partial Rendering significantly increases the bandwidth usage!</em>
        To such an extent that some tests can show orders of magnitude more bandwidth usage with Partial Rendering
        compared to the Ra-Ajax way of doing Ajax.
    </p>
    <p>
        So not only can we, since we're not using Partial Rendering, create really advanced functionality without
        having to resort to JavaScript in the application layer, but we can also significantly reduce the
        bandwidth usage and therefor the speed and responsiveness of our applications.
    </p>
    <p>
        In fact when Jason Diamond, who "invented" Ajax Libraries for ASP.NET utilizing the whole Page Life Cycle,
        created <a rel="nofollow" href="http://anthemdotnet.com/">Anthem.NET</a> he himself refered to the Partial Rendering
        (though before it had a name) as a "dirty hack". Most server-side binded Ajax Libraries, including those 
        for ASP.NET today are exclusively created utilizing Partial Rendering. 
    </p>
    <p>
        Sending JSON, which again maps to Client-Side JavaScript functions, will take you to orders of magnitude 
        higher complexity before forcing you to use custom JavaScript.
    </p>
    <a href="Ajax-HiddenField.aspx">On to Ajax HiddenField</a>
</asp:Content>

