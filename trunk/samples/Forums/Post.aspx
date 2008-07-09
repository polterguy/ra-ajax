<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Post.aspx.cs" 
    Inherits="Forums_Post" 
    Title="Untitled Page" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <div 
        runat="server" 
        id="pnlLinkToLogin" 
        style="position:absolute;top:290px;right:5px;background-color:Yellow;border:solid 1px #333;padding:15px;">
        Login at <a href="Forums.aspx">Main forum page</a> to post or reply.
    </div>

    <h1 runat="server" id="headerParent"></h1>
    <i runat="server" id="dateParent"></i>
    <p runat="server" id="contentParent"></p>

    <ra:Panel runat="server" ID="postsWrapper">
        <asp:Repeater runat="server" ID="repReplies">
            <HeaderTemplate>
            </HeaderTemplate>
            <FooterTemplate>
            </FooterTemplate>
            <ItemTemplate>
                <table class="forumTable">
                    <tr>
                        <td>
                            <%# Eval("Header") %>
                        </td>
                        <td>
                            <%# Eval("Operator.Username") %>
                        </td>
                        <td>
                            <%# Eval("Created") %>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <%# Eval("Body") %>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </ra:Panel>
    
    <ra:Panel 
        runat="server" 
        style="background-color:#f9f9f9;border:solid 1px #333;padding:15px;width:70%;margin-top:50px;"
        ID="pnlReply">
        <table>
            <tr>
                <td>Header</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="header" 
                        style="width:419px" />
                </td>
            </tr>
            <tr>
                <td>Body</td>
                <td>
                    <ra:TextArea 
                        runat="server" 
                        ID="body" 
                        Columns="50" 
                        Rows="10" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right;">
                    <ra:Button 
                        runat="server" 
                        ID="newSubmit" 
                        OnClick="newSubmit_Click"
                        Text="Save" />
                </td>
            </tr>
        </table>
    </ra:Panel>
    <div style="height:500px;">
        &nbsp;
    </div>

</asp:Content>

