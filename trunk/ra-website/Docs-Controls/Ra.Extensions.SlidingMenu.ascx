<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.SlidingMenu.ascx.cs" 
    Inherits="Docs_Controls_SlidingMenu" %>

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

<ext:SlidingMenu 
    runat="server" 
    style="width:250px;height:200px;" 
    OnItemClicked="slider_ItemClicked" 
    OnNavigate="slider_Navigate"
    ID="slider">

    <ext:SlidingMenuLevel 
        runat="server" 
        ID="root">
        <ext:SlidingMenuItem 
            runat="server" 
            ID="file"
            Text="File">
            <ext:SlidingMenuLevel 
                runat="server" 
                Caption="Some files"
                ID="files">
                <ext:SlidingMenuItem 
                    runat="server" 
                    ID="open" 
                    Text="Open" />
                <ext:SlidingMenuItem 
                    runat="server" 
                    ID="save" 
                    Text="Save" />
                <ext:SlidingMenuItem 
                    runat="server" 
                    ID="saveAs" 
                    Text="Save as...">
                    <ext:SlidingMenuLevel 
                        runat="server" 
                        ID="saveAsDocument">
                        <ext:SlidingMenuItem 
                            runat="server" 
                            ID="pdf" 
                            Text="PDF" />
                        <ext:SlidingMenuItem 
                            runat="server" 
                            ID="odf" 
                            Text="ODF">
                            <ext:SlidingMenuLevel 
                                runat="server" 
                                ID="odfs">
                                <ext:SlidingMenuItem 
                                    runat="server" 
                                    ID="odf1" 
                                    Text="ODF1" />
                                <ext:SlidingMenuItem 
                                    runat="server" 
                                    ID="odf2" 
                                    Text="ODF2" />
                            </ext:SlidingMenuLevel>
                        </ext:SlidingMenuItem>
                    </ext:SlidingMenuLevel>
                </ext:SlidingMenuItem>
            </ext:SlidingMenuLevel>
        </ext:SlidingMenuItem>
        <ext:SlidingMenuItem 
            runat="server" 
            ID="edit" 
            Text="Edit">
            <ext:SlidingMenuLevel 
                runat="server" 
                ID="edits">
                <ext:SlidingMenuItem 
                    runat="server" 
                    ID="copy" 
                    Text="Copy" />
                <ext:SlidingMenuItem 
                    runat="server" 
                    ID="paste" 
                    Text="Paste" />
                <ext:SlidingMenuItem 
                    runat="server" 
                    ID="cut" 
                    Text="Cut" />
            </ext:SlidingMenuLevel>
        </ext:SlidingMenuItem>
        <ext:SlidingMenuItem 
            runat="server" 
            ID="window" 
            Text="Window">
            <ext:SlidingMenuLevel 
                runat="server" 
                Caption="Windows"
                OnGetChildNodes="window_GetChildNodes"
                ID="SliderMenuLevel1" />
        </ext:SlidingMenuItem>
        <ext:SlidingMenuItem 
            runat="server" 
            ID="settings" 
            Text="Settings" />
    </ext:SlidingMenuLevel>

</ext:SlidingMenu>
