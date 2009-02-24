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
    <link href="media/skins/Sapphire/Sapphire-1.1.css" rel="stylesheet" type="text/css" />
    <link href="media/ViewPortSample-1.0.css" rel="stylesheet" type="text/css" />
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
                <div class="statusItem">
                    Kickstart Your Web Apps with a <strong>Ra-Ajax Starter-Kit</strong>
                </div>
                <div class="statusItem">
                    Here you can put your company logo
                </div>
                <div class="statusItem">
                    <ra:Label 
                        runat="server" 
                        ID="lblStatus" 
                        Text="This is a C# Ajax Starter-Kit" />
                </div>
            </ra:Panel>

            <!-- Menu -->
            <ext:Menu 
                runat="server" 
                ID="menu" 
                OnMenuItemSelected="menu_MenuItemSelected">
                <ext:MenuItems runat="server" ID="mainItems">
                    <ext:MenuItem runat="server" ID="file">
                        File
                        <ext:MenuItems runat="server" ID="fileMenus">
                            <ext:MenuItem runat="server" id="openFile">
                                Open file
                            </ext:MenuItem>
                            <ext:MenuItem runat="server" id="saveFile">
                                Save file
                            </ext:MenuItem>
                            <ext:MenuItem runat="server" id="saveFileAs">
                                Save file as...
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
                    <ext:MenuItem runat="server" ID="windows">
                        Windows
                        <ext:MenuItems runat="server" ID="windowsSub">
                            <ext:MenuItem runat="server" id="arrange">
                                Arrange
                                <ext:MenuItems runat="server" ID="arrWindows">
                                    <ext:MenuItem runat="server" id="leftAligned">
                                        Left-Aligned
                                    </ext:MenuItem>
                                    <ext:MenuItem runat="server" id="rightAligned">
                                        Right-Aligned
                                    </ext:MenuItem>
                                </ext:MenuItems>
                            </ext:MenuItem>
                            <ext:MenuItem runat="server" id="closeAll">
                                Close all
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
                Closable="false">
                <div style="height:250px;overflow:auto;">
                    <ext:Tree 
                        runat="server" 
                        ID="tree" 
                        OnSelectedNodeChanged="tree_SelectedNodeChanged"
                        Expansion="SingleClickPlusSign">

                        <ext:TreeNodes ID="TreeNodes1" runat="server" Expanded="true">
                            <ext:TreeNode runat="server" ID="good" Text="Wow samples...">
                                <ext:TreeNodes ID="TreeNodes2" runat="server" Expanded="true">
                                    <ext:TreeNode runat="server" ID="TreeNode10">
                                        <a href="Viewport-Calendar-Starter-Kit.aspx">Calendar Starter-Kit (C#)</a>
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="TreeNode9">
                                        <a href="Viewport-GridView-Sample.aspx">GridView Starter-Kit (VB)</a>
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="TreeNode1">
                                        <a runat="server" href="~">Main Ajax Samples</a>
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="TreeNode5">
                                        <a href="Ajax-TreeView.aspx">Ajax TreeView Sample</a>
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="TreeNode2">
                                        <a href="Ajax-TabControl.aspx">Ajax TabControl Sample</a>
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="TreeNode3">
                                        <a href="Ajax-Calendar.aspx">Ajax Calendar Sample</a>
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="TreeNode4">
                                        <a href="Ajax-Wizard.aspx">Ajax Wizard Sample</a>
                                    </ext:TreeNode>
                                    <ext:TreeNode runat="server" ID="TreeNode6">
                                        <a href="http://ra-ajax.org">Main Ra-Ajax website</a>
                                    </ext:TreeNode>
                                </ext:TreeNodes>
                            </ext:TreeNode>
                            <ext:TreeNode runat="server" ID="bad">
                                <span title="This is the stuff we really dislike... :(">
                                    Proprietary lock-in crap
                                </span>
                                <ext:TreeNodes ID="TreeNodes6" runat="server">
                                    <ext:TreeNode runat="server" ID="rigid" Text="Adobe Flex" />
                                    <ext:TreeNode runat="server" ID="silvercrap" Text="Silverlight" />
                                    <ext:TreeNode runat="server" ID="ActiveX" Text="ActiveX">
                                        <ext:TreeNodes ID="TreeNodes7" runat="server">
                                            <ext:TreeNode runat="server" ID="activex1" Text="ActiveX 1.0" />
                                            <ext:TreeNode runat="server" ID="activex2" Text="ActiveX 2.0" />
                                        </ext:TreeNodes>
                                    </ext:TreeNode>
                                </ext:TreeNodes>
                            </ext:TreeNode>
                            <ext:TreeNode runat="server" ID="recipes">
                                <span title="Cakes...">
                                    Cakes recipes
                                </span>
                                <ext:TreeNodes ID="cakeRecipes" runat="server">
                                    <ext:TreeNode runat="server" ID="brownie" Text="Brownies" />
                                    <ext:TreeNode runat="server" ID="applecake" Text="Apple Cake" />
                                    <ext:TreeNode runat="server" ID="crackers" Text="Crackers">
                                        <ext:TreeNodes ID="cracks" runat="server">
                                            <ext:TreeNode runat="server" ID="ritz" Text="Ritz" />
                                            <ext:TreeNode runat="server" ID="chochips" Text="Chocolate chips" />
                                        </ext:TreeNodes>
                                    </ext:TreeNode>
                                </ext:TreeNodes>
                            </ext:TreeNode>

                            <ext:TreeNode runat="server" ID="dynamicNode" Text="Dynamically loaded nodes...">
                                <ext:TreeNodes ID="dynamicNodes" runat="server" />
                            </ext:TreeNode>

                        </ext:TreeNodes>
                    </ext:Tree>
                </div>
            </ext:Window>

            <!-- Bottom left -->
            <ext:Window 
                runat="server" 
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
                        <h2 style="text-align:center;">Bottom left</h2>
                        <p>
                            <ra:Label 
                                runat="server" 
                                ID="lbl" 
                                style="font-weight:bold;"
                                Text="Size of Viewport" />
                        </p>
                        <p>
                            As you can see here the resizing will trigger a server-side event which in turn will resize
                            the windows so that at all times the Window will "fill the Viewport" except down to 
                            some "minimum threshold"...
                        </p>
                        <p>
                            The resizing is possible due to the Ra-Ajax ResizeHandler Control which will trigger a
                            server-side event every time the browser window is resized. Then the bottom-left and right
                            Ajax Windows will be resized to make sure the entire browser window is "intelligently used".
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
                        <ext:TabControl runat="server" ID="tab">
                            <ext:TabView 
                                Caption="Starter-Kit Tab 1" 
                                runat="server" 
                                ID="tab1" 
                                style="background:White url('media/ajax.jpg') no-repeat;"
                                CssClass="content">
                                <h1>Sapphire Ra-Ajax Viewport Starter-Kit</h1>
                                <p>
                                    This is something we call an <em>Ajax Starter-Kit</em>. A Starter-Kit
                                    is something you can start out with which will give you some default layout and code 
                                    to start out with. Normally this would speed up your initial web application
                                    development so that you start flying by the first second instead of having to
                                    fiddle with your own layout from day 1.
                                </p>
                                <p>
                                    Note though that there's no real code in this starter-kit. It's merely meant as a
                                    fast GUI flyout. So you still need to do your own database connections, queries and
                                    so on. But hopefully this will make it possible for you to start your projects
                                    very fast instead of spending several days just to get the "initial layout" correct.
                                </p>
                                <p>
                                    This starter-kit is an <em>Ajax Viewport Starter-Kit</em>. An Ajax Viewport is something
                                    that makes sure that the entire browser surface is intelligently used and that the content
                                    of the page is resized so that when the browser is resized the rest of the page is also 
                                    resized.
                                    
                                </p>
                                <p>
                                    This Starter-Kit is licensed just as the rest of Ra-Ajax as LGPL. If this does not suite
                                    your needs you can also purchase a commercial license of this Starter-Kit by visiting 
                                    our "buy page" in our main website.
                                </p>
                                <p>
                                    The skin we're using here is called "sapphire" but you can probably change to
                                    other skins if you're not happy with the current look. Though this starter-kit
                                    is created towards the Sapphire skin and might need some minor adjustment to
                                    work towards other skins. Especially if that other skin have different size
                                    of the borders and such for the Ajax Windows, Ajax TabControls etc...
                                </p>
                            </ext:TabView>
                            <ext:TabView 
                                Caption="Tab 2" 
                                runat="server" 
                                ID="tab2" 
                                style="background:White url('media/ajax2.jpg') no-repeat;"
                                CssClass="content">
        	                    <h1>2nd Tab</h1>
        	                    <p>
        	                        The sample for this Starter-Kit displays all the content of the page by default. This should
        	                        probably be changed if you want to authenticate access to your site. This can be done by
        	                        un-commenting one line of code in the codebehind for this page.
        	                    </p>
        	                    <p>
        	                        Or if you don't want authenticated access at all to your website but rather a completely open
        	                        solution, then that too is very easy. Just remove some few lines of code, delete the login Window
        	                        and then you have a "public website" Starter-Kit.
        	                    </p>
        	                    <h2>Hire us!</h2>
        	                    <p>
        	                        This Starter-Kit was actually created as a task for a customer of us which needed his project
        	                        starting off the ground flying and didn't want to spend too much time fiddling with the starting
        	                        of his own project. If you have some layout you would like to have especially made for *your*
        	                        needs then please send us an <a href="mailto:thomas@ra-ajax.org">email</a> with the details.
        	                        In general terms if you have some piece of work you would like to do where we can keep the
        	                        copyright and the nature of the project is such that it also would benefit other users of Ra-Ajax
        	                        then you get the "cheap price" for our hours.
        	                    </p>
        	                    <p>
        	                        If you have the need to keep the copyright for the work we do for you, then this is also possible.
        	                        We have experience in a lot of fields, obviously we're experts in Ajax for ASP.NET but we're also
        	                        very skilled in O/RM, design, architecture, DotNetNuke, and even WinForms.
        	                    </p>
        	                    <p>
        	                        We are the creators and copyright holders of Ra-Ajax. And probably (obviously) the gurus in this
        	                        world when it comes to Ra-Ajax development. We also do training in use of Ra-Ajax. If you have needs
        	                        we have the skills!
        	                    </p>
                            </ext:TabView>
                            <ext:TabView 
                                Caption="Third" 
                                runat="server" 
                                ID="tab3" 
                                CssClass="content">
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
            Caption="Please login"
            Visible="false" 
            Closable="false" 
            DefaultWidget="loginBtn"
            style="width:350px;position:relative;z-index:5000;margin-left:auto;margin-right:auto;display:none;"
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
        </ext:Window>
    </form>
</body>
</html>
