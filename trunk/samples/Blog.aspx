<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Blog.aspx.cs" 
    Inherits="Blog" 
    ValidateRequest="false"
    Title="Untitled Page" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">
    <div class="blog">

        <h1>Software Cravings</h1>
        <p>
            A blog about Open Source, Open Standards, Open Web, RIA, Ajax, Philosophy, 42, 
            and other things of significantly larger importance than anything else in this world...
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
        <ra:Button 
            runat="server" 
            ID="btnCreate" 
            OnClick="btnCreate_Click"
            Text="Create new blog" 
            Visible="false" />
        <ra:Panel 
            runat="server" 
            ID="pnlNewBlog"
            Visible="false"
            style="position:absolute;top:290px;left:250px;background-color:Yellow;border:solid 1px #333;padding:15px;width:500px;height:500px;">
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
        </ra:Panel>
        <ra:Panel 
            runat="server" 
            ID="pnlImages"
            Visible="false"
            style="position:absolute;top:315px;left:275px;background-color:Yellow;border:solid 1px #333;padding:15px;width:450px;height:450px;overflow:auto;">
            <ra:Button 
                runat="server" 
                ID="btnImagesClose" 
                OnClick="btnImagesClose_Click"
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
            <asp:Repeater runat="server" ID="repImages">
                <ItemTemplate>
                    <img src='<%# Container.DataItem %>' alt='Blog image' />
                    <br />
                    <asp:TextBox 
                        runat="server" 
                        style="width:440px;font-size:small;"
                        Text='<%# "&lt;img src=\"" + Container.DataItem + "\" alt=\"Blog image\" />"%>' />
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </ra:Panel>
        <div style="height:500px;">&nbsp;</div>
    </div>
</asp:Content>

