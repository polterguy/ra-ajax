<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-CheckBox.aspx.cs" 
    Inherits="AjaxCheckBox" 
    Title="Ajax CheckBox Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ajax CheckBox Sample</h1>
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
    <br />
    <br />
    <h2>About the Ra-Ajax Inheritance Hierarchy</h2>
    <p>
        Ra-Ajax does *NOT* inherit from the "native WebControls". This means that a Ra-Ajax CheckBox is NOT
        also a <em>System.Web.UI.WebControls.CheckBox</em> control. There are several reasons to that. Some
        of those reasons I will try to argue for here...
    </p>
    <br />
    <h3>The CssStyleCollection problem</h3>
    <p>
        First of all there's the problem with the <em>CssStyleCollection</em> property. This class is first
        of all sealed. This means you cannot inherit from it. Second of all the CTOR (constructor) is *INTERNAL*.
        Both of these are very stupid decisions from Microsoft since it effectively eliminates the possibility 
        of in any ways "change" the behavior of the style property on your WebControls.
    </p>
    <p>
        To circumvent this in Ra-Ajax we actually dropped inheriting from <em>WebControl</em> and consistantly instead
        inherited from <em>Control</em> and then implemented our "own" Style collection class(es). This makes it
        possible for Ra-Ajax to intervene changes sent to the Style property and then send only those back to
        the client when changes are done on the server.
    </p>
    <br />
    <h3>The Font, Border, Color, properties problems...</h3>
    <p>
        Microsoft when creating the WebControl, Button, CheckBox classes and so on decided they should have "short hand"
        versions or "shortcuts" for the most "common" properties in the style collection. That was not their greatest 
        hour in regards to API development. Most of the API in ASP.NET and especially .Net is very cleverly put together
        and they have some very beautiful concepts like the fact that System.String is immutable (and others) but
        the redundancy in the WebControl class is NOT beautiful. You should always prefer to have ONE entry from your
        API when developing APIs. By having two entries into setting effectively the same property Microsoft 
        effectively created a problem for WebControl developers which is especially hard to bypass in Ajax Libraries
        written on top of ASP.NET.
    </p>
    <br />
    <h3>Ra-Ajax does NOT inherit from WebControl!</h3>
    <p>
        Now if you combine those two reasons above you effectively get Ra-Ajax in a nutshell. None of the Ra-Ajax
        Controls are inheriting indirectly or in any ways from the <em>System.Web.UI.WebControls.WebControl</em> class.
        This is intentionally and virtually saved us for months of work and debugging when creating Ra-Ajax. It also
        created a far superior product and did put us in a WAY sweater spot than what we could achieve if we were
        trying to implement support for the "native Style" property and support all the redundant properties in 
        WebControl.
    </p>
    <a href="Ajax-DropDownList.aspx">On top Ajax DropDownList</a>
</asp:Content>

