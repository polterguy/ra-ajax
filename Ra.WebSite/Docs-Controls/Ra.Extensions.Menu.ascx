<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Menu.ascx.cs" 
    Inherits="Docs_Controls_Menu" %>

<ra:Label 
    runat="server" 
    Text="Changes..."
    ID="lbl" />

<ra:Menu runat="server" ID="menu" OnMenuItemSelected="ItemSelected">

    <ra:MenuItems runat="server" ID="root">
        <ra:MenuItem runat="server" ID="file" Text="Files">
            <ra:MenuItems runat="server" ID="files">
                <ra:MenuItem runat="server" ID="save" Text="Save" />
                <ra:MenuItem runat="server" ID="load" Text="Load" />
                <ra:MenuItem runat="server" ID="saveas" Text="Save as..." />
                <ra:MenuItem runat="server" ID="Print" Text="Print" />
            </ra:MenuItems>
        </ra:MenuItem>
        <ra:MenuItem runat="server" ID="edit" Text="Edit">
            <ra:MenuItems runat="server" ID="edits">
                <ra:MenuItem runat="server" ID="copy" Text="Copy" />
                <ra:MenuItem runat="server" ID="paste" Text="Paste" />
                <ra:MenuItem runat="server" ID="cut" Text="Cut" />
            </ra:MenuItems>
        </ra:MenuItem>
    </ra:MenuItems>

</ra:Menu>

<div style="height:200px;">&nbsp;</div>