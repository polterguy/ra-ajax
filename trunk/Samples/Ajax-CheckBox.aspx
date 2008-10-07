<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-CheckBox.aspx.cs" 
    Inherits="Samples.AjaxCheckBox" 
    Title="Ra-Ajax CheckBox Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra-Ajax Samples - CheckBox</h1>
    <p>
        Here is the reference example for the <em>Ra-Ajax CheckBox Control</em>. Here we have Event Handlers for 
        <em>OnCheckedChanged, OnMouseOut and OnMouseOver</em>. As you can see the Ra-Ajax CheckBox is quite
        similar to the <a href="Ajax-Button.aspx">Ra-Ajax Button</a>. Though instead of the OnClick Event
        it handles the <em>OnCheckedChanged</em> Event. In addition it has a <em>Checked</em> property
        which basically is true if the CheckBox is checked and otherwise false.
    </p>
    <ra:CheckBox 
        runat="server" 
        ID="btn"  
        OnCheckedChanged="checkedchanged" 
        OnMouseOut="mouseout" 
        OnMouseOver="mouseover" 
        Text="Ajax CheckBox" />
    <h2>About the Ra-Ajax Inheritance Hierarchy</h2>
    <p>
        Ra-Ajax does not inherit from the native ASP.NET WebControls. This means that a Ra-Ajax CheckBox is not
        also a <em>System.Web.UI.WebControls.CheckBox</em> control. There are several reasons behind that choice. Some
        of those reasons I will try to give you here...
    </p>
    <h2>The CssStyleCollection problem</h2>
    <p>
        First of all there's the problem with the <em>CssStyleCollection</em> property. This is a class in ASP.NET
        and actually the type of the <em>Style property</em> when you are using ASP.NET WebControls. This class is first
        of all sealed. This means you cannot inherit from it. Second of all the CTOR (constructor) is internal.
        Both of these decisions are not among the brightest decisions from Microsoft, since it effectively eliminates the possibility 
        of changing the default behavior of the style property on your own custom WebControls.
    </p>
    <p>
        To circumvent this in Ra-Ajax we actually dropped inheriting from <em>WebControl</em> and consistantly instead
        inherited from <em>Control</em> and then implemented our "own" Style collection class(es). This makes it
        possible for Ra-Ajax to intervene changes sent to the Style property and then send only those back to
        the client when changes are done on the server.
    </p>
    <h2>The Font, Border, Color, properties problems...</h2>
    <p>
        Microsoft, when creating the WebControl, Button, CheckBox classes and so on, decided they should have "short hand"
        versions or "shortcuts" for the most commonly used properties in the style collection. That was not their greatest 
        hour in regards to API development. Most of the API in ASP.NET and especially .Net is very cleverly put together
        and they have some very beautiful concepts like the fact that System.String is immutable (and others) but
        the redundancy in the WebControl class is not beautiful. You should always prefer to have <em>one</em> entry 
        from your API when developing APIs. By having two entries for setting effectively the same property, Microsoft 
        effectively created a problem for WebControl developers which is especially hard to bypass in Ajax Libraries
        written on top of ASP.NET. Someone should have told Microsoft about 
        <a href="http://en.wikipedia.org/wiki/You_Ain%27t_Gonna_Need_It">YAGNI</a> ;)
    </p>
    <h2>Ra-Ajax does not inherit from WebControl!</h2>
    <p>
        Now if you combine those two reasons above, you effectively get Ra-Ajax in a nutshell. None of the Ra-Ajax
        Controls are inheriting, directly, indirectly or in any other shape or form, from the <em>System.Web.UI.WebControls.WebControl</em> 
        class. This is intentional and virtually saved us for months of work and debugging when creating Ra-Ajax. It also
        created a far better Ajax Library and did put us in a far sweeter spot.
    </p>
    <a href="Ajax-DropDownList.aspx">On top Ajax DropDownList</a>
</asp:Content>

