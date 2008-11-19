<%@ Page 
    Language="C#" 
    AutoEventWireup="true"  
    CodeFile="Viewport-Calendar-Starter-Kit.aspx.cs" 
    Inherits="Samples.CalendarStarter" %>

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
    <title>Ra-Ajax Calendar Starter-Kit Sample</title>
    <link href="media/skins/Sapphire/Sapphire-0.8.1.css" rel="stylesheet" type="text/css" />
    <link href="media/Calendar-0.8.1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
        <!-- Main surface -->
        <ext:ResizeHandler 
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

        <!-- Calendar begin -->
        <ext:Calendar 
            runat="server" 
            ID="calendarStart" 
            Caption="Start of period"
            StartsOn="Sunday"
            OnSelectedValueChanged="calendarStart_SelectedValueChanged" 
            OnRenderDay="calendarStart_RenderDay"
            style="width:170px;position:absolute;left:5px;top:63px;"
            CssClass="calendar" />

        <!-- Calendar end -->
        <ext:Calendar 
            runat="server" 
            ID="calendarEnd" 
            Caption="End of period" 
            StartsOn="Sunday"
            OnRenderDay="calendarEnd_RenderDay"
            OnSelectedValueChanged="calendarEnd_SelectedValueChanged"
            style="width:170px;position:absolute;left:180px;top:63px;"
            CssClass="calendar" />

        <!-- Bottom left parts -->
        <ext:Window 
            runat="server" 
            ID="wndBottomLeft" 
            Caption="Top left"
            style="width:345px;position:absolute;left:5px;top:238px;" 
            Movable="false" 
            Closable="false"
            CssClass="window">
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
            <ra:Button 
                runat="server" 
                Text="Create new Activity..."
                style="height:34px;width:100%;"
                OnClick="create_Click"
                ID="create" />
        </ext:Window>

        <!-- Right - Main Content -->
        <ext:Window 
            runat="server" 
            CssClass="window" 
            Closable="false" 
            Movable="false"
            Caption="Main content"
            style="width:600px;position:absolute;top:63px;left:355px;"
            ID="wndRight">
            <ra:Panel 
                runat="server" 
                ID="pnlRight" 
                style="height:300px;overflow:auto;">
                <div style="padding:15px;">
                    <ra:Panel 
                        runat="server" 
                        ID="editPnl"
                        style="display:none;">
                        <ra:HiddenField 
                            runat="server" 
                            ID="activityId" />
                        <ext:InPlaceEdit 
                            runat="server"
                            Tag="h2"
                            style="cursor:pointer;"
                            ID="activityHeader" />
                        <ra:TextArea 
                            runat="server" 
                            style="background-color:Transparent;width:50%;height:170px;border:dotted 1px #999;float:left;margin-right:10px;"
                            ID="activityBody" />
                        <ext:Calendar 
                            runat="server" 
                            ID="activityWhen" 
                            StartsOn="Sunday"
                            style="width:170px;float:left;"
                            CssClass="calendar" />
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
                            <strong>
                                A proprietary license (Commercial License) for this Ajax Starter-Kit can be 
                                purchased for $29 at our 
                                <a href="http://ra-ajax.org/Starter-Kits.aspx">Ajax Starter-Kits shop</a>.
                            </strong>
                        </p>
                        <p>
                            To check out some of our other examples click one of the links below;
                        </p>
                        <ul>
                            <li><a runat="server" href="~/">Main Ajax Samples</a></li>
                            <li><a href="Viewport-Sample.aspx">Sapphire Ajax Starter-Kit Sample (C#)</a></li>
                            <li><a href="Viewport-GridView-Sample.aspx">GridView Ajax Starter-Kit Sample (VB.NET)</a></li>
                            <li><a href="Ajax-TreeView.aspx">Ajax TreeView Sample</a></li>
                            <li><a href="Ajax-TabControl.aspx">Ajax TabControl Sample</a></li>
                            <li><a href="Ajax-Calendar.aspx">Ajax Calendar Sample</a></li>
                            <li><a href="Ajax-Wizard.aspx">Ajax Wizard Sample</a></li>
                            <li><a href="http://ra-ajax.org">Main Ra-Ajax Website</a></li>
                        </ul>
                    </ra:Panel>
                </div>
            </ra:Panel>
        </ext:Window>

        <!-- Create new activity Window -->
        <ext:Window 
            runat="server" 
            CssClass="window" 
            Visible="false"
            Caption="Create new Activity"
            style="width:600px;position:relative;top:25px;margin-left:auto;margin-right:auto;z-index:5000;"
            ID="createWindow">
            <div style="height:220px;margin:15px;position:relative;">
                <div style="float:left;width:65%;">
                    <div 
                        style="float:left;width:20%;height:25px;text-align:right;padding-right:5px;">
                        Header
                    </div>
                    <div style="float:left;width:70%;height:25px;">
                        <ra:TextBox 
                            runat="server" 
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
                            style="width:100%;height:148px;border:dotted 1px #999;margin-right:10px;"
                            ID="createBody" />
                    </div>
                </div>
                <div style="float:left;width:34%;">
                    <ext:Calendar 
                        runat="server" 
                        ID="createDate" 
                        StartsOn="Sunday"
                        style="width:170px;"
                        CssClass="calendar" />
                </div>
                <ra:Button 
                    runat="server" 
                    ID="createBtn" 
                    OnClick="createBtn_Click"
                    style="position:absolute;bottom:0;right:0;"
                    Text="Save" />
            </div>
            <ra:BehaviorObscurable runat="server" ID="obscurer" ZIndex="4999" />
        </ext:Window>

    </form>
    <script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>
</body>
</html>
