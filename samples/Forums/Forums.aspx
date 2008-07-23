<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Forums.aspx.cs" 
    Inherits="Forums_Forums" 
    Title="Ra Ajax Forum posts" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Forums for Ra Ajax</h1>
    <p style="width:450px;">
        Feel free to post your questions here and I will try to answer as best I can, though remember that
        Ra Ajax is a NON-commercial project which means that I don't get paid for helping you. If you are 
        a senior ASP.NET/Ra Ajax developer yourself and like to help out the project then answering forum
        questions is the place to start :)
    </p>
    <ra:Panel 
        runat="server" 
        ID="pnlLoggedIn" 
        style="position:absolute;top:290px;right:5px;background-color:Yellow;border:solid 1px #333;padding:15px;">
        Welcome: 
        <ra:Label 
            runat="server" 
            ID="usernameLoggedIn" />
        <br />
        <ra:Button 
            runat="server" 
            ID="createNewPost" 
            OnClick="createNewPost_Click"
            Text="New post" />
        <br />
        <ra:Button 
            runat="server" 
            ID="profile" 
            OnClick="profile_Click"
            Text="Profile" />
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
    <ra:Panel 
        runat="server" 
        ID="pnlRegister" 
        Visible="false"
        style="position:absolute;top:290px;right:5px;background-color:Yellow;border:solid 1px #333;padding:15px;">
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
    </ra:Panel>

    <ra:Panel 
        runat="server" 
        ID="pnlProfile" 
        Visible="false"
        style="position:absolute;top:290px;left:250px;background-color:Yellow;border:solid 1px #333;padding:15px;">
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
    </ra:Panel>

    <ra:Panel 
        runat="server" 
        ID="pnlNewPost" 
        Visible="false"
        style="position:absolute;top:290px;left:250px;background-color:Yellow;border:solid 1px #333;padding:15px;">
        <table>
            <tr>
                <td>Header</td>
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
                        Text="Save" />
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
    </ra:Panel>

    Search: 
    <ra:TextBox 
        runat="server" 
        OnKeyUp="search_KeyUp"
        ID="search" />
    <ra:Panel runat="server" ID="postsWrapper" style="margin-bottom:25px;">
        <asp:Repeater runat="server" ID="forumPostsRepeater">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Header</th>
                        <th>Username</th>
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
        </asp:Repeater>
    </ra:Panel>
    <ra:Label 
        runat="server"
        style="font-style:italic;"
        ID="informationLabel" />
    <div style="height:500px;">
        &nbsp;
    </div>

</asp:Content>

