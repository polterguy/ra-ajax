<%@ Page 
    Language="VB"
    AutoEventWireup="true"  
    CodeFile="Viewport-GridView-Sample.aspx.vb" 
    Inherits="Samples.ViewportGridView" %>

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
    <title>GridView/DataGrid Ra-Ajax Starter-Kit</title>
    <link href="media/skins/Sapphire/Sapphire-0.8.0.css" rel="stylesheet" type="text/css" />
    <link href="media/ViewPortGridViewSample.css" rel="stylesheet" type="text/css" />
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
                Kickstart Your Web Apps with the <strong>Ra-Ajax GridView/DataGrid Starter-Kit</strong>
            </ra:Panel>

            <!-- Menu -->
            <ext:Menu 
                runat="server" 
                ID="menu" 
                CssClass="menu">
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
                Closable="false"
                CssClass="window">
                <div style="height:178px;overflow:auto;margin:25px;border:dotted 1px #999; background-color:#fefefe;padding:10px;">
                    <p>Here is your left view</p>
                    <p>Filter GridView</p>
                    <p>
                        <ra:TextBox 
                            runat="server" 
                            OnKeyUp="filter_KeyUp"
                            ID="filter" />
                    </p>
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
                CssClass="window" 
                Closable="false" 
                Movable="false" 
                OnCreateNavigationalButtons="wndRight_CreateNavigationalButtons"
                Caption="Main Content - "
                style="width:750px;position:absolute;top:63px;left:260px;"
                ID="wndRight">

                <ra:Panel 
                    runat="server" 
                    ID="pnlRightOuter" 
                    style="height:488px;overflow:auto;">
                    <h1 style="margin-left:auto;margin-right:auto;text-align:center;">GridView or DataGrid Ajax Starter-Kit</h1>
                    <ra:Panel 
                        runat="server" 
                        ID="pnlRight" 
                        style="padding:5px;">
                        <asp:GridView 
                            runat="server" 
                            ID="grid" 
                            BackColor="LightGoldenrodYellow" 
                            BorderColor="Tan" 
                            style="width:100%;"
                            BorderWidth="1px" 
                            CellPadding="2" 
                            ForeColor="Black" 
                            AutoGenerateColumns="false"
                            GridLines="None">
                            <FooterStyle 
                                BackColor="Tan" />
                            <PagerStyle 
                                BackColor="PaleGoldenrod" 
                                ForeColor="DarkSlateBlue" 
                                HorizontalAlign="Center" />
                            <SelectedRowStyle 
                                BackColor="DarkSlateBlue" 
                                ForeColor="GhostWhite" />
                            <HeaderStyle 
                                BackColor="Tan" 
                                Font-Bold="True" />
                            <AlternatingRowStyle 
                                BackColor="PaleGoldenrod" />
                            <Columns>
                                <asp:BoundField 
                                    DataField="Name" 
                                    HeaderText="Name" />
                                <asp:BoundField 
                                    DataField="Address" 
                                    HeaderText="Address" />
                                <asp:BoundField 
                                    DataField="Birthday" 
                                    HeaderText="Birthday" 
                                    DataFormatString="{0:dddd dd.MMMM yyyy}" />
                                <asp:TemplateField HeaderText="Edit">
                                    <HeaderTemplate>
                                        Edit
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <ra:HiddenField 
                                            runat="server" 
                                            Value='<%#Eval("ID") %>' />
                                        <ra:LinkButton 
                                            runat="server" 
                                            OnClick="EditEntry"
                                            Text="Edit..." />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ra:Panel>
                </ra:Panel>
            </ext:Window>
        </ra:Panel>

        <!-- Login Window -->
        <ext:Window 
            runat="server" 
            CssClass="window" 
            Caption="Edit entry"
            Visible="false" 
            Closable="true"
            style="width:550px;position:relative;z-index:5000;margin-left:auto;margin-right:auto;"
            ID="editWindow">
            <div style="padding:15px;position:relative;">
                <table class="loginTbl">
                    <tr>
                        <td>Name</td>
                        <td>
                            <ra:TextBox 
                                runat="server" 
                                ID="editName" />
                        </td>
                    </tr>
                    <tr>
                        <td>Address</td>
                        <td>
                            <ra:TextBox 
                                runat="server" 
                                ID="editAdr" />
                        </td>
                    </tr>
                    <tr>
                        <td>Date of birth</td>
                        <td>
                            <ra:LinkButton 
                                runat="server" 
                                OnClick="editShowCalendar_Click"
                                ID="editShowCalendar" />
                            <ext:Calendar 
                                runat="server" 
                                ID="editBirth" 
                                Visible="false" 
                                OnDateClicked="editBirth_DateClicked"
                                CssClass="calendar" />
                        </td>
                    </tr>
                </table>
                <ra:HiddenField 
                    runat="server" 
                    ID="editHidden" />
                <ra:Button 
                    runat="server" 
                    ID="editSave" 
                    Text="Save" 
                    OnClick="editSave_Click"
                    style="position:absolute;right:0px;bottom:0px;" />
            </div>
            <ra:BehaviorObscurable runat="server" ID="BehaviorObscurable1" ZIndex="4999" />
        </ext:Window>
        <ext:Window 
            runat="server" 
            CssClass="window" 
            Caption="Please login"
            Visible="false" 
            Closable="false"
            style="width:350px;position:relative;z-index:5000;margin-left:auto;margin-right:auto;"
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
    <script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>
</body>
</html>
