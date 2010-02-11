<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Widgets.SlidingMenu.ascx.cs" 
    Inherits="Docs_Controls_SlidingMenu" %>

<ra:Label 
    runat="server" 
    Text="Changes..."
    ID="lbl" />

<br />
<ra:Button 
    runat="server" 
    Text="Switch to SaveAs..."
    OnClick="switchToSaveAs_Click"
    ID="switchToSaveAs" />

<ra:Button 
    runat="server" 
    Text="Switch to Window-0-1-2..."
    OnClick="switchToWindow_Click"
    ID="switchToWindow" />

<div 
    style="width:400px;overflow:hidden;position:relative;" 
    id="something">
    <ra:Panel 
        runat="server" 
        ID="customBreadParent" 
        CssClass="bread-crumb-parent" />
</div>

<ra:SlidingMenu 
    runat="server" 
    style="width:250px;height:200px;" 
    OnItemClicked="slider_ItemClicked" 
    CustomBreadCrumbControl="customBreadParent"
    OnNavigate="slider_Navigate"
    ID="slider">

    <ra:SlidingMenuLevel 
        runat="server" 
        ID="root">

        <ra:SlidingMenuItem runat="server" ID="file" Text="File">

            <ra:SlidingMenuLevel runat="server" Caption="Some files" ID="files">
                <ra:SlidingMenuItem runat="server" ID="open" Text="Open" />
                <ra:SlidingMenuItem runat="server" ID="save" Text="Save" />
                <ra:SlidingMenuItem runat="server" ID="saveAs" Text="Save as...">
                    <ra:SlidingMenuLevel runat="server" ID="saveAsDocument">
                        <ra:SlidingMenuItem runat="server" ID="pdf" Text="PDF" />
                        <ra:SlidingMenuItem runat="server" ID="odf" Text="ODF">
                            <ra:SlidingMenuLevel runat="server" ID="odfs">
                                <ra:SlidingMenuItem 
                                    runat="server" 
                                    ID="odf1" 
                                    Text="ODF1" />
                                <ra:SlidingMenuItem 
                                    runat="server" 
                                    ID="odf2" 
                                    Text="ODF2" />
                            </ra:SlidingMenuLevel>
                        </ra:SlidingMenuItem>
                    </ra:SlidingMenuLevel>
                </ra:SlidingMenuItem>
            </ra:SlidingMenuLevel>
        </ra:SlidingMenuItem>

        <ra:SlidingMenuItem runat="server" ID="edit" Text="Edit">
            <ra:SlidingMenuLevel runat="server" ID="edits">
                <ra:SlidingMenuItem runat="server" ID="copy" Text="Copy" />
                <ra:SlidingMenuItem runat="server" ID="paste" Text="Paste" />
                <ra:SlidingMenuItem runat="server" ID="cut" Text="Cut" />
            </ra:SlidingMenuLevel>
        </ra:SlidingMenuItem>

        <ra:SlidingMenuItem runat="server" ID="window" Text="Window">
            <ra:SlidingMenuLevel 
                runat="server" 
                Caption="Windows"
                OnGetChildNodes="window_GetChildNodes"
                ID="SliderMenuLevel1" />
        </ra:SlidingMenuItem>

        <ra:SlidingMenuItem runat="server" ID="settings" Text="Settings" />

    </ra:SlidingMenuLevel>

</ra:SlidingMenu>

<p style="margin:5px;">
    Above you can see both statically and dynamically create SlidingMenuItems.
    The Window Menu Item for instance is dynamically created. This means
    it will have zero overhead to your page before the user explicitly clicks
    it to display its children. This means that you can have SlidingMenus with
    virtually millions of MenuItems without making the performance drop anything
    at all.
</p>
<p style="margin:5px;">
    Notice also how the bread-crumb animates when you click 5 or more times 
    into the Window hierarchy.
</p>