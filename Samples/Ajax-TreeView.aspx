<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-TreeView.aspx.cs" 
    Inherits="Samples.TreeView" 
    Title="Ra-Ajax TreeView Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra Ajax Samples - TreeView</h1>
    <p>
        TreeView sample...
    </p>
    <ext:TreeView runat="server" ID="tree">
        <ext:TreeViewItem runat="server" ID="item1">
            Open Web great things
            <ext:TreeViewItem runat="server" ID="child_1">
                Ajax
            </ext:TreeViewItem>
            <ext:TreeViewItem runat="server" ID="child_2">
                (X)HTML
            </ext:TreeViewItem>
            <ext:TreeViewItem runat="server" ID="child_3">
                CSS
            </ext:TreeViewItem>
        </ext:TreeViewItem>
        <ext:TreeViewItem runat="server" ID="item2">
            Proprietary lock-in crap
        </ext:TreeViewItem>
    </ext:TreeView>
</asp:Content>
