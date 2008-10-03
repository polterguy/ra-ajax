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
            <ext:TreeViewItem runat="server" ID="good_1">
                Ajax
            </ext:TreeViewItem>
            <ext:TreeViewItem runat="server" ID="good_2">
                (X)HTML
            </ext:TreeViewItem>
            <ext:TreeViewItem runat="server" ID="good_3">
                CSS
            </ext:TreeViewItem>
        </ext:TreeViewItem>
        <ext:TreeViewItem runat="server" ID="item2">
            Proprietary lock-in crap
            <ext:TreeViewItem runat="server" ID="bad_1">
                Adobe Flex
            </ext:TreeViewItem>
            <ext:TreeViewItem runat="server" ID="bad_2">
                Silverlight
            </ext:TreeViewItem>
            <ext:TreeViewItem runat="server" ID="bad_3">
                ActiveX
                <ext:TreeViewItem runat="server" ID="activex_1">
                    ActiveX 1.0
                </ext:TreeViewItem>
                <ext:TreeViewItem runat="server" ID="activex_2">
                    ActiveX 2.0
                </ext:TreeViewItem>
            </ext:TreeViewItem>
        </ext:TreeViewItem>
    </ext:TreeView>
</asp:Content>
