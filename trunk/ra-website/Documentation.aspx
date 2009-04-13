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
    <ra:Label 
        runat="server" 
        CssClass="inherits"
        Visible="false"
        ID="inherit" />
    <ra:Label 
        runat="server" 
        Text="Reference documentation for Ra-Ajax. Click a class in the class browser above and see its documentation"
        CssClass="description"
        ID="description" />
    <ra:Panel runat="server" id="repWrapper">
        <asp:Repeater runat="server" ID="repProperties">
            <HeaderTemplate>
                <ul class="docsList">
            </HeaderTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
            <ItemTemplate>
                <li>
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
        style="width:480px;position:absolute;top:5px;right:5px;z-index:50;"
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

