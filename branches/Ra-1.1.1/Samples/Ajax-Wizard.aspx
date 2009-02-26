<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Wizard.aspx.cs" 
    Inherits="Samples.AjaxWizard" 
    Title="Ra-Ajax Wizard Sample" %>

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

    <h1>Ajax Wizard Sample</h1>
    <p>
        This is an example of how to create an <em>Ajax Wizard Control</em>.
    </p>

    <ext:Window 
	    runat="server"
	    Caption="Ajax Wizard Window"
	    Movable="false"
	    OnCreateNavigationalButtons="wizard_CreateNavigationalButtons"
	    style="width:420px;margin-bottom:15px;"
	    id="wizard">

        <div style="overflow:hidden;">
	        <ra:Panel 
	            runat="server" 
	            ID="wizardContent" 
	            style="padding:15px 15px 5px 15px;width:1200px;height:150px;position:relative;overflow:hidden;">
	            <div style="width:380px;float:left;padding-right:20px;">
	                <h3>Wizard sample</h3>
	                <p>
	                    This is an Ajax Wizard Control.
	                </p>
	            </div>
	            <div style="width:380px;float:left;padding-right:20px;">
	                <h3>Wizard 2nd pane</h3>
	                <p>
	                    As you can see...
	                </p>
	            </div>
	            <div style="width:380px;float:left;padding-right:20px;">
	                <h3>Wizard 3rd pane</h3>
	                <p>
	                    ...as you click the previous and next buttons you can navigate back and forth...
	                </p>
	                <p>
	                    And you can also have <ra:LinkButton runat="server" ID="lnk" Text="controls" OnClick="lnk_Click" /> 
	                    within the Wizard Control...
	                </p>
	            </div>
	        </ra:Panel>
	    </div>

	</ext:Window>
	<p>
        In fact this is just a window which handles the <em>CreateNavigationalButtons</em> event from which
        it creates a couple of extra buttons which serves as the <em>previous</em> and <em>next</em> buttons.
        This means that you can add up any <em>"action buttons"</em> you wish yourself. This can also include
        buttons for minimizing and maximizing the window. Or to show a <em>context sensitive menu</em> or
        as we have done here - created an Ajax Wizard control :)
	</p>
	<p>
	    BTW, only the <em>"Alphacube"</em> skin have actually the previous and next buttons within. This means
	    that the Wizard Action Buttons will only show if you're using the Alphacube skin...
	</p>
	<p>
	    Also this sample demonstrates that you can create Ajax Windows which are not movable and are statically
	    displayed in your page...
	</p>
	<a href="Ajax-MessageBox.aspx">On to Ajax MessageBox sample...</a>
</asp:Content>




