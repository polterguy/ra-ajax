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
        style="position:absolute;top:290px;left:250px;background-color:Yellow;border:solid 1px #333;padding:15px;">
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
                <td>
                    <ra:Button 
                        runat="server" 
                        ID="finishRegister" 
                        Text="Finish" 
                        OnClick="finishRegister_Click" />
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
                </td>
            </tr>
        </table>
    </ra:Panel>

    <ra:Panel runat="server" ID="postsWrapper">
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
    <div style="height:500px;">
        &nbsp;
    </div>

</asp:Content>

