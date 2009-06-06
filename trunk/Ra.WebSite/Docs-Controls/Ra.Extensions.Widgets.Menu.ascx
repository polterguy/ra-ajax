<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Widgets.Menu.ascx.cs" 
    Inherits="Docs_Controls_Menu" %>

<ra:Label 
    runat="server" 
    Text="Changes..."
    ID="lbl" />

<ra:Menu 
    runat="server" 
    ID="menu" 
    OnMenuItemSelected="ItemSelected" 
    ExpandMethod="Hover">

    <ra:MenuItems runat="server" ID="root">
        <ra:MenuItem runat="server" ID="file" Text="Files">
            <ra:MenuItems runat="server" ID="files">
                <ra:MenuItem runat="server" ID="save" Text="Save" />
                <ra:MenuItem runat="server" ID="load" Text="Load" />
                <ra:MenuItem runat="server" ID="saveas" Text="Save as..." />
                <ra:MenuItem runat="server" ID="Print" Text="Print">
                    <ra:MenuItems runat="server" ID="printOptions">
                        <ra:MenuItem 
                            runat="server" 
                            ID="fastPrint" 
                            Text="Fast Print" />
                        <ra:MenuItem 
                            runat="server" 
                            ID="printPreview" 
                            Text="Print Preview">
                            <ra:MenuItems 
                                runat="server" 
                                ID="previews">
                                <ra:MenuItem 
                                    runat="server" 
                                    ID="preview1" 
                                    Text="Preview 1" />
                                <ra:MenuItem 
                                    runat="server" 
                                    ID="preview2" 
                                    Text="Preview 2" />
                            </ra:MenuItems>
                        </ra:MenuItem>
                    </ra:MenuItems>
                </ra:MenuItem>
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

<div style="height:250px;">&nbsp;</div>