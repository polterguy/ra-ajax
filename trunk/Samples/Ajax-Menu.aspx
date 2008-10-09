<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Menu.aspx.cs" 
    Inherits="Samples.Menu" 
    Title="Ra-Ajax Menu Sample" %>

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

    <h1>Ra-Ajax Samples - Menu</h1>
    <p>
        Ajax Menu sample
    </p>
    <ext:Menu runat="server" ID="menu">
        <ext:MenuItems runat="server">
            <ext:Menu runat="server" ID="menuFile">
                File
                <ext:MenuItems runat="server">
                    <ext:Menu runat="server" id="load">
                        Load
                    </ext:Menu>
                </ext:MenuItems>
            </ext:Menu>
            <ext:Menu runat="server" ID="edit">
                Edit
                <ext:MenuItems runat="server">
                    <ext:Menu runat="server" id="copy">
                        Copy
                    </ext:Menu>
                </ext:MenuItems>
            </ext:Menu>
            <ext:Menu runat="server" ID="options">
                Options
                <ext:MenuItems runat="server">
                    <ext:Menu runat="server" id="configuration">
                        Configuration
                    </ext:Menu>
                </ext:MenuItems>
            </ext:Menu>
        </ext:MenuItems>
    </ext:Menu>
</asp:Content>
