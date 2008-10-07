<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Button.aspx.cs" 
    Inherits="Samples.AjaxButton" 
    Title="Ra-Ajax Button Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra-Ajax Samples - Button</h1>
    <p>
        We will try to create one simple and easy to understand sample of every control there is in Ra-Ajax. While
        creating these reference samples we will also try to write something intelligent about some of the general
        concepts of Ra-Ajax. This is the <em>Ajax Button</em> example.
    </p>
    <ra:Button 
        runat="server" 
        ID="btn" 
        OnClick="click" 
        OnMouseOut="mouseout" 
        OnMouseOver="mouseover" 
        Text="Ajax Button" />
    <p>
        As you can see we have here created Event Handlers for <em>OnClick, OnMouseOut and OnMouseOver</em>.
        In addition the button also has events for <em>Focus and Blur</em>. If you try to move the mouse
        over the button you will see that it changes text to "mouseover" while if you try to move the
        mouse out it will yet again change to "mouseout" and when clicked it will show the text "clicked".
        In addition the <em>OnClick</em> event handler will manipulate the styles, changing the background color
        of the button to Yellow.
    </p>
    <h2>CSS Styles and Ra-Ajax</h2>
    <p>
        In addition, the <em>Ra-Ajax Button</em>, as most other Ra-Ajax Controls, has a style 
        collection through its "Style" or "style" property. The <em>style</em> property just maps to the 
        CSS styles and you can set any CSS style you wish here. A small subset of different styles are 
        given here for references, but for a more exhaustive reference I would encourage you to go to e.g. 
        <a href="http://www.w3.org/TR/REC-CSS2/">the W3C website</a>.
    </p>
    <ul class="bulList">
        <li>background-color - (color)</li>
        <li>color - (color)</li>
        <li>font-style - (italic, normal, oblique and inherit)</li>
        <li>position - (absolute, fixed, relative, static and inherit)</li>
        <li>left - (number + unit (px, % etc))</li>
        <li>top - (number + unit (px, % etc))</li>
    </ul>
    <p>
        These styles will then be transformed to their JS DOM counterparts when sent to the client,
        which means that e.g. <em>background-color</em> will become <em>backgroundColor</em>. This is a general
        pattern for being able to set styles through JavaScript code. After the transformation and when the Ajax 
        Request is finished they will be transfered back to the client as JSON which again maps to the Style
        JavaScript method in <em>Control.js</em> which is the <em>Ajax Control JavaScript wrapper file</em> for
        most Ra-Ajax controls.
    </p>
    <p>
        But all this is completely abstracted away so you don't have to think of anything else than changing
        styles for your controls by using the <em>Style property</em> of your Ajax Controls.
    </p>
    <a href="Ajax-CheckBox.aspx">On top Ajax CheckBox sample</a>
</asp:Content>

