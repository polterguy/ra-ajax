<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="Samples._Default" 
    Title="Ra-Ajax Samples" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra-Ajax Samples</h1>

    <ra:Panel runat="server" style="opacity:0;" ID="thumbsWrapper">
        
        <ra:Panel runat="server" ID="thumbs1" CssClass="thumbs">
            <a href="Viewport-Calendar-Starter-Kit.aspx" class="links">
                <span class="image1">&nbsp;</span>
                <span class="header">Calendar Starter-Kit</span>
                <span class="text">Starter-kit for your Calendar Application. Features Calendar, DataGrids, Modal Windows and more. Written in C#.</span>
            </a>
            <ra:BehaviorUnveiler runat="server" id="unveiler1" />
        </ra:Panel>

        <ra:Panel runat="server" ID="thumbs2" style="opacity:0.3;" CssClass="thumbs">
            <a href="Viewport-Sample.aspx" class="links">
                <span class="image2">&nbsp;</span>
                <span class="header">Viewport Starter-Kit</span>
                <span class="text">Starter-kit for Ajax Login. Features TreeView, TabControl, Modal Windows and more. Written in C#.</span>
            </a>
            <ra:BehaviorUnveiler runat="server" id="unveiler2" />
        </ra:Panel>

        <ra:Panel runat="server" ID="thumbs3" style="opacity:0.3;" CssClass="thumbs">
            <a href="Viewport-GridView-Sample.aspx" class="links">
                <span class="image3">&nbsp;</span>
                <span class="header">DataGrid Starter-Kit</span>
                <span class="text">Starter-kit for Ajax DataGrid. Features GridView, Modal Windows, keyboard shortcuts and more. Written in VB.NET.</span>
            </a>
            <ra:BehaviorUnveiler runat="server" id="unveiler3" />
        </ra:Panel>

        <ra:Panel runat="server" ID="thumbs4" style="opacity:0.3;" CssClass="thumbs">
            <a href="Viewport-RSS-Starter-Kit.aspx" class="links">
                <span class="image4">&nbsp;</span>
                <span class="header">RSS Starter-Kit</span>
                <span class="text">Nice starting point for creating an Ajax RSS portal. Written in C#.</span>
            </a>
            <ra:BehaviorUnveiler runat="server" id="unveiler4" />
        </ra:Panel>

        <ra:Panel runat="server" ID="thumbs5" style="opacity:0.3;" CssClass="thumbs">
            <a href="Ajax-Forum-Starter-Kit.aspx" class="links">
                <span class="image5">&nbsp;</span>
                <span class="header">Forum Starter-Kit</span>
                <span class="text">For those who wants to create an Ajax Forum. Written in C#.</span>
            </a>
            <ra:BehaviorUnveiler runat="server" id="unveiler5" />
        </ra:Panel>

    </ra:Panel>
    <p>
        Ra-Ajax is an <em>Open Source - LGPL licensed Ajax Library for ASP.NET</em> and Mono. This is our samples
        which you will also get when you <a href="http://code.google.com/p/ra-ajax/">download</a> Ra-Ajax.
    </p>
</asp:Content>

