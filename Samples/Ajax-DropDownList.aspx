<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-DropDownList.aspx.cs" 
    Inherits="AjaxDropDownList" 
    Title="Ajax DropDownList Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ajax DropDownList Sample</h1>
    <p>
        This is our <em>Ajax DropDownList</em> reference sample. All though we try to create most samples 
        like a "story" giving away clues to important things about Ra-Ajax as we progress which means that
        you definitely *should* read them all sequentially and rather return to them for "reference purposes". 
        The "Control Samples" obviously have to be more of "bread and butter" constructions. Here you can 
        see how you can use our <em>Ajax DropDownList</em> in your own projects. We're just handling the 
        MouseOut, MouseOver and the SelectedIdexChanged events, but there are also others for Focus events 
        and Blur events. In case you haven't noticed yet you should click the "Show code" button in the 
        top/right corner since a large reason to use Ra-Ajax is its beautiful code model which enables you 
        to write complex Ajax Applications <em>without even knowing what JavaScript IS</em>.
    </p>
    <ra:DropDownList 
        runat="server" 
        ID="list" 
        OnSelectedIndexChanged="selectedchanged" 
        OnMouseOut="mouseout" 
        OnMouseOver="mouseover">
        <ra:ListItem Text="Item 1" Value="Item1" />
        <ra:ListItem Text="Item 2" Value="Item2" />
    </ra:DropDownList>
    <br />
    <ra:Label 
        runat="server" 
        ID="lblResults" 
        Text="Changes as you hover DropDownList"
        style="color:#999;font-style:italic;" />
    <br />
    <ra:Label 
        runat="server" 
        ID="lblResults2" 
        Text="Changes as you SELECT items"
        style="color:#999;font-style:italic;" />
    <br />
    <p>
        Try to hover over the above DropDownList and then select one of the items in it afterwards...
    </p>
    <br />
    <h2>Ra-Ajax and Partial Rendering</h2>
    <p>
        Ra-Ajax does not use <em>Partial Rendering</em> almost at all, only when strictly necessary Ra-Ajax
        will turn to Partial Rendering. Instead Ra-Ajax sends JSON from the server which defines which 
        properties on the controls that have changed. Then this JSON maps to methods in the <em>Control.js</em>
        JavaScript file. Which JSON is sent is determined according to which properties have been changed
        on the server. This means that only the things you actually change on all controls will be sent
        from the server and back to the client. Then the changed properties will be sent from the JavaScript
        JSON handler and to the specific method which handles just "that" JSON value. For instance if you
        check out the Ajax Request sent back from the server when you hover over the Ajax DropDownList above
        you will see that it send something like this;
    </p>
    <p>
        <em>Ra.Control.$('ctl00_cnt1_lblResults').handleJSON({"Text":"mouse over"});</em>
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
        the list of (yes it is a list all though the above sample only shows ONE entry in it) mappings
        and use the first as the name of a function (<em>Text</em>) and send the last as the <em>value</em>
        to that function.
    </p>
    <p>
        Then the Control.js (or the Ra.Control "type") have a function called <em>Text</em> which then again
        will set the innerHTML (through the <em>Ra.Element.setContent</em> function) of the DOM element.
    </p>
    <br />
    <h2>Advantages of "ditching" Partial Rendering</h2>
    <p>
        Partial Rendering is very easy to implement, however it has significant disadvantages to the alternatives
        which among other things includes <em>Custom JavaScript</em> plus the method of Ra-Ajax. First of all
        it is almost impossible to do complex things without resorting to Custom JavaScript. Imagine if you
        had a Panel and inside that Panel you had a DropDownList or a TextBox. Then when the user selects the 
        "wrong value" you want the background-color of that Panel to become Red to signalize this to the user.
        Now if you had a DropDownList the DropDownList would *loose focus* if you were to do this with Partial 
        Rendering. Even worse though it is for a TextBox since then you could even run the risk of that the user
        had written something into the TextBox after the Ajax Request was being initiated but BEFORE the Ajax 
        Request had returned. This would then when the TextBox was "re-drawn" since it was inside of the Panel
        remove characters that you actually had written and make writing of text inside that TextBox almost 
        impossible.
    </p>
    <p>
        The other disadvantage it has is that Partial Rendering significantly increases the amount of bandwidth 
        usage! Let me repeat that again; <strong>Partial Rendering significantly increases the bandwidth usage!</strong>
        To such an extend that some tests can see orders of magnitudes more bandwidth usage with Partial Rendering
        than the Ra-Ajax way of doing this.
    </p>
    <p>
        So not only can we since we're not using Partial Rendering create really advanced functionality without
        having to resort to JavaScript in the application layer, but we can also significantly reduce the
        bandwidth usage and therefor the speed and responsiveness of our applications.
    </p>
    <p>
        In fact when Jason Diamond which "invented" Ajax Libraries for ASP.NET utilizing the whole Page Life Cycle
        created <a href="http://anthemdotnet.com/">Anthem.NET</a> he himself refered to the Partial Rendering
        (though before it had a name) as a "dirty hack". Most server-side binded Ajax Libraries, including those 
        for ASP.NET today are exclusively created utilizing Partial Rendering. 
    </p>
    <p>
        Sending JSON which again maps to Client-Side JavaScript functions will take you to orders of magnitudes 
        higher complexity before forcing you to use "Custom JavaScript"...
    </p>
    <a href="ajax-hiddenfield.aspx">On to Ajax HiddenField</a>
</asp:Content>

