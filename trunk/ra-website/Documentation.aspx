<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Documentation.aspx.cs" 
    Inherits="RaWebsite.Documentation" 
    Title="Ra-Ajax Documentation" %>

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

    <ra:Label 
        runat="server"
        Tag="h1" 
        ID="header" />
    <ra:Panel 
        runat="server" 
        ID="pnlInfo">
        <p class="docsInfo">
            This is the reference documentation for Ra-Ajax. Click any of the classes in the TreeView above
            to see the documentation for that class.
        </p>
    </ra:Panel>
    <ra:Panel 
        runat="server" 
        Visible="false"
        style="margin:0;overflow:hidden;display:none;"
        ID="pnlInherits">
        <h2 style="margin:5px;">Inherits</h2>
        <ra:LinkButton 
            runat="server" 
            OnClick="ViewInherited"
            CssClass="inherits"
            ID="inherit" />
    </ra:Panel>
    <ra:Panel 
        runat="server" 
        Visible="false"
        style="margin:0;overflow:hidden;display:none;"
        ID="pnlDescription">
        <h2 style="margin:5px;">Description</h2>
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
    </ra:Panel>
    <ext:Window 
        runat="server" 
        style="width:480px;position:absolute;top:5px;right:15px;z-index:50;"
        Caption="Class browser"
        ID="wnd">
        <div style="height:120px;overflow:auto;">
            <ext:Tree 
                runat="server" 
                OnSelectedNodeChanged="tree_SelectedNodeChanged"
                ID="tree">
                <ext:TreeNodes 
                    runat="server" 
                    ID="root" />
            </ext:Tree>
        </div>
    </ext:Window>
</asp:Content>

