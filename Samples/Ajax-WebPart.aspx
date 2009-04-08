<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-WebPart.aspx.cs" 
    Inherits="Samples.WebPart" 
    Title="Ra-Ajax WebPart Sample" %>

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
    runat="server">

    <h1>Ra-Ajax Samples - WebPart</h1>
    <div style="width:500px;height:200px;border:dashed 1px #eee;">
        <div style="width:498px;height:198px;">
            <ra:Button 
                runat="server" 
                ID="btn" 
                Text="Show again" 
                Visible="false" 
                OnClick="btn_Click" />
            <ext:WebPart 
                runat="server" 
                Caption="WebPart sample" 
                style="width:250px;"
                OnClosed="webpart_Closed"
                ID="webpart">
                <div>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
                    Vestibulum ultricies, nisl a suscipit blandit, lectus velit 
                    molestie purus, sed scelerisque massa lorem gravida risus. 
                </div>
            </ext:WebPart>
        </div>
    </div>
    <p>
        Ra-Ajax WebPart example. Use when you need a mini-window.
    </p>
</asp:Content>
