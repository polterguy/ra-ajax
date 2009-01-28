<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="ChartDataCollector.ascx.cs" 
    Inherits="ChartDataCollector" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<table>
    <tr>
        <td>First value</td>
        <td>
            <ra:TextBox runat="server" ID="first" Text="90" />
        </td>
    </tr>
    <tr>
        <td>Second value</td>
        <td>
            <ra:TextBox runat="server" ID="second" Text="15" />
        </td>
    </tr>
    <tr>
        <td>Third value</td>
        <td>
            <ra:TextBox runat="server" ID="third" Text="45" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <ra:Label 
                runat="server" 
                ID="err" 
                style="color:Red;" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <ra:Button 
                runat="server" 
                ID="save" 
                AccessKey="V" 
                Tooltip="V is shortcut key"
                OnClick="save_Click"
                Text="View Graph" />
        </td>
    </tr>
</table>



