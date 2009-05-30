<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Tree.ascx.cs" 
    Inherits="Docs_Controls_Tree" %>

<ra:Label 
    runat="server" 
    Text="Changes..."
    ID="lbl" />

<ra:Tree 
    runat="server" 
    style="width:250px;height:200px;overflow:auto;" 
    OnSelectedNodeChanged="tree_ItemClicked"
    ID="tree">

    <ra:TreeNodes 
        runat="server" 
        ID="root">

        <ra:TreeNode runat="server" ID="file" Text="File">

            <ra:TreeNodes runat="server" Caption="Some files" ID="files">
                <ra:TreeNode runat="server" ID="open" Text="Open" />
                <ra:TreeNode runat="server" ID="save" Text="Save" />
                <ra:TreeNode runat="server" ID="saveAs" Text="Save as...">
                    <ra:TreeNodes runat="server" ID="saveAsDocument">
                        <ra:TreeNode runat="server" ID="pdf" Text="PDF" />
                        <ra:TreeNode runat="server" ID="odf" Text="ODF">
                            <ra:TreeNodes runat="server" ID="odfs">
                                <ra:TreeNode 
                                    runat="server" 
                                    ID="odf1" 
                                    Text="ODF1" />
                                <ra:TreeNode 
                                    runat="server" 
                                    ID="odf2" 
                                    Text="ODF2" />
                            </ra:TreeNodes>
                        </ra:TreeNode>
                    </ra:TreeNodes>
                </ra:TreeNode>
            </ra:TreeNodes>
        </ra:TreeNode>

        <ra:TreeNode runat="server" ID="edit" Text="Edit">
            <ra:TreeNodes runat="server" ID="edits">
                <ra:TreeNode runat="server" ID="copy" Text="Copy" />
                <ra:TreeNode runat="server" ID="paste" Text="Paste" />
                <ra:TreeNode runat="server" ID="cut" Text="Cut" />
            </ra:TreeNodes>
        </ra:TreeNode>

        <ra:TreeNode runat="server" ID="window" Text="Window">
            <ra:TreeNodes 
                runat="server" 
                Caption="Windows"
                OnGetChildNodes="window_GetChildNodes"
                ID="SliderMenuLevel1" />
        </ra:TreeNode>

        <ra:TreeNode runat="server" ID="settings" Text="Settings" />

    </ra:TreeNodes>

</ra:Tree>

<p style="margin:5px;">
    Above you can see both statically and dynamically create TreeNodes.
    The Window Menu Item for instance is dynamically created. This means
    it will have zero overhead to your page before the user explicitly clicks
    it to display its children. This means that you can have Trees with
    virtually millions of TreeNodes without making the performance drop anything
    at all.
</p>
