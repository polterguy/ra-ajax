<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Combining.ascx.cs" 
    Inherits="UC.Combining" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<table class="nameTbl">
    <tr>
        <td>Name</td>
        <td>
            <ra:TextBox 
                runat="server" 
                ID="name" />
        </td>
    </tr>
    <tr>
        <td>Age</td>
        <td>
            <ra:TextBox 
                runat="server" 
                ID="age" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <ra:Label 
                runat="server" 
                ID="err" 
                CssClass="errLbl" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <ra:Button 
                runat="server" 
                ID="Button1" 
                Text="Submit" 
                OnClick="submit_Click" />
        </td>
    </tr>
</table>