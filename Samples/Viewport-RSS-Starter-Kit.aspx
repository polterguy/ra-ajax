<%@ Page 
    Language="C#" 
    AutoEventWireup="true"  
    CodeFile="Viewport-RSS-Starter-Kit.aspx.cs" 
    Inherits="Samples.RSSStarterKit" %>

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
    <title>Ra-Ajax RSS Starter-Kit</title>
    <link href="media/skins/steel/Steel-0.8.5.css" rel="stylesheet" type="text/css" />
    <link href="media/RssStarterKit-0.8.5.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
        <ext:ResizeHandler 
            runat="server" 
            OnResized="resizer_Resized"
            ID="resizer" />

        <!-- "Statusbar" part -->
        <ra:Panel 
            runat="server" 
            ID="pnlStatus" 
            CssClass="status">
            Kickstart Your Web Apps with the <strong>Ra-Ajax RSS Starter-Kit</strong>
        </ra:Panel>

        <!-- Top left parts -->
        <ext:Window 
            runat="server" 
            ID="wndTopLeft" 
            Caption="RSS Items"
            style="width:400px;position:absolute;left:5px;top:35px;" 
            Movable="false" 
            Closable="false"
            CssClass="window">
            <div style="height:250px;overflow:auto;">
                <ext:Tree 
                    runat="server" 
                    ID="tree" 
                    OnSelectedNodeChanged="tree_SelectedNodeChanged"
                    CssClass="tree"
                    Expansion="SingleClickEntireRow">
                    <ext:TreeNodes ID="RSSFeeds" runat="server" />
                </ext:Tree>
            </div>
        </ext:Window>

        <!-- Bottom left -->
        <ext:Window 
            runat="server" 
            CssClass="window" 
            Closable="false" 
            Movable="false"
            Caption="RSS Feeds"
            style="width:400px;position:absolute;left:5px;top:324px;"
            ID="wndBottomLeft">
            <ra:Panel 
                runat="server" 
                ID="pnlLeft" 
                style="height:200px;overflow:auto;">
                <div style="padding:2px 1px 1px 2px;">
                    <ra:Panel runat="server" ID="gridWrapper">
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
                                        Go to website
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <a rel="nofollow" href='<%#Eval("WebLink")%>'><%#Eval("Title")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ra:Panel>
                </div>
            </ra:Panel>
            <div style="height:34px;">
                <!-- Safari doesn't respect height style on input form elements...!! -->
                <ra:Button 
                    runat="server" 
                    Text="Add new RSS feed..."
                    style="height:34px;width:100%;"
                    OnClick="add_Click"
                    ID="add" />
            </div>
        </ext:Window>

        <!-- Right - main content -->
        <ext:Window 
            runat="server" 
            CssClass="window" 
            Closable="false" 
            Movable="false"
            Caption="Read RSS Item - choose in the TreeView to the left"
            style="width:650px;position:absolute;top:35px;left:410px;"
            ID="wndRight">

            <ra:Panel 
                runat="server" 
                ID="pnlRight" 
                style="background:White url('media/ajax.jpg') no-repeat;min-height:280px;overflow:auto;padding:15px;">
                <div style="padding:5px;">
                    <ra:Label runat="server" ID="header" Tag="h1" Text="Ra-Ajax RSS Starter-Kit (C#)" />
                    <ra:Label runat="server" ID="date" Tag="p" style="font-style:italic;" />
                    <ra:Label runat="server" ID="body" Tag="p" />
                    <ra:Label runat="server" ID="link" Tag="p" />
                    <ra:Panel runat="server" ID="intro">
                        <p>
                            Here's an example of how to create an Ajax RSS Reader using Ra-Ajax.
                        </p>
                        <p>
                            This sample is an <a href="http://ra-ajax.org/Starter-Kits.aspx">Ajax Starter-Kit</a>
                            and can be commercially purchased if you want to start your applications with it for
                            $29. <a href="http://ra-ajax.org/Starter-Kits.aspx">Read more about how to purchase an Ajax Starter-Kit...</a>
                        </p>
                        <p>
                            It gives you a head start on your Ajax Applications if an RSS reader is something you'd like
                            to build by providing a fundament which you can build your own Ajax RSS reader on top of.
                        </p>
                        <p>
                            So if you have the need for an <em>Ajax Really Simple Syndication Start-Kit</em> (Ajax RSS reader)
                            then $29 for this Starter-Kit would probably be money well spent.
                        </p>
                    </ra:Panel>
                </div>
            </ra:Panel>
        </ext:Window>


        <!-- Create new activity Window -->
        <ext:Window 
            runat="server" 
            CssClass="window" 
            Visible="false"
            Caption="Add new RSS Feed - MUST be RSS 2.0" 
            DefaultWidget="addBtn"
            style="width:450px;position:relative;top:25px;margin-left:auto;margin-right:auto;z-index:5000;"
            ID="addWindow">
            <div style="height:80px;padding:15px;position:relative;">
                <div style="width:90%;">
                    <div 
                        style="float:left;width:20%;height:25px;text-align:right;padding-right:5px;">
                        URL
                    </div>
                    <div style="float:left;width:70%;height:25px;">
                        <ra:TextBox 
                            runat="server" 
                            style="width:100%;"
                            ID="addUrl" />
                    </div>
                </div>
                <br style="clear:both;" />
                <ra:Label runat="server" ID="errLbl" />
                <ra:Button 
                    runat="server" 
                    ID="addBtn" 
                    OnClick="addBtn_Click"
                    style="position:absolute;bottom:0;right:0;"
                    Text="Save" />
            </div>
            <ra:BehaviorObscurable runat="server" ID="obscurer" />
        </ext:Window>
    </form>
    <script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>
</body>
</html>
