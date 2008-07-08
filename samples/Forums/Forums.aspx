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
                </td>
            </tr>
        </table>
    </ra:Panel>

    <asp:Repeater runat="server" ID="forumPostsRepeater">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Eval("Header") %>
                </td>
                <td>
                    <%# Eval("Operator.Username") %>
                </td>
                <td>
                    <%# Eval("Created") %>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>

