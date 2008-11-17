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
    <title>Ra-Ajax Calendar Starter-Kit</title>
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
            Kickstart Your Web Apps with a <strong>Ra-Ajax Starter-Kit</strong>
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
            OnSelectedValueChanged="calendarStart_SelectedValueChanged" 
            OnRenderDay="calendarStart_RenderDay"
            Caption="Start of period"
            style="width:170px;position:absolute;left:5px;top:63px;"
            CssClass="calendar" />

        <!-- Calendar end -->
        <ext:Calendar 
            runat="server" 
            ID="calendarEnd" 
            Caption="End of period"
            OnRenderDay="calendarEnd_RenderDay"
            OnSelectedValueChanged="calendarEnd_SelectedValueChanged"
            style="width:170px;position:absolute;left:180px;top:63px;"
            CssClass="calendar" />

        <!-- Bottom left parts -->
        <ext:Window 
            runat="server" 
            ID="wndBottomLeft" 
            Caption="Top left"
            style="width:345px;position:absolute;left:5px;top:222px;" 
            Movable="false" 
            Closable="false"
            CssClass="window">
            <ra:Panel 
                runat="server" 
                ID="pnlBottomLeft" 
                style="height:300px;overflow:auto;">
                Bottom left
            </ra:Panel>
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
                bottom left
            </ra:Panel>
        </ext:Window>

    </form>
    <script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>
</body>
</html>
