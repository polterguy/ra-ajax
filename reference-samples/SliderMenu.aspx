<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="SliderMenu.aspx.cs" 
    Inherits="RefSamples.SliderMenuSample" 
    Title="Slider Menu Sample" %>

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
    Runat="Server">

    <div style="width:400px;overflow:hidden;position:relative;" id="something">
        <ra:Panel 
            runat="server" 
            ID="customBreadParent" 
            CssClass="bread-crumb-parent" />
    </div>
    <h1>Slider Menu Sample</h1>
    <div>
        <ext:SlidingMenu 
            runat="server" 
            style="width:250px;height:200px;" 
            OnItemClicked="slider_ItemClicked"
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
                    ID="view" 
                    Text="View">
                    <ext:SlidingMenuLevel 
                        runat="server" 
                        OnGetChildNodes="views_GetChildNodes"
                        ID="views" />
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
    </div>
    <ra:Label 
        runat="server" 
        ID="lbl" />

</asp:Content>

