<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Forums.aspx.cs" 
    Inherits="RaWebsite.Forums_Forums" 
    Title="Ra-Ajax Forum posts" %>

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

    <h1>Ra-Ajax Forums</h1>
    <p>
        Feel free to post your questions here and hopefully someone will try to answer as best they can, though 
        remember that Ra-Ajax is a NON-commercial project which means that none of the people answering you 
        won't get paid for helping you. If you are a senior ASP.NET/Ra-Ajax developer yourself and like to 
        help out the project then answering forum questions is the place to start. Politeness helps :)
    </p>
    <ext:Window 
	    runat="server"
	    ID="pnlNewPost"
	    Caption="Add New Post"
	    CssClass="window"
	    Visible="false"
	    style="position:absolute;top:450px;left:324px;padding:15px;width:550px;z-index:1001;">
        <ra:BehaviorObscurable runat="server" id="obscurer" />
	    <div style="padding:10px;text-align:center;">
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
                            Rows="10"
                            style="width:419px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:right;">
                        <ra:Button 
                            runat="server" 
                            ID="newSubmit" 
                            OnClick="newSubmit_Click"
                            Text="Add" />
                        <ra:Button 
                            runat="server" 
                            ID="newPostCancel" 
                            OnClick="newPostCancel_Click"
                            Text="Discard" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <ra:Label 
                            runat="server" 
                            style="color:Red;"
                            ID="lblErrorPost" />
                    </td>
                </tr>
            </table>
        </div>
    </ext:Window>

        
    <ra:Panel
        runat="server" 
        ID="newPostPanel"
        Visible="false"
        style="float:left;">
        <ra:LinkButton 
            runat="server" 
            ID="addNewPostButton"
            Text="Add New Post"
            OnClick="addNewPostButton_Click" />
        |
    </ra:Panel>    
    &nbsp;Filter: 
    <ra:TextBox 
        runat="server" 
        OnKeyUp="search_KeyUp"
        ID="search" />  
    <ra:Panel runat="server" ID="postsWrapper" style="margin-bottom: 10px; margin-top: 10px;">
        <asp:Repeater runat="server" ID="forumPostsRepeater">
            <HeaderTemplate>
                <table style="border: solid 1px #aaa;width:628px;" cellpadding="2" cellspacing="3">
                    <tr style="background-color:#FF9B00;color:#222;font-weight:normal;text-align:center;">
                        <th>Topic</th>
                        <th style="width:80px;">Posted By</th>
                        <th style="width:140px;">Date</th>
                        <th>Replies</th>
                    </tr>
            </HeaderTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <a runat="server" href='<%# "~/Forums/" + Eval("Url") %>'>
                            <%# Eval("Header") %>
                        </a>
                    </td>
                    <td style="text-align:center;">
                        <%# Eval("Operator.Username") %>
                    </td>
                    <td style="text-align:center;">
                        <%# ((DateTime)Eval("Created")).ToString("dd.MMM yy - HH:mm", System.Globalization.CultureInfo.InvariantCulture)%>
                    </td>
                    <td style="text-align:center;">
                        <%# Eval("NoReplies") %>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="background-color:#ccc;">
                    <td>
                        <a id="A1" runat="server" href='<%# "~/Forums/" + Eval("Url") %>'>
                            <%# Eval("Header") %>
                        </a>
                    </td>
                    <td style="text-align:center;">
                        <%# Eval("Operator.Username") %>
                    </td>
                    <td style="text-align:center;">
                        <%# ((DateTime)Eval("Created")).ToString("dd.MMM yy - HH:mm")%>
                    </td>
                    <td style="text-align:center;">
                        <%# Eval("NoReplies") %>
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>
    </ra:Panel>
    
    <p style="text-align:right;">
        <ra:LinkButton 
            runat="server" 
            ID="previous" 
            OnClick="previous_Click"
            Text="&laquo; Previous" />
        <ra:LinkButton 
            runat="server" 
            ID="next" 
            OnClick="next_Click"
            Text="Next &raquo;" />
    </p>
    
    <ra:Label 
        runat="server"
        ID="informationLabel"
        Text="{0} registered users have posted {1} posts. There are currently {2} user(s) browsing these forums." />
        
    <div style="height:500px;">
        &nbsp;
    </div>

</asp:Content>

