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
    <p>
        This is our Ajax WebPart sample. A WebPart is kind of like a minimalistic Window, though with far less overhead
        and some features that sets it apart functionally.
    </p>
    <div style="width:500px;height:200px;border:dashed 1px #eee;">
        <div style="width:498px;height:198px;">
            <ext:WebPart 
                runat="server" 
                Caption="WebPart sample"
                style="width:250px;"
                ID="webpart">
                <div>
                    This is an example of an Ajax WebPart. As you can see it is "kind of" like a Window though
                    it has some nice features which are not found in the Ajax Window control. Also it is a far more
                    "lightweight" control then the Window and uses way less DOM elements and will therefor be far
                    nicer to small browsers like for instance phone based browsers and such.</div>
            </ext:WebPart>
        </div>
    </div>
</asp:Content>
