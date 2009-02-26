<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Todo.aspx.cs" 
    Inherits="RaWebsite.Todo" 
    Title="TODO list for Ra-Ajax" %>

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
    Runat="Server">

    <h1>TODO list for Ra-Ajax</h1>
    <p>
        This is the TODO list for Ra-Ajax. If you have a bug you wish to report or a request for
        a feature then please login or create a user account at the <a href="Forums/Forums.aspx">Forums</a>
        and create your TODO item.
    </p>
    <ra:CheckBox 
        runat="server" 
        ID="viewFinished" 
        Checked="false" 
        OnCheckedChanged="viewFinished_CheckedChanged"
        Text="See finished ones" />
    <br />
    <ra:TextBox 
        runat="server" 
        OnKeyUp="filter_KeyUp"
        ID="filter" />
    <ra:Panel runat="server" ID="repWrapper">
        <asp:Repeater runat="server" ID="repTodo">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Date</th>
                        <th>Type</th>
                        <th>Header</th>
                        <th>Operator</th>
                        <th>Finished</th>
                        <th>Body</th>
                    </tr>
            </HeaderTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
            <ItemTemplate>
                <tr>
                    <td style="vertical-align:top;">
                        <ra:HiddenField 
                            runat="server" 
                            Value='<%# Eval("Id") %>' />
                        <%# ((DateTime)Eval("Created")).ToString("MMM d, yyyy", System.Globalization.CultureInfo.InvariantCulture) %>
                    </td>
                    <td style="vertical-align:top;">
                        <%# Eval("TypeOfTodo") %>
                    </td>
                    <td style="vertical-align:top;">
                        <ra:LinkButton 
                            runat="server" 
                            OnClick="ClickDetails"
                            Text='<%# Eval("Header") %>' />
                    </td>
                    <td style="vertical-align:top;">
                        <%# Eval("Operator.Username") %>
                    </td>
                    <td style="vertical-align:top;">
                        <ra:CheckBox 
                            runat="server" 
                            Checked='<%# Eval("Finished") %>' 
                            OnCheckedChanged="FinishedChecked"
                            Enabled="<%# Entity.Operator.Current != null && Entity.Operator.Current.IsAdmin %>" />
                    </td>
                    <td style="vertical-align:top;">
                        <ra:Panel 
                            runat="server" 
                            Visible="false" 
                            style="width:2px;height:2px;background-color:Yellow;border:solid 1px Black;padding:5px;overflow:auto;">
                            <%# Eval("Body") %>
                        </ra:Panel>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </ra:Panel>
    <br />
    <ra:Panel 
        runat="server" 
        style="width:400px;background-color:Yellow;border:solid 1px Black;padding:5px;"
        ID="pnlAdd">
        <table>
            <tr>
                <td>Type</td>
                <td>
                    <ra:SelectList 
                        runat="server" 
                        ID="type">
                        <ra:ListItem Text="Bug" />
                        <ra:ListItem Text="Feature request" />
                    </ra:SelectList>
                </td>
            </tr>
            <tr>
                <td>Header</td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="header" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">Body</td>
                <td>
                    <ra:TextArea 
                        runat="server" 
                        Rows="10" 
                        Columns="40"
                        ID="body" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <ra:Button 
                        runat="server" 
                        ID="save" 
                        Text="Save" 
                        OnClick="save_Click" />
                </td>
            </tr>
        </table>
    </ra:Panel>

</asp:Content>

