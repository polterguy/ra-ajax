<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="AdminMode.ascx.cs" 
    Inherits="AdminMode" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ra:Panel 
    runat="server" 
    style="width:100%;background-color:Yellow;border:solid 1px #999;padding:5px;position:relative;padding-top:25px;"
    Visible="false"
    ID="users">
    <ra:LinkButton 
        runat="server" 
        ID="closeUsers" 
        OnClick="closeUsers_Click"
        style="position:absolute;right:2px;top:2px;"
        Text="X" />
    <ra:Panel runat="server" ID="repUsersWrapper">
        <asp:Repeater runat="server" ID="repUsers">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Username</th>
                        <th>Password</th>
                        <th>Created</th>
                        <th>Email</th>
                        <th>Admin</th>
                        <th>Confirmed</th>
                        <th>Delete user</th>
                    </tr>
            </HeaderTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <ra:HiddenField 
                            runat="server" 
                            Value='<%# Eval("Id") %>' />
                        <ext:InPlaceEdit 
                            CssClass="inplaceedit"
                            runat="server" 
                            OnTextChanged="UsernameChanged"
                            Text='<%# Eval("Username") %>' />
                    </td>
                    <td>
                        <ext:InPlaceEdit 
                            CssClass="inplaceedit"
                            runat="server" 
                            OnTextChanged="PasswordChanged"
                            Text='<%# Eval("Password") %>' />
                    </td>
                    <td>
                        <%# ((DateTime)Eval("Created")).ToString("MMM dd, yyyy", System.Globalization.CultureInfo.InvariantCulture) %>
                    </td>
                    <td>
                        <ext:InPlaceEdit 
                            CssClass="inplaceedit"
                            runat="server" 
                            OnTextChanged="EmailChanged"
                            Text='<%# Eval("Email") %>' />
                    </td>
                    <td>
                        <ra:CheckBox 
                            runat="server" 
                            OnCheckedChanged="IsAdminChanged"
                            Checked='<%# Eval("IsAdmin") %>' />
                    </td>
                    <td>
                        <ra:CheckBox 
                            runat="server" 
                            OnCheckedChanged="ConfirmedChanged"
                            Checked='<%# Eval("Confirmed") %>' />
                    </td>
                    <td>
                        <ra:LinkButton 
                            runat="server" 
                            OnClick="DeleteUser"
                            Text='Delete' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </ra:Panel>
    <br />
    <br />
    <h3>Create new user</h3>
    <table>
        <tr>
            <td>Username</td>
            <td>
                <ra:TextBox 
                    runat="server" 
                    ID="nUsername" />
            </td>
        </tr>
        <tr>
            <td>Password</td>
            <td>
                <ra:TextBox 
                    runat="server" 
                    ID="nPassword" />
            </td>
        </tr>
        <tr>
            <td>Email</td>
            <td>
                <ra:TextBox 
                    runat="server" 
                    ID="nEmail" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <ra:CheckBox 
                    runat="server" 
                    Text="IsAdmin"
                    ID="nIsAdmin" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:right;">
                <ra:Button 
                    runat="server" 
                    ID="nCreate" 
                    OnClick="nCreate_Click"
                    Text="Save" />
            </td>
        </tr>
    </table>
</ra:Panel>

<div 
    style="margin-top:15px;width:80%;background-color:Yellow;border:solid 1px #999;padding:5px;">
    <ra:LinkButton 
        runat="server" 
        ID="adminUsers" 
        OnClick="adminUsers_Click"
        Text="Administrate users" />
</div>