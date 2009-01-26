<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="DynamicControl.aspx.cs" 
    Inherits="Samples.DynamicControlSample" 
    Title="Ra-Ajax Dynamic Control Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">
    
    <ra:Button ID="staticButton" runat="server" Text="Add Dynamic Button" OnClick="staticButton_Click" />
    <ra:Label ID="label" runat="server" />

    <ra:Dynamic runat="server" ID="dynamicControl1" OnLoadControls="dynamicControl1_LoadControls" />

</asp:Content>
