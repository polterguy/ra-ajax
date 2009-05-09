<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Tree.ascx.cs" 
    Inherits="Docs_Controls_Tree" %>

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

<ext:Tree 
    runat="server" 
    style="width:250px;height:200px;overflow:auto;" 
    OnSelectedNodeChanged="tree_ItemClicked" 
    ID="tree">

    <ext:TreeNodes 
        runat="server" 
        ID="root">

        <ext:TreeNode runat="server" ID="file" Text="File">

            <ext:TreeNodes runat="server" Caption="Some files" ID="files">
                <ext:TreeNode runat="server" ID="open" Text="Open" />
                <ext:TreeNode runat="server" ID="save" Text="Save" />
                <ext:TreeNode runat="server" ID="saveAs" Text="Save as...">
                    <ext:TreeNodes runat="server" ID="saveAsDocument">
                        <ext:TreeNode runat="server" ID="pdf" Text="PDF" />
                        <ext:TreeNode runat="server" ID="odf" Text="ODF">
                            <ext:TreeNodes runat="server" ID="odfs">
                                <ext:TreeNode 
                                    runat="server" 
                                    ID="odf1" 
                                    Text="ODF1" />
                                <ext:TreeNode 
                                    runat="server" 
                                    ID="odf2" 
                                    Text="ODF2" />
                            </ext:TreeNodes>
                        </ext:TreeNode>
                    </ext:TreeNodes>
                </ext:TreeNode>
            </ext:TreeNodes>
        </ext:TreeNode>

        <ext:TreeNode runat="server" ID="edit" Text="Edit">
            <ext:TreeNodes runat="server" ID="edits">
                <ext:TreeNode runat="server" ID="copy" Text="Copy" />
                <ext:TreeNode runat="server" ID="paste" Text="Paste" />
                <ext:TreeNode runat="server" ID="cut" Text="Cut" />
            </ext:TreeNodes>
        </ext:TreeNode>

        <ext:TreeNode runat="server" ID="window" Text="Window">
            <ext:TreeNodes 
                runat="server" 
                Caption="Windows"
                OnGetChildNodes="window_GetChildNodes"
                ID="SliderMenuLevel1" />
        </ext:TreeNode>

        <ext:TreeNode runat="server" ID="settings" Text="Settings" />

    </ext:TreeNodes>

</ext:Tree>

<p style="margin:5px;">
    Above you can see both statically and dynamically create TreeNodes.
    The Window Menu Item for instance is dynamically created. This means
    it will have zero overhead to your page before the user explicitly clicks
    it to display its children. This means that you can have Trees with
    virtually millions of TreeNodes without making the performance drop anything
    at all.
</p>
