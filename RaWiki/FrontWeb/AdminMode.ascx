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
    style="width:80%;background-color:Yellow;border:solid 1px #999;padding:5px;"
    Visible="false"
    ID="users">
    <ra:Panel runat="server" ID="repUsersWrapper">
        <asp:Repeater runat="server" ID="repUsers">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Username</th>
                        <th>Password</th>
                        <th>Created</th>
                        <th>Email</th>
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