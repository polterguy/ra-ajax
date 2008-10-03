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
        This is our TreeView sample. The Ra-Ajax TreeView control is very flexible in addition to being a good friend
        with your browser. You can choose to render items "statically" in markup or "dynamically" through event handlers.
        And you can mix these two methods in the same TreeView control just as you wish.
    </p>
    <ext:TreeView runat="server" ID="tree" CssClass="tree" style="width:250px;">
        <ext:TreeViewItem runat="server" ID="item1">
            Open Web great things
            <ext:TreeViewItem runat="server" ID="good_1">
                Ajax
            </ext:TreeViewItem>
            <ext:TreeViewItem runat="server" ID="good_2" OnGetChildItems="good_2_GetChildItems">
                HTML
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
    <p>
        The above TreeView has two root TreeViewItems. Both of these root items are not expanded. By clicking on the
        plus sign left of the root items you can expand those root items. In both of the root items there are three
        child TreeViewItems where one of those have dynamically rendered child items. If you look at the code you
        will see that the HTML child items of the first root item have an event handler for retrieving child items
        but no directly "statically" rendered items within it. The child TreeViewItems of the "HTML" element will
        not be populated to the client (browser) before you actually expand it. While all the other items will
        be rendered directly into the markup as HTML but hidden through CSS if they are not expanded.
    </p>
    <h2>Advantages of the Ra-Ajax TreeView</h2>
    <p>
        This means that all the statically rendered items will in fact be "pure HTML" which means they will be very
        search engine friendly and also easy to interact with through screen-readers and such. While the dynamically 
        created items (child items of HTML node) will not use any bandwidth before you physically expand the HTML 
        TreeViewItem. This means that it is possible to have extremely large TreeView nodes while at the same time
        have some of the nodes be visible as pure markup. This means that you can render x nodes (where x is a small number)
        which are initially being rendered into the page HTML which will be 100% "pure" HTML and visible for search
        engines while at the same time have extremely large TreeViewItem children of those which are rendered on a
        demand basis and will not use any bandwidth at all before rendered.        
    </p>
    <p>
        So in the same page where some of your nodes are visible to search engines you can still have extremely large
        TreeViewNodes within the same TreeView which have like 50,000 nodes and such but will not mess with the
        responsiveness of your application. This makes the Ra-Ajax TreeView useful for both "link trees" and
        "huge nodes collection trees".
    </p>
    <h2>Flexibility</h2>
    <p>
        In addition you can add up any control you wish inside of your TreeViewItems, including CheckBoxes, RadioButtons,
        Labels, LinkButtons and "pure" HTML like links and such. And you can trap event handlers for those controls - 
        both "normal" ASP.NET Controls and Ra-Ajax controls and everything will interact 100% perfect with the rest
        of your page.
    </p>
</asp:Content>
