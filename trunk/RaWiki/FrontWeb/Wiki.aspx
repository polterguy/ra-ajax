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

    <ext:TabControl 
        runat="server" 
        ID="tab" 
        OnActiveTabViewChanged="tab_ActiveTabViewChanged"
        CssClass="tab-control">

        <ext:TabView 
            runat="server" 
            ID="tab1" 
            Caption="Read" 
            CssClass="content">

            <h1 runat="server" id="header_read"></h1>
            <asp:Literal runat="server" ID="content" />
            <ra:Label 
                runat="server" 
                ID="warning" 
                style="color:Red;margin-top:25px;display:block;"
                Visible="false" />

        </ext:TabView>

        <ext:TabView runat="server" ID="tab2" Caption="Edit" CssClass="content">

            <h1 runat="server" id="header_edit">
                Create article; 
                <ext:InPlaceEdit 
                    runat="server" 
                    ID="headerInPlace" />
            </h1>
            <div>
                <ra:ImageButton 
                    runat="server" 
                    ID="bold" 
                    ImageUrl="media/x.png" 
                    CssClass="editor-bold editor-btn" 
                    AlternateText="Make bold"
                    OnClick="bold_Click" />
                <ra:ImageButton 
                    runat="server" 
                    ID="italic" 
                    ImageUrl="media/x.png" 
                    CssClass="editor-italic editor-btn" 
                    AlternateText="Make italic"
                    OnClick="italic_Click" />
                <ra:ImageButton 
                    runat="server" 
                    ID="underscore" 
                    ImageUrl="media/x.png" 
                    CssClass="editor-underscore editor-btn" 
                    AlternateText="Make underscore"
                    OnClick="underscore_Click" />
                <ra:ImageButton 
                    runat="server" 
                    ID="strike" 
                    ImageUrl="media/x.png" 
                    CssClass="editor-strike editor-btn" 
                    AlternateText="Make strikethrough"
                    OnClick="strike_Click" />
                <ra:ImageButton 
                    runat="server" 
                    ID="bullets" 
                    ImageUrl="media/x.png" 
                    CssClass="editor-bullets editor-btn" 
                    AlternateText="Make bulleted list"
                    OnClick="bullets_Click" />
                <ra:ImageButton 
                    runat="server" 
                    ID="numbers" 
                    ImageUrl="media/x.png" 
                    CssClass="editor-numbers editor-btn" 
                    AlternateText="Make numbered list"
                    OnClick="numbers_Click" />
                <ra:ImageButton 
                    runat="server" 
                    ID="link" 
                    ImageUrl="media/x.png" 
                    CssClass="editor-link editor-btn" 
                    AlternateText="Make hyperlink"
                    OnClick="link_Click" />
                Templates; 
                <ra:DropDownList 
                    runat="server" 
                    OnSelectedIndexChanged="template_SelectedIndexChanged"
                    ID="template">
                    <ra:ListItem Text="Choose template..." Value="none" />
                    <ra:ListItem Text="Normal" Value="normal" />
                    <ra:ListItem Text="List" Value="list" />
                </ra:DropDownList>
            </div>
            <ra:Panel 
                runat="server" 
                ID="pnlLnk" 
                style="background-color:Yellow;padding:5px;"
                Visible="false">
                URL: <ra:TextBox 
                    runat="server" 
                    ID="txtLnk" />
                Title: <ra:TextBox 
                    runat="server" 
                    Text="title of link"
                    ID="title" />
                <ra:Button 
                    runat="server" 
                    ID="createLnk" 
                    OnClick="createLnk_Click"
                    Text="Create link" />
                <ra:Button 
                    runat="server" 
                    ID="cancelLnk" 
                    OnClick="cancelLnk_Click"
                    Text="Cancel" />
            </ra:Panel>
            <ext:RichEdit 
                runat="server" 
                style="border:solid 1px #999;padding:5px;"
                ID="richedit" />
            <ra:Button 
                runat="server" 
                ID="save" 
                OnClick="save_Click"
                Text="Save" />

        </ext:TabView>
    </ext:TabControl>

</asp:Content>

