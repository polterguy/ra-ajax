<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Post.aspx.cs" 
    Inherits="RaWebsite.Forums_Post" 
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
        Login or create user at 
        <br />
        <a href="Forums.aspx">Main forum page</a> to post or reply.
    </div>
    <div class="forumPost">
        <h2 style="float:left;margin-top:0;" runat="server" id="headerParent"></h2>
        <i style="float:right;" runat="server" id="dateParent"></i>
        <br style="clear: both;" />
        <p runat="server" id="contentParent"></p>
        <em runat="server" id="operatorInfo"></em>
    </div>
    
    <ra:Panel runat="server" ID="postsWrapper">
        <asp:Repeater runat="server" ID="repReplies">
            <HeaderTemplate>
            </HeaderTemplate>
            <FooterTemplate>
            </FooterTemplate>
            <ItemTemplate>
                <div class="forumPost">
                    <h2 style="float:left;margin-top:0;"><%# Eval("Header") %></h2>
                    <i style="float:right;"><%# string.Format("{0} {1}", Eval("Operator.Signature") == string.Empty? string.Empty : Eval("Operator.Signature") + " -", ((DateTime)Eval("Created")).ToString("d.MMM yy HH:mm", System.Globalization.CultureInfo.InvariantCulture)) %></i>
                    <br style="clear: both;" />
                    <p><%# Eval("Body") %></p>
                    <i>Posted by <%# Eval("Operator.Username") %> who has <%# Eval("Operator.NumberOfPosts")%> posts</i>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </ra:Panel>

    <ra:Panel 
        runat="server" 
        style="background-color:#f9f9f9;padding:15px;"
        ID="pnlReply">
        <h2 style="margin-top:50px;border-bottom:solid 1px black;">Reply</h2>
        <table>
            <tr>
                <td>Subject:</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="header" 
                        style="width:419px" />
                </td>
            </tr>
            <tr>
                <td>Body:</td>
                <td>
                    <ra:TextArea 
                        runat="server" 
                        ID="body" 
                        Rows="5"
                        style="width:419px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:right;">
                    <ra:Button 
                        runat="server" 
                        ID="newSubmit" 
                        OnClick="newSubmit_Click"
                        Text="Post Reply" />
                </td>
            </tr>
        </table>
    </ra:Panel>
    <div style="height:500px;">
        &nbsp;
    </div>

</asp:Content>

