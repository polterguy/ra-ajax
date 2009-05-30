<%@ Page 
    Language="VB"
    AutoEventWireup="true"  
    CodeFile="Viewport-GridView-Sample.aspx.vb" 
    Inherits="Samples.ViewportGridView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GridView/DataGrid Ra-Ajax Starter-Kit</title>
    <link href="media/skins/sapphire/sapphire.css" rel="stylesheet" type="text/css" />
    <link href="media/ViewPortGridViewSample-1.0.css" rel="stylesheet" type="text/css" />
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
                Kickstart Your Web Apps with the <strong>Ra-Ajax VB.NET GridView/DataGrid Starter-Kit</strong>
            </ra:Panel>

            <!-- Menu -->
            <ra:Menu 
                runat="server" 
                ID="menu">
                <ra:MenuItems runat="server" ID="mainItems">
                    <ra:MenuItem runat="server" ID="file">
                        File
                        <ra:MenuItems runat="server" ID="fileMenus">
                            <ra:MenuItem runat="server" id="openFileMenu">
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
                <div style="height:228px;overflow:auto;border:dotted 1px #999; background-color:#fefefe;padding:10px;">
                    <p>Here is your left view</p>
                    <p>Filter GridView</p>
                    <p>
                        <ra:TextBox 
                            runat="server" 
                            AccessKey="F"
                            OnKeyUp="filter_KeyUp"
                            ID="filter" />
                    </p>
                    <p>
                        ALT+SHIFT+F gives focus to filtering TextBox in FireFox. ALT + F in Chrome
                    </p>
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
                        <ul>
                            <li><a runat="server" href="~/">Main Ajax Samples</a></li>
                            <li><a href="http://ra-ajax.org/">Main Ra-Ajax Website</a></li>
                        </ul>
                    </div>
                </ra:Panel>
            </ra:Window>

            <!-- Right - main content -->
            <ra:Window 
                runat="server" 
                Closable="false" 
                Movable="false" 
                OnCreateTitleBarControls="wndRight_CreateTitleBarControls"
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
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <ra:LinkButton 
                                            runat="server"
                                            OnClick="SortName" 
                                            Text="Name" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("Name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <ra:LinkButton 
                                            runat="server" 
                                            OnClick="SortAddress" 
                                            Text="Address" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("Address")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <ra:LinkButton 
                                            runat="server" 
                                            OnClick="SortBirthday" 
                                            Text="Birthday" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#Eval("Birthday", "{0:dddd dd.MMMM yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
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
            </ra:Window>
        </ra:Panel>

        <!-- Login Window -->
        <ra:Window 
            runat="server" 
            Caption="Edit entry" 
            DefaultWidget="editSave"
            Visible="false" 
            Closable="true"
            style="width:350px;position:relative;z-index:5000;margin-left:auto;margin-right:auto;"
            ID="editWindow">
            <div style="padding:15px;position:relative;">
                <table class="loginTbl">
                    <tr>
                        <td>Name</td>
                        <td>
                            <ra:TextBox 
                                runat="server" 
                                OnEscPressed="EscClicked"
                                ID="editName" />
                        </td>
                    </tr>
                    <tr>
                        <td>Address</td>
                        <td>
                            <ra:TextBox 
                                runat="server" 
                                OnEscPressed="EscClicked"
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
                            <ra:Calendar 
                                runat="server" 
                                ID="editBirth" 
                                Visible="false" 
                                OnDateClicked="editBirth_DateClicked" />
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
                    style="position:absolute;right:10px;bottom:10px;" />
            </div>
            <ra:BehaviorObscurable runat="server" ID="BehaviorObscurable1" />
        </ra:Window>
    </form>
</body>
</html>
