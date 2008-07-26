<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Wiki.aspx.cs" 
    Inherits="Wiki" 
    ValidateRequest="false"
    Title="Untitled Page" %>

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

    <ext:TabControl runat="server" ID="tab" CssClass="tab-control">
        <ext:TabView runat="server" ID="tab1" Caption="Read" CssClass="content">
            <h1 runat="server" id="header_read"></h1>
        </ext:TabView>
        <ext:TabView runat="server" ID="tab2" Caption="Edit" CssClass="content">
            <h1 runat="server" id="header_edit">
                Create article; 
                <ext:InPlaceEdit 
                    runat="server" 
                    ID="headerInPlace" />
            </h1>
            <ext:RichEdit 
                runat="server" 
                OnKeyUp="richedit_KeyUp"
                style="border:solid 1px #999;padding:5px;"
                ID="richedit" />
        </ext:TabView>
    </ext:TabControl>
    <ra:TextArea 
        runat="server" 
        ID="dummy" 
        Columns="80" 
        Rows="15" />

</asp:Content>

