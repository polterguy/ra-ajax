<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Login.aspx.cs" 
    Inherits="RaWebsite.Login" 
    Title="Ra-Ajax Login Page" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <ra:Panel runat="server" ID="pnlLogin" Visible="false" style="border:solid 1px #aaa;padding:5px;width:300px;">
        <h1 style="margin-top:0;">Enter Your Login Credentials</h1>
        <table style="width:100%;">
            <tr style="width:100%;">
                <td>Username:</td>
                <td style="width:100%;">
                    <ra:TextBox  style="width:100%;" runat="server" ID="username" />
                </td>
            </tr>
            <tr>
                <td>Password:</td>
                <td>
                    <ra:TextBox style="width:100%;" runat="server" ID="pwd" TextMode="password" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <ra:CheckBox runat="server" ID="remember" Text="Remember me" />
                </td>
            </tr>
            <tr>
                <td style="text-align:right;" colspan="2">
                    <ra:Button runat="server" ID="login" Text="Login" OnClick="login_Click" />
                    or 
                    <ra:Button runat="server" ID="register" Text="Register" OnClick="register_Click" />
                </td>
            </tr>
        </table>
    </ra:Panel>
    
    <ra:Panel runat="server" Visible="false" ID="pnlConfirmRegistration" style="background-color:Yellow;border:solid 1px #333;padding:5px;">
        <span runat="server" id="newUserWelcome"></span>
    </ra:Panel>
    
    <ra:Panel runat="server" ID="pnlRegister" Visible="false" style="border:solid 1px #aaa;width:350px;padding:5px;"> 
	    <h1 style="margin-top:0;">Register</h1>
        <table style="width:100%;">
            <tr>
                <td>Username:</td>
                <td style="width:65%;">
                    <ra:TextBox style="width:100%;" runat="server" ID="newUsername" />
                </td>
            </tr>
            <tr>
                <td>Password:</td>
                <td>
                    <ra:TextBox style="width:100%;" runat="server" ID="newPassword" TextMode="password" />
                </td>
            </tr>
            <tr>
                <td>Repeat password:</td>
                <td>
                    <ra:TextBox style="width:100%;" runat="server" ID="newPasswordRepeat" TextMode="password" />
                </td>
            </tr>
            <tr>
                <td>Email:</td>
                <td>
                    <ra:TextBox style="width:100%;" runat="server" ID="newEmail" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right;">
                    <ra:Button runat="server" ID="finishRegister" Text="Finish" OnClick="finishRegister_Click" />
                </td>
            </tr>
        </table>
    </ra:Panel>
    
    <ra:Panel runat="server" ID="pnlProfile" Visible="false" style="border:solid 1px #aaa;padding:5px;width:400px;height:300px;">
	    <h1 style="margin-top:0;">Edit Your Profile</h1>
        <table>
            <tr>
                <td>Password:</td>
                <td>
                    <ra:TextBox runat="server" ID="changePassword" style="width:250px;" TextMode="password" />
                </td>
            </tr>
            <tr>
                <td>Repeat password:</td>
                <td>
                    <ra:TextBox runat="server" ID="changePasswordConfirm" style="width:250px;" TextMode="password" />
                </td>
            </tr>
            <tr>
                <td>Email:</td>
                <td>
                    <ra:TextBox runat="server" style="width:250px;" ID="changeEmail" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">Signature:</td>
                <td>
                    <ra:TextArea Columns="40" Rows="5" runat="server" style="width:250px;" ID="changeSignature" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right;">
                    <ra:Button runat="server" ID="btnChangeProfile" Text="Save" OnClick="btnChangeProfile_Click" />
                </td>
            </tr>
        </table>
    </ra:Panel>
    
    <ra:Label
        runat="server" 
        ID="resultLabel" 
        Visible="false"
        style="width:350px;margin-top:20px;padding:5px;border:solid 1px #aaa;background-color:Yellow;color:#c00;" />
    
</asp:Content>

