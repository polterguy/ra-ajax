<%@ Page 
    Language="C#" 
    AutoEventWireup="true"  
    CodeFile="Viewport-Sample.aspx.cs" 
    Inherits="Samples.Viewport" %>

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
            <ra:ResizeHandler 
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
            <ra:Menu 
                runat="server" 
                ID="menu" 
                OnMenuItemSelected="menu_MenuItemSelected">
                <ra:MenuItems runat="server" ID="mainItems">
                    <ra:MenuItem runat="server" ID="file">
                        File
                        <ra:MenuItems runat="server" ID="fileMenus">
                            <ra:MenuItem runat="server" id="openFile">
                                Open file
                            </ra:MenuItem>
                            <ra:MenuItem runat="server" id="saveFile">
                                Save file
                            </ra:MenuItem>
                            <ra:MenuItem runat="server" id="saveFileAs">
                                Save file as...
                            </ra:MenuItem>
                        </ra:MenuItems>
                    </ra:MenuItem>
                    <ra:MenuItem runat="server" ID="edit">
                        Edit
                        <ra:MenuItems runat="server" ID="editMenus">
                            <ra:MenuItem runat="server" id="copy">
                                Copy
                            </ra:MenuItem>
                            <ra:MenuItem runat="server" id="paste">
                                Paste
                            </ra:MenuItem>
                            <ra:MenuItem runat="server" id="cut">
                                Cut
                            </ra:MenuItem>
                        </ra:MenuItems>
                    </ra:MenuItem>
                    <ra:MenuItem runat="server" ID="windows">
                        Windows
                        <ra:MenuItems runat="server" ID="windowsSub">
                            <ra:MenuItem runat="server" id="arrange">
                                Arrange
                                <ra:MenuItems runat="server" ID="arrWindows">
                                    <ra:MenuItem runat="server" id="leftAligned">
                                        Left-Aligned
                                    </ra:MenuItem>
                                    <ra:MenuItem runat="server" id="rightAligned">
                                        Right-Aligned
                                    </ra:MenuItem>
                                </ra:MenuItems>
                            </ra:MenuItem>
                            <ra:MenuItem runat="server" id="closeAll">
                                Close all
                            </ra:MenuItem>
                        </ra:MenuItems>
                    </ra:MenuItem>
                </ra:MenuItems>
            </ra:Menu>

            <!-- Top left parts -->
            <ra:Window 
                runat="server" 
                ID="wndTopLeft" 
                Caption="Top left"
                style="width:250px;position:absolute;left:5px;top:63px;" 
                Movable="false" 
                Closable="false">
                <div style="height:250px;overflow:auto;">
                    <ra:Tree 
                        runat="server" 
                        ID="tree" 
                        OnSelectedNodeChanged="tree_SelectedNodeChanged"
                        Expansion="SingleClickPlusSign">

                        <ra:TreeNodes ID="TreeNodes1" runat="server" Expanded="true">
                            <ra:TreeNode runat="server" ID="good" Text="Wow samples...">
                                <ra:TreeNodes ID="TreeNodes2" runat="server" Expanded="true">
                                    <ra:TreeNode runat="server" ID="TreeNode1">
                                        <a runat="server" href="~">Main Ajax Samples</a>
                                    </ra:TreeNode>
                                    <ra:TreeNode runat="server" ID="TreeNode6">
                                        <a href="http://ra-ajax.org">Main Ra-Ajax website</a>
                                    </ra:TreeNode>
                                </ra:TreeNodes>
                            </ra:TreeNode>
                            <ra:TreeNode runat="server" ID="bad">
                                <span title="This is the stuff we really dislike... :(">
                                    Proprietary lock-in crap
                                </span>
                                <ra:TreeNodes ID="TreeNodes6" runat="server">
                                    <ra:TreeNode runat="server" ID="rigid" Text="Adobe Flex" />
                                    <ra:TreeNode runat="server" ID="silvercrap" Text="Silverlight" />
                                    <ra:TreeNode runat="server" ID="ActiveX" Text="ActiveX">
                                        <ra:TreeNodes ID="TreeNodes7" runat="server">
                                            <ra:TreeNode runat="server" ID="activex1" Text="ActiveX 1.0" />
                                            <ra:TreeNode runat="server" ID="activex2" Text="ActiveX 2.0" />
                                        </ra:TreeNodes>
                                    </ra:TreeNode>
                                </ra:TreeNodes>
                            </ra:TreeNode>
                            <ra:TreeNode runat="server" ID="recipes">
                                <span title="Cakes...">
                                    Cakes recipes
                                </span>
                                <ra:TreeNodes ID="cakeRecipes" runat="server">
                                    <ra:TreeNode runat="server" ID="brownie" Text="Brownies" />
                                    <ra:TreeNode runat="server" ID="applecake" Text="Apple Cake" />
                                    <ra:TreeNode runat="server" ID="crackers" Text="Crackers">
                                        <ra:TreeNodes ID="cracks" runat="server">
                                            <ra:TreeNode runat="server" ID="ritz" Text="Ritz" />
                                            <ra:TreeNode runat="server" ID="chochips" Text="Chocolate chips" />
                                        </ra:TreeNodes>
                                    </ra:TreeNode>
                                </ra:TreeNodes>
                            </ra:TreeNode>

                            <ra:TreeNode runat="server" ID="dynamicNode" Text="Dynamically loaded nodes...">
                                <ra:TreeNodes ID="dynamicNodes" runat="server" />
                            </ra:TreeNode>

                        </ra:TreeNodes>
                    </ra:Tree>
                </div>
            </ra:Window>

            <!-- Bottom left -->
            <ra:Window 
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
            </ra:Window>

            <!-- Right - main content -->
            <ra:Window 
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
                        <ra:TabControl runat="server" ID="tab">
                            <ra:TabView 
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
                                    This Starter-Kit is licensed just as the rest of Ra-Ajax as GPL. If this does not suite
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
                            </ra:TabView>
                            <ra:TabView 
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
                            </ra:TabView>
                            <ra:TabView 
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
                            </ra:TabView>
                        </ra:TabControl>
                    </div>
                </ra:Panel>
            </ra:Window>
        </ra:Panel>

        <!-- Login Window -->
        <ra:Window 
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
            <ra:BehaviorObscurable runat="server" ID="loginModal" />
        </ra:Window>
    </form>
</body>
</html>
