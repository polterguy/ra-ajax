<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-DataGrid.aspx.cs" 
    Inherits="Samples.AjaxDataGrid" 
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
        BackColor="LightGoldenrodYellow" 
        BorderColor="Tan" 
        style="width:100%;"
        BorderWidth="1px" 
        CellPadding="2" 
        ForeColor="Black"
        AutoGenerateColumns="false"
        GridLines="None">
        <FooterStyle 
            BackColor="Tan" />
        <PagerStyle 
            BackColor="PaleGoldenrod" 
            ForeColor="DarkSlateBlue" 
            HorizontalAlign="Center" />
        <SelectedRowStyle 
            BackColor="DarkSlateBlue" 
            ForeColor="GhostWhite" />
        <HeaderStyle 
            BackColor="Tan" 
            Font-Bold="True" />
        <AlternatingRowStyle 
            BackColor="PaleGoldenrod" />
        <Columns>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <ext:InPlaceEdit 
                        runat="server" 
                        OnTextChanged="NameChanged"
                        style="color:Blue;cursor:pointer;"
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
    <p>
	    <ra:Label 
		    runat="server" 
		    id="lbl" 
		    Text="Watch me as you edit stuff" 
		    CssClass="updateLbl" />
    </p>
	<p>
	    Try to edit the names above by clicking them and moving focus out of the TextBox afterwards or
	    change the value of the CheckBoxes etc. This way you will see that this is truly an "Ajax DataGrid" ;)
	</p>
	<h2>Ra-Ajax and ASP.NET Controls</h2>
	<p>
	    As you can see above it is very easy to combine Ra-Ajax controls together with the conventional controls
	    from both ASP.NET and other 3rd Party Library controls. This is because for ASP.NET the Ra-Ajax controls
	    looks like "normal controls" and ASP.NET will threat them like just that. Due to this you can combine
	    the Ajax Controls in Ra-Ajax as you wish with any other controls like we've done above with the ASP.NET
	    GridView to create an Ajax DataGrid.
	</p>
	<p>
	    So even though Ra-Ajax doesn't really have an Ajax DataGrid it is still very easy to build one utilizing
	    your existing knowledge from conventional ASP.NET and other 3rd Party Vendor's controls.
	</p>
	<p>
	    Now the above sample is very "naive" and that is its intentions since we want to keep the code so small
	    that you can easily understand it in less than 5 minutes. But you could of course create all sorts of
	    nice candy things and utilize all the Behaviors and Ajax Controls in Ra-Ajax together with all the 
	    "killer features" of both the ASP.NET GridView, Repeater and anything you wish. Have fun, but remember that
	    ASP.NET has a nasty habit of adding way too many properties on their controls to do formatting and that
	    this should really be done with CSS and not properties like the native ASP.NET controls does it. 
	</p>
	<p>
	    Also try to "sniff" the bandwidth usage using e.g. FireBug or something similar to see how little data
	    is actually transfered across the wire when you edit something. I think you'll be surprised...
	</p>
	<a href="Ajax-TreeView.aspx">On to Ajax TreeView sample...</a>
</asp:Content>





