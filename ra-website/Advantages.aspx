<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Advantages.aspx.cs" 
    Inherits="RaWebsite.Advantages" 
    Title="Ra-Ajax Advantages" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Ra-Ajax Advantages</h1>
    <p>
        When using Ra-Ajax your apps will be;
    </p>
    <ul>
        <li><strong>Faster</strong> - Ra-Ajax has less than 15KB of JavaScript and 
            virtually no overhead on the server.</li>
        <li><strong>Maintainable</strong> - Due to that Ra-Ajax abstracts away 
            JavaScript your code (which will be in pure C# or VB.NET) will be 
            cleaner and easier to maintain.</li>
        <li><strong>Responsive</strong> - Ra-Ajax creates very small and efficient 
            requests to your server.</li>
    </ul>
    <p>
        If you have e.g. <a href="http://getfirebug.com">FireBug</a> installed, then turn 
        it on and check out the JavaScript files for Ra or check out the requests going 
        to and from the server while making Ajax Callbacks below.
    </p>
    <br />
    <ra:CheckBox runat="server" ID="chk1" Text="Milk" OnCheckedChanged="chk_CheckedChanged" />
    <ra:CheckBox runat="server" ID="chk2" Text="Honey" OnCheckedChanged="chk_CheckedChanged" />
    <ra:CheckBox runat="server" ID="chk3" Text="Bread" OnCheckedChanged="chk_CheckedChanged" />
    <ra:CheckBox runat="server" ID="chk4" Text="Butter" OnCheckedChanged="chk_CheckedChanged" />
    <br />
    <ra:Label style="color:Green;" runat="server" ID="lblResults" />
    <br />
    <ra:RadioButton 
        runat="server" 
        ID="rdo1" 
        Text="Pay with Visa" 
        GroupName="Payment" 
        OnCheckedChanged="rdo_CheckedChanged" />
    <ra:RadioButton 
        runat="server" 
        ID="rdo2" 
        Text="Run with the groceries" 
        GroupName="Payment" 
        OnCheckedChanged="rdo_CheckedChanged" />
    <br />
    <div style="float:left;height:300px;">
        <ra:Panel 
            runat="server" 
            ID="panelResults" 
            style="display:none;width:500px;padding:15px;border:solid 1px Black;">
            <ra:Label 
                runat="server" 
                ID="lblEndResults" />
        </ra:Panel>
    </div>

</asp:Content>

