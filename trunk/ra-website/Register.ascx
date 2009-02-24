<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Register.ascx.cs" 
    Inherits="RaWebsite.Register" %>
    
<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>
    
<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>
    
<table runat="server" id="registerationTable" style="margin-bottom:0em;">
    <tr>
        <td>Username:</td>
        <td style="width:100%;">
            <ra:TextBox style="width:95%;" runat="server" ID="newUsername" />
        </td>
    </tr>
    <tr>
        <td>Password:</td>
        <td>
            <ra:TextBox style="width:95%;" runat="server" ID="newPassword" TextMode="password" />
        </td>
    </tr>
    <tr>
        <td>Repeat it:</td>
        <td>
            <ra:TextBox style="width:95%;" runat="server" ID="newPasswordRepeat" TextMode="password" />
        </td>
    </tr>
    <tr>
        <td>Email:</td>
        <td>
            <ra:TextBox style="width:95%;" runat="server" ID="newEmail" />
        </td>
    </tr>
    <tr>
        <td style="text-align:right;" colspan="2">
            <ra:Label runat="server" ID="resultLabel" Visible="false" style="color:#c00;float:left;" />
            <ra:Button runat="server" ID="finishRegister" Text="Register" OnClick="finishRegister_Click" style="margin-right:5px;" />
        </td>
    </tr>
</table>
<table runat="server" id="registerationFinishedTable" style="margin-bottom:0em;display:none;">
    <tr>
        <td>
            <span style="color:#c00;float:left;">Thank you for registering. Please check your email for comfirmation.</span>
        </td>
    </tr>
</table>