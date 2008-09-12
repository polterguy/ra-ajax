<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
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

    <ra:Panel 
        runat="server" 
        ID="imagesPnl" 
        Visible="false"
        style="width:800px;background-color:Yellow;position:absolute;border:solid 1px Black;overflow:auto;padding:15px;">
        <ra:Button 
            runat="server" 
            ID="closeImages" 
            OnClick="closeImages_Click"
            Text="Close" />
        <asp:FileUpload 
            runat="server" 
            ID="uploadImage" />
        <asp:Button 
            runat="server" 
            ID="btnUploadFile" 
            OnClick="btnUploadFile_Click"
            Text="Upload file" />
        <br />
        <div style="overflow:auto;border:solid 1px Black;height:95%;">
            <asp:Repeater runat="server" id="repImages">
                <ItemTemplate>
                    <ra:ImageButton 
                        ImageUrl='<%# Container.DataItem %>' 
                        AlternateText="Image" 
                        OnClick="ImageSelected"
                        runat="server" />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </ra:Panel>

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

        <ext:TabView 
            runat="server" 
            ID="tab2" 
            Caption="Edit" 
            CssClass="content">

            <ext:Timer 
                runat="server" 
                ID="keepSessionTimer" 
                OnTick="keepSessionTimer_Tick" 
                Milliseconds="120000" />

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
                Formatting; 
                <ra:DropDownList 
                    runat="server" 
                    OnSelectedIndexChanged="formattingDDL_SelectedIndexChanged"
                    ID="formattingDDL">
                    <ra:ListItem Text="Choose formatting..." Value="none" />
                    <ra:ListItem Text="Header 2" Value="h2" />
                    <ra:ListItem Text="Header 3" Value="h3" />
                    <ra:ListItem Text="New Paragraph" Value="paragraph" />
                    <ra:ListItem Text="Quote" Value="quote" />
                </ra:DropDownList>
                Templates; 
                <ra:DropDownList 
                    runat="server" 
                    OnSelectedIndexChanged="template_SelectedIndexChanged"
                    ID="template">
                    <ra:ListItem Text="Choose template..." Value="none" />
                    <ra:ListItem Text="Normal" Value="normal" />
                    <ra:ListItem Text="List" Value="list" />
                </ra:DropDownList>
                <ra:Button 
                    runat="server" 
                    ID="btnShowImages" 
                    OnClick="btnShowImages_Click"
                    Text="Images" />
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
            <ra:CheckBox 
                runat="server" 
                ID="siteWide" 
                Text="Site wide link" />

        </ext:TabView>

        <ext:TabView 
            runat="server" 
            ID="whatlinks" 
            Caption="What links here" 
            CssClass="content">

            <h1>What links here </h1>
            <p>
                To link to this page from another page in this wiki, please use this text;
                <ext:InPlaceEdit 
                    runat="server" 
                    style="color:#494;font-weight:bold;" 
                    ID="linkToThis" />
                You can modify the last parts, but please use the wiki link to maintain the integrity
                of the data in the wiki and make it possible to track pages etc instead of "normal links"
                since they will not be counted and used to create relationships between pages.
            </p>
            <p>
                Note also that you can actually link to NON-existant articles and a "create article" link will
                be automatically created for you.
            </p>
            <asp:Repeater runat="server" ID="repLinks">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <a href='<%# Eval("Url") + ".wiki" %>'>
                                <%# Eval("Header") %>
                            </a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </ext:TabView>

        <ext:TabView 
            runat="server" 
            ID="revisions" 
            Caption="Revisions" 
            CssClass="content">

            <h1>Revisions of article</h1>
            <ra:Panel runat="server" ID="repRevisionsWrapper">
                <asp:Repeater runat="server" ID="repRevisions">
                    <HeaderTemplate>
                        <table>
                    </HeaderTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <ra:HiddenField 
                                    runat="server" 
                                    Value='<%# Eval("Id") %>' />
                                <%# Eval("Operator.Username") %>
                            </td>
                            <td>
                                <ra:LinkButton 
                                    runat="server" 
                                    OnClick="RevisionClicked"
                                    Text='<%# ((DateTime)Eval("Created")).ToString("dddd, dd MMM yyyy - HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) %>' />
                            </td>
                            <td>
                                <ra:Button 
                                    runat="server" 
                                    OnClick="RevertToRevision"
                                    Visible='<%# Engine.Entities.Operator.Current != null %>'
                                    Text="Revert back" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </ra:Panel>
            <ra:Panel runat="server" ID="revView">
                <asp:Literal runat="server" ID="litRevisions" />
            </ra:Panel>

        </ext:TabView>

    </ext:TabControl>
    <ra:Button 
        runat="server" 
        ID="delete" 
        Text="Delete article" 
        style="margin-top:25px;"
        OnClick="delete_Click" />

</asp:Content>
