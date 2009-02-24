<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="EditProfile.ascx.cs" 
    Inherits="RaWebsite.EditProfile" %>
    
<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>
    
<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>
    
<table style="margin-bottom:0em;">
    <tr>
        <td>Nickname:</td>
        <td style="width:100%;">
            <ra:TextBox runat="server" style="width:95%" ID="changeNickname" />
        </td>
    </tr>
    <tr>
        <td>Password:</td>
        <td>
            <ra:TextBox style="width:95%;" runat="server" ID="changePassword" TextMode="password" />
        </td>
    </tr>
    <tr>
        <td>Repeat it:</td>
        <td>
            <ra:TextBox style="width:95%;" runat="server" ID="changePasswordConfirm" TextMode="password" />
        </td>
    </tr>
    <tr>
        <td>Email:</td>
        <td>
            <ra:TextBox style="width:95%;" runat="server" ID="changeEmail" />
        </td>
    </tr>
    <tr>
        <td style="text-align:right;" colspan="2">
            <ra:Label runat="server" ID="resultLabel" Visible="false" style="color:#c00;float:left;" />
            <ra:Button runat="server" ID="btnChangeProfile" Text="Update" OnClick="btnChangeProfile_Click" style="margin-right:5px;" />
        </td>
    </tr>
</table>