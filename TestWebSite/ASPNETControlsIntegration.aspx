<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="ASPNETControlsIntegration.aspx.cs" 
    Inherits="ASPNETControlsIntegration" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>Untitled Page</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:TextBox runat="server" ID="txt" Text="text" />
                <asp:TextBox runat="server" ID="txtArea" TextMode="MultiLine" Text="text" />
                <asp:DropDownList runat="server" ID="ddl">
                    <asp:ListItem Value="first" />
                    <asp:ListItem Value="second" Selected="true" />
                </asp:DropDownList>
                <asp:CheckBox runat="server" ID="chk1" />
                <asp:CheckBox runat="server" ID="chk2" Checked="true" />
                <asp:RadioButton runat="server" ID="rdo1" />
                <asp:RadioButton runat="server" ID="rdo2" Checked="true" />
                <ra:Button runat="server" ID="btn" Text="Submits form with RA-Ajax" OnClick="btn_Click" />
                <br />
                <ra:Label runat="server" ID="lbl" Text="Set to values" />

                <br />
                <br />

                <ra:TextBox runat="server" ID="txtRa" Text="text" />
                <ra:TextArea runat="server" ID="txtAreaRa" Text="text" />
                <ra:SelectList runat="server" ID="ddlRa">
                    <ra:ListItem Value="first" />
                    <ra:ListItem Value="second" Selected="true" />
                </ra:SelectList>
                <ra:CheckBox runat="server" ID="chk1Ra" />
                <ra:CheckBox runat="server" ID="chk2Ra" Checked="true" />
                <ra:RadioButton runat="server" ID="rdo1Ra" />
                <ra:RadioButton runat="server" ID="rdo2Ra" Checked="true" />

                <asp:Button runat="server" ID="btnASP" Text="Submits form with NON-Ajax" OnClick="btnASP_Click" />
                <br />
                <asp:Label runat="server" ID="lblASP" Text="Set to values" />

            </div>
        </form>
    </body>
</html>
