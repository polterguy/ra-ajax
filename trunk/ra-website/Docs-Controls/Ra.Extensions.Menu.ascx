<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Menu.ascx.cs" 
    Inherits="Docs_Controls_Menu" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ra:Label 
    runat="server" 
    Text="Changes..."
    ID="lbl" />

<ext:Menu runat="server" ID="menu" OnMenuItemSelected="ItemSelected">

    <ext:MenuItems runat="server" ID="root">
        <ext:MenuItem runat="server" ID="file" Text="Files">
            <ext:MenuItems runat="server" ID="files">
                <ext:MenuItem runat="server" ID="save" Text="Save" />
                <ext:MenuItem runat="server" ID="load" Text="Load" />
                <ext:MenuItem runat="server" ID="saveas" Text="Save as..." />
                <ext:MenuItem runat="server" ID="Print" Text="Print" />
            </ext:MenuItems>
        </ext:MenuItem>
        <ext:MenuItem runat="server" ID="edit" Text="Edit">
            <ext:MenuItems runat="server" ID="edits">
                <ext:MenuItem runat="server" ID="copy" Text="Copy" />
                <ext:MenuItem runat="server" ID="paste" Text="Paste" />
                <ext:MenuItem runat="server" ID="cut" Text="Cut" />
            </ext:MenuItems>
        </ext:MenuItem>
    </ext:MenuItems>

</ext:Menu>

<div style="height:200px;">&nbsp;</div>