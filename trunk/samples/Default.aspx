<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="_Default" 
    Title="Ra Ajax Samples" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
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
        style="border:solid 1px Black;background-color:Yellow;">
        <ra:Label runat="server" ID="lblResults" />
    </ra:Panel>
</asp:Content>

