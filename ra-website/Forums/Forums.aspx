<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Forums.aspx.cs" 
    Inherits="Samples.Forums_Forums" 
    Title="Ra Ajax Forum posts" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>
    
<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Ra-Ajax Forums</h1>
    <p style="width:450px;">
        Feel free to post your questions here and hopefully someone will try to answer as best they can, though 
        remember that Ra Ajax is a NON-commercial project which means that none of the people answering you 
        won't get paid for helping you. If you are a senior ASP.NET/Ra Ajax developer yourself and like to 
        help out the project then answering forum questions is the place to start :)
        <br />
        Politeness helps :)
    </p>
    <ra:Panel 
        runat="server" 
        ID="pnlLoggedIn" 
        style="position:absolute;top:290px;right:5px;background-color:Yellow;border:solid 1px #333;padding:15px;">
        Welcome 
        <ra:Label 
            runat="server" 
            ID="usernameLoggedIn" />
        <br />
        <ra:LinkButton 
            runat="server" 
            ID="createNewPost" 
            OnClick="createNewPost_Click"
            Text="Add New Post" />
        <br />
        <ra:LinkButton 
            runat="server" 
            ID="profile" 
            OnClick="profile_Click"
            Text="Edit Profile" />
    </ra:Panel>
    <ra:Panel 
        runat="server" 
        ID="pnlLogin" 
        style="position:absolute;top:290px;right:5px;background-color:Yellow;border:solid 1px #333;padding:15px;">
        <table>
            <tr>
                <td>Username</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="username" />
                </td>
            </tr>
            <tr>
                <td>Password</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="pwd" 
                        TextMode="password" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <ra:CheckBox 
                        runat="server" 
                        ID="remember" 
                        Text="Remember me" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <ra:Button 
                        runat="server" 
                        ID="login" 
                        Text="Login" 
                        OnClick="login_Click" />
                    or 
                    <ra:Button 
                        runat="server" 
                        ID="register" 
                        Text="Register" 
                        OnClick="register_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <ra:Label 
                        runat="server" 
                        style="color:Red;"
                        ID="lblError" />
                </td>
            </tr>
        </table>
    </ra:Panel>
    <ra:Panel 
        runat="server" 
        Visible="false"
        ID="pnlConfirmRegistration" 
        style="position:absolute;top:190px;right:5px;background-color:Yellow;border:solid 1px #333;padding:15px;">
        <span runat="server" id="newUserWelcome"></span>
    </ra:Panel>
    <ext:Window 
	    runat="server"
	    ID="pnlRegister"
	    Caption="Register"
	    CssClass="alphacube"
	    Visible="false"
	    style="position:absolute;top:290px;left:250px;padding:15px;"> 
        <table>
            <tr>
                <td>Username</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="newUsername" />
                </td>
            </tr>
            <tr>
                <td>Password</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="newPassword" 
                        TextMode="password" />
                </td>
            </tr>
            <tr>
                <td>Repeat password</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="newPasswordRepeat" 
                        TextMode="password" />
                </td>
            </tr>
            <tr>
                <td>Email</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="newEmail" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right;">
                    <ra:Button 
                        runat="server" 
                        ID="finishRegister" 
                        Text="Finish" 
                        OnClick="finishRegister_Click" />
                    <ra:Button 
                        runat="server" 
                        ID="btnCancelRegistration" 
                        Text="Cancel" 
                        OnClick="btnCancelRegistration_Click" />
                </td>
            </tr>
        </table>
    </ext:Window>

    <ext:Window 
	    runat="server"
        ID="pnlProfile" 
	    Caption="Edit Your Profile"
	    CssClass="alphacube"
	    Visible="false"
	    style="position:absolute;top:290px;left:250px;padding:15px;">
        <table>
            <tr>
                <td>Password</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="changePassword" 
                        style="width:250px;"
                        TextMode="password" />
                </td>
            </tr>
            <tr>
                <td>Repeat password</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="changePasswordConfirm" 
                        style="width:250px;"
                        TextMode="password" />
                </td>
            </tr>
            <tr>
                <td>Email</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        style="width:250px;"
                        ID="changeEmail" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">Signature</td>
                <td>
                    <ra:TextArea 
                        Columns="40" 
                        Rows="5" 
                        runat="server" 
                        ID="changeSignature" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right;">
                    <ra:Button 
                        runat="server" 
                        ID="btnChangeProfile" 
                        Text="Save" 
                        OnClick="btnChangeProfile_Click" />
                    <ra:Button 
                        runat="server" 
                        ID="btnCancelSavingProfile" 
                        Text="Cancel" 
                        OnClick="btnCancelSavingProfile_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <ra:Label 
                        runat="server" 
                        ID="lblErrorProfile" 
                        style="color:Red;" />
                </td>
            </tr>
        </table>
    </ext:Window>

    <ext:Window 
	    runat="server"
	    ID="pnlNewPost"
	    Caption="Add New Post"
	    CssClass="alphacube"
	    Visible="false"
	    style="position:absolute;top:290px;left:250px;padding:15px;">
        <table>
            <tr>
                <td>Subject</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="header" 
                        style="width:419px" />
                </td>
            </tr>
            <tr>
                <td>Body</td>
                <td>
                    <ra:TextArea 
                        runat="server" 
                        ID="body" 
                        Columns="50" 
                        Rows="10" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right;">
                    <ra:Button 
                        runat="server" 
                        ID="newSubmit" 
                        OnClick="newSubmit_Click"
                        Text="Add" />
                    <ra:Button 
                        runat="server" 
                        ID="newPostCancel" 
                        OnClick="newPostCancel_Click"
                        Text="Discard" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <ra:Label 
                        runat="server" 
                        style="color:Red;"
                        ID="lblErrorPost" />
                </td>
            </tr>
        </table>
    </ext:Window>

    Search: 
    <ra:TextBox 
        runat="server" 
        OnKeyUp="search_KeyUp"
        ID="search" />
    <ra:Panel runat="server" ID="postsWrapper" style="margin-bottom:25px;margin-top:12px;">
        <asp:Repeater runat="server" ID="forumPostsRepeater">
            <HeaderTemplate>
                <table style="border:solid 1px black;width:90%;" cellpadding="2" cellspacing="3">
                    <tr style="background-color:#FF9B00;color:#222;font-weight:normal;">
                        <th>Topic</th>
                        <th>Posted By</th>
                        <th>Date</th>
                        <th>Replies</th>
                    </tr>
            </HeaderTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <a runat="server" href='<%# "~/Forums/" + Eval("Url") %>'>
                            <%# Eval("Header") %>
                        </a>
                    </td>
                    <td>
                        <%# Eval("Operator.Username") %>
                    </td>
                    <td>
                        <%# ((DateTime)Eval("Created")).ToString("dd.MMM yy - HH:mm")%>
                    </td>
                    <td>
                        <%# Eval("NoReplies") %>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="background-color:#ccc;">
                    <td>
                        <a id="A1" runat="server" href='<%# "~/Forums/" + Eval("Url") %>'>
                            <%# Eval("Header") %>
                        </a>
                    </td>
                    <td>
                        <%# Eval("Operator.Username") %>
                    </td>
                    <td>
                        <%# ((DateTime)Eval("Created")).ToString("dd.MMM yy - HH:mm")%>
                    </td>
                    <td>
                        <%# Eval("NoReplies") %>
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>
    </ra:Panel>
    <ra:LinkButton 
        runat="server" 
        ID="previous" 
        OnClick="previous_Click"
        Text="&lt;&lt; Previous" />
    |
    <ra:LinkButton 
        runat="server" 
        ID="next" 
        OnClick="next_Click"
        Text="Next &gt;&gt;" />
    <br />
    <ra:Label 
        runat="server"
        style="font-style:italic;"
        ID="informationLabel" />
    <div style="height:500px;">
        &nbsp;
    </div>

</asp:Content>

