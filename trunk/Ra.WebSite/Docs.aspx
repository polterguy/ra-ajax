<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Docs.aspx.cs" 
    Inherits="RaWebsite.Docs" 
    Title="Ra-Ajax Documentation" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">
    <ra:Window 
        runat="server" 
        OnCreateTitleBarControls="wnd_CreateTitleBarControls" 
        Closable="false"
        Movable="false"
        Caption="Class browser"
        ID="wnd"
        style="margin:0 15px 15px 0">
        <div style="height:120px;overflow:auto;">
            <ra:Tree 
                runat="server" 
                OnSelectedNodeChanged="tree_SelectedNodeChanged"
                ID="tree">
                <ra:TreeNodes runat="server" ID="topNodes">
                    <ra:TreeNode 
                        runat="server" 
                        ID="rootNode" 
                        Text="Classes Reference Documentation">
                        <ra:TreeNodes 
                            runat="server" 
                            ID="root" />
                    </ra:TreeNode>
                    <ra:TreeNode 
                        runat="server" 
                        ID="tutorials" 
                        Text="Tutorials">
                        <ra:TreeNodes 
                            runat="server" 
                            ID="rootTutorials" />
                    </ra:TreeNode>
                </ra:TreeNodes>
            </ra:Tree>
        </div>
    </ra:Window>
    
    <ra:Label 
        runat="server"
        Tag="h1" 
        ID="header" />
    
    <ra:Panel 
        runat="server" 
        ID="pnlInfo">
        <div class="docsInfo">
            <p>
                This is the reference documentation for Ra-Ajax. Click any of the classes in the TreeView above
                to see the documentation for that class. If there is anything wrong with the documentation in
                any ways, then please let us know by sending either <a href="mailto:thomas@ra-ajax.org">Thomas</a>
                or <a href="mailto:kariem@ra-ajax.org">Kariem</a> an email.
            </p>
            <p>
                If you don't find the information you need here, you might try <a href="http://stacked.ra-ajax.org">our forums</a>
                or if you need even more support and/or help we are available to hire for consultancy work with Ra-Ajax.
                If you want to hire us then please send <a href="mailto:thomas@ra-ajax.org">Thomas Hansen</a> an email
                shortly describing what you want us to do for you.
            </p>
            <h2>Need even more help...?</h2>
            <p>
                We have <a href="http://stacked.ra-ajax.org">publicly available forums</a> where
                you can post your questions. And if that's not even enough take a look at our
                <a href="SoftwareFactory.aspx">consulting offerings</a> which btw is the only
                way to get hold of a proprietary license of Ra-Ajax.
            </p>
        </div>
    </ra:Panel>
    <ra:Panel 
        runat="server" 
        Visible="false"
        style="margin:0;overflow:hidden;display:none;opacity:0.3;"
        ID="pnlInherits">
        <h2 style="margin:5px;">Inherits</h2>
        <ra:LinkButton 
            runat="server" 
            OnClick="ViewInherited"
            CssClass="inherits"
            ID="inherit" />
        <ra:BehaviorUnveiler 
            runat="server" 
            id="unveiler" />
    </ra:Panel>
    <ra:Panel 
        runat="server" 
        Visible="false"
        style="margin:0;overflow:hidden;display:none;"
        ID="pnlDescription">
        <ra:Label 
            runat="server" 
            ID="descTag" 
            Tag="h2"
            Text="Description" 
            style="margin:5px;" />
        <ra:Label 
            runat="server" 
            CssClass="description"
            ID="description" />
    </ra:Panel>
    <ra:Panel 
        runat="server" 
        style="margin:0;overflow:hidden;"
        Visible="false"
        id="repWrapper">
        <h2 style="margin:5px;">Functions, Properties and Events</h2>
        <asp:Repeater runat="server" ID="repProperties">
            <HeaderTemplate>
                <ul class="docsList">
            </HeaderTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
            <ItemTemplate>
                <li class='<%#Eval("Kind")%>'>
                    <ra:LinkButton 
                        runat="server" 
                        Xtra='<%#Eval("ID")%>'
                        OnClick="PropertyChosen"
                        Text='<%#Eval("Name")%>' />
                    <ra:Panel 
                        runat="server" 
                        CssClass="property"
                        Visible="false">
                        <ra:Label runat="server" />
                    </ra:Panel>
                </li>
            </ItemTemplate>
        </asp:Repeater>

        <ra:TabControl 
            runat="server" 
            ID="tab">

            <ra:TabView 
                runat="server" 
                ID="sample" 
                style="padding:15px;"
                Caption="Sample">
                <ra:Dynamic 
                    runat="server" 
                    OnReload="sampleDyn_Reload"
                    ID="sampleDyn" />
            </ra:TabView>

            <ra:TabView 
                runat="server" 
                ID="codeTab" 
                style="padding:15px;"
                Caption="Code">
                <ra:Label 
                    runat="server" 
                    Tag="pre"
                    ID="codeLbl" />
            </ra:TabView>

            <ra:TabView 
                runat="server" 
                ID="markupTab" 
                style="padding:15px;"
                Caption=".ASPX">
                <ra:Label 
                    runat="server" 
                    Tag="pre"
                    ID="markupLbl" />
            </ra:TabView>

        </ra:TabControl>

    </ra:Panel>
</asp:Content>

