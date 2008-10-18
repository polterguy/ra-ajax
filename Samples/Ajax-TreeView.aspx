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

    <h1>Ra-Ajax Samples - TreeView</h1>
    <p>
        This is our TreeView sample. The Ra-Ajax TreeView control is very flexible in addition to being a good friend
        with your browser. You can choose to render items "statically" in markup or "dynamically" through event handlers.
        And you can mix these two methods in the same TreeView control just as you wish.
    </p>
    <div style="overflow:auto;">
        <ext:Tree 
            runat="server" 
            ID="tree" 
            CssClass="tree" 
            Expansion="SingleClickPlusSign"
            OnSelectedNodeChanged="selected"
            style="width:250px;float:left;">
            
            <ra:CheckBox 
                runat="server" 
                ID="allowMultiSelectionCheckBox" 
                Text="Allow Multiple Selection" 
                style="margin-bottom:5px;"
                OnCheckedChanged="allowMultiSelectionCheckBox_CheckedChanged" />
                
            <ext:TreeNodes runat="server" Expanded="true">
                <ext:TreeNode runat="server" ID="good">
                    <span title="This is the stuff we all LOVE! :)">
                        Open Web (expand me first)
                    </span>
                    <ext:TreeNodes runat="server">
                        <ext:TreeNode runat="server" ID="ajax">
                            Ajax
                            <ext:TreeNodes runat="server">
                                <ext:TreeNode runat="server" ID="jQuery">
                                    <a href="http://jquery.com">jQuery</a>
                                </ext:TreeNode>
                                <ext:TreeNode runat="server" ID="prototype">
                                    <a href="http://prototypejs.org/">Prototype.js</a>
                                </ext:TreeNode>
                                <ext:TreeNode runat="server" ID="mooTools">
                                    <a href="http://mootools.net/">mootools</a>
                                </ext:TreeNode>
                            </ext:TreeNodes>
                        </ext:TreeNode>
                        <ext:TreeNode runat="server" ID="html">
                            HTML
                            <ext:TreeNodes runat="server" OnGetChildNodes="good_2_GetChildNodes" />
                        </ext:TreeNode>
                        <ext:TreeNode runat="server" ID="css">
                            CSS
                            <ext:TreeNodes runat="server">
                                <ext:TreeNode runat="server" ID="why_cool">
                                    <ra:LinkButton runat="server" ID="lnkCool1" Text="Click me!" OnClick="lnkCool1_Click" />
                                </ext:TreeNode>
                                <ext:TreeNode runat="server" ID="why_cool2">
                                    <ra:LinkButton runat="server" ID="lnkCool2" Text="Me TOO!" OnClick="lnkCool2_Click" />
                                </ext:TreeNode>
                            </ext:TreeNodes>
                        </ext:TreeNode>
                    </ext:TreeNodes>
                </ext:TreeNode>
                <ext:TreeNode runat="server" ID="bad">
                    <span title="This is the stuff we really dislike... :(">
                        Proprietary lock-in crap
                    </span>
                    <ext:TreeNodes runat="server">
                        <ext:TreeNode runat="server" ID="flex">
                            Adobe Flex
                        </ext:TreeNode>
                        <ext:TreeNode runat="server" ID="silverlight">
                            Silverlight
                        </ext:TreeNode>
                        <ext:TreeNode runat="server" ID="activex">
                            ActiveX
                            <ext:TreeNodes runat="server">
                                <ext:TreeNode runat="server" ID="activex1">
                                    ActiveX 1.0
                                </ext:TreeNode>
                                <ext:TreeNode runat="server" ID="activex2">
                                    ActiveX 2.0
                                </ext:TreeNode>
                            </ext:TreeNodes>
                        </ext:TreeNode>
                    </ext:TreeNodes>
                </ext:TreeNode>
                <ext:TreeNode runat="server" ID="huge_collection">
                    <span title="This node will take some time to expand, be patient... Also be CAREFUL e.g. Internet Explorer might actually CRASH when expanding this TreeViewNode">
                        HUGE collection of TreeNodes
                    </span>
                    <ext:TreeNodes ID="huge_collection_node" runat="server" OnGetChildNodes="get_huge" />
                </ext:TreeNode>
            </ext:TreeNodes>
        </ext:Tree>
        <ra:Panel 
            runat="server" 
            ID="pnl" 
            style="margin:15px;padding:10px;border:solid 1px #999;background-color:#fafafa;float:left;width:150px;text-align:left;">
            <p>
                <ra:Label 
                    runat="server" 
                    ID="pnlOutput1" 
                    Text="Try to select and expand TreeNodes" />
            </p>
            <p>
                <ra:Label 
                    runat="server" 
                    ID="pnlOutput2" />
            </p>
        </ra:Panel>
    </div>
    <p>
        The above TreeView has three root TreeNodes. None of these root items are expanded. Click to expand any
        TreeViewNode. In the two first ones there are three child TreeNodes where one of those have dynamically 
        rendered child items. If you look at the code you will see that the HTML child items of the first root 
        item have an event handler for retrieving child items but no directly "statically" rendered items within it. 
        The child TreeNodes of the "HTML" element will not be populated to the client (browser) before you actually 
        expand it. While all the other items will be rendered directly into the markup as HTML but hidden through 
        CSS if they are not expanded.
    </p>
    <p>
        You can set the rendering of "Child TreeView Items" on a per TreeViewItem basis. This means that some nodes can 
        be statically created and thereby be visible for search-engine spiders and so on, while other TreeViewItems
        in the same TreeView can have "dynamically created" items which only will use bandwidth if actually expanded.
    </p>
    <p>
        This can be seen in the "HUGE collection" TreeViewNode. None of its children are rendered in the markup, but
        when expanded you will actually get <strong>*500* new items</strong>. Still the initial rendering of the page
        is very small and those 500 items will not pollute the initial rendering of the page at all. Though when that
        node has been rendered the DOM will be so stuffed with HTML elements that the entire page will feel like going
        into "syrup". And for older browsers (IE) the browser might even CRASH when expanding this node. Though we still
        felt like having the "500 nodes" node there to show of the capabilities of the TreeView Control in Ra-Ajax.
    </p>
    <h2>Advantages of the Ra-Ajax TreeView</h2>
    <p>
        This means that all the statically rendered items will in fact be "pure HTML" which means they will be very
        search engine friendly and also easy to interact with through screen-readers and such. While the dynamically 
        created items (child items of HTML node) will not use any bandwidth before you physically expand the HTML 
        TreeNode. This means that it is possible to have extremely large TreeView nodes while at the same time
        have some of the nodes be visible as pure markup. This means that you can render x nodes (where x is a small number)
        which are initially being rendered into the page HTML which will be 100% "pure" HTML and visible for search
        engines while at the same time have extremely large TreeNode children of those which are rendered on a
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
        In addition you can add up any control you wish inside of your TreeNodes, including CheckBoxes, RadioButtons,
        Labels, LinkButtons and "pure" HTML like links and such. And you can trap event handlers for those controls - 
        both "normal" ASP.NET Controls and Ra-Ajax controls and everything will interact 100% perfect with the rest
        of your page.
    </p>
    <a href="Ajax-Menu.aspx">On to Ajax Menu</a>
</asp:Content>
