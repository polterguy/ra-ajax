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
        Below is a small example of an Ajax Customer Relationship System built using Ra-Ajax
    </p>
    <p>
        <ra:TextBox 
            runat="server" 
            Text="Filter" 
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
                        <th>Edit</th>
                        <th>Delete</th>
                    </tr>
            </HeaderTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
            <ItemTemplate>
                <tr class="odd">
                    <td>
                        <%# Eval("Name") %>
                    </td>
                    <td>
                        <%# Eval("Address") %>
                    </td>
                    <td style="text-align:center;">
                        <ext:ExtButton 
                            runat="server" 
                            Xtra='<%# Eval("ID") %>' 
                            OnClick="ViewCustomer"
                            Text="Edit" />
                    </td>
                    <td style="text-align:center;">
                        <ext:ExtButton 
                            runat="server" 
                            Xtra='<%# Eval("ID") %>' 
                            OnClick="DeleteCustomer"
                            Text="Delete" />
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="even">
                    <td>
                        <%# Eval("Name") %>
                    </td>
                    <td>
                        <%# Eval("Address") %>
                    </td>
                    <td style="text-align:center;">
                        <ext:ExtButton ID="ExtButton1" 
                            runat="server" 
                            Xtra='<%# Eval("ID") %>' 
                            OnClick="ViewCustomer"
                            Text="Edit" />
                    </td>
                    <td style="text-align:center;">
                        <ext:ExtButton ID="ExtButton2" 
                            runat="server" 
                            Xtra='<%# Eval("ID") %>' 
                            OnClick="DeleteCustomer"
                            Text="Delete" />
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>
    </ra:Panel>
    <ext:ExtButton 
        runat="server" 
        ID="btnNew" 
        OnClick="btnNew_Click" 
        AccessKey="N" 
        Tooltip="Keyboard shortcut N"
        Text="New Customer" />
    <ext:Window 
        runat="server" 
        style="position:absolute;left:150px;top:150px;width:500px;z-index:5;"
        Visible="false" 
        DefaultWidget="ok"
        ID="createNew">
        <div style="padding:5px;">
            <ra:HiddenField 
                runat="server" 
                ID="custID" />
            <table class="dataGrid">
                <tr>
                    <td>Name</td>
                    <td>
                        <ra:TextBox 
                            runat="server" 
                            OnEscPressed="cancel_Click"
                            ID="name" />
                    </td>
                </tr>
                <tr>
                    <td>Address</td>
                    <td>
                        <ra:TextBox 
                            runat="server" 
                            OnEscPressed="cancel_Click"
                            ID="adr" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:right;">
                        <ext:ExtButton 
                            runat="server" 
                            ID="ok" 
                            OnClick="ok_Click" 
                            Text="OK" />
                        <ext:ExtButton 
                            runat="server" 
                            ID="cancel" 
                            OnClick="cancel_Click"
                            Text="Cancel" />
                    </td>
                </tr>
            </table>
        </div>
        <ra:BehaviorObscurable 
            runat="server" 
            ID="obscurer" />
    </ext:Window>
    <ra:Panel 
        runat="server" 
        ID="infoPanel" 
        style="margin-top:15px;padding:15px;border:dashed 1px #aaa;opacity:0.3;cursor:pointer;">
        <p>
            Features shown in this sample;
        </p>
        <ul class="list">
            <li>The Ra-Ajax Event system - OnBlur, OnFocus, OnKeyUp etc</li>
            <li>ReRendering non-Ajax controls</li>
            <li>Using Ra-Ajax controls inside DataBinded controls</li>
            <li>Using modal Ajax Windows</li>
        </ul>
        <ra:BehaviorUnveiler 
            runat="server" 
            ID="unveilInfo" />
    </ra:Panel>
</asp:Content>
