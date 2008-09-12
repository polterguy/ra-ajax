<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-DataGrid.aspx.cs" 
    Inherits="AjaxDataGrid" 
    Title="Ra-Ajax DataGrid Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ajax DataGrid Sample</h1>
    <p>
    	This is our <em>Ajax DataGrid sample</em>. There's a lot of controls Ra-Ajax does not have, an Ajax DataGrid
    	is one example. Though due to the modular design of Ra-Ajax, most controls you're missing is very easy to
    	implement yourself using either existing Ra-Ajax Controls or combining them with 3rd Party Controls like
    	we've done here with the ASP.NET <em>GridView Control</em>.
    </p>
	<asp:GridView 
		runat="server" 
		ID="datagrid"
		style="width:100%;background-color:#eee;"
		GridLines="Horizontal"
		AutoGenerateColumns="False">
		<Columns>
			<asp:TemplateField HeaderText="Name">
				<ItemTemplate>
					<ext:InPlaceEdit 
						runat="server" 
						OnTextChanged="NameChanged"
						Text='<%# Eval("Name")%>' />
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="IsAdmin">
				<ItemTemplate>
					<ra:CheckBox 
						runat="server" 
						Text="IsAdmin" 
						OnCheckedChanged="AdminChanged"
						Checked='<%# Eval("IsAdmin")%>' />
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	<br />
	<ra:Label 
		runat="server" 
		id="lbl" 
		Text="Watch me as you edit stuff" style="font-style:italic;color:#999;" />
	<br />
	<p>
		Try to edit the names above by clicking them and moving focus out of the TextBox afterwards or
		change the value of the CheckBoxes etc. This way you will see that this is truly an "Ajax DataGrid" ;)
	</p>
</asp:Content>
