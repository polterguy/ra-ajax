<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="RSS-Sample.aspx.cs" 
    Inherits="Samples.RSSSample" 
    Title="Ra-Ajax RSS Sample" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra-Ajax RSS Sample</h1>
    <p>
        How to create an RSS Reader
    </p>
    <div style="width:650px;">
        <ra:WebPart 
            runat="server" 
            Caption="RSS Application"
            style="width:450px;"
            ID="webpart">
            <div>
                <asp:Repeater runat="server" ID="rep">
                    <ItemTemplate>
                        <div>
                            <ra:LinkButton 
                                runat="server" 
                                CssClass="rssLink"
                                OnClick="ShowItems"
                                Text='<%#Eval("Header") %>'
                                Xtra='<%#Eval("Id") %>' />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ra:WebPart>
    </div>
    <ra:Window 
        runat="server" 
        Visible="false"
        style="z-index:100;position:absolute;top:25px;left:25px;width:700px;"
        ID="wndBlog">
        <div style="height:500px;overflow:auto;padding:15px;">
            <ra:Label 
                runat="server" 
                ID="header" 
                Tag="h2" />
            <ra:Label 
                runat="server" 
                ID="content" 
                Tag="p" />
        </div>
        <ra:BehaviorObscurable 
            runat="server"
            ID="obscurer" />
    </ra:Window>
    <ra:Panel 
        runat="server" 
        ID="infoPanel" 
        style="margin-top:15px;padding:15px;border:dashed 1px #aaa;opacity:0.3;cursor:pointer;">
        <p>
            Features shown in this sample;
        </p>
        <ul class="list">
            <li>The Ra-Ajax WebParts control</li>
            <li>Fetching and reading RSS</li>
            <li>Using Ra-Ajax controls inside DataBinded controls</li>
            <li>Using modal Ajax Windows</li>
        </ul>
        <ra:BehaviorUnveiler 
            runat="server" 
            ID="unveilInfo" />
    </ra:Panel>
</asp:Content>
