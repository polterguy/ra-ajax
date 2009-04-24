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

    <h1>Slider Menu Sample</h1>
    <div>
        <ext:SliderMenu 
            runat="server" 
            style="width:250px;height:200px;" 
            OnItemClicked="slider_ItemClicked"
            ID="slider">

            <ext:SliderMenuLevel 
                runat="server" 
                ID="root">
                <ext:SliderMenuItem 
                    runat="server" 
                    ID="file"
                    Text="File">
                    <ext:SliderMenuLevel 
                        runat="server" 
                        ID="files">
                        <ext:SliderMenuItem 
                            runat="server" 
                            ID="open" 
                            Text="Open" />
                        <ext:SliderMenuItem 
                            runat="server" 
                            ID="save" 
                            Text="Save" />
                        <ext:SliderMenuItem 
                            runat="server" 
                            ID="saveAs" 
                            Text="Save as...">
                            <ext:SliderMenuLevel 
                                runat="server" 
                                ID="saveAsDocument">
                                <ext:SliderMenuItem 
                                    runat="server" 
                                    ID="pdf" 
                                    Text="PDF" />
                                <ext:SliderMenuItem 
                                    runat="server" 
                                    ID="odf" 
                                    Text="ODF">
                                    <ext:SliderMenuLevel 
                                        runat="server" 
                                        ID="odfs">
                                        <ext:SliderMenuItem 
                                            runat="server" 
                                            ID="odf1" 
                                            Text="ODF1" />
                                        <ext:SliderMenuItem 
                                            runat="server" 
                                            ID="odf2" 
                                            Text="ODF2" />
                                    </ext:SliderMenuLevel>
                                </ext:SliderMenuItem>
                            </ext:SliderMenuLevel>
                        </ext:SliderMenuItem>
                    </ext:SliderMenuLevel>
                </ext:SliderMenuItem>
                <ext:SliderMenuItem 
                    runat="server" 
                    ID="edit" 
                    Text="Edit">
                    <ext:SliderMenuLevel 
                        runat="server" 
                        ID="edits">
                        <ext:SliderMenuItem 
                            runat="server" 
                            ID="copy" 
                            Text="Copy" />
                        <ext:SliderMenuItem 
                            runat="server" 
                            ID="paste" 
                            Text="Paste" />
                        <ext:SliderMenuItem 
                            runat="server" 
                            ID="cut" 
                            Text="Cut" />
                    </ext:SliderMenuLevel>
                </ext:SliderMenuItem>
                <ext:SliderMenuItem 
                    runat="server" 
                    ID="view" 
                    Text="View">
                    <ext:SliderMenuLevel 
                        runat="server" 
                        OnGetChildNodes="views_GetChildNodes"
                        ID="views" />
                </ext:SliderMenuItem>
                <ext:SliderMenuItem 
                    runat="server" 
                    ID="window" 
                    Text="Window">
                    <ext:SliderMenuLevel 
                        runat="server" 
                        OnGetChildNodes="window_GetChildNodes"
                        ID="SliderMenuLevel1" />
                </ext:SliderMenuItem>
                <ext:SliderMenuItem 
                    runat="server" 
                    ID="settings" 
                    Text="Settings" />
            </ext:SliderMenuLevel>

        </ext:SliderMenu>
    </div>
    <ra:Label 
        runat="server" 
        ID="lbl" />

</asp:Content>

