<%@ Page 
    Language="C#" 
    AutoEventWireup="true"  
    CodeFile="Ajax-Forum-Starter-Kit.aspx.cs" 
    Inherits="Samples.AjaxForum" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>An Ajax Forum Starter-Kit</title>
    <link id="steel" runat="server" href="media/skins/steel/Steel-1.1.css" rel="stylesheet" type="text/css" />
    <link id="sapphire" visible="false" runat="server" href="media/skins/Sapphire/Sapphire-1.1.css" rel="stylesheet" type="text/css" />
    <link href="media/Forum-Starter-Kit-1.0.css" rel="stylesheet" type="text/css" />
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
            Kickstart Your Web Apps with the <strong>Ra-Ajax Forum Starter-Kit</strong> - Skin; 
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
            Caption="Top left parts"
            style="width:400px;position:absolute;left:5px;top:35px;" 
            Movable="false" 
            Closable="false">
            <div style="height:220px;overflow:auto;padding:15px;">
                <h1>Ajax Forum Starter-Kit</h1>
                <p>
                    This is a starter-kit for you to use when you need
                    to create something which is like the codeproject.com
                    forum.
                </p>
                <p>
                    As you can see by expanding items you can view items inline
                    and you can also reply to the inline. If this is something
                    you'd like to have in your own app, feel free to use it...
                </p>
                <p>
                    All the "action" is happening in the right window btw, these
                    other windows are here just for fun...
                </p>
                <p>
                    Thanx to some HTML "magic" you're also able to scroll these Windows...
                </p>
                <p>
                    ...as you can see...!!
                </p>
                <p>
                    <strong>
                        A proprietary license (Commercial License) for this Ajax Starter-Kit can be 
                        purchased for $29 at our 
                        <a href="http://ra-ajax.org/Starter-Kits.aspx">Ajax Starter-Kits shop</a>.
                    </strong>
                </p>
            </div>
        </ra:Window>

        <!-- Bottom left -->
        <ra:Window 
            runat="server" 
            Closable="false" 
            Movable="false"
            Caption="Bottom left parts"
            style="width:400px;position:absolute;left:5px;top:324px;"
            ID="wndBottomLeft">
            <ra:Panel 
                runat="server" 
                ID="pnlLeft" 
                style="height:200px;overflow:auto;">
                <div style="padding:15px;">
                    <ul>
                        <li><a id="A1" runat="server" href="~/">Main Ajax Samples</a></li>
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
            Caption="Try expanding and replying to items in this forum"
            style="width:650px;position:absolute;top:35px;left:410px;"
            ID="wndRight">

            <ra:Panel 
                runat="server" 
                ID="pnlRight" 
                style="background:White url('media/ajax.jpg') no-repeat;min-height:280px;overflow:auto;padding:15px;">
                <div style="padding:5px;">
                    <ra:Tree 
                        runat="server" 
                        ID="tree" 
                        CssClass="tree"
                        Expansion="None"
                        OnSelectedNodeChanged="selected"
                        style="width:750px;margin:25px auto 25px auto;">

                        <div class="headerTop">Header</div>
                        <div class="usernameTop">Username</div>
                        <div class="dateTop">Date</div>
                        <div class="headerSpacer">&nbsp;</div>

                        <ra:TreeNodes 
                            ID="rootNodes" 
                            runat="server" 
                            Expanded="true" />

                        <div class="pager">
                            <ra:LinkButton 
                                runat="server" 
                                ID="previous" 
                                OnClick="previous_Click"
                                Text="<< previous" />
                            <ra:LinkButton 
                                runat="server" 
                                ID="newPost" 
                                OnClick="newPost_Click"
                                Text="New post" />
                            <ra:LinkButton 
                                runat="server" 
                                ID="next" 
                                OnClick="next_Click"
                                Text="next >>" />
                        </div>
                    </ra:Tree>
                </div>
            </ra:Panel>
        </ra:Window>

        <ra:Window 
            runat="server" 
            ID="newPostWnd" 
            Visible="false"
            CssClass="window newPostWnd" 
            Caption="New post">

            <div style="padding:15px;height:250px;position:relative;">
                Header
                <ra:TextBox 
                    runat="server" 
                    CssClass="newPostHeader" 
                    OnEscPressed="EscPressed"
                    ID="newPostHeader" />
                Body
                <ra:TextArea 
                    runat="server" 
                    CssClass="newPostBody"
                    ID="newPostBody" />
                <ra:Button 
                    runat="server" 
                    ID="newPostSave" 
                    CssClass="newPostSave"
                    OnClick="newPostSave_Click"
                    Text="Save" />
            </div>
            <ra:HiddenField 
                runat="server" 
                ID="parentPostID" />
            <ra:BehaviorObscurable 
                runat="server" 
                ID="obscureNewWindow" />

        </ra:Window>

    </form>
</body>
</html>
