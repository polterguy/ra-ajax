<%@ Page 
    Language="C#" 
    AutoEventWireup="true"  
    CodeFile="Viewport-RSS-Starter-Kit.aspx.cs" 
    Inherits="Samples.RSSStarterKit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ra-Ajax RSS Starter-Kit</title>
    <link id="steel" runat="server" href="media/skins/steel/steel.css" rel="stylesheet" type="text/css" />
    <link id="sapphire" visible="false" runat="server" href="media/skins/sapphire/sapphire.css" rel="stylesheet" type="text/css" />
    <link href="media/RssStarterKit-1.0.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
        <ra:ResizeHandler 
            runat="server" 
            OnResized="resizer_Resized"
            ID="resizer" />

        <!-- "Statusbar" part -->
        <ra:Panel 
            runat="server" 
            ID="pnlStatus" 
            CssClass="status">
            Kickstart Your Web Apps with the <strong>Ra-Ajax RSS Starter-Kit</strong> - Skin; 
            <asp:DropDownList 
                runat="server" 
                ID="skin" 
                style="font-size:0.8em;" 
                AutoPostBack="true" 
                OnSelectedIndexChanged="skin_SelectedIndexChanged">
                <asp:ListItem Text="Steel" />
                <asp:ListItem Text="Sapphire" />
            </asp:DropDownList>
        </ra:Panel>

        <!-- Top left parts -->
        <ra:Window 
            runat="server" 
            ID="wndTopLeft" 
            Caption="RSS Items"
            style="width:400px;position:absolute;left:5px;top:35px;" 
            Movable="false" 
            Closable="false">
            <div style="height:250px;overflow:auto;">
                <ra:Tree 
                    runat="server" 
                    ID="tree" 
                    OnSelectedNodeChanged="tree_SelectedNodeChanged"
                    Expansion="SingleClickEntireRow">
                    <ra:TreeNodes ID="RSSFeeds" runat="server" />
                </ra:Tree>
            </div>
        </ra:Window>

        <!-- Bottom left -->
        <ra:Window 
            runat="server" 
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
                                        <a rel="nofollow" target="_blank" href='<%#Eval("WebLink")%>'><%#Eval("Title")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Delete
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <ra:HiddenField 
                                            runat="server" 
                                            Value='<%#Eval("Id")%>' />
                                        <ra:Button 
                                            ID="Button1" 
                                            runat="server" 
                                            OnClick="DeleteFeed"
                                            Text="Delete" />
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
                    AccessKey="A" 
                    Tooltip="Keyboard shortcut ALT+SHIFT+A (FireFox)"
                    ID="add" />
            </div>
        </ra:Window>

        <!-- Right - main content -->
        <ra:Window 
            runat="server" 
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
                        <p>
                            These Ajax Starter-Kits doesn't contain any database logic. Normally you would use them
                            in combination with some sort of database in a "real" application. But to keep the code
                            easy to understand we've deliberately avoided adding complexity like databases and such.
                        </p>
                    </ra:Panel>
                </div>
            </ra:Panel>
        </ra:Window>


        <!-- Create new activity Window -->
        <ra:Window 
            runat="server" 
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
                            OnEscPressed="addUrl_EscPressed"
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
        </ra:Window>
    </form>
</body>
</html>
