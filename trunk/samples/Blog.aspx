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

    <h1 runat="server" id="header">Ravings from </h1>

    <ra:Panel runat="server" ID="blogWrapper">
        <asp:Repeater runat="server" ID="repBlogs">
            <ItemTemplate>
                <h3>
                    <a href='<%# Eval("Url") %>'><%# Eval("Header") %></a>
                </h3>
                <i><%# ((DateTime)Eval("Created")).ToString("dd.MMM yy") %></i>
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
    </ra:Panel>
    <div style="height:500px;">&nbsp;</div>

</asp:Content>

