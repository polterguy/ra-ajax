<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Menu.aspx.cs" 
    Inherits="Samples.Menu" 
    Title="Ra-Ajax Menu Sample" %>

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

    <h1>Ra-Ajax Samples - Menu</h1>
    <p>
        This is our Ajax Menu sample. As most "advanced samples" its purpose is more to serve as an example of how
        you can build your own Ajax Menu than an extensive Ajax Menu by itself.
    </p>
    <ra:Label 
        runat="server" 
        CssClass="infoLbl" 
        Text="Watch me change..."
        ID="lbl" />
    <ext:Menu runat="server" ID="menu" CssClass="menu" OnMenuItemSelected="menu_MenuItemSelected">
        <ext:MenuItems runat="server" ID="mainItems">
            <ext:MenuItem runat="server" ID="menuFile">
                File
                <ext:MenuItems runat="server" ID="fileMenus">
                    <ext:MenuItem runat="server" id="load">
                        Open...
                    </ext:MenuItem>
                    <ext:MenuItem runat="server" id="save">
                        Save
                    </ext:MenuItem>
                    <ext:MenuItem runat="server" id="saveAs">
                        Save As...
                    </ext:MenuItem>
                </ext:MenuItems>
            </ext:MenuItem>
            <ext:MenuItem runat="server" ID="edit">
                Edit
                <ext:MenuItems runat="server" ID="editMenus">
                    <ext:MenuItem runat="server" id="copy">
                        Copy
                    </ext:MenuItem>
                    <ext:MenuItem runat="server" id="paste">
                        Paste
                    </ext:MenuItem>
                    <ext:MenuItem runat="server" id="cut">
                        Cut
                    </ext:MenuItem>
                </ext:MenuItems>
            </ext:MenuItem>
            <ext:MenuItem runat="server" ID="options">
                Options
                <ext:MenuItems runat="server" ID="optionsMenu">
                    <ext:MenuItem runat="server" id="configuration">
                        Configuration
                        <ext:MenuItems runat="server" ID="configItems">
                            <ext:MenuItem runat="server" id="config1">
                                Configuration 1
                            </ext:MenuItem>
                            <ext:MenuItem runat="server" id="config2">
                                Configuration 2
                            </ext:MenuItem>
                        </ext:MenuItems>
                    </ext:MenuItem>
                    <ext:MenuItem runat="server" id="settings">
                        Settings...
                    </ext:MenuItem>
                </ext:MenuItems>
            </ext:MenuItem>
        </ext:MenuItems>
    </ext:Menu>
    <p>
        Try to select a menu item from the menu above.
    </p>
    <p>
        As you can see a menu can have child menus and so on into "infinity". And when selecting a root menu its parent
        menu is collapsed.
    </p>
    <p>
        Our menu is built entirely from UL and LI elements instead of spans and div HTML elements. This makes it more
        accessible and easier to skin. Though since our Ajax Menu is our newest Ajax Citizen it's still kind of rough
        and you should expect changes to the DOM structure. Though as most other "complex controls" it adds ZERO to the
        JavaScript load since it's entirely composed of other existing Ajax controls like Panel, Labels and so on.
    </p>
    <h2>Reusing existing Ajax Controls</h2>
    <p>
        In fact this is a pattern being used through the entire library - that we reuse existing Ajax Controls to
        build more complex Ajax Controls. This makes the JavaScript load for your Ra-Ajax pages 
        <strong>orders of magnitudes smaller</strong> than if you did the same thing in virtually ANY other Ajax
        Framework. Including Prototype, jQuery, MooTools, Dojo Toolkit, ASP.NET AJAX and so on. <strong>The amount 
        of JavaScript will always be significantly lower with Ra-Ajax than ANY other known Ajax Framework</strong>.
    </p>
    <a href="Ajax-WebMethod.aspx">On to Ajax WebMethods</a>
</asp:Content>
