<%@ Page 
    Language="C#" 
    AutoEventWireup="true"  
    CodeFile="Viewport-Calendar-Starter-Kit.aspx.cs" 
    Inherits="Samples.CalendarStarter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ra-Ajax Calendar Starter-Kit Sample</title>
    <link href="media/skins/Sapphire/Sapphire-1.1.css" rel="stylesheet" type="text/css" />
    <link href="media/Calendar-1.0.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
        <!-- Main surface -->
        <ra:ResizeHandler 
            runat="server" 
            OnResized="resizer_Resized"
            ID="resizer" />

        <!-- "Statusbar" part -->
        <ra:Panel 
            runat="server" 
            ID="pnlStatus" 
            CssClass="status">
            <ra:Label 
                runat="server" 
                ID="status" 
                Text="Kickstart Your Web Apps with a <strong>Ra-Ajax Starter-Kit</strong>" />
        </ra:Panel>

        <!-- Menu -->
        <ra:Menu 
            runat="server" 
            ID="menu">
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

        <!-- Calendar begin -->
        <ra:Calendar 
            runat="server" 
            ID="calendarStart" 
            Caption="Start of period"
            StartsOn="Sunday"
            OnSelectedValueChanged="calendarStart_SelectedValueChanged" 
            OnRenderDay="calendarStart_RenderDay"
            style="width:170px;position:absolute;left:5px;top:63px;" />

        <!-- Calendar end -->
        <ra:Calendar 
            runat="server" 
            ID="calendarEnd" 
            Caption="End of period" 
            StartsOn="Sunday"
            OnRenderDay="calendarEnd_RenderDay"
            OnSelectedValueChanged="calendarEnd_SelectedValueChanged"
            style="width:170px;position:absolute;left:180px;top:63px;" />

        <!-- Bottom left parts -->
        <ra:Window 
            runat="server" 
            ID="wndBottomLeft" 
            Caption="Top left"
            style="width:345px;position:absolute;left:5px;top:238px;" 
            Movable="false" 
            Closable="false">
            <ra:Panel 
                runat="server" 
                ID="pnlBottomLeft" 
                style="height:284px;overflow:auto;padding:5px;">
                <asp:GridView 
                    runat="server" 
                    ID="grid" 
                    AutoGenerateColumns="false"
                    BackColor="White" 
                    BorderColor="#DEDFDE" 
                    Font-Size="0.8em"
                    BorderStyle="None" 
                    BorderWidth="1px" 
                    CellPadding="4" 
                    Width="100%"
                    ForeColor="Black" 
                    GridLines="Vertical">

                    <FooterStyle BackColor="#CCCC99" />
                    <RowStyle BackColor="#F7F7DE" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />

                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Date
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Eval("When", "{0:dddd dd.MMM - yy}")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Header
                            </HeaderTemplate>
                            <ItemTemplate>
                                <ra:HiddenField 
                                    runat="server" 
                                    Value='<%#Eval("ID") %>' />
                                <ra:LinkButton 
                                    runat="server" 
                                    OnClick="EditItem"
                                    Tooltip='<%#Eval("Body")%>'
                                    Text='<%#Eval("Header")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Delete
                            </HeaderTemplate>
                            <ItemTemplate>
                                <ra:HiddenField 
                                    runat="server" 
                                    Value='<%#Eval("ID") %>' />
                                <ra:LinkButton 
                                    runat="server" 
                                    OnClick="DeleteItem"
                                    Tooltip='<%#Eval("Body")%>'
                                    Text="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </ra:Panel>
            <div style="height:34px;">
                <!-- Safari doesn't respect height style on input form elements...!! -->
                <ra:Button 
                    runat="server" 
                    Text="Create new Activity..."
                    AccessKey="C" 
                    Tooltip="Keyboard shortcut ALT+SHIFT+C (FireFox)"
                    style="height:34px;width:100%;"
                    OnClick="create_Click"
                    ID="create" />
            </div>
        </ra:Window>

        <!-- Right - Main Content -->
        <ra:Window 
            runat="server" 
            Closable="false" 
            Movable="false"
            Caption="Main content"
            style="width:600px;position:absolute;top:63px;left:355px;"
            ID="wndRight">
            <ra:Panel 
                runat="server" 
                ID="pnlRight" 
                style="height:300px;overflow:auto;background-color:White;">
                <div style="padding:15px;">
                    <ra:Panel 
                        runat="server" 
                        ID="editPnl"
                        style="display:none;">
                        <ra:HiddenField 
                            runat="server" 
                            ID="activityId" />
                        <ra:InPlaceEdit 
                            runat="server"
                            Tag="h2"
                            style="cursor:pointer;"
                            ID="activityHeader" />
                        <ra:TextArea 
                            runat="server" 
                            style="background-color:Transparent;width:50%;height:170px;border:dotted 1px #999;float:left;margin-right:10px;"
                            ID="activityBody" />
                        <ra:Calendar 
                            runat="server" 
                            ID="activityWhen" 
                            StartsOn="Sunday"
                            style="width:170px;float:left;" />
                        <br style="clear:both;" />
                        <ra:Button 
                            runat="server" 
                            ID="save" 
                            Text="Save" 
                            OnClick="save_Click" />
                        <ra:Button 
                            runat="server" 
                            ID="close" 
                            Text="Close" 
                            OnClick="close_Click" />
                    </ra:Panel>
                    <ra:Panel 
                        runat="server" 
                        ID="intro" 
                        style="background:White url('media/ajax.jpg') no-repeat;min-height:280px;">
                        <h1>Ra-Ajax Starter-Kit for Calendar Applications - C#</h1>
                        <p>
                            This is an example of how to utilize Ra-Ajax to build a Calendar Application
                            like e.g. GCalendar or something similar. It is written in C# and would probably
                            give you a head start if you need a calendar based application.
                        </p>
                        <p>
                            As you can see to the left you can choose the period to view activities from by 
                            changing the Start and End dates. Then the GridView to the left below the Calendars
                            will re-bind and show whatever activities are within that period.
                        </p>
                        <p>
                            Then when you click one of the activities you will get to see it's details and
                            also edit it.
                        </p>
                        <p>
                            Note that this is just a "Starter-Kit" and by far not a complete application in
                            any ways. Think of it like 200 lines of head-start code which you can use yourself
                            in your own applications if you need a calendar based application yourself.
                        </p>
                        <p>
                            To check out some of our other examples click one of the links below;
                        </p>
                        <ul>
                            <li><a runat="server" href="~/">Back to samples</a></li>
                        </ul>
                    </ra:Panel>
                </div>
            </ra:Panel>
        </ra:Window>

        <!-- Create new activity Window -->
        <ra:Window 
            runat="server" 
            Visible="false"
            Caption="Create new Activity"
            style="width:600px;position:relative;top:25px;margin-left:auto;margin-right:auto;z-index:5000;"
            ID="createWindow">
            <div style="height:220px;padding:15px;position:relative;">
                <div style="float:left;width:65%;">
                    <div 
                        style="float:left;width:20%;height:25px;text-align:right;padding-right:5px;">
                        Header
                    </div>
                    <div style="float:left;width:70%;height:25px;">
                        <ra:TextBox 
                            runat="server" 
                            OnEscPressed="createWindow_EscPressed"
                            style="width:100%;"
                            ID="createHeader" />
                    </div>
                    <div 
                        style="clear:both;float:left;width:20%;height:150px;text-align:right;vertical-align:top;padding-right:5px;">
                        Body
                    </div>
                    <div style="float:left;width:70%;height:150px;">
                        <ra:TextArea 
                            runat="server" 
                            OnEscPressed="createWindow_EscPressed"
                            style="width:100%;height:148px;border:dotted 1px #999;margin-right:10px;"
                            ID="createBody" />
                    </div>
                </div>
                <div style="float:left;width:34%;">
                    <ra:Calendar 
                        runat="server" 
                        ID="createDate" 
                        StartsOn="Sunday"
                        style="width:170px;" />
                </div>
                <ra:Button 
                    runat="server" 
                    ID="createBtn" 
                    OnClick="createBtn_Click"
                    style="position:absolute;bottom:0;right:0;"
                    Text="Save" />
            </div>
            <ra:BehaviorObscurable runat="server" ID="obscurer" />
        </ra:Window>

    </form>
</body>
</html>
