<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Controls.aspx.cs" 
    Inherits="Controls" 
    Title="Ajax Controls in Ra" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Ajax Controls in Ra Ajax</h1>
    <p>
        Ra has all the <em>basic Ajax Controls</em> you need for your Ajax needs in the core.
        Other more advanced Ajax controls can be composed out of combining the basic ones together.
        In addition due to Ra Ajax Open Source model you are likely to find community extension Ajax
        Controls created on top of the Ra Ajax core.
    </p>
    <br />
    <h2>Kitchen sink</h2>
    <p>
        Under here are all Ajax Controls from the core.
    </p>
    <ra:Button 
        runat="server" 
        ID="btn" 
        Text="Ajax Button" 
        OnClick="btn_Click" />
    Ajax Button
    <br />
    <br />
    <ra:CheckBox 
        runat="server" 
        ID="chk" 
        Text="Ajax CheckBox" 
        OnCheckedChanged="chk_CheckedChanged" />
    <br />
    <br />
    <ra:DropDownList 
        runat="server" 
        ID="drop" 
        OnSelectedIndexChanged="drop_SelectedIndexChanged">
        <ra:ListItem Value="Value1" />
        <ra:ListItem Value="Value2" />
        <ra:ListItem Value="Value3" />
    </ra:DropDownList>
    Ajax DropDownList or select list
    <br />
    <br />
    <ra:HiddenField 
        runat="server" 
        ID="hid" 
        Value="Unknown" />
    <ra:Button 
        runat="server" 
        ID="btn2" 
        Text="Display HiddenField value" 
        OnClick="btn2_Click" />
    Ajax HiddenField
    <br />
    <br />
    <ra:Image 
        runat="server" 
        ID="img" 
        AlternateText="Ajax Image" 
        ImageUrl="media/ajax.png" />
    Ajax Image
    <br />
    <br />
    <ra:ImageButton 
        runat="server" 
        ID="imgBtn" 
        AlternateText="Ajax ImageButton" 
        ImageUrl="media/ajax.png" 
        OnClick="imgBtn_Click"
        CssClass="ajaxImageButton" />
    <ra:Label
        runat="server" 
        ID="lblIMGButton" 
        Text="Ajax ImageButton" />
    <br />
    <br />
    <ra:Label 
        runat="server" 
        ID="lbl" 
        Text="Ajax Label" />
    <ra:Button 
        runat="server" 
        ID="btn3" 
        Text="Click to change text of Label" 
        OnClick="btn3_Click" />
    <br />
    <br />
    <ra:Panel 
        runat="server" 
        ID="pnl" 
        style="width:150px;background-color:Yellow;border:solid 1px Black;padding:15px;">
        Ajax Panel
    </ra:Panel>
    <ra:Button 
        runat="server" 
        Text="Click to animate panel" 
        ID="btn4" 
        OnClick="btn4_Click" />
    <br />
    <br />
    <ra:RadioButton 
        runat="server" 
        ID="rdo1" 
        Text="Ajax RadioButton" 
        GroupName="radioButtonGroup"
        OnCheckedChanged="rdo_CheckedChanged" />
    <ra:RadioButton 
        runat="server" 
        ID="rdo2" 
        Text="Ajax RadioButton" 
        GroupName="radioButtonGroup"
        OnCheckedChanged="rdo_CheckedChanged" />
    <br />
    <br />
    <ra:TextArea 
        runat="server" 
        ID="txtArea" 
        Text="Write something here and move focus" 
        OnTextChanged="txtArea_TextChanged" />
    <br />
    <br />
    <ra:TextBox 
        runat="server" 
        ID="txtBox" 
        Text="Write something here and move focus" 
        OnTextChanged="txtBox_TextChanged" />
    <br />
    <br />
    <ra:LinkButton 
        runat="server" 
        ID="lnkBtn" 
        Text="Link button" 
        OnClick="lnkBtn_Click" />

</asp:Content>

