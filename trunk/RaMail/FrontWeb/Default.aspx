<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="_Default" 
    Title="Untitled Page" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1 id="header" runat="server">Ra Mail</h1>
    <p>
        Welcome to RaMail
    </p>
    <asp:LinkButton 
        runat="server" 
        ID="logout" 
        Text="Logout" 
        OnClick="logout_Click" />
    <br />
    <ra:LinkButton 
        runat="server" 
        ID="create" 
        OnClick="create_Click"
        Text="Create new account" />
    <ra:Panel 
        runat="server" 
        ID="newAccount" 
        style="width:100%;height:250px;background-color:Yellow;border:solid 1px Black;"
        Visible="false">
        <table>
            <tr>
                <td>Server</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="actServer" />
                </td>
            </tr>
            <tr>
                <td>Username</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="actUsername" />
                </td>
            </tr>
            <tr>
                <td>Password</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="actPwd" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right;">
                    <ra:Button
                        runat="server" 
                        ID="createBtn" 
                        OnClick="createBtn_Click"
                        Text="Create" />
                </td>
            </tr>
        </table>
    </ra:Panel>
    <ra:Panel runat="server" ID="repPopWrapper">
        <asp:Repeater runat="server" ID="repPop">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <a href='<%# "Account.aspx?id=" + Eval("ID") %>'>
                            <%#Eval("Username") %> @ <%#Eval("Pop3Server") %>
                        </a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </ra:Panel>

</asp:Content>

