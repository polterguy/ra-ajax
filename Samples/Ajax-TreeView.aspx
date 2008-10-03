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
    <div style="overflow:auto;">
        <ext:TreeView runat="server" ID="tree" CssClass="tree" style="width:250px;float:left;">
            <ext:TreeViewItem runat="server" ID="good" OnSelected="selected">
                Open Web great things
                <ext:TreeViewItem runat="server" ID="ajax" OnSelected="selected">
                    Ajax
                    <ext:TreeViewItem runat="server" ID="jQuery" OnSelected="selected">
                        <a href="http://jquery.com">jQuery</a>
                    </ext:TreeViewItem>
                    <ext:TreeViewItem runat="server" ID="prototype" OnSelected="selected">
                        <a href="http://prototypejs.org/">Prototype.js</a>
                    </ext:TreeViewItem>
                    <ext:TreeViewItem runat="server" ID="mooTools" OnSelected="selected">
                        <a href="http://mootools.net/">mootools</a>
                    </ext:TreeViewItem>
                </ext:TreeViewItem>
                <ext:TreeViewItem runat="server" ID="html" OnGetChildItems="good_2_GetChildItems" OnSelected="selected">
                    HTML
                </ext:TreeViewItem>
                <ext:TreeViewItem runat="server" ID="css" OnSelected="selected">
                    CSS
                    <ext:TreeViewItem runat="server" ID="why_cool" OnSelected="selected">
                        <ra:LinkButton runat="server" ID="lnkCool1" Text="Is cool!" />
                    </ext:TreeViewItem>
                    <ext:TreeViewItem runat="server" ID="why_cool2" OnSelected="selected">
                        <ra:LinkButton runat="server" ID="lnkCool2" Text="Is WAY cool!" />
                    </ext:TreeViewItem>
                </ext:TreeViewItem>
            </ext:TreeViewItem>
            <ext:TreeViewItem runat="server" ID="bad" OnSelected="selected">
                Proprietary lock-in crap
                <ext:TreeViewItem runat="server" ID="flex" OnSelected="selected">
                    Adobe Flex
                </ext:TreeViewItem>
                <ext:TreeViewItem runat="server" ID="silverlight" OnSelected="selected">
                    Silverlight
                </ext:TreeViewItem>
                <ext:TreeViewItem runat="server" ID="activex" OnSelected="selected">
                    ActiveX
                    <ext:TreeViewItem runat="server" ID="activex1" OnSelected="selected">
                        ActiveX 1.0
                    </ext:TreeViewItem>
                    <ext:TreeViewItem runat="server" ID="activex2" OnSelected="selected">
                        ActiveX 2.0
                    </ext:TreeViewItem>
                </ext:TreeViewItem>
            </ext:TreeViewItem>
        </ext:TreeView>
        <ra:Panel 
            runat="server" 
            ID="pnl" 
            style="margin:15px;padding:10px;border:solid 1px #999;background-color:#fafafa;float:left;width:150px;text-align:left;">
            <p>
                <ra:Label 
                    runat="server" 
                    ID="pnlOutput1" 
                    Text="Try to select and expand TreeViewItems" />
            </p>
            <p>
                <ra:Label 
                    runat="server" 
                    ID="pnlOutput2" />
            </p>
        </ra:Panel>
    </div>
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
