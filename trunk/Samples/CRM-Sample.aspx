<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="CRM-Sample.aspx.cs" 
    Inherits="Samples.CRMSample" 
    Title="Ra-Ajax CRM Sample" %>

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

    <h1>Ra-Ajax CRM Sample</h1>
    <p>
        Below is a small example of an Ajax Customer Relationship System built using Ra-Ajax.
    </p>
    <p>
        <ra:TextBox 
            runat="server" 
            Text="Search" 
            OnFocused="search_Focused" 
            OnBlur="search_Blur"
            OnKeyUp="search_KeyUp" 
            ID="search" />
    </p>
    <ra:Panel 
        runat="server" 
        ID="repWrp">
        <asp:Repeater runat="server" ID="rep">
            <HeaderTemplate>
                <table class="dataGrid">
                    <tr>
                        <th>Name</th>
                        <th>Adress</th>
                        <th>Activities</th>
                    </tr>
            </HeaderTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("Name") %>
                    </td>
                    <td>
                        <%# Eval("Address") %>
                    </td>
                    <td>
                        <ext:ExtButton 
                            runat="server" 
                            Xtra='<%# Eval("ID") %>' 
                            OnClick="ViewContacts"
                            Text="View" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </ra:Panel>
    <ra:Panel 
        runat="server" 
        ID="infoPanel" 
        style="padding:15px;border:dashed 1px #aaa;">
        <p>
            Features shown in this sample;
        </p>
        <ul class="bulList">
            <li>The Ra-Ajax Event system - OnBlur, OnFocus, OnKeyUp etc</li>
            <li>ReRendering non-Ajax controls</li>
            <li>Using Ra-Ajax controls inside DataBinded controls</li>
            <li>Using modal Ajax Windows</li>
            <li>Using the Ra-Ajax TabControl</li>
        </ul>
        <p>
            Feel free to use this sample as the starting point for your own Ajax Application.
        </p>
    </ra:Panel>
</asp:Content>
