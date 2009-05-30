<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Login.ascx.cs" 
    Inherits="RaWebsite.Login" %>
    
<table style="margin-bottom:0em;">
    <tr>
        <td>Username:</td>
        <td style="width:100%;">
            <ra:TextBox style="width:95%;" runat="server" ID="username" />
        </td>
    </tr>
    <tr>
        <td>Password:</td>
        <td>
            <ra:TextBox style="width:95%;" runat="server" ID="pwd" TextMode="password" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <ra:CheckBox runat="server" ID="remember" Text="Remember me" />
        </td>
    </tr>
    <tr>
        <td style="text-align:right;" colspan="2">
            <ra:Label runat="server" ID="resultLabel" Visible="false" style="color:#c00;float:left;" />
            <ra:Button runat="server" ID="login" Text="Login" OnClick="login_Click" style="margin-right:5px;" />
        </td>
    </tr>
</table>
