<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Blog.aspx.cs" 
    Inherits="RaWebsite.Blog" 
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
    <div class="blog">

        <h1>Software Cravings at the wall of Ajax</h1>
        <p>
            A blog about Open Source, Open Standards, Open Web, RIA, Ajax, Philosophy, 42, 
            <a href="http://www.mono-project.com/Main_Page">Mono</a> and other things of 
            significantly larger importance than anything else in this world...
        </p>
        <p>
            <em>All code at this blog is licensed under the MIT license.</em>
        </p>

        <ra:Panel runat="server" ID="blogWrapper">
            <asp:Repeater runat="server" ID="repBlogs">
                <ItemTemplate>
                    <ra:HiddenField ID="HiddenField1" 
                        runat="server" 
                        Value='<%# Eval("Id") %>' />
                    <h2>
                        <a href='<%# Eval("Url") %>'><%# Eval("Header") %></a>
                    </h2>
                    <ra:Button 
                        runat="server" 
                        Visible='<%# Entity.Operator.Current != null && Entity.Operator.Current.Id == ((Entity.Operator)Eval("Operator")).Id %>'
                        OnClick="EditBlog"
                        Text="Edit" />
                    <ra:Button 
                        runat="server" 
                        Visible='<%# Entity.Operator.Current != null && Entity.Operator.Current.Id == ((Entity.Operator)Eval("Operator")).Id %>'
                        OnClick="DeleteBlog"
                        Text="Delete" />
                    <i class="date"><%# ((DateTime)Eval("Created")).ToString("MMMM dd, yyyy", System.Globalization.CultureInfo.InvariantCulture) %></i>
                    <br />
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </ra:Panel>
        <ra:LinkButton ID="newerPosts" runat="server" Text="&laquo; newer posts" OnClick="newerPosts_Click" Visible="false" />
        <ra:LinkButton ID="olderPosts" runat="server" Text="older posts &raquo;" OnClick="olderPosts_Click" />
        <ra:Button 
            runat="server" 
            ID="btnCreate" 
            OnClick="btnCreate_Click"
            Text="Create new blog" 
            Visible="false" />
        <ext:Window 
	        runat="server"
	        ID="pnlNewBlog"
	        Caption="Add New Blog Post"
	        CssClass="alphacube"
	        Visible="false"
	        style="position:absolute;top:290px;left:250px;"> 
	        <div style="padding:15px;">
                <ra:HiddenField 
                    runat="server" 
                    Value=""
                    ID="hidBlogId" />
                <ra:TextBox 
                    runat="server" 
                    style="width:450px;"
                    ID="txtHeader" />
                <br />
                <ra:TextArea 
                    runat="server" 
                    ID="txtBody" 
                    Columns="60" 
                    Rows="25" />
                 <br />
                 <ra:Button 
                    runat="server" 
                    ID="btnSave" 
                    Text="Save blog" 
                    OnClick="btnSave_Click" />
                 <ra:Button 
                    runat="server" 
                    ID="btnCancelSave" 
                    Text="Cancel" 
                    OnClick="btnCancelSave_Click" />
                 <ra:Button 
                    runat="server" 
                    ID="btnImages" 
                    Text="Images" 
                    OnClick="btnImages_Click" />
            </div>
        </ext:Window>
        <ext:Window 
	        runat="server"
	        ID="pnlImages"
	        Caption="Add New Image"
	        CssClass="alphacube"
	        Visible="false"
	        style="position:absolute;top:290px;left:250px;width:600px;padding:5px;"> 
	        <div style="padding:15px;overflow:auto;height:500px;">
            <asp:FileUpload 
                runat="server" 
                ID="uploadImage" />
            <asp:Button 
                runat="server" 
                ID="btnUploadFile" 
                OnClick="btnUploadFile_Click"
                Text="Upload File" />
            <br />
            <asp:Repeater runat="server" ID="repImages">
                <ItemTemplate>
                    <div style="margin:5px;padding:5px;border:solid 1px #333;overflow:hidden;">
                        <img src='<%# Container.DataItem %>' alt='Blog image' />
                        <br /><br />
                        <asp:TextBox 
                            runat="server" 
                            ID="imgTextBox"
                            style="width:100%;font-size:small;"
                            Text='<%# "&lt;img src=\"" + Container.DataItem + "\" alt=\"Blog image\" />"%>' />
                        <br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            </div>
        </ext:Window>
        <div style="height:500px;">&nbsp;</div>
    </div>
</asp:Content>

