<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="_Default" 
    Title="Ra Ajax Samples - Home" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Ra Ajax Samples Home</h1>
    <p>
        Ra Ajax is an <strong>Ajax library for ASP.NET and Mono</strong> but its general nature on the client-side
        will make it very useful also for other server-side bindings like PHP, J2EE RubyOnRails etc. Ra Ajax
        is licensed under an MIT(ish) license which basically says <em>"use as you wish, but don't fork the code"</em>.
        Ra Ajax is built around the assumption that JavaScript is hard and not something Application Developers should
        spend time on doing. Every single sample here in the samples section is written entirely without <em>one line
        of Custom JavaScript</em>. This makes you;
    </p>
    <ul>
        <li>More productive</li>
        <li>Less frustrated</li>
        <li>More happy</li>
    </ul>
    <p>
        Try out the Ra Ajax "Hello World" application below.
    </p>
    <br />
    <table style="float:left;">
        <tr>
            <td>
                First name:
            </td>
            <td>
                <ra:TextBox runat="server" ID="txtFirstName" />
            </td>
        </tr>
        <tr>
            <td>
                Surname:
            </td>
            <td>
                <ra:TextBox runat="server" ID="txtSurname" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:right;">
                <ra:Button runat="server" ID="btnSubmit" Text="Save" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>

    <ra:Panel 
        runat="server" 
        ID="pnlResults" 
        Visible="false" 
        style="border:solid 1px Black;background-color:Yellow;width:400px;text-align:center;padding:25px;float:left;">
        <ra:Label runat="server" ID="lblResults" />
        <p>
            Notice how there was no "custom JavaScript" written to show this Panel. Everything was done on the
            server in pure C# and resembles the ASP.NET WebControls way of writing Web Applications.
        </p>
    </ra:Panel>
</asp:Content>

