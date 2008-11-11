<%@ Page 
    Language="C#" 
    AutoEventWireup="true"  
    CodeFile="Viewport-Sample.aspx.cs" 
    Inherits="Samples.Viewport" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sapphire Ra-Ajax Viewport Starter-Kit</title>
    <link href="media/skins/Sapphire/Sapphire-0.7.6.css" rel="stylesheet" type="text/css" />
    <link href="media/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
        <!-- Main surface -->
        <ra:Panel 
            runat="server" 
            ID="everything" 
            class="main">
            <ext:ResizeHandler 
                runat="server" 
                OnResized="resizer_Resized"
                ID="resizer" />

            <!-- "Statusbar" part -->
            <ra:Panel 
                runat="server" 
                ID="pnlStatus" 
                CssClass="status">
                Name of your application or company - maybe a logo...?
            </ra:Panel>

            <!-- Menu -->
            <ext:Menu 
                runat="server" 
                ID="menu" 
                CssClass="menu" 
                OnMenuItemSelected="menu_MenuItemSelected">
                <ext:MenuItems runat="server" ID="mainItems">
                    <ext:MenuItem runat="server" ID="WebStandards">
                        Web Standards
                        <ext:MenuItems runat="server" ID="fileMenus">
                            <ext:MenuItem runat="server" id="HTML">
                                HTML
                            </ext:MenuItem>
                            <ext:MenuItem runat="server" id="CSS">
                                CSS
                            </ext:MenuItem>
                            <ext:MenuItem runat="server" id="JavaScript">
                                JavaScript
                            </ext:MenuItem>
                        </ext:MenuItems>
                    </ext:MenuItem>
                    <ext:MenuItem runat="server" ID="Ajax">
                        Ajax
                        <ext:MenuItems runat="server" ID="editMenus">
                            <ext:MenuItem runat="server" id="jQuery">
                                jQuery
                            </ext:MenuItem>
                            <ext:MenuItem runat="server" id="Prototype">
                                Prototype
                            </ext:MenuItem>
                            <ext:MenuItem runat="server" id="MooTools">
                                MooTools
                            </ext:MenuItem>
                        </ext:MenuItems>
                    </ext:MenuItem>
                    <ext:MenuItem runat="server" ID="Lockin">
                        Lock-in
                        <ext:MenuItems runat="server" ID="optionsMenu">
                            <ext:MenuItem runat="server" id="Microsoft">
                                Microsoft
                                <ext:MenuItems runat="server" ID="configItems">
                                    <ext:MenuItem runat="server" id="ActiveX">
                                        ActiveX
                                    </ext:MenuItem>
                                    <ext:MenuItem runat="server" id="Silverlight">
                                        Silverlight
                                    </ext:MenuItem>
                                </ext:MenuItems>
                            </ext:MenuItem>
                            <ext:MenuItem runat="server" id="AdobeFlex">
                                Adobe Flex
                            </ext:MenuItem>
                        </ext:MenuItems>
                    </ext:MenuItem>
                </ext:MenuItems>
            </ext:Menu>

            <!-- Top left parts -->
            <ext:Window 
                runat="server" 
                ID="wndTopLeft" 
                Caption="Top left"
                style="width:250px;position:absolute;left:5px;top:63px;" 
                Movable="false" 
                Closable="false"
                CssClass="window">
                <div style="height:250px;overflow:auto;">
                    <ext:Tree 
                        runat="server" 
                        ID="tree" 
                        CssClass="tree"
                        Expansion="SingleClickPlusSign">
                        
                        
                        <ext:TreeNodes ID="TreeNodes1" runat="server" Expanded="true">
                            <ext:TreeNode runat="server" ID="good">
                                <span title="This is the stuff we all LOVE! :)">
                                    Other samples
                                </span>
                                <ext:TreeNodes ID="TreeNodes2" runat="server" Expanded="true">
                                    <ext:TreeNode runat="server" ID="TreeNode1">
                                        <a href="Default.aspx">Main samples</a>
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="TreeNode5">
                                        <a href="Ajax-TreeView.aspx">TreeView sample</a>
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="TreeNode2">
                                        <a href="Ajax-TabControl.aspx">TabControl sample</a>
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="TreeNode3">
                                        <a href="Ajax-Calendar.aspx">Calendar sample</a>
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="TreeNode4">
                                        <a href="Ajax-Wizard.aspx">Wizard sample</a>
                                    </ext:TreeNode>
                                </ext:TreeNodes>
                            </ext:TreeNode>
                            <ext:TreeNode runat="server" ID="bad">
                                <span title="This is the stuff we really dislike... :(">
                                    Proprietary lock-in crap
                                </span>
                                <ext:TreeNodes ID="TreeNodes6" runat="server">
                                    <ext:TreeNode runat="server" ID="flex">
                                        Adobe Flex
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="TreeNode7">
                                        Silverlight
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="TreeNode8">
                                        ActiveX
                                        <ext:TreeNodes ID="TreeNodes7" runat="server">
                                            <ext:TreeNode runat="server" ID="activex1">
                                                ActiveX 1.0
                                            </ext:TreeNode>
                                            <ext:TreeNode runat="server" ID="activex2">
                                                ActiveX 2.0
                                            </ext:TreeNode>
                                        </ext:TreeNodes>
                                    </ext:TreeNode>
                                </ext:TreeNodes>
                            </ext:TreeNode>
                            <ext:TreeNode runat="server" ID="recipes">
                                <span title="Cakes...">
                                    Cakes recipes
                                </span>
                                <ext:TreeNodes ID="cakeRecipes" runat="server">
                                    <ext:TreeNode runat="server" ID="brownie">
                                        Brownies
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="applecake">
                                        Apple Cake
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="crackers">
                                        Crackers
                                        <ext:TreeNodes ID="cracks" runat="server">
                                            <ext:TreeNode runat="server" ID="ritz">
                                                Ritz
                                            </ext:TreeNode>
                                            <ext:TreeNode runat="server" ID="chochips">
                                                Chocolate chips
                                            </ext:TreeNode>
                                        </ext:TreeNodes>
                                    </ext:TreeNode>
                                </ext:TreeNodes>
                            </ext:TreeNode>


                        </ext:TreeNodes>
                    </ext:Tree>
                </div>
            </ext:Window>

            <!-- Bottom left -->
            <ext:Window 
                runat="server" 
                CssClass="window" 
                Closable="false" 
                Movable="false"
                Caption="Bottom left"
                style="width:250px;position:absolute;left:5px;top:352px;"
                ID="wndBottomLeft">
                <ra:Panel 
                    runat="server" 
                    ID="pnlLeft" 
                    style="height:200px;overflow:auto;">
                    <div style="padding:5px;">
                        <h2>Bottom left</h2>
                        <ra:Label 
                            runat="server" 
                            ID="lbl" 
                            Text="Size of Viewport" />
                        <p>
                            As you can see here the resizing will trigger a server-side event which in turn will resize
                            the windows so that at all times the Window will "fill the Viewport" except down to 
                            some "minimum threshold"...
                        </p>
                        <p>
                            Also all Windows are created such that when there is too much text to view at the same 
                            time with their current size then scrollbars will appear and make the Window scrollable...
                        </p>
                    </div>
                </ra:Panel>
            </ext:Window>

            <!-- Right - main content -->
            <ext:Window 
                runat="server" 
                CssClass="window" 
                Closable="false" 
                Movable="false"
                Caption="Right - main content"
                style="width:750px;position:absolute;top:63px;left:260px;"
                ID="wndRight">
                <ra:Panel 
                    runat="server" 
                    ID="pnlRight" 
                    style="height:488px;overflow:auto;">
                    <div style="padding:5px;">
                        <ext:TabControl runat="server" ID="tab" CssClass="tab">
                            <ext:TabView 
                                Caption="Ajax TabControl view 1" 
                                runat="server" 
                                ID="tab1" 
                                style="background:White url('media/ajax.jpg') no-repeat;"
                                CssClass="content">
                                <h1>Sapphire Ra-Ajax Viewport Starter-Kit</h1>
                                <p>
                                    Basically a minimalistic implementation of a viewport. This is also a "starter kit" which
                                    means it's a complete finished Visual Studio solution you can immediately start
                                    using as a template for your own stuff.
                                </p>
                                <p>
                                    Since the TabControl by default has 100% width the TabControl too will follow as you resize
                                    the Viewport. This whole magic was made possible by the newly created ResizeHandler Ajax Control
                                    which will trigger an Ajax Callback and raise an event on the server-side when the Viewport
                                    is resized.
                                </p>
                                <p>
                                    Notice how we've also set a background image for this TabView by modifying the 
                                    styles. This is mostly for fun or "because we can" ... ;)
                                    
                                </p>
                                <p>
                                    Notice though that this is just a "starter kit" and doesn't really contain any
                                    logic. But it's hopefully a pretty nice way to "start your projects running".
                                </p>
                                <p>
                                    This skin we're using here is called "sapphire" but you can probably change to
                                    other skins if you're not happy with the current look. Though this starter-kit
                                    is created towards the Sapphire skin and might need some minor adjustment to
                                    work towards another skin, especially if that other skin have different size
                                    of the borders and such for the Ajax Windows, Ajax TabControls etc...
                                </p>
                            </ext:TabView>
                            <ext:TabView Caption="Tab view 2" runat="server" ID="tab2" CssClass="content">
        	                    <h1>Second TabView</h1>
        	                    <p>
        	                        Here's another TabView.
        	                    </p>
        	                    <p>
        	                        Of course everything in here is 100% interchangeable and "configurable"
        	                    </p>
        	                    <p>
        	                        The Viewport is created so that the right and bottom left parts will resize
        	                        down to some minimum threshold value as you resize the browser. This is possible
        	                        due to the ResizeHandler control which is a part of the Extension project.
        	                    </p>
        	                    <p>
        	                        Also when you unncomment the line in the OnPreRender method the login control
        	                        will be a "true" login control and actually hide everything which contains 
        	                        sensitive data completely from the client and not make it a part of the HTML.
        	                    </p>
                            </ext:TabView>
                            <ext:TabView Caption="Third" runat="server" ID="tab3" CssClass="content">
        	                    <h1>Third TabView</h1>
        	                    <p>
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                        Epsim lorum considari, 
        	                    </p>
                            </ext:TabView>
                        </ext:TabControl>
                    </div>
                </ra:Panel>
            </ext:Window>
        </ra:Panel>

        <!-- Login Window -->
        <ext:Window 
            runat="server" 
            CssClass="window" 
            Caption="Please login"
            Visible="false"
            style="width:350px;position:absolute;top:150px;left:260px;z-index:5000;"
            ID="loginWnd">
            <div style="height:150px;">
                <table class="loginTbl">
                    <tr>
                        <td colspan="2">
                            <ra:Label 
                                runat="server" 
                                ID="loginInfo" 
                                Text="Please login with admin/admin" />
                        </td>
                    </tr>
                    <tr>
                        <td>Username:</td>
                        <td>
                            <ra:TextBox runat="server" ID="username" />
                        </td>
                    </tr>
                    <tr>
                        <td>Password:</td>
                        <td>
                            <ra:TextBox runat="server" ID="password" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:right;">
                            <ra:Button 
                                runat="server" 
                                ID="loginBtn" 
                                Text="Login" 
                                OnClick="loginBtn_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <ra:BehaviorObscurable runat="server" ID="loginModal" ZIndex="4999" />
        </ext:Window>
    </form>
</body>
</html>
