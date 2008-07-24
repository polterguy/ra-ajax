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
    <ra:Panel runat="server" ID="repUsersWrapper">
        <ra:LinkButton 
            runat="server" 
            ID="closeUsers" 
            OnClick="closeUsers_Click"
            style="position:absolute;right:2px;top:2px;"
            Text="X" />
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
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </ra:Panel>
</ra:Panel>

<div 
    style="margin-top:15px;width:80%;background-color:Yellow;border:solid 1px #999;padding:5px;">
    <ra:LinkButton 
        runat="server" 
        ID="adminUsers" 
        OnClick="adminUsers_Click"
        Text="Administrate users" />
</div>